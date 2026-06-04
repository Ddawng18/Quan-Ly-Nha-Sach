using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DTO;
using BookStoreApp.Utilities;

namespace BookStoreApp.DAL.Repositories;

public class ReportRepository : IReportRepository
{
    public IReadOnlyList<ReportRowDto> GetReports()
    {
        var paidOrders = FakeDatabase.Orders.Where(o => o.PaymentStatus == "Paid").ToList();
        var pendingOrders = FakeDatabase.Orders.Where(o => o.PaymentStatus == "Pending").ToList();

        return
        [
            new ReportRowDto { ReportType = "Revenue", Description = "Total paid revenue", Count = paidOrders.Count, Amount = paidOrders.Sum(o => o.TotalAmount) },
            new ReportRowDto { ReportType = "Revenue", Description = "Pending payment amount", Count = pendingOrders.Count, Amount = pendingOrders.Sum(o => o.TotalAmount) },
            new ReportRowDto { ReportType = "Inventory", Description = "Low stock titles (<10)", Count = FakeDatabase.Books.Count(b => b.QuantityInStock < 10), Amount = 0 }
        ];
    }

    public ReportSectionDto GetRevenueSummary()
    {
        var paid = FakeDatabase.Orders.Where(o => o.PaymentStatus == OrderStatus.Paid).ToList();
        var pending = FakeDatabase.Orders.Where(o => o.PaymentStatus == OrderStatus.Pending).ToList();
        var cancelled = FakeDatabase.Orders.Where(o => o.PaymentStatus == OrderStatus.Cancelled).ToList();

        return new ReportSectionDto
        {
            SectionName = "Revenue Summary",
            Headers = ["Status", "Order Count", "Total Amount"],
            Rows =
            [
                ["Paid", paid.Count.ToString(), paid.Sum(o => o.TotalAmount).ToString("N2")],
                ["Pending", pending.Count.ToString(), pending.Sum(o => o.TotalAmount).ToString("N2")],
                ["Cancelled", cancelled.Count.ToString(), cancelled.Sum(o => o.TotalAmount).ToString("N2")],
                ["All", FakeDatabase.Orders.Count.ToString(), FakeDatabase.Orders.Sum(o => o.TotalAmount).ToString("N2")]
            ]
        };
    }

    public ReportSectionDto GetRevenueByPeriodReport(string period)
    {
        var normalized = string.IsNullOrWhiteSpace(period) ? "Day" : period.Trim();
        Func<DateTime, string> keySelector = normalized.Equals("Month", StringComparison.OrdinalIgnoreCase)
            ? d => d.ToString("yyyy-MM")
            : normalized.Equals("Week", StringComparison.OrdinalIgnoreCase)
                ? d => $"{d.Year}-W{System.Globalization.ISOWeek.GetWeekOfYear(d):00}"
                : d => d.ToString("yyyy-MM-dd");

        var rows = FakeDatabase.Orders
            .Where(o => o.PaymentStatus == OrderStatus.Paid)
            .GroupBy(o => keySelector(o.OrderDate))
            .OrderBy(g => g.Key)
            .Select(g => new List<string> { g.Key, g.Count().ToString(), g.Sum(o => o.TotalAmount).ToString("N2") })
            .ToList();

        return new ReportSectionDto
        {
            SectionName = $"Revenue By {normalized}",
            Headers = [normalized, "Paid Orders", "Revenue"],
            Rows = rows
        };
    }

    public ReportSectionDto GetBestSellingBooksReport(int topN = 10)
    {
        var rows = FakeDatabase.OrderDetails
            .GroupBy(d => d.BookID)
            .Select(g =>
            {
                var title = FakeDatabase.Books.FirstOrDefault(b => b.BookID == g.Key)?.Title ?? $"Book #{g.Key}";
                return new List<string> { title, g.Sum(x => x.Quantity).ToString() };
            })
            .OrderByDescending(r => int.Parse(r[1]))
            .Take(Math.Max(1, topN))
            .ToList();

        return new ReportSectionDto
        {
            SectionName = "Best Selling Books",
            Headers = ["Book", "Sold"],
            Rows = rows
        };
    }

    public ReportSectionDto GetLowStockReport(int threshold = 10)
    {
        var rows = FakeDatabase.Books
            .Where(b => !b.IsDeleted && b.QuantityInStock <= threshold)
            .Select(b => new List<string> { b.Title, b.QuantityInStock.ToString(), b.SellPrice.ToString("N2") })
            .ToList();

        return new ReportSectionDto
        {
            SectionName = $"Low Stock Report (<= {threshold})",
            Headers = ["Book", "Quantity", "Sell Price"],
            Rows = rows
        };
    }

    public ReportSectionDto GetSlowMovingReport(int daysWithoutSales = 90)
    {
        var cutoff = DateTime.Now.AddDays(-daysWithoutSales);
        var soldBookIds = FakeDatabase.OrderDetails
            .Where(d => FakeDatabase.Orders.Any(o => o.OrderID == d.OrderID && o.OrderDate >= cutoff))
            .Select(d => d.BookID)
            .Distinct()
            .ToHashSet();

        var rows = FakeDatabase.Books
            .Where(b => !b.IsDeleted && !soldBookIds.Contains(b.BookID))
            .Select(b => new List<string>
            {
                b.Title,
                b.QuantityInStock.ToString(),
                b.LastSoldDate?.ToString("dd/MM/yyyy") ?? "Never"
            })
            .ToList();

        return new ReportSectionDto
        {
            SectionName = $"Slow Moving Books (no sales in {daysWithoutSales} days)",
            Headers = ["Book", "Stock", "Last Sold"],
            Rows = rows
        };
    }
}
