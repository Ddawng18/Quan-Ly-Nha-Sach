using BookStoreApp.DTO;

namespace BookStoreApp.DAL.Interfaces;

public interface IImportRepository
{
    IReadOnlyList<ImportReceiptViewDto> GetAll();
    IReadOnlyList<ImportReceiptViewDto> GetBySupplier(int supplierId);
    IReadOnlyList<ImportDetailViewDto> GetDetails(int importId);
    void CreateImport(ImportReceipt receipt, IReadOnlyList<ImportDetail> details);
}
