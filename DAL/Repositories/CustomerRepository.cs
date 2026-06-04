using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DTO;
using BookStoreApp.Utilities;

namespace BookStoreApp.DAL.Repositories;

public class CustomerRepository : ICustomerRepository
{
    public IReadOnlyList<Customer> GetAll() => FakeDatabase.Customers.ToList();

    public Customer? GetById(int customerId) =>
        FakeDatabase.Customers.FirstOrDefault(c => c.CustomerID == customerId);

    public void Add(Customer customer)
    {
        customer.CustomerID = FakeDatabase.Customers.Count == 0
            ? 1
            : FakeDatabase.Customers.Max(c => c.CustomerID) + 1;
        customer.CreatedDate = DateTime.Now;
        FakeDatabase.Customers.Add(customer);
    }

    public void Update(Customer customer)
    {
        var index = FakeDatabase.Customers.FindIndex(c => c.CustomerID == customer.CustomerID);
        if (index >= 0)
        {
            FakeDatabase.Customers[index] = customer;
        }
    }

    public void Delete(int customerId)
    {
        var customer = FakeDatabase.Customers.FirstOrDefault(c => c.CustomerID == customerId);
        if (customer is not null)
        {
            FakeDatabase.Customers.Remove(customer);
        }
    }

    public CustomerStatsDto GetStats()
    {
        var now = DateTime.Now;
        return new CustomerStatsDto
        {
            TotalCustomers = FakeDatabase.Customers.Count,
            NewThisMonth = FakeDatabase.Customers.Count(c =>
                c.CreatedDate.Year == now.Year && c.CreatedDate.Month == now.Month),
            TotalLoyaltyPoints = FakeDatabase.Customers.Sum(c => c.LoyaltyPoints)
        };
    }

    public void UpdateLoyaltyPoints(int customerId, int loyaltyPoints)
    {
        var customer = FakeDatabase.Customers.FirstOrDefault(c => c.CustomerID == customerId);
        if (customer is not null)
        {
            customer.LoyaltyPoints = Math.Max(0, loyaltyPoints);
        }
    }

    public IReadOnlyList<CustomerPurchaseDto> GetPurchaseHistory(int customerId)
    {
        var orders = FakeDatabase.Orders.Where(o => o.CustomerID == customerId).ToList();
        var result = new List<CustomerPurchaseDto>();

        foreach (var order in orders)
        {
            var details = FakeDatabase.OrderDetails.Where(d => d.OrderID == order.OrderID).ToList();
            if (details.Count == 0)
            {
                result.Add(new CustomerPurchaseDto
                {
                    OrderID = order.OrderID,
                    OrderDate = order.OrderDate,
                    BookTitle = "(No details)",
                    Quantity = 0,
                    UnitPrice = 0,
                    Subtotal = 0,
                    OrderTotal = order.TotalAmount,
                    PaymentStatus = order.PaymentStatus
                });
                continue;
            }

            foreach (var detail in details)
            {
                var bookTitle = FakeDatabase.Books.FirstOrDefault(b => b.BookID == detail.BookID)?.Title ?? $"Book #{detail.BookID}";
                result.Add(new CustomerPurchaseDto
                {
                    OrderID = order.OrderID,
                    OrderDate = order.OrderDate,
                    BookTitle = bookTitle,
                    Quantity = detail.Quantity,
                    UnitPrice = detail.UnitPrice,
                    Subtotal = detail.Subtotal,
                    OrderTotal = order.TotalAmount,
                    PaymentStatus = order.PaymentStatus
                });
            }
        }

        return result.OrderByDescending(p => p.OrderDate).ToList();
    }
}
