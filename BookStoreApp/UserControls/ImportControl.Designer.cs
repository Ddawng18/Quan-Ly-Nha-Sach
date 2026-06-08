namespace BookStoreApp.UserControls;

partial class ImportControl
{
    private System.ComponentModel.IContainer components = null!;

    private Button       btnNewImport        = null!;
    private Button       btnRefresh          = null!;
    private Label        lblFilterSupplier   = null!;
    private ComboBox     cboFilterSupplier   = null!;
    private DataGridView dgvReceipts         = null!;
    private Label        lblDetailTitle      = null!;
    private DataGridView dgvDetails          = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.Dock = DockStyle.Fill;

        // ── Toolbar ──────────────────────────────────────────
        btnNewImport = new Button
        {
            Text      = "+ Lập đơn nhập hàng",
            Location  = new Point(12, 12),
            Width     = 160, Height = 30,
            BackColor = Color.FromArgb(0, 122, 204),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnNewImport.Click += btnNewImport_Click;

        btnRefresh = new Button
        {
            Text     = "↺ Làm mới",
            Location = new Point(182, 12),
            Width    = 90, Height = 30
        };
        btnRefresh.Click += btnRefresh_Click;

        lblFilterSupplier = new Label
        {
            Text     = "Lọc theo NCC:",
            Location = new Point(290, 18),
            AutoSize = true
        };

        cboFilterSupplier = new ComboBox
        {
            Location      = new Point(380, 14),
            Width         = 220,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        cboFilterSupplier.SelectedIndexChanged += cboFilterSupplier_SelectedIndexChanged;

        // ── Danh sách phiếu nhập ─────────────────────────────
        dgvReceipts = new DataGridView
        {
            Location              = new Point(12, 52),
            Size                  = new Size(960, 280),
            ReadOnly              = true,
            AllowUserToAddRows    = false,
            AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill,
            SelectionMode         = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect           = false,
            BackgroundColor       = SystemColors.Window,
            Anchor                = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };
        dgvReceipts.SelectionChanged += dgvReceipts_SelectionChanged;

        // ── Chi tiết phiếu ───────────────────────────────────
        lblDetailTitle = new Label
        {
            Text     = "Chi tiết phiếu nhập:",
            Location = new Point(12, 344),
            AutoSize = true,
            Font     = new Font(Font.FontFamily, 9, FontStyle.Bold)
        };

        dgvDetails = new DataGridView
        {
            Location            = new Point(12, 364),
            Size                = new Size(960, 200),
            ReadOnly            = true,
            AllowUserToAddRows  = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            BackgroundColor     = SystemColors.Window,
            Anchor              = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
        };

        Controls.AddRange([
            btnNewImport, btnRefresh,
            lblFilterSupplier, cboFilterSupplier,
            dgvReceipts,
            lblDetailTitle, dgvDetails
        ]);
    }
}
