using BookStoreApp.BLL;
using BookStoreApp.DTO;
using BookStoreApp.Theme;

namespace BookStoreApp.Forms;

public partial class ImportForm : Form
{
    private readonly IImportService _importService = ServiceLocator.ImportService;
    private readonly IBookService _bookService = ServiceLocator.BookService;
    private readonly ISupplierService _supplierService = ServiceLocator.SupplierService;
    private readonly IEmployeeService _employeeService = ServiceLocator.EmployeeService;

    private readonly List<ImportDetail> _lines = [];

    public ImportForm()
    {
        InitializeComponent();
        ApplyTheme();
        LoadLookups();
        RefreshGrid();
    }

    private void ApplyTheme()
    {
        BackColor = AppTheme.MainBackground;
        AppTheme.ApplyGridStyle(dgvLines);
    }

    private void LoadLookups()
    {
        cboSupplier.DisplayMember = nameof(Supplier.SupplierName);
        cboSupplier.ValueMember = nameof(Supplier.SupplierID);
        cboSupplier.DataSource = _supplierService.GetSuppliers().ToList();

        cboEmployee.DisplayMember = nameof(Employee.FullName);
        cboEmployee.ValueMember = nameof(Employee.EmployeeID);
        cboEmployee.DataSource = _employeeService.GetEmployees().ToList();

        var books = _bookService.GetBooks().Where(b => !b.IsDeleted).ToList();
        cboBook.DisplayMember = nameof(Book.Title);
        cboBook.ValueMember = nameof(Book.BookID);
        cboBook.DataSource = books;
    }

    private void btnAddLine_Click(object sender, EventArgs e)
    {
        if (cboBook.SelectedItem is not Book book)
        {
            MessageBox.Show("Please select a book.", "Import",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var qty = (int)numQuantity.Value;
        var price = numImportPrice.Value;

        if (qty <= 0)
        {
            MessageBox.Show("Quantity must be greater than 0.", "Import",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (price <= 0)
        {
            MessageBox.Show("Import price must be greater than 0.", "Import",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var existing = _lines.FirstOrDefault(l => l.BookID == book.BookID);
        if (existing is not null)
        {
            existing.Quantity += qty;
            existing.Subtotal = existing.ImportPrice * existing.Quantity;
        }
        else
        {
            _lines.Add(new ImportDetail
            {
                BookID = book.BookID,
                Quantity = qty,
                ImportPrice = price,
                Subtotal = price * qty
            });
        }

        RefreshGrid();
        UpdateTotal();
    }

    private void btnRemoveLine_Click(object sender, EventArgs e)
    {
        if (dgvLines.CurrentRow?.Index is not int idx || idx < 0 || idx >= _lines.Count)
            return;

        _lines.RemoveAt(idx);
        RefreshGrid();
        UpdateTotal();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        var receipt = new ImportReceipt
        {
            SupplierID = cboSupplier.SelectedValue is int sid ? sid : 0,
            EmployeeID = cboEmployee.SelectedValue is int eid ? eid : 0,
            Note = txtNote.Text.Trim()
        };

        var result = _importService.CreateImport(receipt, _lines);
        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        MessageBox.Show($"Import receipt #{receipt.ImportID} saved successfully!\n" +
                        $"Inventory has been updated automatically.",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void RefreshGrid()
    {
        var books = _bookService.GetBooks().ToDictionary(b => b.BookID, b => b.Title);

        dgvLines.DataSource = _lines.Select(l => new
        {
            Book = books.GetValueOrDefault(l.BookID, $"ID#{l.BookID}"),
            Quantity = l.Quantity,
            ImportPrice = l.ImportPrice,
            Subtotal = l.Subtotal
        }).ToList();
    }

    private void UpdateTotal()
    {
        lblTotal.Text = $"Total: {_lines.Sum(l => l.Subtotal):N0} VND";
    }

    private void numImportPrice_ValueChanged(object sender, EventArgs e)
    {

    }
}
