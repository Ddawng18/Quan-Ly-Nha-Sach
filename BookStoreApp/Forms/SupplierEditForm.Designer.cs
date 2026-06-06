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
        // 
        // lblSupplierName
        // 
        lblSupplierName.AutoSize = true;
        lblSupplierName.Location = new Point(24, 24);
        lblSupplierName.Name = "lblSupplierName";
        lblSupplierName.Size = new Size(108, 20);
        lblSupplierName.TabIndex = 9;
        lblSupplierName.Text = "Supplier Name";
        // 
        // txtSupplierName
        // 
        txtSupplierName.Location = new Point(24, 48);
        txtSupplierName.Name = "txtSupplierName";
        txtSupplierName.Size = new Size(400, 27);
        txtSupplierName.TabIndex = 8;
        // 
        // lblAddress
        // 
        lblAddress.AutoSize = true;
        lblAddress.Location = new Point(24, 79);
        lblAddress.Name = "lblAddress";
        lblAddress.Size = new Size(62, 20);
        lblAddress.TabIndex = 7;
        lblAddress.Text = "Address";
        // 
        // txtAddress
        // 
        txtAddress.Location = new Point(24, 103);
        txtAddress.Multiline = true;
        txtAddress.Name = "txtAddress";
        txtAddress.Size = new Size(400, 27);
        txtAddress.TabIndex = 6;
        // 
        // lblEmail
        // 
        lblEmail.AutoSize = true;
        lblEmail.Location = new Point(24, 134);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new Size(46, 20);
        lblEmail.TabIndex = 5;
        lblEmail.Text = "Email";
        // 
        // txtEmail
        // 
        txtEmail.Location = new Point(24, 158);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(400, 27);
        txtEmail.TabIndex = 4;
        // 
        // lblPhone
        // 
        lblPhone.AutoSize = true;
        lblPhone.Location = new Point(24, 189);
        lblPhone.Name = "lblPhone";
        lblPhone.Size = new Size(50, 20);
        lblPhone.TabIndex = 3;
        lblPhone.Text = "Phone";
        // 
        // txtPhone
        // 
        txtPhone.Location = new Point(24, 213);
        txtPhone.Name = "txtPhone";
        txtPhone.Size = new Size(400, 27);
        txtPhone.TabIndex = 2;
        // 
        // btnSave
        // 
        btnSave.BackColor = Color.FromArgb(33, 150, 243);
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(24, 256);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(120, 40);
        btnSave.TabIndex = 1;
        btnSave.Text = "Save";
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += btnSave_Click;
        // 
        // btnCancel
        // 
        btnCancel.Location = new Point(150, 256);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(120, 40);
        btnCancel.TabIndex = 0;
        btnCancel.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;
        // 
        // SupplierEditForm
        // 
        AcceptButton = btnSave;
        CancelButton = btnCancel;
        ClientSize = new Size(454, 316);
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
        Name = "SupplierEditForm";
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
