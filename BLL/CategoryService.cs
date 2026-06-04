using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DAL.Repositories;
using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public IReadOnlyList<Category> GetCategories() => _categoryRepository.GetAll();

    public IReadOnlyList<Category> SearchCategories(string searchText)
    {
        var categories = _categoryRepository.GetAll();

        if (string.IsNullOrWhiteSpace(searchText))
        {
            return categories;
        }

        return categories
            .Where(c => c.CategoryName.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public Category? GetCategory(int categoryId) => _categoryRepository.GetById(categoryId);

    public ValidationResult AddCategory(Category category)
    {
        var validation = Validate(category, isUpdate: false);
        if (!validation.IsValid)
        {
            return validation;
        }

        _categoryRepository.Add(category);
        return ValidationResult.Ok();
    }

    public ValidationResult UpdateCategory(Category category)
    {
        var validation = Validate(category, isUpdate: true);
        if (!validation.IsValid)
        {
            return validation;
        }

        if (_categoryRepository.GetById(category.CategoryID) is null)
        {
            return ValidationResult.Fail("Category not found.");
        }

        _categoryRepository.Update(category);
        return ValidationResult.Ok();
    }

    public ValidationResult DeleteCategory(int categoryId)
    {
        if (_categoryRepository.GetById(categoryId) is null)
        {
            return ValidationResult.Fail("Category not found.");
        }

        if (_categoryRepository.IsInUse(categoryId))
        {
            return ValidationResult.Fail("Cannot delete: category is used by books.");
        }

        _categoryRepository.Delete(categoryId);
        return ValidationResult.Ok();
    }

    private static ValidationResult Validate(Category category, bool isUpdate)
    {
        if (isUpdate && category.CategoryID <= 0)
        {
            return ValidationResult.Fail("Invalid category ID.");
        }

        if (string.IsNullOrWhiteSpace(category.CategoryName))
        {
            return ValidationResult.Fail("Category name is required.");
        }

        return ValidationResult.Ok();
    }
}
