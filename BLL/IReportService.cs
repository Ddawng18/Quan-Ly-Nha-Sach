using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public interface IReportService
{
    IReadOnlyList<ReportRowDto> GetReports();
    IReadOnlyList<ReportRowDto> SearchReports(string searchText);
    ReportSectionDto GetRevenueSummary();
    ReportSectionDto GetRevenueByPeriodReport(string period);
    ReportSectionDto GetBestSellingBooksReport(int topN = 10);
    ReportSectionDto GetLowStockReport(int threshold = 10);
    ReportSectionDto GetSlowMovingReport(int daysWithoutSales = 90);
    ReportSectionDto GetImportReport();
    IReadOnlyList<ReportSectionDto> GetAllReportSections();
}
