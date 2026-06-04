using BookStoreApp.BLL;
using BookStoreApp.Theme;

namespace BookStoreApp.UserControls;

public partial class DashboardControl : UserControl
{
    private readonly IDashboardService _dashboardService = ServiceLocator.DashboardService;

    public DashboardControl()
    {
        InitializeComponent();
        ApplyTheme();
        LoadDashboard();
    }

    private void ApplyTheme()
    {
        BackColor = AppTheme.MainBackground;
        panelToolbar.BackColor = AppTheme.MainBackground;
        AppTheme.StyleRefreshButton(btnRefresh);
        AppTheme.ApplyGridStyle(dgvDashboard);
        AppTheme.ApplyGridStyle(dgvRecentOrders);
        AppTheme.ApplyGridStyle(dgvBestSelling);
    }

    private void LoadDashboard()
    {
        dgvDashboard.DataSource = null;
        dgvDashboard.DataSource = _dashboardService.SearchMetrics(txtSearch.Text).ToList();
        ConfigureMetricColumns();

        dgvRecentOrders.DataSource = null;
        dgvRecentOrders.DataSource = _dashboardService.GetRecentOrders().ToList();

        dgvBestSelling.DataSource = null;
        dgvBestSelling.DataSource = _dashboardService.GetBestSellingBooks().ToList();

        if (dgvRecentOrders.Columns["Total"] is DataGridViewColumn total)
        {
            total.DefaultCellStyle.Format = "N2";
            total.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
    }

    private void ConfigureMetricColumns()
    {
        if (dgvDashboard.Columns.Count == 0)
        {
            return;
        }

        SetColumn(dgvDashboard, "Metric", "Metric", 220, 0);
        SetColumn(dgvDashboard, "Value", "Value", 200, 1);
    }

    private static void SetColumn(DataGridView grid, string property, string header, int width, int displayIndex)
    {
        if (grid.Columns[property] is not DataGridViewColumn column)
        {
            return;
        }

        column.Visible = true;
        column.HeaderText = header;
        column.Width = width;
        column.DisplayIndex = displayIndex;
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        LoadDashboard();
    }

    private void btnSearch_Click(object sender, EventArgs e) => LoadDashboard();

    private void txtSearch_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            LoadDashboard();
        }
    }
}
