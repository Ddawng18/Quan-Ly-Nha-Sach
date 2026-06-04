using BookStoreApp.DTO;

namespace BookStoreApp.DAL.Interfaces;

public interface IOrderRepository
{
    IReadOnlyList<OrderViewDto> GetAll();
    IReadOnlyList<OrderViewDto> GetByDateRange(DateTime? fromDate, DateTime? toDate);
    Order? GetOrder(int orderId);
    IReadOnlyList<OrderDetailViewDto> GetDetails(int orderId);
    void CreateOrder(Order order, IReadOnlyList<OrderDetail> details);
    void UpdateStatus(int orderId, string status);
}
