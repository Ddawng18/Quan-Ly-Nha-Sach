using BookStoreApp.DTO;

namespace BookStoreApp.DAL.Interfaces;

public interface IReportRepository
{
    IReadOnlyList<ReportRowDto> GetReports();
    ReportSectionDto GetRevenueSummary();
    ReportSectionDto GetRevenueByPeriodReport(string period);
    ReportSectionDto GetBestSellingBooksReport(int topN = 10);
    ReportSectionDto GetLowStockReport(int threshold = 10);
    ReportSectionDto GetSlowMovingReport(int daysWithoutSales = 90);
}
