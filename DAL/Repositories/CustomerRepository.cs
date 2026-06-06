using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DTO;
using Microsoft.Data.SqlClient;

namespace BookStoreApp.DAL.Repositories;

public class CustomerRepository : ICustomerRepository
{
    public IReadOnlyList<Customer> GetAll()
    {
        var list = new List<Customer>();
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand("SELECT * FROM Customers ORDER BY FullName", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) list.Add(MapCustomer(reader));
        return list;
    }

    public Customer? GetById(int id)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand("SELECT * FROM Customers WHERE CustomerID = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        using var reader = cmd.ExecuteReader();
        return reader.Read() ? MapCustomer(reader) : null;
    }

    public void Add(Customer c)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(@"
            INSERT INTO Customers (FullName, Phone, Address, LoyaltyPoints, CreatedDate)
            OUTPUT INSERTED.CustomerID VALUES (@name, @phone, @addr, @pts, GETDATE())", conn);
        BindCustomer(cmd, c);
        c.CustomerID = (int)cmd.ExecuteScalar();
    }

    public void Update(Customer c)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(@"
            UPDATE Customers SET FullName=@name, Phone=@phone, Address=@addr, LoyaltyPoints=@pts
            WHERE CustomerID = @id", conn);
        BindCustomer(cmd, c);
        cmd.Parameters.AddWithValue("@id", c.CustomerID);
        cmd.ExecuteNonQuery();
    }

    public void UpdateLoyaltyPoints(int customerId, int points)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(
            "UPDATE Customers SET LoyaltyPoints = @pts WHERE CustomerID = @id", conn);
        cmd.Parameters.AddWithValue("@pts", points);
        cmd.Parameters.AddWithValue("@id",  customerId);
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand("DELETE FROM Customers WHERE CustomerID = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public CustomerStatsDto GetStats()
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(@"
            SELECT
                COUNT(*)                                        AS Total,
                ISNULL(SUM(LoyaltyPoints), 0)                  AS TotalPoints,
                COUNT(CASE WHEN MONTH(CreatedDate) = MONTH(GETDATE())
                            AND YEAR(CreatedDate)  = YEAR(GETDATE())
                           THEN 1 END)                         AS NewThisMonth
            FROM Customers", conn);
        using var reader = cmd.ExecuteReader();
        reader.Read();
        return new CustomerStatsDto
        {
            TotalCustomers     = (int)reader["Total"],
            TotalLoyaltyPoints = (int)reader["TotalPoints"],
            NewThisMonth       = (int)reader["NewThisMonth"]
        };
    }

    public IReadOnlyList<CustomerPurchaseDto> GetPurchaseHistory(int customerId)
    {
        var list = new List<CustomerPurchaseDto>();
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(@"
            SELECT
                od.OrderID,
                o.OrderDate,
                b.Title          AS BookTitle,
                od.Quantity,
                od.UnitPrice,
                od.Subtotal,
                o.TotalAmount    AS OrderTotal,
                o.PaymentStatus
            FROM OrderDetails od
            JOIN Orders  o ON o.OrderID  = od.OrderID
            JOIN Books   b ON b.BookID   = od.BookID
            WHERE o.CustomerID = @id
            ORDER BY o.OrderDate DESC", conn);
        cmd.Parameters.AddWithValue("@id", customerId);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            list.Add(new CustomerPurchaseDto
            {
                OrderID       = (int)reader["OrderID"],
                OrderDate     = (DateTime)reader["OrderDate"],
                BookTitle     = reader["BookTitle"].ToString()!,
                Quantity      = (int)reader["Quantity"],
                UnitPrice     = (decimal)reader["UnitPrice"],
                Subtotal      = (decimal)reader["Subtotal"],
                OrderTotal    = (decimal)reader["OrderTotal"],
                PaymentStatus = reader["PaymentStatus"].ToString()!
            });
        return list;
    }

    private static void BindCustomer(SqlCommand cmd, Customer c)
    {
        cmd.Parameters.AddWithValue("@name",  c.FullName);
        cmd.Parameters.AddWithValue("@phone", (object?)c.Phone   ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@addr",  (object?)c.Address ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@pts",   c.LoyaltyPoints);
    }

    private static Customer MapCustomer(SqlDataReader r) => new()
    {
        CustomerID    = (int)r["CustomerID"],
        FullName      = r["FullName"].ToString()!,
        Phone         = r["Phone"]?.ToString()   ?? "",
        Address       = r["Address"]?.ToString() ?? "",
        LoyaltyPoints = (int)r["LoyaltyPoints"],
        CreatedDate   = (DateTime)r["CreatedDate"]
    };
}
