using BookStoreApp.BLL;
using BookStoreApp.DTO;
using BookStoreApp.Forms;
using BookStoreApp.Theme;

namespace BookStoreApp.UserControls;

public partial class CustomersControl : UserControl
{
    private readonly ICustomerService _customerService = ServiceLocator.CustomerService;

    public CustomersControl()
    {
        InitializeComponent();
        cboFilter.Items.AddRange(["All customers", "New this month"]);
        cboFilter.SelectedIndex = 0;
        ApplyTheme();
        LoadCustomers();
    }

    private void ApplyTheme()
    {
        BackColor = AppTheme.MainBackground;
        panelToolbar.BackColor = AppTheme.MainBackground;
        AppTheme.StyleActionButton(btnAdd, AppTheme.Add);
        AppTheme.StyleActionButton(btnEdit, AppTheme.Edit);
        AppTheme.StyleActionButton(btnDelete, AppTheme.Delete);
        AppTheme.StyleRefreshButton(btnRefresh);
        AppTheme.StyleActionButton(btnSearch, AppTheme.Add);
        AppTheme.ApplyGridStyle(dgvCustomers);
        AppTheme.ApplyGridStyle(dgvPurchaseHistory);
    }

    private void LoadCustomers()
    {
        var stats = _customerService.GetStats();
        lblStats.Text = $"Total: {stats.TotalCustomers} | New this month: {stats.NewThisMonth} | Points: {stats.TotalLoyaltyPoints}";

        var customers = _customerService.SearchCustomers(txtSearch.Text).AsEnumerable();
        if (cboFilter.SelectedIndex == 1)
        {
            var now = DateTime.Now;
            customers = customers.Where(c =>
                c.CreatedDate.Year == now.Year && c.CreatedDate.Month == now.Month);
        }

        dgvCustomers.DataSource = null;
        dgvCustomers.DataSource = customers.ToList();
        ConfigureGridColumns();
        LoadPurchaseHistory();
    }

    private void ConfigureGridColumns()
    {
        if (dgvCustomers.Columns.Count == 0)
        {
            return;
        }

        var columns = new (string Property, string Header, int Width)[]
        {
            ("CustomerID", "CustomerID", 90),
            ("FullName", "FullName", 180),
            ("Phone", "Phone", 120),
            ("Address", "Address", 200),
            ("LoyaltyPoints", "LoyaltyPoints", 110),
            ("CreatedDate", "CreatedDate", 140)
        };

        var displayIndex = 0;
        foreach (var (property, header, width) in columns)
        {
            if (dgvCustomers.Columns[property] is not DataGridViewColumn column)
            {
                continue;
            }

            column.Visible = true;
            column.HeaderText = header;
            column.Width = width;
            column.DisplayIndex = displayIndex++;
        }

        if (dgvCustomers.Columns["CreatedDate"] is DataGridViewColumn createdDate)
        {
            createdDate.DefaultCellStyle.Format = "dd/MM/yyyy";
        }
    }

    private Customer? GetSelectedCustomer()
    {
        if (dgvCustomers.CurrentRow?.DataBoundItem is Customer customer)
        {
            return customer;
        }

        return null;
    }

    private void LoadPurchaseHistory()
    {
        var selected = GetSelectedCustomer();
        if (selected is null)
        {
            dgvPurchaseHistory.DataSource = null;
            return;
        }

        dgvPurchaseHistory.DataSource = null;
        dgvPurchaseHistory.DataSource = _customerService.GetPurchaseHistory(selected.CustomerID).ToList();

        if (dgvPurchaseHistory.Columns["OrderDate"] is DataGridViewColumn orderDate)
        {
            orderDate.DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        foreach (var colName in new[] { "UnitPrice", "Subtotal", "OrderTotal" })
        {
            if (dgvPurchaseHistory.Columns[colName] is DataGridViewColumn col)
            {
                col.DefaultCellStyle.Format = "N2";
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }
    }

    private void dgvCustomers_SelectionChanged(object sender, EventArgs e) => LoadPurchaseHistory();

    private void btnAdd_Click(object sender, EventArgs e)
    {
        using var form = new CustomerEditForm();
        if (form.ShowDialog(FindForm()) != DialogResult.OK)
        {
            return;
        }

        var result = _customerService.AddCustomer(form.Customer);
        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage, "Add Customer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        LoadCustomers();
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        var selected = GetSelectedCustomer();
        if (selected is null)
        {
            MessageBox.Show("Please select a customer.", "Edit Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var customer = _customerService.GetCustomer(selected.CustomerID);
        if (customer is null)
        {
            MessageBox.Show("Customer not found.", "Edit Customer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            LoadCustomers();
            return;
        }

        using var form = new CustomerEditForm(customer);
        if (form.ShowDialog(FindForm()) != DialogResult.OK)
        {
            return;
        }

        var result = _customerService.UpdateCustomer(form.Customer);
        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage, "Edit Customer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        LoadCustomers();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        var selected = GetSelectedCustomer();
        if (selected is null)
        {
            MessageBox.Show("Please select a customer.", "Delete Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (MessageBox.Show($"Delete \"{selected.FullName}\"?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }

        var result = _customerService.DeleteCustomer(selected.CustomerID);
        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage, "Delete Customer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        LoadCustomers();
    }

    private void cboFilter_SelectedIndexChanged(object sender, EventArgs e) => LoadCustomers();

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        cboFilter.SelectedIndex = 0;
        LoadCustomers();
    }

    private void btnSearch_Click(object sender, EventArgs e) => LoadCustomers();

    private void txtSearch_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            LoadCustomers();
        }
    }
}
