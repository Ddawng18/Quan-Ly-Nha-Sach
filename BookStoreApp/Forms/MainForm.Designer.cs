using BookStoreApp.Theme;

namespace BookStoreApp.Forms;

partial class MainForm
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

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        panelSidebar = new Panel();
        lblAppTitle = new Label();
        btnDashboard = new Button();
        btnBooks = new Button();
        btnCategories = new Button();
        btnSuppliers = new Button();
        btnCustomers = new Button();
        btnOrders = new Button();
        btnReports = new Button();
        btnEmployees = new Button();
        btnLogout = new Button();
        panelTopBar = new Panel();
        lblPageTitle = new Label();
        lblUser = new Label();
        panelContent = new Panel();
        panelSidebar.SuspendLayout();
        panelTopBar.SuspendLayout();
        SuspendLayout();
        //
        // panelSidebar
        //
        panelSidebar.BackColor = AppTheme.Sidebar;
        panelSidebar.Controls.Add(btnLogout);
        panelSidebar.Controls.Add(btnEmployees);
        panelSidebar.Controls.Add(btnReports);
        panelSidebar.Controls.Add(btnOrders);
        panelSidebar.Controls.Add(btnCustomers);
        panelSidebar.Controls.Add(btnSuppliers);
        panelSidebar.Controls.Add(btnCategories);
        panelSidebar.Controls.Add(btnBooks);
        panelSidebar.Controls.Add(btnDashboard);
        panelSidebar.Controls.Add(lblAppTitle);
        panelSidebar.Dock = DockStyle.Left;
        panelSidebar.Name = "panelSidebar";
        panelSidebar.Padding = new Padding(0, 0, 0, 16);
        panelSidebar.Size = new Size(220, 600);
        panelSidebar.TabIndex = 0;
        //
        // lblAppTitle
        //
        lblAppTitle.Dock = DockStyle.Top;
        lblAppTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblAppTitle.ForeColor = Color.White;
        lblAppTitle.Name = "lblAppTitle";
        lblAppTitle.Size = new Size(220, 72);
        lblAppTitle.TabIndex = 0;
        lblAppTitle.Text = "Book Store";
        lblAppTitle.TextAlign = ContentAlignment.MiddleCenter;
        //
        // btnDashboard
        //
        btnDashboard.Dock = DockStyle.Top;
        btnDashboard.Name = "btnDashboard";
        btnDashboard.Size = new Size(220, 40);
        btnDashboard.TabIndex = 1;
        btnDashboard.Text = "Dashboard";
        btnDashboard.TextAlign = ContentAlignment.MiddleLeft;
        btnDashboard.Padding = new Padding(16, 0, 0, 0);
        btnDashboard.Click += btnDashboard_Click;
        AppTheme.StyleSidebarButton(btnDashboard, isActive: false);
        //
        // btnBooks
        //
        btnBooks.Dock = DockStyle.Top;
        btnBooks.Name = "btnBooks";
        btnBooks.Size = new Size(220, 40);
        btnBooks.TabIndex = 2;
        btnBooks.Text = "Books";
        btnBooks.TextAlign = ContentAlignment.MiddleLeft;
        btnBooks.Padding = new Padding(16, 0, 0, 0);
        btnBooks.Click += btnBooks_Click;
        AppTheme.StyleSidebarButton(btnBooks, isActive: false);
        //
        // btnCategories
        //
        btnCategories.Dock = DockStyle.Top;
        btnCategories.Name = "btnCategories";
        btnCategories.Size = new Size(220, 40);
        btnCategories.TabIndex = 3;
        btnCategories.Text = "Categories";
        btnCategories.TextAlign = ContentAlignment.MiddleLeft;
        btnCategories.Padding = new Padding(16, 0, 0, 0);
        btnCategories.Click += btnCategories_Click;
        AppTheme.StyleSidebarButton(btnCategories, isActive: false);
        //
        // btnSuppliers
        //
        btnSuppliers.Dock = DockStyle.Top;
        btnSuppliers.Name = "btnSuppliers";
        btnSuppliers.Size = new Size(220, 40);
        btnSuppliers.TabIndex = 4;
        btnSuppliers.Text = "Suppliers";
        btnSuppliers.TextAlign = ContentAlignment.MiddleLeft;
        btnSuppliers.Padding = new Padding(16, 0, 0, 0);
        btnSuppliers.Click += btnSuppliers_Click;
        AppTheme.StyleSidebarButton(btnSuppliers, isActive: false);
        //
        // btnCustomers
        //
        btnCustomers.Dock = DockStyle.Top;
        btnCustomers.Name = "btnCustomers";
        btnCustomers.Size = new Size(220, 40);
        btnCustomers.TabIndex = 5;
        btnCustomers.Text = "Customers";
        btnCustomers.TextAlign = ContentAlignment.MiddleLeft;
        btnCustomers.Padding = new Padding(16, 0, 0, 0);
        btnCustomers.Click += btnCustomers_Click;
        AppTheme.StyleSidebarButton(btnCustomers, isActive: false);
        //
        // btnOrders
        //
        btnOrders.Dock = DockStyle.Top;
        btnOrders.Name = "btnOrders";
        btnOrders.Size = new Size(220, 40);
        btnOrders.TabIndex = 6;
        btnOrders.Text = "Orders";
        btnOrders.TextAlign = ContentAlignment.MiddleLeft;
        btnOrders.Padding = new Padding(16, 0, 0, 0);
        btnOrders.Click += btnOrders_Click;
        AppTheme.StyleSidebarButton(btnOrders, isActive: false);
        //
        // btnReports
        //
        btnReports.Dock = DockStyle.Top;
        btnReports.Name = "btnReports";
        btnReports.Size = new Size(220, 40);
        btnReports.TabIndex = 7;
        btnReports.Text = "Reports";
        btnReports.TextAlign = ContentAlignment.MiddleLeft;
        btnReports.Padding = new Padding(16, 0, 0, 0);
        btnReports.Click += btnReports_Click;
        AppTheme.StyleSidebarButton(btnReports, isActive: false);
        //
        // btnEmployees
        //
        btnEmployees.Dock = DockStyle.Top;
        btnEmployees.Name = "btnEmployees";
        btnEmployees.Size = new Size(220, 40);
        btnEmployees.TabIndex = 8;
        btnEmployees.Text = "Employees";
        btnEmployees.TextAlign = ContentAlignment.MiddleLeft;
        btnEmployees.Padding = new Padding(16, 0, 0, 0);
        btnEmployees.Click += btnEmployees_Click;
        AppTheme.StyleSidebarButton(btnEmployees, isActive: false);
        //
        // btnLogout
        //
        btnLogout.Dock = DockStyle.Bottom;
        btnLogout.Name = "btnLogout";
        btnLogout.Size = new Size(220, 40);
        btnLogout.TabIndex = 9;
        btnLogout.Text = "Logout";
        btnLogout.Click += btnLogout_Click;
        AppTheme.StyleSidebarButton(btnLogout, isActive: false);
        //
        // panelTopBar
        //
        panelTopBar.BackColor = Color.White;
        panelTopBar.Controls.Add(lblUser);
        panelTopBar.Controls.Add(lblPageTitle);
        panelTopBar.Dock = DockStyle.Top;
        panelTopBar.Name = "panelTopBar";
        panelTopBar.Padding = new Padding(16, 0, 16, 0);
        panelTopBar.Size = new Size(980, 56);
        panelTopBar.TabIndex = 1;
        //
        // lblPageTitle
        //
        lblPageTitle.Dock = DockStyle.Fill;
        lblPageTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblPageTitle.ForeColor = AppTheme.Sidebar;
        lblPageTitle.Name = "lblPageTitle";
        lblPageTitle.Text = "Dashboard";
        lblPageTitle.TextAlign = ContentAlignment.MiddleLeft;
        //
        // lblUser
        //
        lblUser.Dock = DockStyle.Right;
        lblUser.ForeColor = Color.FromArgb(107, 114, 128);
        lblUser.Name = "lblUser";
        lblUser.Size = new Size(200, 56);
        lblUser.TabIndex = 1;
        lblUser.Text = "Signed in";
        lblUser.TextAlign = ContentAlignment.MiddleRight;
        //
        // panelContent
        //
        panelContent.BackColor = AppTheme.MainBackground;
        panelContent.Dock = DockStyle.Fill;
        panelContent.Name = "panelContent";
        panelContent.Padding = new Padding(8);
        panelContent.TabIndex = 2;
        //
        // MainForm
        //
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = AppTheme.MainBackground;
        ClientSize = new Size(1200, 600);
        Controls.Add(panelContent);
        Controls.Add(panelTopBar);
        Controls.Add(panelSidebar);
        MinimumSize = new Size(1024, 600);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Book Store Management";
        panelSidebar.ResumeLayout(false);
        panelTopBar.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Panel panelSidebar;
    private Label lblAppTitle;
    private Button btnDashboard;
    private Button btnBooks;
    private Button btnCategories;
    private Button btnSuppliers;
    private Button btnCustomers;
    private Button btnOrders;
    private Button btnReports;
    private Button btnEmployees;
    private Button btnLogout;
    private Panel panelTopBar;
    private Label lblPageTitle;
    private Label lblUser;
    private Panel panelContent;
}
