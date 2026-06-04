using BookStoreApp.DTO;

namespace BookStoreApp.Forms;

public partial class SupplierEditForm : Form
{
    private readonly Supplier? _existing;

    public Supplier Supplier { get; private set; } = new();

    public SupplierEditForm()
    {
        InitializeComponent();
        Text = "Add Supplier";
    }

    public SupplierEditForm(Supplier supplier)
        : this()
    {
        _existing = supplier;
        Text = "Edit Supplier";
        txtSupplierName.Text = supplier.SupplierName;
        txtAddress.Text = supplier.Address;
        txtEmail.Text = supplier.Email;
        txtPhone.Text = supplier.Phone;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        Supplier = new Supplier
        {
            SupplierID = _existing?.SupplierID ?? 0,
            SupplierName = txtSupplierName.Text.Trim(),
            Address = txtAddress.Text.Trim(),
            Email = txtEmail.Text.Trim(),
            Phone = txtPhone.Text.Trim()
        };

        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
