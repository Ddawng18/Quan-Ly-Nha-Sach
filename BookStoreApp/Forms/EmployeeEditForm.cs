using BookStoreApp.DTO;

namespace BookStoreApp.Forms;

public partial class EmployeeEditForm : Form
{
    private readonly Employee? _existing;

    public Employee Employee { get; private set; } = new();

    public EmployeeEditForm()
    {
        InitializeComponent();
        cboRole.SelectedItem = "Staff";
        Text = "Add Employee";
    }

    public EmployeeEditForm(Employee employee)
        : this()
    {
        _existing = employee;
        Text = "Edit Employee";
        txtFullName.Text = employee.FullName;
        txtPhone.Text = employee.Phone;
        txtPosition.Text = employee.Position;
        cboRole.SelectedItem = string.IsNullOrWhiteSpace(employee.Role) ? "Staff" : employee.Role;
        numSalary.Value = employee.Salary < numSalary.Minimum ? numSalary.Minimum
            : employee.Salary > numSalary.Maximum ? numSalary.Maximum : employee.Salary;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        Employee = new Employee
        {
            EmployeeID = _existing?.EmployeeID ?? 0,
            FullName = txtFullName.Text.Trim(),
            Phone = txtPhone.Text.Trim(),
            Position = txtPosition.Text.Trim(),
            Role = cboRole.SelectedItem?.ToString() ?? "Staff",
            Salary = numSalary.Value,
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
