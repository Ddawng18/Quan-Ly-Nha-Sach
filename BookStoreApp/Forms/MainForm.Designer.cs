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
        btnLogout = new Button();
        btnEmployees = new Button();
        btnReports = new Button();
        btnOrders = new Button();
        btnImport = new Button();
        btnCustomers = new Button();
        btnSuppliers = new Button();
        btnCategories = new Button();
        btnBooks = new Button();
        btnDashboard = new Button();
        lblAppTitle = new Label();
        panelTopBar = new Panel();
        lblUser = new Label();
        lblPageTitle = new Label();
        panelContent = new Panel();
        panelSidebar.SuspendLayout();
        panelTopBar.SuspendLayout();
        SuspendLayout();
        //
        // panelSidebar
        //
        panelSidebar.BackColor = Color.FromArgb(31, 41, 55);
        panelSidebar.Controls.Add(btnLogout);
        panelSidebar.Controls.Add(btnEmployees);
        panelSidebar.Controls.Add(btnReports);
        panelSidebar.Controls.Add(btnOrders);
        panelSidebar.Controls.Add(btnImport);
        panelSidebar.Controls.Add(btnCustomers);
        panelSidebar.Controls.Add(btnSuppliers);
        panelSidebar.Controls.Add(btnCategories);
        panelSidebar.Controls.Add(btnBooks);
        panelSidebar.Controls.Add(btnDashboard);
        panelSidebar.Controls.Add(lblAppTitle);
        panelSidebar.Dock = DockStyle.Left;
        panelSidebar.Location = new Point(0, 0);
        panelSidebar.Name = "panelSidebar";
        panelSidebar.Padding = new Padding(0, 0, 0, 16);
        panelSidebar.Size = new Size(220, 600);
        panelSidebar.TabIndex = 0;
        //
        // btnLogout
        //
        btnLogout.Dock = DockStyle.Bottom;
        btnLogout.Location = new Point(0, 544);
        btnLogout.Name = "btnLogout";
        btnLogout.Size = new Size(220, 40);
        btnLogout.TabIndex = 9;
        btnLogout.Text = "Logout";
        btnLogout.FlatStyle = FlatStyle.Flat;
        btnLogout.FlatAppearance.BorderSize = 0;
        btnLogout.ForeColor = Color.FromArgb(241, 245, 249);
        btnLogout.Click += btnLogout_Click;
        //
        // btnEmployees
        //
        btnEmployees.Dock = DockStyle.Top;
        btnEmployees.Location = new Point(0, 392);
        btnEmployees.Name = "btnEmployees";
        btnEmployees.Padding = new Padding(16, 0, 0, 0);
        btnEmployees.Size = new Size(220, 40);
        btnEmployees.TabIndex = 8;
        btnEmployees.Text = "Employees";
        btnEmployees.TextAlign = ContentAlignment.MiddleLeft;
        btnEmployees.FlatStyle = FlatStyle.Flat;
        btnEmployees.FlatAppearance.BorderSize = 0;
        btnEmployees.Click += btnEmployees_Click;
        //
        // btnReports
        //
        btnReports.Dock = DockStyle.Top;
        btnReports.Location = new Point(0, 352);
        btnReports.Name = "btnReports";
        btnReports.Padding = new Padding(16, 0, 0, 0);
        btnReports.Size = new Size(220, 40);
        btnReports.TabIndex = 7;
        btnReports.Text = "Reports";
        btnReports.TextAlign = ContentAlignment.MiddleLeft;
        btnReports.FlatStyle = FlatStyle.Flat;
        btnReports.FlatAppearance.BorderSize = 0;
        btnReports.Click += btnReports_Click;
        //
        // btnOrders
        //
        btnOrders.Dock = DockStyle.Top;
        btnOrders.Location = new Point(0, 312);
        btnOrders.Name = "btnOrders";
        btnOrders.Padding = new Padding(16, 0, 0, 0);
        btnOrders.Size = new Size(220, 40);
        btnOrders.TabIndex = 6;
        btnOrders.Text = "Orders";
        btnOrders.TextAlign = ContentAlignment.MiddleLeft;
        btnOrders.FlatStyle = FlatStyle.Flat;
        btnOrders.FlatAppearance.BorderSize = 0;
        btnOrders.Click += btnOrders_Click;
        //
        // btnImport
        //
        btnImport.Dock = DockStyle.Top;
        btnImport.Location = new Point(0, 272);
        btnImport.Name = "btnImport";
        btnImport.Padding = new Padding(16, 0, 0, 0);
        btnImport.Size = new Size(220, 40);
        btnImport.TabIndex = 10;
        btnImport.Text = "Import";
        btnImport.TextAlign = ContentAlignment.MiddleLeft;
        btnImport.FlatStyle = FlatStyle.Flat;
        btnImport.FlatAppearance.BorderSize = 0;
        btnImport.Click += btnImport_Click;
        //
        // btnCustomers
        //
        btnCustomers.Dock = DockStyle.Top;
        btnCustomers.Location = new Point(0, 232);
        btnCustomers.Name = "btnCustomers";
        btnCustomers.Padding = new Padding(16, 0, 0, 0);
        btnCustomers.Size = new Size(220, 40);
        btnCustomers.TabIndex = 5;
        btnCustomers.Text = "Customers";
        btnCustomers.TextAlign = ContentAlignment.MiddleLeft;
        btnCustomers.FlatStyle = FlatStyle.Flat;
        btnCustomers.FlatAppearance.BorderSize = 0;
        btnCustomers.Click += btnCustomers_Click;
        //
        // btnSuppliers
        //
        btnSuppliers.Dock = DockStyle.Top;
        btnSuppliers.Location = new Point(0, 192);
        btnSuppliers.Name = "btnSuppliers";
        btnSuppliers.Padding = new Padding(16, 0, 0, 0);
        btnSuppliers.Size = new Size(220, 40);
        btnSuppliers.TabIndex = 4;
        btnSuppliers.Text = "Suppliers";
        btnSuppliers.TextAlign = ContentAlignment.MiddleLeft;
        btnSuppliers.FlatStyle = FlatStyle.Flat;
        btnSuppliers.FlatAppearance.BorderSize = 0;
        btnSuppliers.Click += btnSuppliers_Click;
        //
        // btnCategories
        //
        btnCategories.Dock = DockStyle.Top;
        btnCategories.Location = new Point(0, 152);
        btnCategories.Name = "btnCategories";
        btnCategories.Padding = new Padding(16, 0, 0, 0);
        btnCategories.Size = new Size(220, 40);
        btnCategories.TabIndex = 3;
        btnCategories.Text = "Categories";
        btnCategories.TextAlign = ContentAlignment.MiddleLeft;
        btnCategories.FlatStyle = FlatStyle.Flat;
        btnCategories.FlatAppearance.BorderSize = 0;
        btnCategories.Click += btnCategories_Click;
        //
        // btnBooks
        //
        btnBooks.Dock = DockStyle.Top;
        btnBooks.Location = new Point(0, 112);
        btnBooks.Name = "btnBooks";
        btnBooks.Padding = new Padding(16, 0, 0, 0);
        btnBooks.Size = new Size(220, 40);
        btnBooks.TabIndex = 2;
        btnBooks.Text = "Books";
        btnBooks.TextAlign = ContentAlignment.MiddleLeft;
        btnBooks.FlatStyle = FlatStyle.Flat;
        btnBooks.FlatAppearance.BorderSize = 0;
        btnBooks.Click += btnBooks_Click;
        //
        // btnDashboard
        //
        btnDashboard.Dock = DockStyle.Top;
        btnDashboard.Location = new Point(0, 72);
        btnDashboard.Name = "btnDashboard";
        btnDashboard.Padding = new Padding(16, 0, 0, 0);
        btnDashboard.Size = new Size(220, 40);
        btnDashboard.TabIndex = 1;
        btnDashboard.Text = "Dashboard";
        btnDashboard.TextAlign = ContentAlignment.MiddleLeft;
        btnDashboard.FlatStyle = FlatStyle.Flat;
        btnDashboard.FlatAppearance.BorderSize = 0;
        btnDashboard.Click += btnDashboard_Click;
        //
        // lblAppTitle
        //
        lblAppTitle.Dock = DockStyle.Top;
        lblAppTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblAppTitle.ForeColor = Color.White;
        lblAppTitle.Location = new Point(0, 0);
        lblAppTitle.Name = "lblAppTitle";
        lblAppTitle.Size = new Size(220, 72);
        lblAppTitle.TabIndex = 0;
        lblAppTitle.Text = "Book Store";
        lblAppTitle.TextAlign = ContentAlignment.MiddleCenter;
        //
        // panelTopBar
        //
        panelTopBar.BackColor = Color.White;
        panelTopBar.Controls.Add(lblUser);
        panelTopBar.Controls.Add(lblPageTitle);
        panelTopBar.Dock = DockStyle.Top;
        panelTopBar.Location = new Point(220, 0);
        panelTopBar.Name = "panelTopBar";
        panelTopBar.Padding = new Padding(16, 0, 16, 0);
        panelTopBar.Size = new Size(980, 56);
        panelTopBar.TabIndex = 1;
        //
        // lblUser
        //
        lblUser.Dock = DockStyle.Right;
        lblUser.ForeColor = Color.FromArgb(107, 114, 128);
        lblUser.Location = new Point(764, 0);
        lblUser.Name = "lblUser";
        lblUser.Size = new Size(200, 56);
        lblUser.TabIndex = 1;
        lblUser.Text = "Signed in";
        lblUser.TextAlign = ContentAlignment.MiddleRight;
        //
        // lblPageTitle
        //
        lblPageTitle.Dock = DockStyle.Fill;
        lblPageTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblPageTitle.ForeColor = Color.FromArgb(31, 41, 55);
        lblPageTitle.Location = new Point(16, 0);
        lblPageTitle.Name = "lblPageTitle";
        lblPageTitle.Size = new Size(948, 56);
        lblPageTitle.TabIndex = 2;
        lblPageTitle.Text = "Dashboard";
        lblPageTitle.TextAlign = ContentAlignment.MiddleLeft;
        //
        // panelContent
        //
        panelContent.BackColor = Color.FromArgb(243, 244, 246);
        panelContent.Dock = DockStyle.Fill;
        panelContent.Location = new Point(220, 56);
        panelContent.Name = "panelContent";
        panelContent.Padding = new Padding(8);
        panelContent.Size = new Size(980, 544);
        panelContent.TabIndex = 2;
        //
        // MainForm
        //
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(243, 244, 246);
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
    private Button btnImport;
    private Button btnOrders;
    private Button btnReports;
    private Button btnEmployees;
    private Button btnLogout;
    private Panel panelTopBar;
    private Label lblPageTitle;
    private Label lblUser;
    private Panel panelContent;
}
