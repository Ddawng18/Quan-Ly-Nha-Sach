using BookStoreApp.DTO;

namespace BookStoreApp.DAL.Interfaces;

public interface ICustomerRepository
{
    IReadOnlyList<Customer> GetAll();
    Customer? GetById(int customerId);
    void Add(Customer customer);
    void Update(Customer customer);
    void UpdateLoyaltyPoints(int customerId, int loyaltyPoints);
    void Delete(int customerId);
    CustomerStatsDto GetStats();
    IReadOnlyList<CustomerPurchaseDto> GetPurchaseHistory(int customerId);
}
