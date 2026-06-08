using System.Data;
using BookStoreApp.BLL;
using BookStoreApp.DTO;
using BookStoreApp.Theme;
using BookStoreApp.Utilities;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace BookStoreApp.UserControls;

public partial class ReportsControl : UserControl
{
    private readonly IReportService _reportService = ServiceLocator.ReportService;

    private static readonly (string Label, Func<IReportService, ReportSectionDto> Factory)[] ReportTypes =
    [
        ("Revenue Summary", s => s.GetRevenueSummary()),
        ("Revenue By Day", s => s.GetRevenueByPeriodReport("Day")),
        ("Revenue By Week", s => s.GetRevenueByPeriodReport("Week")),
        ("Revenue By Month", s => s.GetRevenueByPeriodReport("Month")),
        ("Best Selling Books", s => s.GetBestSellingBooksReport()),
        ("Low Stock", s => s.GetLowStockReport()),
        ("Slow Moving (90 days)", s => s.GetSlowMovingReport()),
        ("Import History", s => s.GetImportReport())
    ];

    public ReportsControl()
    {
        InitializeComponent();
        cboReportType.Items.AddRange(ReportTypes.Select(r => r.Label).Cast<object>().ToArray());
        if (cboReportType.Items.Count > 0)
        {
            cboReportType.SelectedIndex = 0;
        }

        numTopN.ValueChanged += (_, _) => LoadReports();
        numLowStockThreshold.ValueChanged += (_, _) => LoadReports();

        ApplyTheme();
        LoadReports();
    }

    private void ApplyTheme()
    {
        BackColor = AppTheme.MainBackground;
        panelToolbar.BackColor = AppTheme.MainBackground;
        AppTheme.StyleActionButton(btnExportCsv, AppTheme.Edit);
        AppTheme.StyleActionButton(btnExportPdf, AppTheme.Edit);
        AppTheme.StyleActionButton(btnExportExcel, AppTheme.Edit);
        AppTheme.ApplyGridStyle(dgvReports);
    }

    private ReportSectionDto GetCurrentSection()
    {
        var index = cboReportType.SelectedIndex;
        if (index < 0 || index >= ReportTypes.Length)
        {
            return _reportService.GetRevenueSummary();
        }

        var label = ReportTypes[index].Label;
        return label switch
        {
            "Best Selling Books" => _reportService.GetBestSellingBooksReport((int)numTopN.Value),
            "Low Stock" => _reportService.GetLowStockReport((int)numLowStockThreshold.Value),
            _ => ReportTypes[index].Factory(_reportService)
        };
    }

    private void LoadReports()
    {
        var section = GetCurrentSection();
        var table = new DataTable();
        foreach (var header in section.Headers)
        {
            table.Columns.Add(header);
        }

        foreach (var row in section.Rows)
        {
            var values = new object[section.Headers.Count];
            for (var i = 0; i < section.Headers.Count; i++)
            {
                values[i] = i < row.Count ? row[i] : string.Empty;
            }

            table.Rows.Add(values);
        }

        dgvReports.DataSource = table;
        LoadChart(section);
    }

    private void LoadChart(ReportSectionDto section)
    {
        var model = new PlotModel { Title = section.SectionName };
        var categoryAxis = new CategoryAxis { Position = AxisPosition.Bottom, Angle = -35 };
        var valueAxis = new LinearAxis { Position = AxisPosition.Left, Minimum = 0 };
        var series = new LineSeries { MarkerType = MarkerType.Circle };

        foreach (var row in section.Rows)
        {
            if (row.Count == 0)
            {
                continue;
            }

            var numericText = row.LastOrDefault(value => decimal.TryParse(value, out _));
            if (numericText is null || !decimal.TryParse(numericText, out var value))
            {
                continue;
            }

            categoryAxis.Labels.Add(row[0]);
            series.Points.Add(new DataPoint(categoryAxis.Labels.Count - 1, (double)value));
        }

        model.Axes.Add(categoryAxis);
        model.Axes.Add(valueAxis);
        model.Series.Add(series);
        plotView.Model = model;
    }

    private void cboReportType_SelectedIndexChanged(object sender, EventArgs e) => LoadReports();

    private void btnExportCsv_Click(object sender, EventArgs e) => ExportReport((path, section) => ReportExporter.ExportToCsv(section, path), "csv", "CSV files (*.csv)|*.csv");

    private void btnExportPdf_Click(object sender, EventArgs e) => ExportReport((path, section) => ReportExporter.ExportToPdf(section, path), "pdf", "PDF files (*.pdf)|*.pdf");

    private void btnExportExcel_Click(object sender, EventArgs e) => ExportReport((path, section) => ReportExporter.ExportToExcel(section, path), "xls", "Excel files (*.xls)|*.xls");

    private void ExportReport(Action<string, ReportSectionDto> export, string extension, string filter)
    {
        using var dialog = new SaveFileDialog
        {
            Filter = filter,
            FileName = $"{GetCurrentSection().SectionName.Replace(' ', '_')}.{extension}"
        };

        if (dialog.ShowDialog(FindForm()) != DialogResult.OK)
        {
            return;
        }

        try
        {
            export(dialog.FileName, GetCurrentSection());
            MessageBox.Show("Export completed.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Export failed: {ex.Message}", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
