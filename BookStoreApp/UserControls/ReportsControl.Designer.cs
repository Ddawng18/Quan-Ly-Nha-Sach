namespace BookStoreApp.UserControls;

partial class ReportsControl
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
        btnExportExcel = new Button();
        btnExportPdf = new Button();
        btnExportCsv = new Button();
        cboReportType = new ComboBox();
        lblReportType = new Label();
        lblTopN = new Label();
        numTopN = new NumericUpDown();
        lblLowStock = new Label();
        numLowStockThreshold = new NumericUpDown();
        splitContainer = new SplitContainer();
        plotView = new OxyPlot.WindowsForms.PlotView();
        dgvReports = new DataGridView();
        panelToolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numTopN).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numLowStockThreshold).BeginInit();
        ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
        splitContainer.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvReports).BeginInit();
        SuspendLayout();
        // 
        // panelToolbar
        // 
        panelToolbar.Controls.Add(numLowStockThreshold);
        panelToolbar.Controls.Add(lblLowStock);
        panelToolbar.Controls.Add(numTopN);
        panelToolbar.Controls.Add(lblTopN);
        panelToolbar.Controls.Add(btnExportExcel);
        panelToolbar.Controls.Add(btnExportPdf);
        panelToolbar.Controls.Add(btnExportCsv);
        panelToolbar.Controls.Add(cboReportType);
        panelToolbar.Controls.Add(lblReportType);
        panelToolbar.Dock = DockStyle.Top;
        panelToolbar.Location = new Point(8, 8);
        panelToolbar.Name = "panelToolbar";
        panelToolbar.Padding = new Padding(0, 0, 0, 8);
        panelToolbar.Size = new Size(784, 88);
        panelToolbar.TabIndex = 1;
        // 
        // btnExportExcel
        // 
        btnExportExcel.Location = new Point(689, 5);
        btnExportExcel.Name = "btnExportExcel";
        btnExportExcel.Size = new Size(88, 36);
        btnExportExcel.TabIndex = 0;
        btnExportExcel.Text = "Excel";
        btnExportExcel.Click += btnExportExcel_Click;
        // 
        // btnExportPdf
        // 
        btnExportPdf.Location = new Point(559, 5);
        btnExportPdf.Name = "btnExportPdf";
        btnExportPdf.Size = new Size(88, 36);
        btnExportPdf.TabIndex = 1;
        btnExportPdf.Text = "PDF";
        btnExportPdf.Click += btnExportPdf_Click;
        // 
        // btnExportCsv
        // 
        btnExportCsv.Location = new Point(430, 5);
        btnExportCsv.Name = "btnExportCsv";
        btnExportCsv.Size = new Size(88, 36);
        btnExportCsv.TabIndex = 2;
        btnExportCsv.Text = "CSV";
        btnExportCsv.Click += btnExportCsv_Click;
        // 
        // cboReportType
        // 
        cboReportType.DropDownStyle = ComboBoxStyle.DropDownList;
        cboReportType.Location = new Point(67, 10);
        cboReportType.Name = "cboReportType";
        cboReportType.Size = new Size(280, 28);
        cboReportType.TabIndex = 3;
        cboReportType.SelectedIndexChanged += cboReportType_SelectedIndexChanged;
        // 
        // lblReportType
        // 
        lblReportType.AutoSize = true;
        lblReportType.Location = new Point(11, 14);
        lblReportType.Name = "lblReportType";
        lblReportType.Size = new Size(54, 20);
        lblReportType.TabIndex = 4;
        lblReportType.Text = "Report";
        // 
        // lblTopN
        // 
        lblTopN.AutoSize = true;
        lblTopN.Location = new Point(11, 56);
        lblTopN.Name = "lblTopN";
        lblTopN.Size = new Size(46, 20);
        lblTopN.Text = "Top N";
        // 
        // numTopN
        // 
        numTopN.Location = new Point(67, 52);
        numTopN.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
        numTopN.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numTopN.Name = "numTopN";
        numTopN.Size = new Size(80, 27);
        numTopN.Value = new decimal(new int[] { 10, 0, 0, 0 });
        // 
        // lblLowStock
        // 
        lblLowStock.AutoSize = true;
        lblLowStock.Location = new Point(166, 56);
        lblLowStock.Name = "lblLowStock";
        lblLowStock.Size = new Size(92, 20);
        lblLowStock.Text = "Low Stock <=";
        // 
        // numLowStockThreshold
        // 
        numLowStockThreshold.Location = new Point(268, 52);
        numLowStockThreshold.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        numLowStockThreshold.Name = "numLowStockThreshold";
        numLowStockThreshold.Size = new Size(80, 27);
        numLowStockThreshold.Value = new decimal(new int[] { 10, 0, 0, 0 });
        // 
        // splitContainer
        // 
        splitContainer.Dock = DockStyle.Fill;
        splitContainer.Location = new Point(8, 96);
        splitContainer.Name = "splitContainer";
        splitContainer.Orientation = Orientation.Horizontal;
        // 
        // splitContainer.Panel1
        // 
        splitContainer.Panel1.Controls.Add(plotView);
        // 
        // splitContainer.Panel2
        // 
        splitContainer.Panel2.Controls.Add(dgvReports);
        splitContainer.Size = new Size(784, 396);
        splitContainer.SplitterDistance = 210;
        splitContainer.TabIndex = 2;
        // 
        // plotView
        // 
        plotView.Dock = DockStyle.Fill;
        plotView.Location = new Point(0, 0);
        plotView.Name = "plotView";
        plotView.Size = new Size(784, 210);
        plotView.TabIndex = 0;
        // 
        // dgvReports
        // 
        dgvReports.AllowUserToAddRows = false;
        dgvReports.AllowUserToDeleteRows = false;
        dgvReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvReports.Dock = DockStyle.Fill;
        dgvReports.Location = new Point(0, 0);
        dgvReports.Name = "dgvReports";
        dgvReports.ReadOnly = true;
        dgvReports.RowHeadersVisible = false;
        dgvReports.RowHeadersWidth = 51;
        dgvReports.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvReports.Size = new Size(784, 182);
        dgvReports.TabIndex = 0;
        // 
        // ReportsControl
        // 
        Controls.Add(splitContainer);
        Controls.Add(panelToolbar);
        Name = "ReportsControl";
        Padding = new Padding(8);
        Size = new Size(800, 500);
        panelToolbar.ResumeLayout(false);
        panelToolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numTopN).EndInit();
        ((System.ComponentModel.ISupportInitialize)numLowStockThreshold).EndInit();
        ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
        splitContainer.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvReports).EndInit();
        ResumeLayout(false);
    }

    private Panel panelToolbar;
    private Label lblReportType;
    private ComboBox cboReportType;
    private Button btnExportCsv;
    private Button btnExportPdf;
    private Button btnExportExcel;
    private Label lblTopN;
    private NumericUpDown numTopN;
    private Label lblLowStock;
    private NumericUpDown numLowStockThreshold;
    private SplitContainer splitContainer;
    private OxyPlot.WindowsForms.PlotView plotView;
    private DataGridView dgvReports;
}
