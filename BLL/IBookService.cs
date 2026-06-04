using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public interface IBookService
{
    IReadOnlyList<Book> GetBooks();
    IReadOnlyList<Book> SearchBooks(string searchText);
    IReadOnlyList<BookViewDto> GetBookViews();
    IReadOnlyList<BookViewDto> SearchBookViews(string searchText);
    IReadOnlyList<BookViewDto> GetFilteredBookViews(BookFilter filter);
    IReadOnlyList<string> GetPublishers();
    Book? GetBook(int bookId);
    ValidationResult AddBook(Book book);
    ValidationResult UpdateBook(Book book);
    ValidationResult DeleteBook(int bookId);
}
