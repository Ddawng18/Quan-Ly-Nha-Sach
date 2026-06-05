using BookStoreApp.BLL;
using BookStoreApp.DTO;
using BookStoreApp.Forms;
using BookStoreApp.Theme;

namespace BookStoreApp.UserControls;

public partial class OrdersControl : UserControl
{
    private readonly IOrderService _orderService = ServiceLocator.OrderService;
    private DataGridView dgvOrderDetails = new();
    private ComboBox cboNewStatus = new();
    private Button btnUpdateStatus = new();

    public OrdersControl()
    {
        InitializeComponent();
        dgvOrders.ColumnHeadersVisible = true;        
        dgvOrderDetails.ColumnHeadersVisible = true; 
        InitializeInvoiceDetailView();
        dtpFrom.Checked = false;
        dtpTo.Checked = false;
        cboPaymentStatus.Items.AddRange(["All", OrderStatus.Paid, OrderStatus.Pending, OrderStatus.Cancelled]);
        cboPaymentStatus.SelectedIndex = 0;
        cboNewStatus.Items.AddRange(OrderStatus.All.Cast<object>().ToArray());
        cboNewStatus.SelectedItem = OrderStatus.Paid;
        ApplyTheme();
        LoadOrders();
    }

    private void InitializeInvoiceDetailView()
    {
        Controls.Remove(dgvOrders);

        var split = new SplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Horizontal,
            SplitterDistance = 300
        };

        dgvOrders.Dock = DockStyle.Fill;
        dgvOrders.SelectionChanged += dgvOrders_SelectionChanged;
        split.Panel1.Controls.Add(dgvOrders);

        var detailToolbar = new Panel { Dock = DockStyle.Top, Height = 44 };
        detailToolbar.Controls.Add(new Label { AutoSize = true, Text = "Set Status", Location = new Point(0, 12) });
        cboNewStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cboNewStatus.Location = new Point(80, 8);
        cboNewStatus.Size = new Size(120, 28);
        detailToolbar.Controls.Add(cboNewStatus);
        btnUpdateStatus.Text = "Update";
        btnUpdateStatus.Location = new Point(212, 4);
        btnUpdateStatus.Size = new Size(88, 36);
        btnUpdateStatus.Click += btnUpdateStatus_Click;
        detailToolbar.Controls.Add(btnUpdateStatus);

        dgvOrderDetails.AllowUserToAddRows = false;
        dgvOrderDetails.AllowUserToDeleteRows = false;
        dgvOrderDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvOrderDetails.Dock = DockStyle.Fill;
        dgvOrderDetails.ReadOnly = true;
        dgvOrderDetails.RowHeadersVisible = false;
        dgvOrderDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

split.Panel2.Controls.Add(dgvOrderDetails);
        split.Panel2.Controls.Add(detailToolbar);
        Controls.Add(split);
        split.BringToFront();
    }

    private void ApplyTheme()
    {
        BackColor = AppTheme.MainBackground;
        panelToolbar.BackColor = AppTheme.MainBackground;
        panelFilters.BackColor = AppTheme.MainBackground;
        AppTheme.StyleActionButton(btnCreate, AppTheme.Add);
        AppTheme.StyleRefreshButton(btnRefresh);
        AppTheme.StyleActionButton(btnApplyFilter, AppTheme.Add);
        AppTheme.StyleActionButton(btnSearch, AppTheme.Add);
        AppTheme.StyleActionButton(btnUpdateStatus, AppTheme.Edit);
        AppTheme.ApplyGridStyle(dgvOrders);
        AppTheme.ApplyGridStyle(dgvOrderDetails);
    }

    private void LoadOrders()
    {
        dgvOrders.DataSource = GetFilteredOrders().ToList();
        ConfigureGridColumns();
        SetColumn("OrderID", "OrderID", 80, 0);
        SetColumn("CustomerID", "CustomerID", 90, 1);
        SetColumn("CustomerName", "CustomerName", 150, 2);
        SetColumn("EmployeeID", "EmployeeID", 90, 3);
        SetColumn("EmployeeName", "EmployeeName", 150, 4);
        SetColumn("OrderDate", "OrderDate", 120, 5);
        SetColumn("SubtotalAmount", "Subtotal", 100, 6);
        SetColumn("DiscountAmount", "Discount", 100, 7);
        SetColumn("TaxAmount", "Tax", 90, 8);
        SetColumn("TotalAmount", "TotalAmount", 110, 9);
        SetColumn("PaymentMethod", "PaymentMethod", 120, 10);
        SetColumn("PaymentStatus", "PaymentStatus", 110, 11);
    }

    private IEnumerable<OrderViewDto> GetFilteredOrders()
    {
        IEnumerable<OrderViewDto> orders = dtpFrom.Checked || dtpTo.Checked
            ? _orderService.GetOrdersByDateRange(
                dtpFrom.Checked ? dtpFrom.Value.Date : null,
                dtpTo.Checked ? dtpTo.Value.Date : null)
            : _orderService.GetOrders();

        if (cboPaymentStatus.SelectedIndex > 0 && cboPaymentStatus.SelectedItem is string status)
        {
            orders = orders.Where(o => o.PaymentStatus.Equals(status, StringComparison.OrdinalIgnoreCase));
        }

        var search = txtSearch.Text.Trim();
        if (!string.IsNullOrWhiteSpace(search))
        {
            orders = orders.Where(o =>
                o.OrderID.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                o.CustomerName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                o.EmployeeName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                o.PaymentStatus.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        return orders;
    }

    private void ClearFilters()
    {
        txtSearch.Clear();
        cboPaymentStatus.SelectedIndex = 0;
        dtpFrom.Checked = false;
        dtpTo.Checked = false;
    }

    private void ConfigureGridColumns()
    {
        if (dgvOrders.Columns.Count == 0)
        {
            return;
        }

        

        if (dgvOrders.Columns["OrderDate"] is DataGridViewColumn orderDate)
        {
            orderDate.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }

        if (dgvOrders.Columns["TotalAmount"] is DataGridViewColumn total)
        {
            total.DefaultCellStyle.Format = "N2";
            total.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        foreach (var columnName in new[] { "SubtotalAmount", "DiscountAmount", "TaxAmount" })
        {
            if (dgvOrders.Columns[columnName] is DataGridViewColumn moneyColumn)
            {
                moneyColumn.DefaultCellStyle.Format = "N2";
                moneyColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }
    }

    private void SetColumn(string property, string header, int width, int displayIndex)
    {
        if (dgvOrders.Columns[property] is not DataGridViewColumn column)
        {
            return;
        }

        column.Visible = true;
        column.HeaderText = header;
        column.Width = width;
        column.DisplayIndex = displayIndex;
    }

    private void btnCreate_Click(object sender, EventArgs e)
    {
        using var form = new OrderCreateForm();
        if (form.ShowDialog(FindForm()) == DialogResult.OK)
        {
            LoadOrders();
        }
    }

    private void btnApplyFilter_Click(object sender, EventArgs e) => LoadOrders();

    private void btnSearch_Click(object sender, EventArgs e) => LoadOrders();

    private void txtSearch_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            LoadOrders();
        }
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        ClearFilters();
        LoadOrders();
    }

    private void dgvOrders_SelectionChanged(object? sender, EventArgs e) => LoadOrderDetails();

    private void LoadOrderDetails()
    {
        if (dgvOrders.CurrentRow?.DataBoundItem is not OrderViewDto order)
        {
            dgvOrderDetails.DataSource = null;
            return;
        }

        dgvOrderDetails.DataSource = null;
        dgvOrderDetails.DataSource = _orderService.GetOrderDetails(order.OrderID).ToList();

        foreach (var columnName in new[] { "UnitPrice", "DiscountAmount", "Subtotal" })
        {
            if (dgvOrderDetails.Columns[columnName] is DataGridViewColumn column)
            {
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }
    }

    private void btnUpdateStatus_Click(object? sender, EventArgs e)
    {
        if (dgvOrders.CurrentRow?.DataBoundItem is not OrderViewDto order)
        {
            MessageBox.Show("Please select an invoice.", "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var status = cboNewStatus.SelectedItem?.ToString() ?? OrderStatus.Paid;
        var result = _orderService.UpdateStatus(order.OrderID, status);
        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage, "Invoice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        LoadOrders();
    }
}
