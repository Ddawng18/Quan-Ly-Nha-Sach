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
        dgvReports = new DataGridView();
        panelToolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvReports).BeginInit();
        SuspendLayout();
        // 
        // panelToolbar
        // 
        panelToolbar.Controls.Add(btnExportExcel);
        panelToolbar.Controls.Add(btnExportPdf);
        panelToolbar.Controls.Add(btnExportCsv);
        panelToolbar.Controls.Add(cboReportType);
        panelToolbar.Controls.Add(lblReportType);
        panelToolbar.Dock = DockStyle.Top;
        panelToolbar.Location = new Point(8, 8);
        panelToolbar.Name = "panelToolbar";
        panelToolbar.Padding = new Padding(0, 0, 0, 8);
        panelToolbar.Size = new Size(784, 48);
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
        // dgvReports
        // 
        dgvReports.AllowUserToAddRows = false;
        dgvReports.AllowUserToDeleteRows = false;
        dgvReports.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvReports.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvReports.Dock = DockStyle.Fill;
        dgvReports.Location = new Point(8, 56);
        dgvReports.Name = "dgvReports";
        dgvReports.ReadOnly = true;
        dgvReports.RowHeadersVisible = false;
        dgvReports.RowHeadersWidth = 51;
        dgvReports.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvReports.Size = new Size(784, 436);
        dgvReports.TabIndex = 0;
        // 
        // ReportsControl
        // 
        Controls.Add(dgvReports);
        Controls.Add(panelToolbar);
        Name = "ReportsControl";
        Padding = new Padding(8);
        Size = new Size(800, 500);
        panelToolbar.ResumeLayout(false);
        panelToolbar.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvReports).EndInit();
        ResumeLayout(false);
    }

    private Panel panelToolbar;
    private Label lblReportType;
    private ComboBox cboReportType;
    private Button btnExportCsv;
    private Button btnExportPdf;
    private Button btnExportExcel;
    private DataGridView dgvReports;
}
