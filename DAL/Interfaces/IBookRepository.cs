namespace BookStoreApp.DAL.Interfaces;

public interface IBookRepository
{
    IReadOnlyList<BookStoreApp.DTO.Book> GetAll();
    BookStoreApp.DTO.Book? GetById(int bookId);
    void Add(BookStoreApp.DTO.Book book);
    void Update(BookStoreApp.DTO.Book book);
    void UpdateStock(int bookId, int quantityInStock, DateTime? lastSoldDate = null);
    void Delete(int bookId);
}
