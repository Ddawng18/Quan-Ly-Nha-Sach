namespace BookStoreApp.Forms;

partial class SupplierEditForm
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
        lblSupplierName = new Label();
        txtSupplierName = new TextBox();
        lblAddress = new Label();
        txtAddress = new TextBox();
        lblEmail = new Label();
        txtEmail = new TextBox();
        lblPhone = new Label();
        txtPhone = new TextBox();
        btnSave = new Button();
        btnCancel = new Button();
        SuspendLayout();
        lblSupplierName.AutoSize = true;
        lblSupplierName.Location = new Point(24, 24);
        lblSupplierName.Text = "Supplier Name";
        txtSupplierName.Location = new Point(24, 48);
        txtSupplierName.Size = new Size(400, 27);
        lblAddress.AutoSize = true;
        lblAddress.Location = new Point(24, 88);
        lblAddress.Text = "Address";
        txtAddress.Location = new Point(24, 112);
        txtAddress.Size = new Size(400, 60);
        txtAddress.Multiline = true;
        lblEmail.AutoSize = true;
        lblEmail.Location = new Point(24, 180);
        lblEmail.Text = "Email";
        txtEmail.Location = new Point(24, 204);
        txtEmail.Size = new Size(400, 27);
        lblPhone.AutoSize = true;
        lblPhone.Location = new Point(24, 244);
        lblPhone.Text = "Phone";
        txtPhone.Location = new Point(24, 268);
        txtPhone.Size = new Size(400, 27);
        btnSave.BackColor = Color.FromArgb(33, 150, 243);
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(24, 316);
        btnSave.Size = new Size(120, 40);
        btnSave.Text = "Save";
        btnSave.Click += btnSave_Click;
        btnCancel.Location = new Point(160, 316);
        btnCancel.Size = new Size(120, 40);
        btnCancel.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;
        AcceptButton = btnSave;
        CancelButton = btnCancel;
        ClientSize = new Size(454, 376);
        Controls.Add(btnCancel);
        Controls.Add(btnSave);
        Controls.Add(txtPhone);
        Controls.Add(lblPhone);
        Controls.Add(txtEmail);
        Controls.Add(lblEmail);
        Controls.Add(txtAddress);
        Controls.Add(lblAddress);
        Controls.Add(txtSupplierName);
        Controls.Add(lblSupplierName);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Supplier";
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblSupplierName;
    private TextBox txtSupplierName;
    private Label lblAddress;
    private TextBox txtAddress;
    private Label lblEmail;
    private TextBox txtEmail;
    private Label lblPhone;
    private TextBox txtPhone;
    private Button btnSave;
    private Button btnCancel;
}
