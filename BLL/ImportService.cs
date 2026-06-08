using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public class ImportService : IImportService
{
    private readonly IImportRepository _importRepository;
    private readonly IBookRepository   _bookRepository;

    public ImportService(IImportRepository importRepository, IBookRepository bookRepository)
    {
        _importRepository = importRepository;
        _bookRepository   = bookRepository;
    }

    public IReadOnlyList<ImportReceiptViewDto> GetAll()
        => _importRepository.GetAll();

    public IReadOnlyList<ImportReceiptViewDto> GetBySupplier(int supplierId)
        => _importRepository.GetBySupplier(supplierId);

    public IReadOnlyList<ImportDetailViewDto> GetDetails(int importId)
        => _importRepository.GetDetails(importId);

    public ValidationResult CreateImport(ImportReceipt receipt, IReadOnlyList<ImportDetail> details)
    {
        if (receipt.SupplierID <= 0)
            return ValidationResult.Fail("Vui lòng chọn nhà cung cấp.");

        if (receipt.EmployeeID <= 0)
            return ValidationResult.Fail("Vui lòng chọn nhân viên thực hiện.");

        if (details.Count == 0)
            return ValidationResult.Fail("Vui lòng thêm ít nhất một đầu sách vào đơn nhập.");

        foreach (var d in details)
        {
            if (d.Quantity <= 0)
                return ValidationResult.Fail("Số lượng nhập phải lớn hơn 0.");

            if (d.ImportPrice < 0)
                return ValidationResult.Fail("Giá nhập không được âm.");

            var book = _bookRepository.GetById(d.BookID);
            if (book is null || book.IsDeleted)
                return ValidationResult.Fail($"Sách ID #{d.BookID} không tồn tại trong hệ thống.");
        }

        // Tính tổng tiền đơn nhập
        receipt.TotalAmount = details.Sum(d => d.Subtotal);

        _importRepository.CreateImport(receipt, details);
        return ValidationResult.Ok();
    }
}
