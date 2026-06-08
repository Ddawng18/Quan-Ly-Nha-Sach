using BookStoreApp.UserControls;
using BookStoreApp.Theme;

namespace BookStoreApp.Forms;

public partial class MainForm : Form
{
    private readonly string _role;

    public MainForm()
        : this("Admin")
    {
    }

    public MainForm(string role)
    {
        _role = role;
        InitializeComponent();
        AppBranding.ApplyFormIcon(this);
        Text = "bookstoreMana";

        if (IsStaff)
        {
            btnCategories.Visible = false;
            btnSuppliers.Visible = false;
            btnCustomers.Visible = false;
            btnReports.Visible = false;
            btnEmployees.Visible = false;
        }

        lblUser.Text = $"Signed in as {role}";
        LoadControl(new DashboardControl(), "Dashboard");
    }

    private bool IsStaff => string.Equals(_role, "Staff", StringComparison.OrdinalIgnoreCase);

    private void LoadControl(UserControl control)
    {
        panelContent.Controls.Clear();
        control.Dock = DockStyle.Fill;
        panelContent.Controls.Add(control);
    }

    private void LoadControl(UserControl control, string pageTitle)
    {
        LoadControl(control);
        lblPageTitle.Text = pageTitle;
        HighlightSidebarButton(pageTitle);
    }

    private void HighlightSidebarButton(string pageTitle)
    {
        foreach (var button in new[] { btnDashboard, btnBooks, btnCategories, btnSuppliers, btnImport, btnCustomers, btnOrders, btnReports, btnEmployees })
        {
            if (!button.Visible)
            {
                continue;
            }

            var isActive = string.Equals(button.Text, pageTitle, StringComparison.OrdinalIgnoreCase);
            AppTheme.StyleSidebarButton(button, isActive);
        }
    }

    private void btnDashboard_Click(object sender, EventArgs e) =>
        LoadControl(new DashboardControl(), "Dashboard");

    private void btnBooks_Click(object sender, EventArgs e) =>
        LoadControl(new BookControl(canAdd: true, canEdit: true, canDelete: true), "Books");

    private void btnCategories_Click(object sender, EventArgs e) =>
        LoadControl(new CategoryControl(), "Categories");

    private void btnSuppliers_Click(object sender, EventArgs e) =>
        LoadControl(new SupplierControl(), "Suppliers");

    private void btnImport_Click(object sender, EventArgs e) =>
        LoadControl(new ImportControl(), "Nhập hàng");

    private void btnCustomers_Click(object sender, EventArgs e) =>
        LoadControl(new CustomersControl(), "Customers");

    private void btnOrders_Click(object sender, EventArgs e) =>
        LoadControl(new OrdersControl(), "Orders");

    private void btnReports_Click(object sender, EventArgs e) =>
        LoadControl(new ReportsControl(), "Reports");

    private void btnEmployees_Click(object sender, EventArgs e) =>
        LoadControl(new EmployeesControl(), "Employees");

    private void btnLogout_Click(object sender, EventArgs e)
    {
        var loginForm = new LoginForm();
        loginForm.FormClosed += (_, _) => Close();
        Hide();
        loginForm.Show();
    }
}
