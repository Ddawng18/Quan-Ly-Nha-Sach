using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DAL.Repositories;
using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public class DashboardService : IDashboardService
{
    private readonly IDashboardRepository _dashboardRepository;

    public DashboardService(IDashboardRepository dashboardRepository)
    {
        _dashboardRepository = dashboardRepository;
    }

    public IReadOnlyList<DashboardMetricDto> GetMetrics() => _dashboardRepository.GetMetrics();

    public IReadOnlyList<DashboardMetricDto> SearchMetrics(string searchText)
    {
        var metrics = _dashboardRepository.GetMetrics();

        if (string.IsNullOrWhiteSpace(searchText))
        {
            return metrics;
        }

        return metrics
            .Where(m =>
                m.Metric.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                m.Value.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public IReadOnlyList<RecentOrderDto> GetRecentOrders(int count = 5) =>
        _dashboardRepository.GetRecentOrders(count);

    public IReadOnlyList<BestSellingBookDto> GetBestSellingBooks(int count = 5) =>
        _dashboardRepository.GetBestSellingBooks(count);
}
