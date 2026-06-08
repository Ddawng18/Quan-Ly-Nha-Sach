using BookStoreApp.BLL;
using BookStoreApp.DTO;

namespace BookStoreApp.UserControls;

public partial class ImportControl : UserControl
{
    private readonly IImportService   _importService   = ServiceLocator.ImportService;
    private readonly ISupplierService _supplierService = ServiceLocator.SupplierService;

    public ImportControl()
    {
        InitializeComponent();
        LoadSupplierFilter();
        RefreshList();
    }

    // ── Khởi tạo bộ lọc nhà cung cấp ───────────────────────
    private void LoadSupplierFilter()
    {
        var suppliers = _supplierService.GetSuppliers().ToList();
        cboFilterSupplier.DisplayMember = nameof(Supplier.SupplierName);
        cboFilterSupplier.ValueMember   = nameof(Supplier.SupplierID);
        cboFilterSupplier.Items.Add(new Supplier { SupplierID = 0, SupplierName = "-- Tất cả --" });
        foreach (var s in suppliers) cboFilterSupplier.Items.Add(s);
        cboFilterSupplier.SelectedIndex = 0;
    }

    // ── Load danh sách phiếu nhập ───────────────────────────
    private void RefreshList()
    {
        IReadOnlyList<ImportReceiptViewDto> receipts;

        if (cboFilterSupplier.SelectedItem is Supplier { SupplierID: > 0 } sup)
            receipts = _importService.GetBySupplier(sup.SupplierID);
        else
            receipts = _importService.GetAll();

        dgvReceipts.DataSource = receipts.Select(r => new
        {
            MãPhiếu     = r.ImportID,
            NhàCungCấp  = r.SupplierName,
            NhânViên    = r.EmployeeName,
            NgàyNhập    = r.ImportDate.ToString("dd/MM/yyyy HH:mm"),
            TổngTiền    = r.TotalAmount,
            GhiChú      = r.Note ?? ""
        }).ToList();

        dgvDetails.DataSource = null;
    }

    // ── Chọn phiếu → hiện chi tiết ──────────────────────────
    private void dgvReceipts_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvReceipts.CurrentRow?.DataBoundItem is null) return;

        // Lấy ImportID từ dòng đang chọn
        if (dgvReceipts.CurrentRow.Index < 0) return;

        var allReceipts = _importService.GetAll();
        if (dgvReceipts.CurrentRow.Index >= allReceipts.Count) return;

        var receipt = allReceipts[dgvReceipts.CurrentRow.Index];
        var details = _importService.GetDetails(receipt.ImportID);

        dgvDetails.DataSource = details.Select(d => new
        {
            Sách        = d.BookTitle,
            ISBN        = d.ISBN,
            SốLượng     = d.Quantity,
            GiáNhập     = d.ImportPrice,
            ThànhTiền   = d.Subtotal
        }).ToList();
    }

    // ── Nút Lập đơn nhập mới ────────────────────────────────
    private void btnNewImport_Click(object sender, EventArgs e)
    {
        using var form = new Forms.ImportForm();
        if (form.ShowDialog() == DialogResult.OK)
            RefreshList();
    }

    // ── Nút Refresh ─────────────────────────────────────────
    private void btnRefresh_Click(object sender, EventArgs e)
    {
        cboFilterSupplier.SelectedIndex = 0;
        RefreshList();
    }

    // ── Bộ lọc thay đổi ─────────────────────────────────────
    private void cboFilterSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshList();
    }
}
