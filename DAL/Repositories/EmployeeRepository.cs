using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DTO;
using BookStoreApp.Utilities;

namespace BookStoreApp.DAL.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    public IReadOnlyList<Employee> GetAll() => FakeDatabase.Employees.ToList();

    public Employee? GetById(int employeeId) =>
        FakeDatabase.Employees.FirstOrDefault(e => e.EmployeeID == employeeId);

    public void Add(Employee employee)
    {
        employee.EmployeeID = FakeDatabase.Employees.Count == 0
            ? 1
            : FakeDatabase.Employees.Max(e => e.EmployeeID) + 1;
        employee.CreatedDate = DateTime.Now;
        FakeDatabase.Employees.Add(employee);
    }

    public void Update(Employee employee)
    {
        var index = FakeDatabase.Employees.FindIndex(e => e.EmployeeID == employee.EmployeeID);
        if (index >= 0)
        {
            FakeDatabase.Employees[index] = employee;
        }
    }

    public void Delete(int employeeId)
    {
        var employee = FakeDatabase.Employees.FirstOrDefault(e => e.EmployeeID == employeeId);
        if (employee is not null)
        {
            FakeDatabase.Employees.Remove(employee);
        }
    }
}
