using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DAL.Repositories;
using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IBookRepository _bookRepository;

    public OrderService(IOrderRepository orderRepository, IBookRepository bookRepository)
    {
        _orderRepository = orderRepository;
        _bookRepository = bookRepository;
    }

    public IReadOnlyList<OrderViewDto> GetOrders() => _orderRepository.GetAll();

    public IReadOnlyList<OrderViewDto> SearchOrders(string searchText)
    {
        var orders = _orderRepository.GetAll();

        if (string.IsNullOrWhiteSpace(searchText))
        {
            return orders;
        }

        return orders
            .Where(o =>
                o.OrderID.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                o.CustomerName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                o.EmployeeName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                o.PaymentStatus.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public IReadOnlyList<OrderViewDto> GetOrdersByDateRange(DateTime? fromDate, DateTime? toDate) =>
        _orderRepository.GetByDateRange(fromDate, toDate);

    public Order? GetOrder(int orderId) => _orderRepository.GetOrder(orderId);

    public IReadOnlyList<OrderDetailViewDto> GetOrderDetails(int orderId) =>
        _orderRepository.GetDetails(orderId);

    public ValidationResult CreateOrder(Order order, IReadOnlyList<OrderDetail> details)
    {
        if (order.CustomerID <= 0)
        {
            return ValidationResult.Fail("Please select a customer.");
        }

        if (order.EmployeeID <= 0)
        {
            return ValidationResult.Fail("Please select an employee.");
        }

        if (string.IsNullOrWhiteSpace(order.PaymentStatus))
        {
            return ValidationResult.Fail("Payment status is required.");
        }

        if (!OrderStatus.All.Contains(order.PaymentStatus))
        {
            return ValidationResult.Fail("Invalid payment status.");
        }

        if (details.Count == 0)
        {
            return ValidationResult.Fail("Add at least one book to the order.");
        }

        foreach (var detail in details)
        {
            if (detail.Quantity <= 0)
            {
                return ValidationResult.Fail("Quantity must be greater than zero.");
            }

            var book = _bookRepository.GetById(detail.BookID);
            if (book is null || book.IsDeleted)
            {
                return ValidationResult.Fail($"Book #{detail.BookID} not found.");
            }

            if (detail.Quantity > book.QuantityInStock)
            {
                return ValidationResult.Fail($"Not enough stock for \"{book.Title}\".");
            }
        }

        _orderRepository.CreateOrder(order, details);
        return ValidationResult.Ok();
    }

    public ValidationResult UpdateStatus(int orderId, string status)
    {
        if (!OrderStatus.All.Contains(status))
        {
            return ValidationResult.Fail("Invalid payment status.");
        }

        var order = _orderRepository.GetOrder(orderId);
        if (order is null)
        {
            return ValidationResult.Fail("Order not found.");
        }

        if (order.PaymentStatus == OrderStatus.Cancelled && status != OrderStatus.Cancelled)
        {
            return ValidationResult.Fail("Cancelled orders cannot be reopened.");
        }

        if (order.PaymentStatus == OrderStatus.Paid && status == OrderStatus.Pending)
        {
            return ValidationResult.Fail("Paid orders cannot return to pending.");
        }

        // Hoàn lại tồn kho khi hủy đơn (chỉ nếu đơn chưa bị hủy trước đó)
        if (status == OrderStatus.Cancelled && order.PaymentStatus != OrderStatus.Cancelled)
        {
            var details = _orderRepository.GetDetails(orderId);
            foreach (var detail in details)
            {
                var book = _bookRepository.GetById(detail.BookID);
                if (book is not null)
                {
                    _bookRepository.UpdateStock(detail.BookID, book.QuantityInStock + detail.Quantity);
                }
            }
        }

        _orderRepository.UpdateStatus(orderId, status);
        return ValidationResult.Ok();
    }
}
