namespace BookStoreApp.UserControls;

partial class BookControl
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
        btnAdd = new Button();
        btnEdit = new Button();
        btnDelete = new Button();
        btnRefresh = new Button();
        txtSearch = new TextBox();
        btnSearch = new Button();
        dgvBooks = new DataGridView();
        panelToolbar.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
        SuspendLayout();
        //
        // panelToolbar
        //
        panelToolbar.Controls.Add(btnSearch);
        panelToolbar.Controls.Add(txtSearch);
        panelToolbar.Controls.Add(btnRefresh);
        panelToolbar.Controls.Add(btnDelete);
        panelToolbar.Controls.Add(btnEdit);
        panelToolbar.Controls.Add(btnAdd);
        panelToolbar.Dock = DockStyle.Top;
        panelToolbar.Location = new Point(8, 8);
        panelToolbar.Name = "panelToolbar";
        panelToolbar.Padding = new Padding(0, 0, 0, 8);
        panelToolbar.Size = new Size(784, 48);
        panelToolbar.TabIndex = 0;
        //
        // btnAdd
        //
        btnAdd.Location = new Point(0, 4);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(100, 36);
        btnAdd.TabIndex = 0;
        btnAdd.Text = "Add";
        btnAdd.Click += btnAdd_Click;
        //
        // btnEdit
        //
        btnEdit.Location = new Point(108, 4);
        btnEdit.Name = "btnEdit";
        btnEdit.Size = new Size(100, 36);
        btnEdit.TabIndex = 1;
        btnEdit.Text = "Edit";
        btnEdit.Click += btnEdit_Click;
        //
        // btnDelete
        //
        btnDelete.Location = new Point(216, 4);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(100, 36);
        btnDelete.TabIndex = 2;
        btnDelete.Text = "Delete";
        btnDelete.Click += btnDelete_Click;
        //
        // btnRefresh
        //
        btnRefresh.Location = new Point(324, 4);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(100, 36);
        btnRefresh.TabIndex = 3;
        btnRefresh.Text = "Refresh";
        btnRefresh.UseVisualStyleBackColor = true;
        btnRefresh.Click += btnRefresh_Click;
        //
        // txtSearch
        //
        txtSearch.Location = new Point(440, 8);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Search by title...";
        txtSearch.Size = new Size(240, 27);
        txtSearch.TabIndex = 4;
        txtSearch.KeyDown += txtSearch_KeyDown;
        //
        // btnSearch
        //
        btnSearch.Location = new Point(688, 4);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(88, 36);
        btnSearch.TabIndex = 5;
        btnSearch.Text = "Search";
        btnSearch.UseVisualStyleBackColor = true;
        btnSearch.Click += btnSearch_Click;
        //
        // dgvBooks
        //
        dgvBooks.AllowUserToAddRows = false;
        dgvBooks.AllowUserToDeleteRows = false;
        dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        dgvBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvBooks.Dock = DockStyle.Fill;
        dgvBooks.ScrollBars = ScrollBars.Both;
        dgvBooks.Name = "dgvBooks";
        dgvBooks.ReadOnly = true;
        dgvBooks.RowHeadersVisible = false;
        dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvBooks.TabIndex = 1;
        //
        // BookControl
        //
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(dgvBooks);
        Controls.Add(panelToolbar);
        Name = "BookControl";
        Padding = new Padding(8);
        Size = new Size(800, 500);
        panelToolbar.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();
        ResumeLayout(false);
    }

    private Panel panelToolbar;
    private Button btnAdd;
    private Button btnEdit;
    private Button btnDelete;
    private Button btnRefresh;
    private TextBox txtSearch;
    private Button btnSearch;
    private DataGridView dgvBooks;
}
