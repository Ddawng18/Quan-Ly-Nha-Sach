using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DTO;
using BookStoreApp.Utilities;

namespace BookStoreApp.DAL.Repositories;

public class OrderRepository : IOrderRepository
{
    public IReadOnlyList<OrderViewDto> GetAll() => MapOrders(FakeDatabase.Orders);

    public IReadOnlyList<OrderViewDto> GetByDateRange(DateTime? fromDate, DateTime? toDate)
    {
        var orders = FakeDatabase.Orders.AsEnumerable();
        if (fromDate.HasValue)
        {
            orders = orders.Where(o => o.OrderDate.Date >= fromDate.Value.Date);
        }

        if (toDate.HasValue)
        {
            orders = orders.Where(o => o.OrderDate.Date <= toDate.Value.Date);
        }

        return MapOrders(orders.ToList());
    }

    public Order? GetOrder(int orderId) =>
        FakeDatabase.Orders.FirstOrDefault(o => o.OrderID == orderId);

    public IReadOnlyList<OrderDetailViewDto> GetDetails(int orderId) =>
        FakeDatabase.OrderDetails
            .Where(d => d.OrderID == orderId)
            .Select(d => new OrderDetailViewDto
            {
                OrderDetailID = d.OrderDetailID,
                OrderID = d.OrderID,
                BookID = d.BookID,
                BookTitle = FakeDatabase.Books.FirstOrDefault(b => b.BookID == d.BookID)?.Title ?? $"Book #{d.BookID}",
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice,
                DiscountAmount = d.DiscountAmount,
                Subtotal = d.Subtotal
            })
            .ToList();

    public void CreateOrder(Order order, IReadOnlyList<OrderDetail> details)
    {
        order.OrderID = FakeDatabase.Orders.Count == 0
            ? 1
            : FakeDatabase.Orders.Max(o => o.OrderID) + 1;
        order.OrderDate = DateTime.Now;
        order.SubtotalAmount = order.SubtotalAmount <= 0 ? details.Sum(d => d.UnitPrice * d.Quantity) : order.SubtotalAmount;
        order.TotalAmount = order.TotalAmount <= 0 ? details.Sum(d => d.Subtotal) : order.TotalAmount;
        FakeDatabase.Orders.Add(order);

        var nextDetailId = FakeDatabase.OrderDetails.Count == 0
            ? 1
            : FakeDatabase.OrderDetails.Max(d => d.OrderDetailID) + 1;

        foreach (var detail in details)
        {
            detail.OrderDetailID = nextDetailId++;
            detail.OrderID = order.OrderID;
            FakeDatabase.OrderDetails.Add(detail);
        }
    }

    public void UpdateStatus(int orderId, string status)
    {
        var order = FakeDatabase.Orders.FirstOrDefault(o => o.OrderID == orderId);
        if (order is not null)
        {
            order.PaymentStatus = status;
        }
    }

    private static IReadOnlyList<OrderViewDto> MapOrders(IEnumerable<Order> orders) =>
        orders
            .Select(o => new OrderViewDto
            {
                OrderID = o.OrderID,
                CustomerID = o.CustomerID,
                CustomerName = FakeDatabase.Customers.FirstOrDefault(c => c.CustomerID == o.CustomerID)?.FullName ?? "-",
                EmployeeID = o.EmployeeID,
                EmployeeName = FakeDatabase.Employees.FirstOrDefault(e => e.EmployeeID == o.EmployeeID)?.FullName ?? "-",
                OrderDate = o.OrderDate,
                SubtotalAmount = o.SubtotalAmount,
                DiscountAmount = o.DiscountAmount,
                TaxAmount = o.TaxAmount,
                TotalAmount = o.TotalAmount,
                PaymentStatus = o.PaymentStatus,
                PaymentMethod = o.PaymentMethod,
                PaymentTransactionId = o.PaymentTransactionId
            })
            .OrderByDescending(o => o.OrderDate)
            .ToList();
}
