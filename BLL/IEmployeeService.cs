using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public interface IEmployeeService
{
    IReadOnlyList<Employee> GetEmployees();
    IReadOnlyList<Employee> SearchEmployees(string searchText);
    Employee? GetEmployee(int employeeId);
    ValidationResult AddEmployee(Employee employee);
    ValidationResult UpdateEmployee(Employee employee);
    ValidationResult DeleteEmployee(int employeeId);
}
