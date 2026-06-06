using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DTO;
using Microsoft.Data.SqlClient;

namespace BookStoreApp.DAL.Repositories;

public class SupplierRepository : ISupplierRepository
{
    public IReadOnlyList<Supplier> GetAll()
    {
        var list = new List<Supplier>();
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand("SELECT * FROM Suppliers ORDER BY SupplierName", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read()) list.Add(MapSupplier(reader));
        return list;
    }

    public Supplier? GetById(int id)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand("SELECT * FROM Suppliers WHERE SupplierID = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        using var reader = cmd.ExecuteReader();
        return reader.Read() ? MapSupplier(reader) : null;
    }

    public void Add(Supplier s)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(@"
            INSERT INTO Suppliers (SupplierName, Address, Email, Phone)
            OUTPUT INSERTED.SupplierID VALUES (@name, @addr, @email, @phone)", conn);
        BindSupplier(cmd, s);
        s.SupplierID = (int)cmd.ExecuteScalar();
    }

    public void Update(Supplier s)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(@"
            UPDATE Suppliers SET SupplierName=@name, Address=@addr, Email=@email, Phone=@phone
            WHERE SupplierID = @id", conn);
        BindSupplier(cmd, s);
        cmd.Parameters.AddWithValue("@id", s.SupplierID);
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand("DELETE FROM Suppliers WHERE SupplierID = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public bool IsInUse(int id)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(
            "SELECT COUNT(1) FROM Books WHERE SupplierID = @id AND IsDeleted = 0", conn);
        cmd.Parameters.AddWithValue("@id", id);
        return (int)cmd.ExecuteScalar() > 0;
    }

    private static void BindSupplier(SqlCommand cmd, Supplier s)
    {
        cmd.Parameters.AddWithValue("@name",  s.SupplierName);
        cmd.Parameters.AddWithValue("@addr",  (object?)s.Address ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@email", (object?)s.Email   ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@phone", (object?)s.Phone   ?? DBNull.Value);
    }

    private static Supplier MapSupplier(SqlDataReader r) => new()
    {
        SupplierID   = (int)r["SupplierID"],
        SupplierName = r["SupplierName"].ToString()!,
        Address      = r["Address"]?.ToString() ?? "",
        Email        = r["Email"]?.ToString()   ?? "",
        Phone        = r["Phone"]?.ToString()   ?? ""
    };
}
