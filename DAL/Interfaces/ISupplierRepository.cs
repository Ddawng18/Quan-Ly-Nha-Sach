using BookStoreApp.DTO;

namespace BookStoreApp.DAL.Interfaces;

public interface ISupplierRepository
{
    IReadOnlyList<Supplier> GetAll();
    Supplier? GetById(int supplierId);
    void Add(Supplier supplier);
    void Update(Supplier supplier);
    void Delete(int supplierId);
    bool IsInUse(int supplierId);
}
