using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public interface IDashboardService
{
    IReadOnlyList<DashboardMetricDto> GetMetrics();
    IReadOnlyList<DashboardMetricDto> SearchMetrics(string searchText);
    IReadOnlyList<RecentOrderDto> GetRecentOrders(int count = 5);
    IReadOnlyList<BestSellingBookDto> GetBestSellingBooks(int count = 5);
}
