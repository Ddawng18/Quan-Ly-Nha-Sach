using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DAL.Repositories;
using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;

    public SupplierService(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public IReadOnlyList<Supplier> GetSuppliers() => _supplierRepository.GetAll();

    public IReadOnlyList<Supplier> SearchSuppliers(string searchText)
    {
        var suppliers = _supplierRepository.GetAll();

        if (string.IsNullOrWhiteSpace(searchText))
        {
            return suppliers;
        }

        return suppliers
            .Where(s =>
                s.SupplierName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                s.Address.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                s.Email.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                s.Phone.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public Supplier? GetSupplier(int supplierId) => _supplierRepository.GetById(supplierId);

    public ValidationResult AddSupplier(Supplier supplier)
    {
        var validation = Validate(supplier, isUpdate: false);
        if (!validation.IsValid)
        {
            return validation;
        }

        _supplierRepository.Add(supplier);
        return ValidationResult.Ok();
    }

    public ValidationResult UpdateSupplier(Supplier supplier)
    {
        var validation = Validate(supplier, isUpdate: true);
        if (!validation.IsValid)
        {
            return validation;
        }

        if (_supplierRepository.GetById(supplier.SupplierID) is null)
        {
            return ValidationResult.Fail("Supplier not found.");
        }

        _supplierRepository.Update(supplier);
        return ValidationResult.Ok();
    }

    public ValidationResult DeleteSupplier(int supplierId)
    {
        if (_supplierRepository.GetById(supplierId) is null)
        {
            return ValidationResult.Fail("Supplier not found.");
        }

        if (_supplierRepository.IsInUse(supplierId))
        {
            return ValidationResult.Fail("Cannot delete: supplier is used by books.");
        }

        _supplierRepository.Delete(supplierId);
        return ValidationResult.Ok();
    }

    private static ValidationResult Validate(Supplier supplier, bool isUpdate)
    {
        if (isUpdate && supplier.SupplierID <= 0)
        {
            return ValidationResult.Fail("Invalid supplier ID.");
        }

        if (string.IsNullOrWhiteSpace(supplier.SupplierName))
        {
            return ValidationResult.Fail("Supplier name is required.");
        }

        if (string.IsNullOrWhiteSpace(supplier.Email))
        {
            return ValidationResult.Fail("Email is required.");
        }

        if (string.IsNullOrWhiteSpace(supplier.Phone))
        {
            return ValidationResult.Fail("Phone is required.");
        }

        return ValidationResult.Ok();
    }
}
