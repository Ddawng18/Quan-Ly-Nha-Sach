using BookStoreApp.DTO;

namespace BookStoreApp.DAL.Interfaces;

public interface IDashboardRepository
{
    IReadOnlyList<DashboardMetricDto> GetMetrics();
    IReadOnlyList<RecentOrderDto> GetRecentOrders(int count = 5);
    IReadOnlyList<BestSellingBookDto> GetBestSellingBooks(int count = 5);
}
