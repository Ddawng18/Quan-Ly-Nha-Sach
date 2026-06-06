using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DTO;
using Microsoft.Data.SqlClient;

namespace BookStoreApp.DAL.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    public IReadOnlyList<Employee> GetAll()
    {
        var list = new List<Employee>();
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand("SELECT * FROM Employees ORDER BY FullName", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) list.Add(MapEmployee(reader));
        return list;
    }

    public Employee? GetById(int id)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand("SELECT * FROM Employees WHERE EmployeeID = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        using var reader = cmd.ExecuteReader();
        return reader.Read() ? MapEmployee(reader) : null;
    }

    public void Add(Employee e)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(@"
            INSERT INTO Employees (FullName, Phone, Salary, Position, Role, CreatedDate)
            OUTPUT INSERTED.EmployeeID VALUES (@name, @phone, @salary, @pos, @role, GETDATE())", conn);
        BindEmployee(cmd, e);
        e.EmployeeID = (int)cmd.ExecuteScalar();
    }

    public void Update(Employee e)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(@"
            UPDATE Employees SET FullName=@name, Phone=@phone, Salary=@salary, Position=@pos, Role=@role
            WHERE EmployeeID = @id", conn);
        BindEmployee(cmd, e);
        cmd.Parameters.AddWithValue("@id", e.EmployeeID);
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand("DELETE FROM Employees WHERE EmployeeID = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    private static void BindEmployee(SqlCommand cmd, Employee e)
    {
        cmd.Parameters.AddWithValue("@name",   e.FullName);
        cmd.Parameters.AddWithValue("@phone",  (object?)e.Phone    ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@salary", e.Salary);
        cmd.Parameters.AddWithValue("@pos",    (object?)e.Position ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@role",   e.Role);
    }

    private static Employee MapEmployee(SqlDataReader r) => new()
    {
        EmployeeID  = (int)r["EmployeeID"],
        FullName    = r["FullName"].ToString()!,
        Phone       = r["Phone"]?.ToString()    ?? "",
        Salary      = (decimal)r["Salary"],
        Position    = r["Position"]?.ToString() ?? "",
        Role        = r["Role"].ToString()!,
        CreatedDate = (DateTime)r["CreatedDate"]
    };
}
