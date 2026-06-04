using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DTO;
using BookStoreApp.Utilities;

namespace BookStoreApp.DAL.Repositories;

public class CategoryRepository : ICategoryRepository
{
    public IReadOnlyList<Category> GetAll() => FakeDatabase.Categories.ToList();

    public Category? GetById(int categoryId) =>
        FakeDatabase.Categories.FirstOrDefault(c => c.CategoryID == categoryId);

    public void Add(Category category)
    {
        category.CategoryID = FakeDatabase.Categories.Count == 0
            ? 1
            : FakeDatabase.Categories.Max(c => c.CategoryID) + 1;
        FakeDatabase.Categories.Add(category);
    }

    public void Update(Category category)
    {
        var index = FakeDatabase.Categories.FindIndex(c => c.CategoryID == category.CategoryID);
        if (index >= 0)
        {
            FakeDatabase.Categories[index] = category;
        }
    }

    public void Delete(int categoryId)
    {
        var category = FakeDatabase.Categories.FirstOrDefault(c => c.CategoryID == categoryId);
        if (category is not null)
        {
            FakeDatabase.Categories.Remove(category);
        }
    }

    public bool IsInUse(int categoryId) =>
        FakeDatabase.Books.Any(b => !b.IsDeleted && b.CategoryID == categoryId);
}
