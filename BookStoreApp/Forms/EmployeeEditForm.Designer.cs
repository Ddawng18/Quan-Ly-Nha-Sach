namespace BookStoreApp.Forms;

partial class EmployeeEditForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblFullName = new Label();
        txtFullName = new TextBox();
        lblPhone = new Label();
        txtPhone = new TextBox();
        lblPosition = new Label();
        txtPosition = new TextBox();
        lblSalary = new Label();
        numSalary = new NumericUpDown();
        lblRole = new Label();
        cboRole = new ComboBox();
        btnSave = new Button();
        btnCancel = new Button();
        ((System.ComponentModel.ISupportInitialize)numSalary).BeginInit();
        SuspendLayout();
        lblFullName.AutoSize = true;
        lblFullName.Location = new Point(24, 24);
        lblFullName.Text = "Full Name";
        txtFullName.Location = new Point(24, 48);
        txtFullName.Size = new Size(400, 27);
        lblPhone.AutoSize = true;
        lblPhone.Location = new Point(24, 88);
        lblPhone.Text = "Phone";
        txtPhone.Location = new Point(24, 112);
        txtPhone.Size = new Size(400, 27);
        lblPosition.AutoSize = true;
        lblPosition.Location = new Point(24, 152);
        lblPosition.Text = "Position";
        txtPosition.Location = new Point(24, 176);
        txtPosition.Size = new Size(400, 27);
        lblSalary.AutoSize = true;
        lblSalary.Location = new Point(24, 216);
        lblSalary.Text = "Salary";
        numSalary.Location = new Point(24, 240);
        numSalary.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
        numSalary.Size = new Size(160, 27);
        numSalary.ThousandsSeparator = true;
        lblRole.AutoSize = true;
        lblRole.Location = new Point(24, 280);
        lblRole.Text = "Role";
        cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
        cboRole.Items.AddRange(["Admin", "Staff"]);
        cboRole.Location = new Point(24, 304);
        cboRole.Size = new Size(160, 28);
        btnSave.BackColor = Color.FromArgb(33, 150, 243);
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(24, 352);
        btnSave.Size = new Size(120, 40);
        btnSave.Text = "Save";
        btnSave.Click += btnSave_Click;
        btnCancel.Location = new Point(160, 352);
        btnCancel.Size = new Size(120, 40);
        btnCancel.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;
        AcceptButton = btnSave;
        CancelButton = btnCancel;
        ClientSize = new Size(454, 412);
        Controls.Add(btnCancel);
        Controls.Add(btnSave);
        Controls.Add(cboRole);
        Controls.Add(lblRole);
        Controls.Add(numSalary);
        Controls.Add(lblSalary);
        Controls.Add(txtPosition);
        Controls.Add(lblPosition);
        Controls.Add(txtPhone);
        Controls.Add(lblPhone);
        Controls.Add(txtFullName);
        Controls.Add(lblFullName);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Employee";
        ((System.ComponentModel.ISupportInitialize)numSalary).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblFullName;
    private TextBox txtFullName;
    private Label lblPhone;
    private TextBox txtPhone;
    private Label lblPosition;
    private TextBox txtPosition;
    private Label lblSalary;
    private NumericUpDown numSalary;
    private Label lblRole;
    private ComboBox cboRole;
    private Button btnSave;
    private Button btnCancel;
}
