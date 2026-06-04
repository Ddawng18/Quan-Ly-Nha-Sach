using BookStoreApp.DTO;

namespace BookStoreApp.DAL.Interfaces;

public interface ICategoryRepository
{
    IReadOnlyList<Category> GetAll();
    Category? GetById(int categoryId);
    void Add(Category category);
    void Update(Category category);
    void Delete(int categoryId);
    bool IsInUse(int categoryId);
}
