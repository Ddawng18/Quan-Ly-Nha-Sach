namespace BookStoreApp.Forms;

partial class OrderCreateForm
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
        lblCustomer = new Label();
        cboCustomer = new ComboBox();
        lblEmployee = new Label();
        cboEmployee = new ComboBox();
        lblPayment = new Label();
        cboPaymentStatus = new ComboBox();
        lblBook = new Label();
        cboBook = new ComboBox();
        lblQuantity = new Label();
        numQuantity = new NumericUpDown();
        btnAddLine = new Button();
        btnRemoveLine = new Button();
        dgvLines = new DataGridView();
        lblTotal = new Label();
        btnSave = new Button();
        btnCancel = new Button();
        ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvLines).BeginInit();
        SuspendLayout();
        lblCustomer.AutoSize = true;
        lblCustomer.Location = new Point(16, 16);
        lblCustomer.Text = "Customer";
        cboCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCustomer.Location = new Point(16, 40);
        cboCustomer.Size = new Size(220, 28);
        lblEmployee.AutoSize = true;
        lblEmployee.Location = new Point(252, 16);
        lblEmployee.Text = "Employee";
        cboEmployee.DropDownStyle = ComboBoxStyle.DropDownList;
        cboEmployee.Location = new Point(252, 40);
        cboEmployee.Size = new Size(220, 28);
        lblPayment.AutoSize = true;
        lblPayment.Location = new Point(488, 16);
        lblPayment.Text = "Payment";
        cboPaymentStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cboPaymentStatus.Location = new Point(488, 40);
        cboPaymentStatus.Size = new Size(120, 28);
        lblBook.AutoSize = true;
        lblBook.Location = new Point(16, 80);
        lblBook.Text = "Book";
        cboBook.DropDownStyle = ComboBoxStyle.DropDownList;
        cboBook.Location = new Point(16, 104);
        cboBook.Size = new Size(360, 28);
        lblQuantity.AutoSize = true;
        lblQuantity.Location = new Point(388, 80);
        lblQuantity.Text = "Qty";
        numQuantity.Location = new Point(388, 104);
        numQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numQuantity.Size = new Size(80, 27);
        numQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
        btnAddLine.Location = new Point(480, 100);
        btnAddLine.Size = new Size(64, 32);
        btnAddLine.Text = "Add";
        btnAddLine.Click += btnAddLine_Click;
        btnRemoveLine.Location = new Point(552, 100);
        btnRemoveLine.Size = new Size(72, 32);
        btnRemoveLine.Text = "Remove";
        btnRemoveLine.Click += btnRemoveLine_Click;
        dgvLines.AllowUserToAddRows = false;
        dgvLines.AllowUserToDeleteRows = false;
        dgvLines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvLines.Location = new Point(16, 148);
        dgvLines.ReadOnly = true;
        dgvLines.RowHeadersVisible = false;
        dgvLines.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvLines.Size = new Size(608, 200);
        lblTotal.Location = new Point(16, 356);
        lblTotal.Size = new Size(300, 24);
        lblTotal.Text = "Total: 0";
        btnSave.BackColor = Color.FromArgb(33, 150, 243);
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(16, 388);
        btnSave.Size = new Size(120, 40);
        btnSave.Text = "Create Order";
        btnSave.Click += btnSave_Click;
        btnCancel.Location = new Point(148, 388);
        btnCancel.Size = new Size(120, 40);
        btnCancel.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;
        ClientSize = new Size(644, 448);
        Controls.Add(btnCancel);
        Controls.Add(btnSave);
        Controls.Add(lblTotal);
        Controls.Add(dgvLines);
        Controls.Add(btnRemoveLine);
        Controls.Add(btnAddLine);
        Controls.Add(numQuantity);
        Controls.Add(lblQuantity);
        Controls.Add(cboBook);
        Controls.Add(lblBook);
        Controls.Add(cboPaymentStatus);
        Controls.Add(lblPayment);
        Controls.Add(cboEmployee);
        Controls.Add(lblEmployee);
        Controls.Add(cboCustomer);
        Controls.Add(lblCustomer);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Create Order";
        ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvLines).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblCustomer;
    private ComboBox cboCustomer;
    private Label lblEmployee;
    private ComboBox cboEmployee;
    private Label lblPayment;
    private ComboBox cboPaymentStatus;
    private Label lblBook;
    private ComboBox cboBook;
    private Label lblQuantity;
    private NumericUpDown numQuantity;
    private Button btnAddLine;
    private Button btnRemoveLine;
    private DataGridView dgvLines;
    private Label lblTotal;
    private Button btnSave;
    private Button btnCancel;
}
