namespace BookStoreApp.UserControls;

partial class SupplierControl
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
        panelToolbar = new Panel();
        btnAdd = new Button();
        btnEdit = new Button();
        btnDelete = new Button();
        btnRefresh = new Button();
        txtSearch = new TextBox();
        btnSearch = new Button();
        dgvSuppliers = new DataGridView();
        panelToolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvSuppliers).BeginInit();
        SuspendLayout();
        panelToolbar.Controls.Add(btnSearch);
        panelToolbar.Controls.Add(txtSearch);
        panelToolbar.Controls.Add(btnRefresh);
        panelToolbar.Controls.Add(btnDelete);
        panelToolbar.Controls.Add(btnEdit);
        panelToolbar.Controls.Add(btnAdd);
        panelToolbar.Dock = DockStyle.Top;
        panelToolbar.Name = "panelToolbar";
        panelToolbar.Padding = new Padding(0, 0, 0, 8);
        panelToolbar.Size = new Size(784, 48);
        btnAdd.Location = new Point(0, 4);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(72, 36);
        btnAdd.Text = "Add";
        btnAdd.Click += btnAdd_Click;
        btnEdit.Location = new Point(76, 4);
        btnEdit.Name = "btnEdit";
        btnEdit.Size = new Size(72, 36);
        btnEdit.Text = "Edit";
        btnEdit.Click += btnEdit_Click;
        btnDelete.Location = new Point(152, 4);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(72, 36);
        btnDelete.Text = "Delete";
        btnDelete.Click += btnDelete_Click;
        btnRefresh.Location = new Point(228, 4);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(80, 36);
        btnRefresh.Text = "Refresh";
        btnRefresh.Click += btnRefresh_Click;
        txtSearch.Location = new Point(320, 8);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Search supplier...";
        txtSearch.Size = new Size(280, 27);
        txtSearch.KeyDown += txtSearch_KeyDown;
        btnSearch.Location = new Point(608, 4);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(88, 36);
        btnSearch.Text = "Search";
        btnSearch.Click += btnSearch_Click;
        dgvSuppliers.AllowUserToAddRows = false;
        dgvSuppliers.AllowUserToDeleteRows = false;
        dgvSuppliers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        dgvSuppliers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvSuppliers.Dock = DockStyle.Fill;
        dgvSuppliers.Name = "dgvSuppliers";
        dgvSuppliers.ReadOnly = true;
        dgvSuppliers.RowHeadersVisible = false;
        dgvSuppliers.ScrollBars = ScrollBars.Both;
        dgvSuppliers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        Controls.Add(dgvSuppliers);
        Controls.Add(panelToolbar);
        Name = "SupplierControl";
        Padding = new Padding(8);
        Size = new Size(800, 500);
        panelToolbar.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvSuppliers).EndInit();
        ResumeLayout(false);
    }

    private Panel panelToolbar;
    private Button btnAdd;
    private Button btnEdit;
    private Button btnDelete;
    private Button btnRefresh;
    private TextBox txtSearch;
    private Button btnSearch;
    private DataGridView dgvSuppliers;
}
