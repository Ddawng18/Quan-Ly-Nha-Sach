namespace BookStoreApp.UserControls;

partial class OrdersControl
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
        btnCreate = new Button();
        btnRefresh = new Button();
        lblPayment = new Label();
        cboPaymentStatus = new ComboBox();
        txtSearch = new TextBox();
        btnSearch = new Button();
        panelFilters = new Panel();
        lblFrom = new Label();
        dtpFrom = new DateTimePicker();
        lblTo = new Label();
        dtpTo = new DateTimePicker();
        btnApplyFilter = new Button();
        dgvOrders = new DataGridView();
        panelToolbar.SuspendLayout();
        panelFilters.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
        SuspendLayout();
        panelToolbar.Controls.Add(btnSearch);
        panelToolbar.Controls.Add(txtSearch);
        panelToolbar.Controls.Add(cboPaymentStatus);
        panelToolbar.Controls.Add(lblPayment);
        panelToolbar.Controls.Add(btnRefresh);
        panelToolbar.Controls.Add(btnCreate);
        panelToolbar.Dock = DockStyle.Top;
        panelToolbar.Name = "panelToolbar";
        panelToolbar.Padding = new Padding(0, 0, 0, 4);
        panelToolbar.Size = new Size(784, 48);
        btnCreate.Location = new Point(0, 4);
        btnCreate.Name = "btnCreate";
        btnCreate.Size = new Size(110, 36);
        btnCreate.Text = "Create Order";
        btnCreate.Click += btnCreate_Click;
        btnRefresh.Location = new Point(116, 4);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(88, 36);
        btnRefresh.Text = "Refresh";
        btnRefresh.Click += btnRefresh_Click;
        lblPayment.AutoSize = true;
        lblPayment.Location = new Point(220, 12);
        lblPayment.Text = "Status";
        cboPaymentStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cboPaymentStatus.Location = new Point(272, 8);
        cboPaymentStatus.Name = "cboPaymentStatus";
        cboPaymentStatus.Size = new Size(110, 28);
        txtSearch.Location = new Point(396, 8);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Order, customer, employee...";
        txtSearch.Size = new Size(220, 27);
        txtSearch.KeyDown += txtSearch_KeyDown;
        btnSearch.Location = new Point(624, 4);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(72, 36);
        btnSearch.Text = "Search";
        btnSearch.Click += btnSearch_Click;
        panelFilters.Controls.Add(btnApplyFilter);
        panelFilters.Controls.Add(dtpTo);
        panelFilters.Controls.Add(lblTo);
        panelFilters.Controls.Add(dtpFrom);
        panelFilters.Controls.Add(lblFrom);
        panelFilters.Dock = DockStyle.Top;
        panelFilters.Name = "panelFilters";
        panelFilters.Padding = new Padding(0, 0, 0, 8);
        panelFilters.Size = new Size(784, 44);
        lblFrom.AutoSize = true;
        lblFrom.Location = new Point(0, 10);
        lblFrom.Text = "From";
        dtpFrom.Format = DateTimePickerFormat.Short;
        dtpFrom.Location = new Point(44, 6);
        dtpFrom.Name = "dtpFrom";
        dtpFrom.ShowCheckBox = true;
        dtpFrom.Size = new Size(130, 27);
        lblTo.AutoSize = true;
        lblTo.Location = new Point(188, 10);
        lblTo.Text = "To";
        dtpTo.Format = DateTimePickerFormat.Short;
        dtpTo.Location = new Point(216, 6);
        dtpTo.Name = "dtpTo";
        dtpTo.ShowCheckBox = true;
        dtpTo.Size = new Size(130, 27);
        btnApplyFilter.Location = new Point(360, 2);
        btnApplyFilter.Name = "btnApplyFilter";
        btnApplyFilter.Size = new Size(100, 36);
        btnApplyFilter.Text = "Apply Filter";
        btnApplyFilter.Click += btnApplyFilter_Click;
        dgvOrders.AllowUserToAddRows = false;
        dgvOrders.AllowUserToDeleteRows = false;
        dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        dgvOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvOrders.Dock = DockStyle.Fill;
        dgvOrders.Name = "dgvOrders";
        dgvOrders.ReadOnly = true;
        dgvOrders.RowHeadersVisible = false;
        dgvOrders.ScrollBars = ScrollBars.Both;
        dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        Controls.Add(dgvOrders);
        Controls.Add(panelFilters);
        Controls.Add(panelToolbar);
        Name = "OrdersControl";
        Padding = new Padding(8);
        Size = new Size(800, 500);
        panelToolbar.ResumeLayout(false);
        panelToolbar.PerformLayout();
        panelFilters.ResumeLayout(false);
        panelFilters.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
        ResumeLayout(false);
    }

    private Panel panelToolbar;
    private Button btnCreate;
    private Button btnRefresh;
    private Label lblPayment;
    private ComboBox cboPaymentStatus;
    private TextBox txtSearch;
    private Button btnSearch;
    private Panel panelFilters;
    private Label lblFrom;
    private DateTimePicker dtpFrom;
    private Label lblTo;
    private DateTimePicker dtpTo;
    private Button btnApplyFilter;
    private DataGridView dgvOrders;
}
