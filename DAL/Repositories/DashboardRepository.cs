using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DTO;
using BookStoreApp.Utilities;

namespace BookStoreApp.DAL.Repositories;

public class DashboardRepository : IDashboardRepository
{
    public IReadOnlyList<DashboardMetricDto> GetMetrics()
    {
        var activeBooks = FakeDatabase.Books.Count(b => !b.IsDeleted);
        var totalStock = FakeDatabase.Books.Where(b => !b.IsDeleted).Sum(b => b.QuantityInStock);
        var lowStock = FakeDatabase.Books.Count(b => !b.IsDeleted && b.QuantityInStock < 10);
        var totalRevenue = FakeDatabase.Orders.Sum(o => o.TotalAmount);
        var pendingOrders = FakeDatabase.Orders.Count(o => o.PaymentStatus == "Pending");

        return
        [
            new DashboardMetricDto { Metric = "Total Books", Value = activeBooks.ToString() },
            new DashboardMetricDto { Metric = "Total Customers", Value = FakeDatabase.Customers.Count.ToString() },
            new DashboardMetricDto { Metric = "Total Employees", Value = FakeDatabase.Employees.Count.ToString() },
            new DashboardMetricDto { Metric = "Total Orders", Value = FakeDatabase.Orders.Count.ToString() },
            new DashboardMetricDto { Metric = "Pending Orders", Value = pendingOrders.ToString() },
            new DashboardMetricDto { Metric = "Total Revenue", Value = totalRevenue.ToString("N0") },
            new DashboardMetricDto { Metric = "Books In Stock", Value = totalStock.ToString() },
            new DashboardMetricDto { Metric = "Low Stock Books (<10)", Value = lowStock.ToString() }
        ];
    }

    public IReadOnlyList<RecentOrderDto> GetRecentOrders(int count = 5) =>
        FakeDatabase.Orders
            .OrderByDescending(o => o.OrderDate)
            .Take(count)
            .Select(o => new RecentOrderDto
            {
                OrderID = o.OrderID,
                Customer = FakeDatabase.Customers.FirstOrDefault(c => c.CustomerID == o.CustomerID)?.FullName ?? "-",
                Total = o.TotalAmount
            })
            .ToList();

    public IReadOnlyList<BestSellingBookDto> GetBestSellingBooks(int count = 5) =>
        FakeDatabase.OrderDetails
            .GroupBy(d => d.BookID)
            .Select(g => new BestSellingBookDto
            {
                Book = FakeDatabase.Books.FirstOrDefault(b => b.BookID == g.Key)?.Title ?? $"Book #{g.Key}",
                Sold = g.Sum(x => x.Quantity)
            })
            .OrderByDescending(x => x.Sold)
            .Take(count)
            .ToList();
}
