using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DTO;
using BookStoreApp.Utilities;

namespace BookStoreApp.DAL.Repositories;

public class BookRepository : IBookRepository
{
    public IReadOnlyList<Book> GetAll() =>
        FakeDatabase.Books.Where(b => !b.IsDeleted).ToList();

    public Book? GetById(int bookId) =>
        FakeDatabase.Books.FirstOrDefault(b => b.BookID == bookId && !b.IsDeleted);

    public void Add(Book book)
    {
        book.BookID = FakeDatabase.Books.Count == 0
            ? 1
            : FakeDatabase.Books.Max(b => b.BookID) + 1;
        FakeDatabase.Books.Add(book);
    }

    public void Update(Book book)
    {
        var index = FakeDatabase.Books.FindIndex(b => b.BookID == book.BookID);
        if (index >= 0)
        {
            FakeDatabase.Books[index] = book;
        }
    }

    public void UpdateStock(int bookId, int quantityInStock, DateTime? lastSoldDate = null)
    {
        var book = FakeDatabase.Books.FirstOrDefault(b => b.BookID == bookId);
        if (book is not null)
        {
            book.QuantityInStock = quantityInStock;
            book.LastSoldDate = lastSoldDate ?? book.LastSoldDate;
        }
    }

    public void Delete(int bookId)
    {
        var book = FakeDatabase.Books.FirstOrDefault(b => b.BookID == bookId);
        if (book is not null)
        {
            book.IsDeleted = true;
        }
    }
}
