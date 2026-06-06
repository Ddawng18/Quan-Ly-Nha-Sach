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
                (CategoryID, SupplierID, Title, Author, ISBN, Publisher,
                 PublishYear, ImportPrice, SellPrice, QuantityInStock,
                 LastImportDate, LastSoldDate, IsDeleted)
            OUTPUT INSERTED.BookID
            VALUES
                (@cat, @sup, @title, @author, @isbn, @pub,
                 @year, @import, @sell, @qty,
                 @lastImport, @lastSold, 0)", conn);

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
                SupplierID      = @sup,
                Title           = @title,
                Author          = @author,
                ISBN            = @isbn,
                Publisher       = @pub,
                PublishYear     = @year,
                ImportPrice     = @import,
                SellPrice       = @sell,
                QuantityInStock = @qty,
                LastImportDate  = @lastImport,
                LastSoldDate    = @lastSold
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
        cmd.Parameters.AddWithValue("@cat",       b.CategoryID);
        cmd.Parameters.AddWithValue("@sup",       b.SupplierID);
        cmd.Parameters.AddWithValue("@title",     b.Title);
        cmd.Parameters.AddWithValue("@author",    b.Author);
        cmd.Parameters.AddWithValue("@isbn",      b.ISBN);
        cmd.Parameters.AddWithValue("@pub",       b.Publisher);
        cmd.Parameters.AddWithValue("@year",      b.PublishYear);
        cmd.Parameters.AddWithValue("@import",    b.ImportPrice);
        cmd.Parameters.AddWithValue("@sell",      b.SellPrice);
        cmd.Parameters.AddWithValue("@qty",       b.QuantityInStock);
        cmd.Parameters.AddWithValue("@lastImport",(object?)b.LastImportDate ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@lastSold",  (object?)b.LastSoldDate   ?? DBNull.Value);
    }

    private static Book MapBook(SqlDataReader r) => new()
    {
        BookID          = (int)r["BookID"],
        CategoryID      = (int)r["CategoryID"],
        SupplierID      = (int)r["SupplierID"],
        Title           = r["Title"].ToString()!,
        Author          = r["Author"].ToString()!,
        ISBN            = r["ISBN"].ToString()!,
        Publisher       = r["Publisher"]?.ToString() ?? "",
        PublishYear     = r["PublishYear"] == DBNull.Value ? 0 : (int)r["PublishYear"],
        ImportPrice     = (decimal)r["ImportPrice"],
        SellPrice       = (decimal)r["SellPrice"],
        QuantityInStock = (int)r["QuantityInStock"],
        LastImportDate  = r["LastImportDate"] == DBNull.Value ? null : (DateTime?)r["LastImportDate"],
        LastSoldDate    = r["LastSoldDate"]   == DBNull.Value ? null : (DateTime?)r["LastSoldDate"],
        IsDeleted       = (bool)r["IsDeleted"]
    };
}
