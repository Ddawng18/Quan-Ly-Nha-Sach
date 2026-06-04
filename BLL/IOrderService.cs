using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public interface IOrderService
{
    IReadOnlyList<OrderViewDto> GetOrders();
    IReadOnlyList<OrderViewDto> SearchOrders(string searchText);
    IReadOnlyList<OrderViewDto> GetOrdersByDateRange(DateTime? fromDate, DateTime? toDate);
    Order? GetOrder(int orderId);
    IReadOnlyList<OrderDetailViewDto> GetOrderDetails(int orderId);
    ValidationResult CreateOrder(Order order, IReadOnlyList<OrderDetail> details);
    ValidationResult UpdateStatus(int orderId, string status);
}
