using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public interface ISupplierService
{
    IReadOnlyList<Supplier> GetSuppliers();
    IReadOnlyList<Supplier> SearchSuppliers(string searchText);
    Supplier? GetSupplier(int supplierId);
    ValidationResult AddSupplier(Supplier supplier);
    ValidationResult UpdateSupplier(Supplier supplier);
    ValidationResult DeleteSupplier(int supplierId);
}
