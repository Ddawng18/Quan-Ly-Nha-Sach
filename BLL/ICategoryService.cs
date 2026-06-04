using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public interface ICategoryService
{
    IReadOnlyList<Category> GetCategories();
    IReadOnlyList<Category> SearchCategories(string searchText);
    Category? GetCategory(int categoryId);
    ValidationResult AddCategory(Category category);
    ValidationResult UpdateCategory(Category category);
    ValidationResult DeleteCategory(int categoryId);
}
