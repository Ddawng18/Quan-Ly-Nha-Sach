using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DTO;
using BookStoreApp.Utilities;

namespace BookStoreApp.DAL.Repositories;

public class SupplierRepository : ISupplierRepository
{
    public IReadOnlyList<Supplier> GetAll() => FakeDatabase.Suppliers.ToList();

    public Supplier? GetById(int supplierId) =>
        FakeDatabase.Suppliers.FirstOrDefault(s => s.SupplierID == supplierId);

    public void Add(Supplier supplier)
    {
        supplier.SupplierID = FakeDatabase.Suppliers.Count == 0
            ? 1
            : FakeDatabase.Suppliers.Max(s => s.SupplierID) + 1;
        FakeDatabase.Suppliers.Add(supplier);
    }

    public void Update(Supplier supplier)
    {
        var index = FakeDatabase.Suppliers.FindIndex(s => s.SupplierID == supplier.SupplierID);
        if (index >= 0)
        {
            FakeDatabase.Suppliers[index] = supplier;
        }
    }

    public void Delete(int supplierId)
    {
        var supplier = FakeDatabase.Suppliers.FirstOrDefault(s => s.SupplierID == supplierId);
        if (supplier is not null)
        {
            FakeDatabase.Suppliers.Remove(supplier);
        }
    }

    public bool IsInUse(int supplierId) =>
        FakeDatabase.Books.Any(b => !b.IsDeleted && b.SupplierID == supplierId);
}
