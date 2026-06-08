using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DTO;
using Microsoft.Data.SqlClient;

namespace BookStoreApp.DAL.Repositories;

public class BookRepository : IBookRepository
{
    public IReadOnlyList<Book> GetAll()
    {
        var books = new List<Book>();
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(
            "SELECT * FROM Books WHERE IsDeleted = 0", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            books.Add(MapBook(reader));
        return books;
    }

    public Book? GetById(int bookId)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(
            "SELECT * FROM Books WHERE BookID = @id AND IsDeleted = 0", conn);
        cmd.Parameters.AddWithValue("@id", bookId);
        using var reader = cmd.ExecuteReader();
        return reader.Read() ? MapBook(reader) : null;
    }

    public void Add(Book book)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(@"
            INSERT INTO Books
                (CategoryID, Title, Author, ISBN, Publisher,
                 PublishYear, SellPrice, IsDeleted)
            OUTPUT INSERTED.BookID
            VALUES
                (@cat, @title, @author, @isbn, @pub,
                 @year, @sell, 0)", conn);

        BindBookParams(cmd, book);
        book.BookID = (int)cmd.ExecuteScalar();
    }

    public void Update(Book book)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(@"
            UPDATE Books SET
                CategoryID      = @cat,
                Title           = @title,
                Author          = @author,
                ISBN            = @isbn,
                Publisher       = @pub,
                PublishYear     = @year,
                SellPrice       = @sell
            WHERE BookID = @id", conn);

        BindBookParams(cmd, book);
        cmd.Parameters.AddWithValue("@id", book.BookID);
        cmd.ExecuteNonQuery();
    }

    public void UpdateStock(int bookId, int quantityInStock, DateTime? lastSoldDate = null)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(@"
            UPDATE Books SET
                QuantityInStock = @qty,
                LastSoldDate    = ISNULL(@lastSold, LastSoldDate)
            WHERE BookID = @id", conn);
        cmd.Parameters.AddWithValue("@qty",      quantityInStock);
        cmd.Parameters.AddWithValue("@lastSold", (object?)lastSoldDate ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@id",       bookId);
        cmd.ExecuteNonQuery();
    }

    public void Delete(int bookId)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(
            "UPDATE Books SET IsDeleted = 1 WHERE BookID = @id", conn);
        cmd.Parameters.AddWithValue("@id", bookId);
        cmd.ExecuteNonQuery();
    }

    // ── helpers ──────────────────────────────────────────────
    private static void BindBookParams(SqlCommand cmd, Book b)
    {
        cmd.Parameters.AddWithValue("@cat",    b.CategoryID);
        cmd.Parameters.AddWithValue("@title",  b.Title);
        cmd.Parameters.AddWithValue("@author", b.Author);
        cmd.Parameters.AddWithValue("@isbn",   b.ISBN);
        cmd.Parameters.AddWithValue("@pub",    b.Publisher);
        cmd.Parameters.AddWithValue("@year",   b.PublishYear);
        cmd.Parameters.AddWithValue("@sell",   b.SellPrice);
    }

    private static Book MapBook(SqlDataReader r) => new()
    {
        BookID          = (int)r["BookID"],
        CategoryID      = (int)r["CategoryID"],
        Title           = r["Title"].ToString()!,
        Author          = r["Author"].ToString()!,
        ISBN            = r["ISBN"].ToString()!,
        Publisher       = r["Publisher"]?.ToString() ?? "",
        PublishYear     = r["PublishYear"] == DBNull.Value ? 0 : (int)r["PublishYear"],
        SellPrice       = (decimal)r["SellPrice"],
        QuantityInStock = (int)r["QuantityInStock"],
        ImportPrice     = (decimal)r["ImportPrice"],
        LastImportDate  = r["LastImportDate"] == DBNull.Value ? null : (DateTime?)r["LastImportDate"],
        LastSoldDate    = r["LastSoldDate"]   == DBNull.Value ? null : (DateTime?)r["LastSoldDate"],
        IsDeleted       = (bool)r["IsDeleted"],
    };
}
