using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DAL.Repositories;
using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public class ReportService : IReportService
{
    private readonly IReportRepository _reportRepository;

    public ReportService(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public IReadOnlyList<ReportRowDto> GetReports() => _reportRepository.GetReports();

    public IReadOnlyList<ReportRowDto> SearchReports(string searchText)
    {
        var reports = _reportRepository.GetReports();

        if (string.IsNullOrWhiteSpace(searchText))
        {
            return reports;
        }

        return reports
            .Where(r =>
                r.ReportType.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                r.Description.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public ReportSectionDto GetRevenueSummary() => _reportRepository.GetRevenueSummary();

    public ReportSectionDto GetRevenueByPeriodReport(string period) =>
        _reportRepository.GetRevenueByPeriodReport(period);

    public ReportSectionDto GetBestSellingBooksReport(int topN = 10) =>
        _reportRepository.GetBestSellingBooksReport(topN);

    public ReportSectionDto GetLowStockReport(int threshold = 10) =>
        _reportRepository.GetLowStockReport(threshold);

    public ReportSectionDto GetSlowMovingReport(int daysWithoutSales = 90) =>
        _reportRepository.GetSlowMovingReport(daysWithoutSales);

    public ReportSectionDto GetImportReport() =>
        _reportRepository.GetImportReport();

    public IReadOnlyList<ReportSectionDto> GetAllReportSections() =>
    [
        GetRevenueSummary(),
        GetRevenueByPeriodReport("Day"),
        GetBestSellingBooksReport(),
        GetLowStockReport(),
        GetSlowMovingReport(),
        GetImportReport()
    ];
}
