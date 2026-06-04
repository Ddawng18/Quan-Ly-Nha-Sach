namespace BookStoreApp.UserControls;

partial class DashboardControl
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
        btnRefresh = new Button();
        splitMain = new SplitContainer();
        dgvDashboard = new DataGridView();
        splitBottom = new SplitContainer();
        dgvRecentOrders = new DataGridView();
        lblRecentOrders = new Label();
        dgvBestSelling = new DataGridView();
        lblBestSelling = new Label();
        panelToolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
        splitMain.Panel1.SuspendLayout();
        splitMain.Panel2.SuspendLayout();
        splitMain.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvDashboard).BeginInit();
        ((System.ComponentModel.ISupportInitialize)splitBottom).BeginInit();
        splitBottom.Panel1.SuspendLayout();
        splitBottom.Panel2.SuspendLayout();
        splitBottom.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvRecentOrders).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvBestSelling).BeginInit();
        SuspendLayout();
        // 
        // panelToolbar
        // 
        panelToolbar.Controls.Add(btnSearch);
        panelToolbar.Controls.Add(txtSearch);
        panelToolbar.Controls.Add(btnRefresh);
        panelToolbar.Dock = DockStyle.Top;
        panelToolbar.Location = new Point(8, 8);
        panelToolbar.Name = "panelToolbar";
        panelToolbar.Padding = new Padding(0, 0, 0, 8);
        panelToolbar.Size = new Size(784, 48);
        panelToolbar.TabIndex = 1;
        // 
        // btnSearch
        // 
        btnSearch.Location = new Point(400, 4);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(88, 36);
        btnSearch.TabIndex = 0;
        btnSearch.Text = "Search";
        btnSearch.Click += btnSearch_Click;
        // 
        // txtSearch
        // 
        txtSearch.Location = new Point(112, 8);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Search metric...";
        txtSearch.Size = new Size(280, 27);
        txtSearch.TabIndex = 1;
        txtSearch.KeyDown += txtSearch_KeyDown;
        // 
        // btnRefresh
        // 
        btnRefresh.Location = new Point(0, 4);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(100, 36);
        btnRefresh.TabIndex = 2;
        btnRefresh.Text = "Refresh";
        btnRefresh.Click += btnRefresh_Click;
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
        splitMain.Panel1.Controls.Add(dgvDashboard);
        // 
        // splitMain.Panel2
        // 
        splitMain.Panel2.Controls.Add(splitBottom);
        splitMain.Size = new Size(784, 436);
        splitMain.SplitterDistance = 309;
        splitMain.TabIndex = 0;
        // 
        // dgvDashboard
        // 
        dgvDashboard.AllowUserToAddRows = false;
        dgvDashboard.AllowUserToDeleteRows = false;
        dgvDashboard.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvDashboard.Dock = DockStyle.Fill;
        dgvDashboard.Location = new Point(0, 0);
        dgvDashboard.Name = "dgvDashboard";
        dgvDashboard.ReadOnly = true;
        dgvDashboard.RowHeadersVisible = false;
        dgvDashboard.RowHeadersWidth = 51;
        dgvDashboard.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvDashboard.Size = new Size(784, 309);
        dgvDashboard.TabIndex = 0;
        // 
        // splitBottom
        // 
        splitBottom.Dock = DockStyle.Fill;
        splitBottom.Location = new Point(0, 0);
        splitBottom.Name = "splitBottom";
        // 
        // splitBottom.Panel1
        // 
        splitBottom.Panel1.Controls.Add(dgvRecentOrders);
        splitBottom.Panel1.Controls.Add(lblRecentOrders);
        // 
        // splitBottom.Panel2
        // 
        splitBottom.Panel2.Controls.Add(dgvBestSelling);
        splitBottom.Panel2.Controls.Add(lblBestSelling);
        splitBottom.Size = new Size(784, 123);
        splitBottom.SplitterDistance = 261;
        splitBottom.TabIndex = 0;
        // 
        // dgvRecentOrders
        // 
        dgvRecentOrders.AllowUserToAddRows = false;
        dgvRecentOrders.AllowUserToDeleteRows = false;
        dgvRecentOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvRecentOrders.ColumnHeadersHeight = 29;
        dgvRecentOrders.Dock = DockStyle.Fill;
        dgvRecentOrders.Location = new Point(0, 41);
        dgvRecentOrders.Name = "dgvRecentOrders";
        dgvRecentOrders.ReadOnly = true;
        dgvRecentOrders.RowHeadersVisible = false;
        dgvRecentOrders.RowHeadersWidth = 51;
        dgvRecentOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvRecentOrders.Size = new Size(261, 82);
        dgvRecentOrders.TabIndex = 0;
        // 
        // lblRecentOrders
        // 
        lblRecentOrders.Dock = DockStyle.Top;
        lblRecentOrders.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblRecentOrders.Location = new Point(0, 0);
        lblRecentOrders.Name = "lblRecentOrders";
        lblRecentOrders.Padding = new Padding(4, 4, 0, 4);
        lblRecentOrders.Size = new Size(261, 41);
        lblRecentOrders.TabIndex = 1;
        lblRecentOrders.Text = "Recent Orders";
        // 
        // dgvBestSelling
        // 
        dgvBestSelling.AllowUserToAddRows = false;
        dgvBestSelling.AllowUserToDeleteRows = false;
        dgvBestSelling.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvBestSelling.ColumnHeadersHeight = 29;
        dgvBestSelling.Dock = DockStyle.Fill;
        dgvBestSelling.Location = new Point(0, 41);
        dgvBestSelling.Name = "dgvBestSelling";
        dgvBestSelling.ReadOnly = true;
        dgvBestSelling.RowHeadersVisible = false;
        dgvBestSelling.RowHeadersWidth = 51;
        dgvBestSelling.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvBestSelling.Size = new Size(519, 82);
        dgvBestSelling.TabIndex = 0;
        // 
        // lblBestSelling
        // 
        lblBestSelling.Dock = DockStyle.Top;
        lblBestSelling.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblBestSelling.Location = new Point(0, 0);
        lblBestSelling.Name = "lblBestSelling";
        lblBestSelling.Padding = new Padding(4, 4, 0, 4);
        lblBestSelling.Size = new Size(519, 41);
        lblBestSelling.TabIndex = 1;
        lblBestSelling.Text = "Best Selling Books";
        // 
        // DashboardControl
        // 
        Controls.Add(splitMain);
        Controls.Add(panelToolbar);
        Name = "DashboardControl";
        Padding = new Padding(8);
        Size = new Size(800, 500);
        panelToolbar.ResumeLayout(false);
        panelToolbar.PerformLayout();
        splitMain.Panel1.ResumeLayout(false);
        splitMain.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
        splitMain.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvDashboard).EndInit();
        splitBottom.Panel1.ResumeLayout(false);
        splitBottom.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitBottom).EndInit();
        splitBottom.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvRecentOrders).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvBestSelling).EndInit();
        ResumeLayout(false);
    }

    private Panel panelToolbar;
    private Button btnRefresh;
    private TextBox txtSearch;
    private Button btnSearch;
    private SplitContainer splitMain;
    private DataGridView dgvDashboard;
    private SplitContainer splitBottom;
    private Label lblRecentOrders;
    private DataGridView dgvRecentOrders;
    private Label lblBestSelling;
    private DataGridView dgvBestSelling;
}
