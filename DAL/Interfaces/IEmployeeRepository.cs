using BookStoreApp.DTO;

namespace BookStoreApp.DAL.Interfaces;

public interface IEmployeeRepository
{
    IReadOnlyList<Employee> GetAll();
    Employee? GetById(int employeeId);
    void Add(Employee employee);
    void Update(Employee employee);
    void Delete(int employeeId);
}
