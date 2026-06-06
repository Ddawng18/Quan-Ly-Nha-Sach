using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DAL.Repositories;
using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public class PosService : IPosService
{
    private readonly IBookRepository _bookRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderService _orderService;
    private readonly ILoyaltyService _loyaltyService;

    public PosService(
        IBookRepository bookRepository,
        ICustomerRepository customerRepository,
        IOrderService orderService,
        ILoyaltyService loyaltyService)
    {
        _bookRepository = bookRepository;
        _customerRepository = customerRepository;
        _orderService = orderService;
        _loyaltyService = loyaltyService;
    }

    public ValidationResult AddOrUpdateLine(IList<CartLine> lines, Book book, int quantity, DiscountType discountType, decimal discountValue)
    {
        if (book is null || book.IsDeleted)
        {
            return ValidationResult.Fail("Book not found.");
        }

        if (quantity <= 0)
        {
            return ValidationResult.Fail("Quantity must be greater than zero.");
        }

        var existing = lines.FirstOrDefault(l => l.BookID == book.BookID);
        var requestedQuantity = quantity + (existing?.Quantity ?? 0);
        if (requestedQuantity > book.QuantityInStock)
        {
            return ValidationResult.Fail($"Only {book.QuantityInStock} copies available for \"{book.Title}\".");
        }

        var discount = CalculateDiscount(book.SellPrice * requestedQuantity, discountType, discountValue);
        if (existing is null)
        {
            lines.Add(new CartLine
            {
                BookID = book.BookID,
                BookTitle = book.Title,
                Quantity = quantity,
                UnitPrice = book.SellPrice,
                DiscountType = discountType,
                DiscountValue = discountValue,
                DiscountAmount = CalculateDiscount(book.SellPrice * quantity, discountType, discountValue),
                Subtotal = Math.Max(0, book.SellPrice * quantity - CalculateDiscount(book.SellPrice * quantity, discountType, discountValue))
            });
        }
        else
        {
            existing.Quantity = requestedQuantity;
            existing.DiscountType = discountType;
            existing.DiscountValue = discountValue;
            existing.DiscountAmount = discount;
            existing.Subtotal = Math.Max(0, existing.UnitPrice * existing.Quantity - discount);
        }

        return ValidationResult.Ok();
    }

    public CartTotals CalculateTotals(IReadOnlyList<CartLine> lines, DiscountType orderDiscountType, decimal orderDiscountValue, decimal taxRate, decimal loyaltyDiscount)
    {
        var subtotalBeforeDiscount = lines.Sum(l => l.UnitPrice * l.Quantity);
        var lineDiscount = lines.Sum(l => l.DiscountAmount);
        var afterLineDiscount = Math.Max(0, subtotalBeforeDiscount - lineDiscount);
        var orderDiscount = CalculateDiscount(afterLineDiscount, orderDiscountType, orderDiscountValue);
        var taxable = Math.Max(0, afterLineDiscount - orderDiscount - loyaltyDiscount);
        var tax = taxRate <= 0 ? 0 : taxable * taxRate / 100m;

        return new CartTotals
        {
            Subtotal = subtotalBeforeDiscount,
            Discount = lineDiscount + orderDiscount,
            LoyaltyDiscount = loyaltyDiscount,
            Tax = tax,
            GrandTotal = taxable + tax
        };
    }

    public CheckoutResult PrepareCheckout(CheckoutRequest request)
    {
        if (request.CustomerID <= 0)
        {
            return Fail("Please select a customer.");
        }

        if (request.EmployeeID <= 0)
        {
            return Fail("Please select an employee.");
        }

        if (request.Lines.Count == 0)
        {
            return Fail("Add at least one book to the cart.");
        }

        var customer = _customerRepository.GetById(request.CustomerID);
        if (customer is null)
        {
            return Fail("Customer not found.");
        }

        foreach (var line in request.Lines)
        {
            var book = _bookRepository.GetById(line.BookID);
            if (book is null)
            {
                return Fail($"Book #{line.BookID} not found.");
            }

            if (line.Quantity > book.QuantityInStock)
            {
                return Fail($"Not enough stock for \"{book.Title}\".");
            }
        }

        var preLoyaltyTotal = CalculateTotals(request.Lines, request.OrderDiscountType, request.OrderDiscountValue, request.TaxRate, 0);
        var redeemable = _loyaltyService.CalculateRedeemablePoints(customer, preLoyaltyTotal.GrandTotal, request.LoyaltyPointsToRedeem);
        var loyaltyDiscount = _loyaltyService.CalculateRedemptionValue(redeemable);
        var totals = CalculateTotals(request.Lines, request.OrderDiscountType, request.OrderDiscountValue, request.TaxRate, loyaltyDiscount);
        var earned = _loyaltyService.CalculateEarnedPoints(totals.GrandTotal);

        var order = new Order
        {
            CustomerID = request.CustomerID,
            EmployeeID = request.EmployeeID,
            SubtotalAmount = totals.Subtotal,
            DiscountAmount = totals.Discount + totals.LoyaltyDiscount,
            TaxAmount = totals.Tax,
            TotalAmount = totals.GrandTotal,
            PaymentStatus = string.IsNullOrWhiteSpace(request.PaymentStatus) ? OrderStatus.Pending : request.PaymentStatus,
            PaymentMethod = string.IsNullOrWhiteSpace(request.PaymentMethod) ? "Cash" : request.PaymentMethod,
            LoyaltyPointsRedeemed = redeemable,
            LoyaltyPointsEarned = earned
        };

        var details = request.Lines.Select(l => new OrderDetail
        {
            BookID = l.BookID,
            Quantity = l.Quantity,
            UnitPrice = l.UnitPrice,
            DiscountAmount = l.DiscountAmount,
            Subtotal = l.Subtotal
        }).ToList();

        return new CheckoutResult
        {
            Validation = ValidationResult.Ok(),
            Order = order,
            Details = details,
            Totals = totals
        };
    }

    public ValidationResult CompleteCheckout(CheckoutResult checkout)
    {
        if (!checkout.Validation.IsValid)
        {
            return checkout.Validation;
        }

        if (checkout.Order is null)
        {
            return ValidationResult.Fail("Checkout order is missing.");
        }

        var result = _orderService.CreateOrder(checkout.Order, checkout.Details);
        if (!result.IsValid)
        {
            return result;
        }

        // Deduct stock — moved from OrderRepository to service orchestration layer
        foreach (var detail in checkout.Details)
        {
            var book = _bookRepository.GetById(detail.BookID);
            if (book is not null)
            {
                var oldStock = book.QuantityInStock;
                book.QuantityInStock = Math.Max(0, book.QuantityInStock - detail.Quantity);
                book.LastSoldDate = DateTime.Now;

               FileLogger.Info(
                    $"Stock: Book #{book.BookID} \"{book.Title}\" {oldStock}→{book.QuantityInStock}");
            }
        }

        var customer = _customerRepository.GetById(checkout.Order.CustomerID);
        if (customer is not null)
        {
            var points = customer.LoyaltyPoints - checkout.Order.LoyaltyPointsRedeemed + checkout.Order.LoyaltyPointsEarned;
            _customerRepository.UpdateLoyaltyPoints(customer.CustomerID, points);
        }

        return ValidationResult.Ok();
    }

    private static CheckoutResult Fail(string message) =>
        new() { Validation = ValidationResult.Fail(message) };

    private static decimal CalculateDiscount(decimal amount, DiscountType type, decimal value)
    {
        if (amount <= 0 || value <= 0 || type == DiscountType.None)
        {
            return 0;
        }

        return type == DiscountType.Percentage
            ? Math.Min(amount, amount * Math.Min(value, 100m) / 100m)
            : Math.Min(amount, value);
    }
}
