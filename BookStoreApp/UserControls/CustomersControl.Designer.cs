namespace BookStoreApp.UserControls;

partial class CustomersControl
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
        btnSearch = new Button();
        txtSearch = new TextBox();
        lblStats = new Label();
        cboFilter = new ComboBox();
        lblFilter = new Label();
        btnRefresh = new Button();
        btnDelete = new Button();
        btnEdit = new Button();
        btnAdd = new Button();
        splitMain = new SplitContainer();
        dgvCustomers = new DataGridView();
        dgvPurchaseHistory = new DataGridView();
        lblPurchaseHistory = new Label();
        panelToolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
        splitMain.Panel1.SuspendLayout();
        splitMain.Panel2.SuspendLayout();
        splitMain.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvPurchaseHistory).BeginInit();
        SuspendLayout();
        // 
        // panelToolbar
        // 
        panelToolbar.Controls.Add(btnSearch);
        panelToolbar.Controls.Add(txtSearch);
        panelToolbar.Controls.Add(lblStats);
        panelToolbar.Controls.Add(cboFilter);
        panelToolbar.Controls.Add(lblFilter);
        panelToolbar.Controls.Add(btnRefresh);
        panelToolbar.Controls.Add(btnDelete);
        panelToolbar.Controls.Add(btnEdit);
        panelToolbar.Controls.Add(btnAdd);
        panelToolbar.Dock = DockStyle.Top;
        panelToolbar.Location = new Point(8, 8);
        panelToolbar.Name = "panelToolbar";
        panelToolbar.Padding = new Padding(0, 0, 0, 8);
        panelToolbar.Size = new Size(969, 48);
        panelToolbar.TabIndex = 1;
        // 
        // btnSearch
        // 
        btnSearch.Location = new Point(885, 6);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(72, 36);
        btnSearch.TabIndex = 0;
        btnSearch.Text = "Search";
        btnSearch.Click += btnSearch_Click;
        // 
        // txtSearch
        // 
        txtSearch.Location = new Point(726, 9);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Search...";
        txtSearch.Size = new Size(140, 27);
        txtSearch.TabIndex = 1;
        txtSearch.KeyDown += txtSearch_KeyDown;
        // 
        // lblStats
        // 
        lblStats.AutoSize = true;
        lblStats.Location = new Point(532, 12);
        lblStats.Name = "lblStats";
        lblStats.Size = new Size(188, 20);
        lblStats.TabIndex = 2;
        lblStats.Text = "Total: 0 | New this month: 0";
        // 
        // cboFilter
        // 
        cboFilter.DropDownStyle = ComboBoxStyle.DropDownList;
        cboFilter.Location = new Point(372, 8);
        cboFilter.Name = "cboFilter";
        cboFilter.Size = new Size(150, 28);
        cboFilter.TabIndex = 3;
        cboFilter.SelectedIndexChanged += cboFilter_SelectedIndexChanged;
        // 
        // lblFilter
        // 
        lblFilter.AutoSize = true;
        lblFilter.Location = new Point(324, 12);
        lblFilter.Name = "lblFilter";
        lblFilter.Size = new Size(42, 20);
        lblFilter.TabIndex = 4;
        lblFilter.Text = "Filter";
        // 
        // btnRefresh
        // 
        btnRefresh.Location = new Point(228, 4);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(88, 36);
        btnRefresh.TabIndex = 5;
        btnRefresh.Text = "Refresh";
        btnRefresh.Click += btnRefresh_Click;
        // 
        // btnDelete
        // 
        btnDelete.Location = new Point(152, 4);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(72, 36);
        btnDelete.TabIndex = 6;
        btnDelete.Text = "Delete";
        btnDelete.Click += btnDelete_Click;
        // 
        // btnEdit
        // 
        btnEdit.Location = new Point(76, 4);
        btnEdit.Name = "btnEdit";
        btnEdit.Size = new Size(72, 36);
        btnEdit.TabIndex = 7;
        btnEdit.Text = "Edit";
        btnEdit.Click += btnEdit_Click;
        // 
        // btnAdd
        // 
        btnAdd.Location = new Point(0, 4);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(72, 36);
        btnAdd.TabIndex = 8;
        btnAdd.Text = "Add";
        btnAdd.Click += btnAdd_Click;
        // 
        // splitMain
        // 
        splitMain.Dock = DockStyle.Fill;
        splitMain.Location = new Point(8, 56);
        splitMain.Name = "splitMain";
        splitMain.Orientation = Orientation.Horizontal;
        // 
        // splitMain.Panel1
        // 
        splitMain.Panel1.Controls.Add(dgvCustomers);
        // 
        // splitMain.Panel2
        // 
        splitMain.Panel2.Controls.Add(dgvPurchaseHistory);
        splitMain.Panel2.Controls.Add(lblPurchaseHistory);
        splitMain.Size = new Size(969, 436);
        splitMain.SplitterDistance = 309;
        splitMain.TabIndex = 0;
        // 
        // dgvCustomers
        // 
        dgvCustomers.AllowUserToAddRows = false;
        dgvCustomers.AllowUserToDeleteRows = false;
        dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvCustomers.Dock = DockStyle.Fill;
        dgvCustomers.Location = new Point(0, 0);
        dgvCustomers.Name = "dgvCustomers";
        dgvCustomers.ReadOnly = true;
        dgvCustomers.RowHeadersVisible = false;
        dgvCustomers.RowHeadersWidth = 51;
        dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCustomers.Size = new Size(969, 309);
        dgvCustomers.TabIndex = 0;
        dgvCustomers.SelectionChanged += dgvCustomers_SelectionChanged;
        // 
        // dgvPurchaseHistory
        // 
        dgvPurchaseHistory.AllowUserToAddRows = false;
        dgvPurchaseHistory.AllowUserToDeleteRows = false;
        dgvPurchaseHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvPurchaseHistory.ColumnHeadersHeight = 29;
        dgvPurchaseHistory.Dock = DockStyle.Fill;
        dgvPurchaseHistory.Location = new Point(0, 35);
        dgvPurchaseHistory.Name = "dgvPurchaseHistory";
        dgvPurchaseHistory.ReadOnly = true;
        dgvPurchaseHistory.RowHeadersVisible = false;
        dgvPurchaseHistory.RowHeadersWidth = 51;
        dgvPurchaseHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvPurchaseHistory.Size = new Size(969, 88);
        dgvPurchaseHistory.TabIndex = 0;
        // 
        // lblPurchaseHistory
        // 
        lblPurchaseHistory.Dock = DockStyle.Top;
        lblPurchaseHistory.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblPurchaseHistory.Location = new Point(0, 0);
        lblPurchaseHistory.Name = "lblPurchaseHistory";
        lblPurchaseHistory.Padding = new Padding(4);
        lblPurchaseHistory.Size = new Size(969, 35);
        lblPurchaseHistory.TabIndex = 1;
        lblPurchaseHistory.Text = "Purchase History";
        // 
        // CustomersControl
        // 
        Controls.Add(splitMain);
        Controls.Add(panelToolbar);
        Name = "CustomersControl";
        Padding = new Padding(8);
        Size = new Size(985, 500);
        panelToolbar.ResumeLayout(false);
        panelToolbar.PerformLayout();
        splitMain.Panel1.ResumeLayout(false);
        splitMain.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
        splitMain.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvPurchaseHistory).EndInit();
        ResumeLayout(false);
    }

    private Panel panelToolbar;
    private Button btnAdd;
    private Button btnEdit;
    private Button btnDelete;
    private Button btnRefresh;
    private Label lblFilter;
    private ComboBox cboFilter;
    private Label lblStats;
    private TextBox txtSearch;
    private Button btnSearch;
    private SplitContainer splitMain;
    private DataGridView dgvCustomers;
    private Label lblPurchaseHistory;
    private DataGridView dgvPurchaseHistory;
}
