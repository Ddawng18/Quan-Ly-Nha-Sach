using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public interface ICustomerService
{
    IReadOnlyList<Customer> GetCustomers();
    IReadOnlyList<Customer> SearchCustomers(string searchText);
    Customer? GetCustomer(int customerId);
    CustomerStatsDto GetStats();
    IReadOnlyList<CustomerPurchaseDto> GetPurchaseHistory(int customerId);
    ValidationResult AddLoyaltyPoints(int customerId, int points);
    ValidationResult RedeemLoyaltyPoints(int customerId, int points);
    ValidationResult AddCustomer(Customer customer);
    ValidationResult UpdateCustomer(Customer customer);
    ValidationResult DeleteCustomer(int customerId);
}
