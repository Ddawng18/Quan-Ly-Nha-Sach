using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DTO;
using Microsoft.Data.SqlClient;

namespace BookStoreApp.DAL.Repositories;

public class DashboardRepository : IDashboardRepository
{
    public IReadOnlyList<DashboardMetricDto> GetMetrics()
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(@"
            SELECT
                (SELECT COUNT(*) FROM Books WHERE IsDeleted = 0)                              AS TotalBooks,
                (SELECT COUNT(*) FROM Customers)                                              AS TotalCustomers,
                (SELECT COUNT(*) FROM Employees)                                              AS TotalEmployees,
                (SELECT COUNT(*) FROM Orders)                                                 AS TotalOrders,
                (SELECT COUNT(*) FROM Orders WHERE PaymentStatus = 'Pending')                 AS PendingOrders,
                (SELECT ISNULL(SUM(TotalAmount),0) FROM Orders WHERE PaymentStatus = 'Paid') AS TotalRevenue,
                (SELECT ISNULL(SUM(QuantityInStock),0) FROM Books WHERE IsDeleted = 0)       AS BooksInStock,
                (SELECT COUNT(*) FROM Books WHERE IsDeleted = 0 AND QuantityInStock < 10)    AS LowStock", conn);

        using var reader = cmd.ExecuteReader();
        reader.Read();
        return new List<DashboardMetricDto>
        {
            new() { Metric = "Total Books",           Value = reader["TotalBooks"].ToString()! },
            new() { Metric = "Total Customers",       Value = reader["TotalCustomers"].ToString()! },
            new() { Metric = "Total Employees",       Value = reader["TotalEmployees"].ToString()! },
            new() { Metric = "Total Orders",          Value = reader["TotalOrders"].ToString()! },
            new() { Metric = "Pending Orders",        Value = reader["PendingOrders"].ToString()! },
            new() { Metric = "Total Revenue",         Value = ((decimal)reader["TotalRevenue"]).ToString("N0") },
            new() { Metric = "Books In Stock",        Value = reader["BooksInStock"].ToString()! },
            new() { Metric = "Low Stock Books (<10)", Value = reader["LowStock"].ToString()! }
        };
    }

    public IReadOnlyList<RecentOrderDto> GetRecentOrders(int count = 5)
    {
        var list = new List<RecentOrderDto>();
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(@"
            SELECT TOP (@count) o.OrderID, c.FullName AS Customer, o.TotalAmount
            FROM Orders o
            LEFT JOIN Customers c ON c.CustomerID = o.CustomerID
            ORDER BY o.OrderDate DESC", conn);
        cmd.Parameters.AddWithValue("@count", count);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            list.Add(new RecentOrderDto
            {
                OrderID  = (int)reader["OrderID"],
                Customer = reader["Customer"]?.ToString() ?? "-",
                Total    = (decimal)reader["TotalAmount"]
            });
        return list;
    }

    public IReadOnlyList<BestSellingBookDto> GetBestSellingBooks(int count = 5)
    {
        var list = new List<BestSellingBookDto>();
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(@"
            SELECT TOP (@count) b.Title AS Book, SUM(od.Quantity) AS Sold
            FROM OrderDetails od
            JOIN Books b ON b.BookID = od.BookID
            GROUP BY b.Title
            ORDER BY Sold DESC", conn);
        cmd.Parameters.AddWithValue("@count", count);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            list.Add(new BestSellingBookDto
            {
                Book = reader["Book"].ToString()!,
                Sold = (int)reader["Sold"]
            });
        return list;
    }
}
