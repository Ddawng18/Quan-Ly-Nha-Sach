namespace BookStoreApp.Forms;

partial class CustomerEditForm
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
        lblAddress = new Label();
        txtAddress = new TextBox();
        btnSave = new Button();
        btnCancel = new Button();
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
        lblAddress.AutoSize = true;
        lblAddress.Location = new Point(24, 152);
        lblAddress.Text = "Address";
        txtAddress.Location = new Point(24, 176);
        txtAddress.Size = new Size(400, 80);
        txtAddress.Multiline = true;
        btnSave.BackColor = Color.FromArgb(33, 150, 243);
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(24, 280);
        btnSave.Size = new Size(120, 40);
        btnSave.Text = "Save";
        btnSave.Click += btnSave_Click;
        btnCancel.Location = new Point(160, 280);
        btnCancel.Size = new Size(120, 40);
        btnCancel.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;
        AcceptButton = btnSave;
        CancelButton = btnCancel;
        ClientSize = new Size(454, 340);
        Controls.Add(btnCancel);
        Controls.Add(btnSave);
        Controls.Add(txtAddress);
        Controls.Add(lblAddress);
        Controls.Add(txtPhone);
        Controls.Add(lblPhone);
        Controls.Add(txtFullName);
        Controls.Add(lblFullName);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Customer";
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblFullName;
    private TextBox txtFullName;
    private Label lblPhone;
    private TextBox txtPhone;
    private Label lblAddress;
    private TextBox txtAddress;
    private Button btnSave;
    private Button btnCancel;
}
