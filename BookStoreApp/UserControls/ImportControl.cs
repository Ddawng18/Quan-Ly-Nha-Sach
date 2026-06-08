using BookStoreApp.BLL;
using BookStoreApp.DTO;
using BookStoreApp.Theme;

namespace BookStoreApp.UserControls;

public partial class ImportControl : UserControl
{
    private readonly IImportService   _importService   = ServiceLocator.ImportService;
    private readonly ISupplierService _supplierService = ServiceLocator.SupplierService;

    public ImportControl()
    {
        InitializeComponent();
        ApplyTheme();
        LoadSupplierFilter();
        RefreshList();
    }

    private void ApplyTheme()
    {
        BackColor = AppTheme.MainBackground;
        AppTheme.ApplyGridStyle(dgvReceipts);
        AppTheme.ApplyGridStyle(dgvDetails);
    }

    private void LoadSupplierFilter()
    {
        var suppliers = _supplierService.GetSuppliers().ToList();
        cboFilterSupplier.DisplayMember = nameof(Supplier.SupplierName);
        cboFilterSupplier.ValueMember   = nameof(Supplier.SupplierID);
        cboFilterSupplier.Items.Add(new Supplier { SupplierID = 0, SupplierName = "-- All --" });
        foreach (var s in suppliers) cboFilterSupplier.Items.Add(s);
        cboFilterSupplier.SelectedIndex = 0;
    }

    private void RefreshList()
    {
        IReadOnlyList<ImportReceiptViewDto> receipts;

        if (cboFilterSupplier.SelectedItem is Supplier { SupplierID: > 0 } sup)
            receipts = _importService.GetBySupplier(sup.SupplierID);
        else
            receipts = _importService.GetAll();

        dgvReceipts.DataSource = receipts.Select(r => new
        {
            ImportID    = r.ImportID,
            Supplier    = r.SupplierName,
            Employee    = r.EmployeeName,
            ImportDate  = r.ImportDate.ToString("dd/MM/yyyy HH:mm"),
            TotalAmount = r.TotalAmount,
            Note        = r.Note ?? ""
        }).ToList();

        dgvDetails.DataSource = null;
    }

    private void dgvReceipts_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvReceipts.CurrentRow?.DataBoundItem is null) return;

        if (dgvReceipts.CurrentRow.Index < 0) return;

        var allReceipts = _importService.GetAll();
        if (dgvReceipts.CurrentRow.Index >= allReceipts.Count) return;

        var receipt = allReceipts[dgvReceipts.CurrentRow.Index];
        var details = _importService.GetDetails(receipt.ImportID);

        dgvDetails.DataSource = details.Select(d => new
        {
            Book        = d.BookTitle,
            ISBN        = d.ISBN,
            Quantity    = d.Quantity,
            ImportPrice = d.ImportPrice,
            Subtotal    = d.Subtotal
        }).ToList();
    }

    private void btnNewImport_Click(object sender, EventArgs e)
    {
        using var form = new Forms.ImportForm();
        if (form.ShowDialog() == DialogResult.OK)
            RefreshList();
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        cboFilterSupplier.SelectedIndex = 0;
        RefreshList();
    }

    private void cboFilterSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshList();
    }
}
