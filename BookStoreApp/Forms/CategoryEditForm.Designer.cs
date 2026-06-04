namespace BookStoreApp.Forms;

partial class CategoryEditForm
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
        lblCategoryName = new Label();
        txtCategoryName = new TextBox();
        btnSave = new Button();
        btnCancel = new Button();
        SuspendLayout();
        lblCategoryName.AutoSize = true;
        lblCategoryName.Location = new Point(24, 24);
        lblCategoryName.Text = "Category Name";
        txtCategoryName.Location = new Point(24, 48);
        txtCategoryName.Size = new Size(400, 27);
        btnSave.BackColor = Color.FromArgb(33, 150, 243);
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(24, 96);
        btnSave.Size = new Size(120, 40);
        btnSave.Text = "Save";
        btnSave.Click += btnSave_Click;
        btnCancel.Location = new Point(160, 96);
        btnCancel.Size = new Size(120, 40);
        btnCancel.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;
        AcceptButton = btnSave;
        CancelButton = btnCancel;
        ClientSize = new Size(454, 156);
        Controls.Add(btnCancel);
        Controls.Add(btnSave);
        Controls.Add(txtCategoryName);
        Controls.Add(lblCategoryName);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Category";
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblCategoryName;
    private TextBox txtCategoryName;
    private Button btnSave;
    private Button btnCancel;
}
