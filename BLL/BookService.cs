using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly ICategoryRepository _categoryRepository;

    public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository)
    {
        _bookRepository = bookRepository;
        _categoryRepository = categoryRepository;
    }

    public IReadOnlyList<Book> GetBooks() => _bookRepository.GetAll();

    public IReadOnlyList<Book> SearchBooks(string searchText)
    {
        var books = _bookRepository.GetAll();

        if (string.IsNullOrWhiteSpace(searchText))
            return books;

        return books
            .Where(x =>
                x.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                x.Author.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                x.ISBN.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public IReadOnlyList<BookViewDto> GetBookViews() => MapToViews(_bookRepository.GetAll());

    public IReadOnlyList<BookViewDto> SearchBookViews(string searchText) =>
        MapToViews(SearchBooks(searchText));

    public IReadOnlyList<BookViewDto> GetFilteredBookViews(BookFilter filter)
    {
        filter ??= new BookFilter();
        var query = _bookRepository.GetAll().AsEnumerable();
        var search = filter.SearchText.Trim();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x =>
                x.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                x.Author.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                x.ISBN.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        if (filter.CategoryId.HasValue)
            query = query.Where(x => x.CategoryID == filter.CategoryId.Value);

        if (!string.IsNullOrWhiteSpace(filter.Publisher))
            query = query.Where(x => x.Publisher.Equals(filter.Publisher, StringComparison.OrdinalIgnoreCase));

        return MapToViews(query);
    }

    public IReadOnlyList<string> GetPublishers() =>
        _bookRepository.GetAll()
            .Select(b => b.Publisher)
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(p => p)
            .ToList();

    public Book? GetBook(int bookId) => _bookRepository.GetById(bookId);

    public ValidationResult AddBook(Book book)
    {
        var validation = Validate(book, isUpdate: false);
        if (!validation.IsValid) return validation;

        _bookRepository.Add(book);
        return ValidationResult.Ok();
    }

    public ValidationResult UpdateBook(Book book)
    {
        var validation = Validate(book, isUpdate: true);
        if (!validation.IsValid) return validation;

        if (_bookRepository.GetById(book.BookID) is null)
            return ValidationResult.Fail("Book not found.");

        _bookRepository.Update(book);
        return ValidationResult.Ok();
    }

    public ValidationResult DeleteBook(int bookId)
    {
        if (_bookRepository.GetById(bookId) is null)
            return ValidationResult.Fail("Book not found.");

        _bookRepository.Delete(bookId);
        return ValidationResult.Ok();
    }

    // ── helpers ──────────────────────────────────────────────

    private IReadOnlyList<BookViewDto> MapToViews(IEnumerable<Book> books) =>
        books.Select(MapToView).ToList();

    private BookViewDto MapToView(Book book)
    {
        var categoryName = _categoryRepository.GetById(book.CategoryID)?.CategoryName ?? "-";

        return new BookViewDto
        {
            BookID       = book.BookID,
            CategoryID   = book.CategoryID,
            CategoryName = categoryName,
            Title        = book.Title,
            Author       = book.Author,
            ISBN         = book.ISBN,
            Publisher    = book.Publisher,
            PublishYear  = book.PublishYear,
            SellPrice    = book.SellPrice,
        };
    }

    private static ValidationResult Validate(Book book, bool isUpdate)
    {
        if (book is null)
            return ValidationResult.Fail("Book data is required.");

        if (isUpdate && book.BookID <= 0)
            return ValidationResult.Fail("Invalid book ID.");

        if (string.IsNullOrWhiteSpace(book.Title))
            return ValidationResult.Fail("Title is required.");

        if (string.IsNullOrWhiteSpace(book.Author))
            return ValidationResult.Fail("Author is required.");

        if (string.IsNullOrWhiteSpace(book.ISBN))
            return ValidationResult.Fail("ISBN is required.");

        if (book.SellPrice < 0)
            return ValidationResult.Fail("Sell price cannot be negative.");

        return ValidationResult.Ok();
    }
}
