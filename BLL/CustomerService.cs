using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DAL.Repositories;
using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public IReadOnlyList<Customer> GetCustomers() => _customerRepository.GetAll();

    public IReadOnlyList<Customer> SearchCustomers(string searchText)
    {
        var customers = _customerRepository.GetAll();

        if (string.IsNullOrWhiteSpace(searchText))
        {
            return customers;
        }

        return customers
            .Where(c =>
                c.FullName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                c.Phone.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                c.Address.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public Customer? GetCustomer(int customerId) => _customerRepository.GetById(customerId);

    public CustomerStatsDto GetStats() => _customerRepository.GetStats();

    public IReadOnlyList<CustomerPurchaseDto> GetPurchaseHistory(int customerId) =>
        _customerRepository.GetPurchaseHistory(customerId);

    public ValidationResult AddLoyaltyPoints(int customerId, int points)
    {
        if (points < 0)
        {
            return ValidationResult.Fail("Points cannot be negative.");
        }

        var customer = _customerRepository.GetById(customerId);
        if (customer is null)
        {
            return ValidationResult.Fail("Customer not found.");
        }

        _customerRepository.UpdateLoyaltyPoints(customerId, customer.LoyaltyPoints + points);
        return ValidationResult.Ok();
    }

    public ValidationResult RedeemLoyaltyPoints(int customerId, int points)
    {
        if (points < 0)
        {
            return ValidationResult.Fail("Points cannot be negative.");
        }

        var customer = _customerRepository.GetById(customerId);
        if (customer is null)
        {
            return ValidationResult.Fail("Customer not found.");
        }

        if (points > customer.LoyaltyPoints)
        {
            return ValidationResult.Fail("Not enough loyalty points.");
        }

        _customerRepository.UpdateLoyaltyPoints(customerId, customer.LoyaltyPoints - points);
        return ValidationResult.Ok();
    }

    public ValidationResult AddCustomer(Customer customer)
    {
        var validation = Validate(customer, isUpdate: false);
        if (!validation.IsValid)
        {
            return validation;
        }

        _customerRepository.Add(customer);
        return ValidationResult.Ok();
    }

    public ValidationResult UpdateCustomer(Customer customer)
    {
        var validation = Validate(customer, isUpdate: true);
        if (!validation.IsValid)
        {
            return validation;
        }

        if (_customerRepository.GetById(customer.CustomerID) is null)
        {
            return ValidationResult.Fail("Customer not found.");
        }

        var existing = _customerRepository.GetById(customer.CustomerID);
        if (existing is not null)
        {
            customer.LoyaltyPoints = existing.LoyaltyPoints;
        }

        _customerRepository.Update(customer);
        return ValidationResult.Ok();
    }

    public ValidationResult DeleteCustomer(int customerId)
    {
        if (_customerRepository.GetById(customerId) is null)
        {
            return ValidationResult.Fail("Customer not found.");
        }

        _customerRepository.Delete(customerId);
        return ValidationResult.Ok();
    }

    private static ValidationResult Validate(Customer customer, bool isUpdate)
    {
        if (isUpdate && customer.CustomerID <= 0)
        {
            return ValidationResult.Fail("Invalid customer ID.");
        }

        if (string.IsNullOrWhiteSpace(customer.FullName))
        {
            return ValidationResult.Fail("Full name is required.");
        }

        if (string.IsNullOrWhiteSpace(customer.Phone))
        {
            return ValidationResult.Fail("Phone is required.");
        }

        return ValidationResult.Ok();
    }
}
