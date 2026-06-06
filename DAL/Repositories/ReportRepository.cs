using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DTO;
using Microsoft.Data.SqlClient;

namespace BookStoreApp.DAL.Repositories;

public class ReportRepository : IReportRepository
{
    public IReadOnlyList<ReportRowDto> GetReports()
    {
        var list = new List<ReportRowDto>();
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(@"
            SELECT 'Revenue'   AS ReportType, 'Total paid revenue'  AS Description,
                   COUNT(*)    AS [Count], SUM(TotalAmount)         AS Amount
            FROM Orders WHERE PaymentStatus = 'Paid'
            UNION ALL
            SELECT 'Revenue', 'Pending payment amount',
                   COUNT(*), SUM(TotalAmount)
            FROM Orders WHERE PaymentStatus = 'Pending'
            UNION ALL
            SELECT 'Inventory', 'Low stock titles (<10)',
                   COUNT(*), 0
            FROM Books WHERE QuantityInStock < 10 AND IsDeleted = 0", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            list.Add(new ReportRowDto
            {
                ReportType  = reader["ReportType"].ToString()!,
                Description = reader["Description"].ToString()!,
                Count       = (int)reader["Count"],
                Amount      = reader["Amount"] == DBNull.Value ? 0 : (decimal)reader["Amount"]
            });
        return list;
    }

    public ReportSectionDto GetRevenueSummary()
    {
        var rows = new List<List<string>>();
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(@"
            SELECT PaymentStatus, COUNT(*) AS Cnt, ISNULL(SUM(TotalAmount),0) AS Total
            FROM Orders GROUP BY PaymentStatus", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            rows.Add([reader["PaymentStatus"].ToString()!, reader["Cnt"].ToString()!, ((decimal)reader["Total"]).ToString("N2")]);
        return new ReportSectionDto
        {
            SectionName = "Revenue Summary",
            Headers     = ["Status", "Order Count", "Total Amount"],
            Rows        = rows
        };
    }

    public ReportSectionDto GetRevenueByPeriodReport(string period)
    {
        var normalized = string.IsNullOrWhiteSpace(period) ? "Day" : period.Trim();
        string groupExpr = normalized.ToLower() switch
        {
            "month" => "FORMAT(OrderDate, 'yyyy-MM')",
            "week"  => "CAST(YEAR(OrderDate) AS VARCHAR) + '-W' + RIGHT('00' + CAST(DATEPART(ISO_WEEK, OrderDate) AS VARCHAR), 2)",
            _       => "FORMAT(OrderDate, 'yyyy-MM-dd')"
        };

        var rows = new List<List<string>>();
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand($@"
            SELECT {groupExpr} AS Period, COUNT(*) AS Cnt, SUM(TotalAmount) AS Revenue
            FROM Orders WHERE PaymentStatus = 'Paid'
            GROUP BY {groupExpr} ORDER BY Period", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            rows.Add([reader["Period"].ToString()!, reader["Cnt"].ToString()!, ((decimal)reader["Revenue"]).ToString("N2")]);
        return new ReportSectionDto
        {
            SectionName = $"Revenue By {normalized}",
            Headers     = [normalized, "Paid Orders", "Revenue"],
            Rows        = rows
        };
    }

    public ReportSectionDto GetBestSellingBooksReport(int topN = 10)
    {
        var rows = new List<List<string>>();
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(@"
            SELECT TOP (@topN) b.Title, SUM(od.Quantity) AS Sold
            FROM OrderDetails od
            JOIN Books b ON b.BookID = od.BookID
            GROUP BY b.Title ORDER BY Sold DESC", conn);
        cmd.Parameters.AddWithValue("@topN", Math.Max(1, topN));
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            rows.Add([reader["Title"].ToString()!, reader["Sold"].ToString()!]);
        return new ReportSectionDto
        {
            SectionName = "Best Selling Books",
            Headers     = ["Book", "Sold"],
            Rows        = rows
        };
    }

    public ReportSectionDto GetLowStockReport(int threshold = 10)
    {
        var rows = new List<List<string>>();
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(@"
            SELECT Title, QuantityInStock, SellPrice FROM Books
            WHERE IsDeleted = 0 AND QuantityInStock <= @threshold
            ORDER BY QuantityInStock", conn);
        cmd.Parameters.AddWithValue("@threshold", threshold);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            rows.Add([reader["Title"].ToString()!, reader["QuantityInStock"].ToString()!, ((decimal)reader["SellPrice"]).ToString("N2")]);
        return new ReportSectionDto
        {
            SectionName = $"Low Stock Report (<= {threshold})",
            Headers     = ["Book", "Quantity", "Sell Price"],
            Rows        = rows
        };
    }

    public ReportSectionDto GetSlowMovingReport(int daysWithoutSales = 90)
    {
        var rows = new List<List<string>>();
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(@"
            SELECT b.Title, b.QuantityInStock,
                   ISNULL(FORMAT(b.LastSoldDate, 'dd/MM/yyyy'), 'Never') AS LastSold
            FROM Books b
            WHERE b.IsDeleted = 0
              AND b.BookID NOT IN (
                  SELECT DISTINCT od.BookID FROM OrderDetails od
                  JOIN Orders o ON o.OrderID = od.OrderID
                  WHERE o.OrderDate >= DATEADD(DAY, -@days, GETDATE())
              )", conn);
        cmd.Parameters.AddWithValue("@days", daysWithoutSales);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            rows.Add([reader["Title"].ToString()!, reader["QuantityInStock"].ToString()!, reader["LastSold"].ToString()!]);
        return new ReportSectionDto
        {
            SectionName = $"Slow Moving Books (no sales in {daysWithoutSales} days)",
            Headers     = ["Book", "Stock", "Last Sold"],
            Rows        = rows
        };
    }
}
