using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DAL.Repositories;
using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public IReadOnlyList<Employee> GetEmployees() => _employeeRepository.GetAll();

    public IReadOnlyList<Employee> SearchEmployees(string searchText)
    {
        var employees = _employeeRepository.GetAll();

        if (string.IsNullOrWhiteSpace(searchText))
        {
            return employees;
        }

        return employees
            .Where(e =>
                e.FullName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                e.Phone.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                e.Position.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public Employee? GetEmployee(int employeeId) => _employeeRepository.GetById(employeeId);

    public ValidationResult AddEmployee(Employee employee)
    {
        var validation = Validate(employee, isUpdate: false);
        if (!validation.IsValid)
        {
            return validation;
        }

        _employeeRepository.Add(employee);
        return ValidationResult.Ok();
    }

    public ValidationResult UpdateEmployee(Employee employee)
    {
        var validation = Validate(employee, isUpdate: true);
        if (!validation.IsValid)
        {
            return validation;
        }

        if (_employeeRepository.GetById(employee.EmployeeID) is null)
        {
            return ValidationResult.Fail("Employee not found.");
        }

        _employeeRepository.Update(employee);
        return ValidationResult.Ok();
    }

    public ValidationResult DeleteEmployee(int employeeId)
    {
        if (_employeeRepository.GetById(employeeId) is null)
        {
            return ValidationResult.Fail("Employee not found.");
        }

        _employeeRepository.Delete(employeeId);
        return ValidationResult.Ok();
    }

    private static ValidationResult Validate(Employee employee, bool isUpdate)
    {
        if (isUpdate && employee.EmployeeID <= 0)
        {
            return ValidationResult.Fail("Invalid employee ID.");
        }

        if (string.IsNullOrWhiteSpace(employee.FullName))
        {
            return ValidationResult.Fail("Full name is required.");
        }

        if (string.IsNullOrWhiteSpace(employee.Position))
        {
            return ValidationResult.Fail("Position is required.");
        }

        if (string.IsNullOrWhiteSpace(employee.Role))
        {
            return ValidationResult.Fail("Role is required.");
        }

        if (!string.Equals(employee.Role, "Admin", StringComparison.OrdinalIgnoreCase) &&
            !string.Equals(employee.Role, "Staff", StringComparison.OrdinalIgnoreCase))
        {
            return ValidationResult.Fail("Role must be Admin or Staff.");
        }

        if (employee.Salary < 0)
        {
            return ValidationResult.Fail("Salary cannot be negative.");
        }

        return ValidationResult.Ok();
    }
}
