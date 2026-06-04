using BookStoreApp.DTO;

namespace BookStoreApp.Forms;

public partial class CustomerEditForm : Form
{
    private readonly Customer? _existing;

    public Customer Customer { get; private set; } = new();

    public CustomerEditForm()
    {
        InitializeComponent();
        Text = "Add Customer";
    }

    public CustomerEditForm(Customer customer)
        : this()
    {
        _existing = customer;
        Text = "Edit Customer";
        txtFullName.Text = customer.FullName;
        txtPhone.Text = customer.Phone;
        txtAddress.Text = customer.Address;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        Customer = new Customer
        {
            CustomerID = _existing?.CustomerID ?? 0,
            FullName = txtFullName.Text.Trim(),
            Phone = txtPhone.Text.Trim(),
            Address = txtAddress.Text.Trim(),
            CreatedDate = _existing?.CreatedDate ?? DateTime.Now
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
