using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DTO;
using Microsoft.Data.SqlClient;

namespace BookStoreApp.DAL.Repositories;

public class CategoryRepository : ICategoryRepository
{
    public IReadOnlyList<Category> GetAll()
    {
        var list = new List<Category>();
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand("SELECT * FROM Categories ORDER BY CategoryName", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            list.Add(new Category { CategoryID = (int)reader["CategoryID"], CategoryName = reader["CategoryName"].ToString()! });
        return list;
    }

    public Category? GetById(int id)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand("SELECT * FROM Categories WHERE CategoryID = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        using var reader = cmd.ExecuteReader();
        return reader.Read()
            ? new Category { CategoryID = (int)reader["CategoryID"], CategoryName = reader["CategoryName"].ToString()! }
            : null;
    }

    public void Add(Category cat)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(
            "INSERT INTO Categories (CategoryName) OUTPUT INSERTED.CategoryID VALUES (@name)", conn);
        cmd.Parameters.AddWithValue("@name", cat.CategoryName);
        cat.CategoryID = (int)cmd.ExecuteScalar();
    }

    public void Update(Category cat)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(
            "UPDATE Categories SET CategoryName = @name WHERE CategoryID = @id", conn);
        cmd.Parameters.AddWithValue("@name", cat.CategoryName);
        cmd.Parameters.AddWithValue("@id",   cat.CategoryID);
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand("DELETE FROM Categories WHERE CategoryID = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }

    public bool IsInUse(int id)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(
            "SELECT COUNT(1) FROM Books WHERE CategoryID = @id AND IsDeleted = 0", conn);
        cmd.Parameters.AddWithValue("@id", id);
        return (int)cmd.ExecuteScalar() > 0;
    }
}
