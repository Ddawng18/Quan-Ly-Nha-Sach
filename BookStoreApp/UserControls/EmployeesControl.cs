using BookStoreApp.BLL;
using BookStoreApp.DTO;
using BookStoreApp.Forms;
using BookStoreApp.Theme;

namespace BookStoreApp.UserControls;

public partial class EmployeesControl : UserControl
{
    private readonly IEmployeeService _employeeService = ServiceLocator.EmployeeService;

    public EmployeesControl()
    {
        InitializeComponent();
        ApplyTheme();
        LoadEmployees();
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
        AppTheme.ApplyGridStyle(dgvEmployees);
    }

    private void LoadEmployees()
    {
        dgvEmployees.DataSource = null;
        dgvEmployees.DataSource = _employeeService.SearchEmployees(txtSearch.Text).ToList();
        ConfigureGridColumns();
    }

    private void ConfigureGridColumns()
    {
        if (dgvEmployees.Columns.Count == 0)
        {
            return;
        }

        SetColumn("EmployeeID", "EmployeeID", 90, 0);
        SetColumn("FullName", "FullName", 180, 1);
        SetColumn("Phone", "Phone", 120, 2);
        SetColumn("Position", "Position", 140, 3);
        SetColumn("Role", "Role", 90, 4);
        SetColumn("Salary", "Salary", 110, 5);
        SetColumn("CreatedDate", "CreatedDate", 130, 6);

        if (dgvEmployees.Columns["Salary"] is DataGridViewColumn salary)
        {
            salary.DefaultCellStyle.Format = "N0";
            salary.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        if (dgvEmployees.Columns["CreatedDate"] is DataGridViewColumn createdDate)
        {
            createdDate.DefaultCellStyle.Format = "dd/MM/yyyy";
        }
    }

    private void SetColumn(string property, string header, int width, int displayIndex)
    {
        if (dgvEmployees.Columns[property] is not DataGridViewColumn column)
        {
            return;
        }

        column.Visible = true;
        column.HeaderText = header;
        column.Width = width;
        column.DisplayIndex = displayIndex;
    }

    private Employee? GetSelectedEmployee()
    {
        if (dgvEmployees.CurrentRow?.DataBoundItem is Employee employee)
        {
            return employee;
        }

        return null;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        using var form = new EmployeeEditForm();
        if (form.ShowDialog(FindForm()) != DialogResult.OK)
        {
            return;
        }

        var result = _employeeService.AddEmployee(form.Employee);
        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage, "Add Employee", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        LoadEmployees();
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        var selected = GetSelectedEmployee();
        if (selected is null)
        {
            MessageBox.Show("Please select an employee.", "Edit Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var employee = _employeeService.GetEmployee(selected.EmployeeID);
        if (employee is null)
        {
            MessageBox.Show("Employee not found.", "Edit Employee", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            LoadEmployees();
            return;
        }

        using var form = new EmployeeEditForm(employee);
        if (form.ShowDialog(FindForm()) != DialogResult.OK)
        {
            return;
        }

        var result = _employeeService.UpdateEmployee(form.Employee);
        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage, "Edit Employee", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        LoadEmployees();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        var selected = GetSelectedEmployee();
        if (selected is null)
        {
            MessageBox.Show("Please select an employee.", "Delete Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (MessageBox.Show($"Delete \"{selected.FullName}\"?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }

        var result = _employeeService.DeleteEmployee(selected.EmployeeID);
        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage, "Delete Employee", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        LoadEmployees();
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        LoadEmployees();
    }

    private void btnSearch_Click(object sender, EventArgs e) => LoadEmployees();

    private void txtSearch_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            LoadEmployees();
        }
    }
}
