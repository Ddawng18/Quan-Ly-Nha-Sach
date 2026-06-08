using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public interface IImportService
{
    IReadOnlyList<ImportReceiptViewDto> GetAll();
    IReadOnlyList<ImportReceiptViewDto> GetBySupplier(int supplierId);
    IReadOnlyList<ImportDetailViewDto> GetDetails(int importId);
    ValidationResult CreateImport(ImportReceipt receipt, IReadOnlyList<ImportDetail> details);
}
