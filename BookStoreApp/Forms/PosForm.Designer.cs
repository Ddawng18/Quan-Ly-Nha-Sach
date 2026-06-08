namespace BookStoreApp.Forms;

partial class PosForm
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
        lblStatus = new Label();
        cboPaymentStatus = new ComboBox();
        lblBook = new Label();
        cboBook = new ComboBox();
        lblQuantity = new Label();
        numQuantity = new NumericUpDown();
        lblMethod = new Label();
        cboPaymentMethod = new ComboBox();
        btnAddLine = new Button();
        btnRemoveLine = new Button();
        lblLineDiscount = new Label();
        cboLineDiscountType = new ComboBox();
        numLineDiscount = new NumericUpDown();
        lblOrderDiscount = new Label();
        cboOrderDiscountType = new ComboBox();
        numOrderDiscount = new NumericUpDown();
        lblTax = new Label();
        numTaxRate = new NumericUpDown();
        lblRedeemPoints = new Label();
        numLoyaltyPoints = new NumericUpDown();
        dgvLines = new DataGridView();
        lblTotal = new Label();
        btnSave = new Button();
        btnCancel = new Button();
        btnPayWithQr = new Button();
        ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numLineDiscount).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numOrderDiscount).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numTaxRate).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numLoyaltyPoints).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvLines).BeginInit();
        SuspendLayout();
        // 
        // lblCustomer
        // 
        lblCustomer.AutoSize = true;
        lblCustomer.Location = new Point(16, 16);
        lblCustomer.Name = "lblCustomer";
        lblCustomer.Size = new Size(72, 20);
        lblCustomer.TabIndex = 28;
        lblCustomer.Text = "Customer";
        // 
        // cboCustomer
        // 
        cboCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCustomer.Location = new Point(16, 40);
        cboCustomer.Name = "cboCustomer";
        cboCustomer.Size = new Size(220, 28);
        cboCustomer.TabIndex = 27;
        // 
        // lblEmployee
        // 
        lblEmployee.AutoSize = true;
        lblEmployee.Location = new Point(252, 16);
        lblEmployee.Name = "lblEmployee";
        lblEmployee.Size = new Size(75, 20);
        lblEmployee.TabIndex = 26;
        lblEmployee.Text = "Employee";
        // 
        // cboEmployee
        // 
        cboEmployee.DropDownStyle = ComboBoxStyle.DropDownList;
        cboEmployee.Location = new Point(252, 40);
        cboEmployee.Name = "cboEmployee";
        cboEmployee.Size = new Size(220, 28);
        cboEmployee.TabIndex = 25;
        // 
        // lblStatus
        // 
        lblStatus.AutoSize = true;
        lblStatus.Location = new Point(488, 16);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(49, 20);
        lblStatus.TabIndex = 24;
        lblStatus.Text = "Status";
        // 
        // cboPaymentStatus
        // 
        cboPaymentStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cboPaymentStatus.Location = new Point(488, 40);
        cboPaymentStatus.Name = "cboPaymentStatus";
        cboPaymentStatus.Size = new Size(120, 28);
        cboPaymentStatus.TabIndex = 23;
        // 
        // lblBook
        // 
        lblBook.AutoSize = true;
        lblBook.Location = new Point(16, 80);
        lblBook.Name = "lblBook";
        lblBook.Size = new Size(43, 20);
        lblBook.TabIndex = 22;
        lblBook.Text = "Book";
        // 
        // cboBook
        // 
        cboBook.DropDownStyle = ComboBoxStyle.DropDownList;
        cboBook.Location = new Point(16, 104);
        cboBook.Name = "cboBook";
        cboBook.Size = new Size(360, 28);
        cboBook.TabIndex = 21;
        // 
        // lblQuantity
        // 
        lblQuantity.AutoSize = true;
        lblQuantity.Location = new Point(388, 80);
        lblQuantity.Name = "lblQuantity";
        lblQuantity.Size = new Size(32, 20);
        lblQuantity.TabIndex = 20;
        lblQuantity.Text = "Qty";
        // 
        // numQuantity
        // 
        numQuantity.Location = new Point(388, 104);
        numQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numQuantity.Name = "numQuantity";
        numQuantity.Size = new Size(80, 27);
        numQuantity.TabIndex = 19;
        numQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // lblMethod
        // 
        lblMethod.AutoSize = true;
        lblMethod.Location = new Point(488, 204);
        lblMethod.Name = "lblMethod";
        lblMethod.Size = new Size(55, 20);
        lblMethod.TabIndex = 18;
        lblMethod.Text = "Method";
        // 
        // cboPaymentMethod
        // 
        cboPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;
        cboPaymentMethod.Items.Add("Cash");
        cboPaymentMethod.Items.Add("QR Payment");
        cboPaymentMethod.Location = new Point(488, 228);
        cboPaymentMethod.Name = "cboPaymentMethod";
        cboPaymentMethod.Size = new Size(136, 28);
        cboPaymentMethod.TabIndex = 17;
        // 
        // btnAddLine
        // 
        btnAddLine.Location = new Point(480, 100);
        btnAddLine.Name = "btnAddLine";
        btnAddLine.Size = new Size(64, 32);
        btnAddLine.TabIndex = 16;
        btnAddLine.Text = "Add";
        btnAddLine.Click += btnAddLine_Click;
        // 
        // btnRemoveLine
        // 
        btnRemoveLine.Location = new Point(552, 100);
        btnRemoveLine.Name = "btnRemoveLine";
        btnRemoveLine.Size = new Size(72, 32);
        btnRemoveLine.TabIndex = 15;
        btnRemoveLine.Text = "Remove";
        btnRemoveLine.Click += btnRemoveLine_Click;
        // 
        // lblLineDiscount
        // 
        lblLineDiscount.AutoSize = true;
        lblLineDiscount.Location = new Point(16, 140);
        lblLineDiscount.Name = "lblLineDiscount";
        lblLineDiscount.Size = new Size(98, 20);
        lblLineDiscount.TabIndex = 14;
        lblLineDiscount.Text = "Line Discount";
        // 
        // cboLineDiscountType
        // 
        cboLineDiscountType.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLineDiscountType.Location = new Point(16, 164);
        cboLineDiscountType.Name = "cboLineDiscountType";
        cboLineDiscountType.Size = new Size(132, 28);
        cboLineDiscountType.TabIndex = 13;
        // 
        // numLineDiscount
        // 
        numLineDiscount.DecimalPlaces = 2;
        numLineDiscount.Location = new Point(160, 164);
        numLineDiscount.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
        numLineDiscount.Name = "numLineDiscount";
        numLineDiscount.Size = new Size(100, 27);
        numLineDiscount.TabIndex = 12;
        numLineDiscount.ThousandsSeparator = true;
        // 
        // lblOrderDiscount
        // 
        lblOrderDiscount.AutoSize = true;
        lblOrderDiscount.Location = new Point(276, 140);
        lblOrderDiscount.Name = "lblOrderDiscount";
        lblOrderDiscount.Size = new Size(109, 20);
        lblOrderDiscount.TabIndex = 11;
        lblOrderDiscount.Text = "Order Discount";
        // 
        // cboOrderDiscountType
        // 
        cboOrderDiscountType.DropDownStyle = ComboBoxStyle.DropDownList;
        cboOrderDiscountType.Location = new Point(276, 164);
        cboOrderDiscountType.Name = "cboOrderDiscountType";
        cboOrderDiscountType.Size = new Size(132, 28);
        cboOrderDiscountType.TabIndex = 10;
        // 
        // numOrderDiscount
        // 
        numOrderDiscount.DecimalPlaces = 2;
        numOrderDiscount.Location = new Point(420, 164);
        numOrderDiscount.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
        numOrderDiscount.Name = "numOrderDiscount";
        numOrderDiscount.Size = new Size(100, 27);
        numOrderDiscount.TabIndex = 9;
        numOrderDiscount.ThousandsSeparator = true;
        // 
        // lblTax
        // 
        lblTax.AutoSize = true;
        lblTax.Location = new Point(536, 140);
        lblTax.Name = "lblTax";
        lblTax.Size = new Size(46, 20);
        lblTax.TabIndex = 8;
        lblTax.Text = "Tax %";
        // 
        // numTaxRate
        // 
        numTaxRate.DecimalPlaces = 2;
        numTaxRate.Location = new Point(536, 164);
        numTaxRate.Name = "numTaxRate";
        numTaxRate.Size = new Size(72, 27);
        numTaxRate.TabIndex = 7;
        // 
        // lblRedeemPoints
        // 
        lblRedeemPoints.AutoSize = true;
        lblRedeemPoints.Location = new Point(16, 204);
        lblRedeemPoints.Name = "lblRedeemPoints";
        lblRedeemPoints.Size = new Size(107, 20);
        lblRedeemPoints.TabIndex = 6;
        lblRedeemPoints.Text = "Redeem Points";
        // 
        // numLoyaltyPoints
        // 
        numLoyaltyPoints.Location = new Point(16, 228);
        numLoyaltyPoints.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
        numLoyaltyPoints.Name = "numLoyaltyPoints";
        numLoyaltyPoints.Size = new Size(120, 27);
        numLoyaltyPoints.TabIndex = 5;
        numLoyaltyPoints.ThousandsSeparator = true;
        // 
        // dgvLines
        // 
        dgvLines.AllowUserToAddRows = false;
        dgvLines.AllowUserToDeleteRows = false;
        dgvLines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvLines.ColumnHeadersHeight = 29;
        dgvLines.Location = new Point(16, 272);
        dgvLines.Name = "dgvLines";
        dgvLines.ReadOnly = true;
        dgvLines.RowHeadersVisible = false;
        dgvLines.RowHeadersWidth = 51;
        dgvLines.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvLines.Size = new Size(608, 220);
        dgvLines.TabIndex = 4;
        // 
        // lblTotal
        // 
        lblTotal.BackColor = Color.Transparent;
        lblTotal.Location = new Point(16, 500);
        lblTotal.Name = "lblTotal";
        lblTotal.Size = new Size(608, 72);
        lblTotal.TabIndex = 3;
        lblTotal.Text = "Total: 0";
        // 
        // btnSave
        // 
        btnSave.BackColor = Color.FromArgb(33, 150, 243);
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(16, 584);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(120, 40);
        btnSave.TabIndex = 2;
        btnSave.Text = "Create Order";
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += btnSave_Click;
        // 
        // btnCancel
        // 
        btnCancel.Location = new Point(148, 584);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(120, 40);
        btnCancel.TabIndex = 1;
        btnCancel.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;
        // 
        // btnPayWithQr
        // 
        btnPayWithQr.BackColor = Color.White;
        btnPayWithQr.FlatStyle = FlatStyle.Flat;
        btnPayWithQr.Location = new Point(280, 584);
        btnPayWithQr.Name = "btnPayWithQr";
        btnPayWithQr.Size = new Size(130, 40);
        btnPayWithQr.TabIndex = 0;
        btnPayWithQr.Text = "Pay with QR";
        btnPayWithQr.UseVisualStyleBackColor = true;
        btnPayWithQr.Click += btnPayWithQr_Click;
        // 
        // PosForm
        // 
        ClientSize = new Size(644, 640);
        Controls.Add(btnPayWithQr);
        Controls.Add(btnCancel);
        Controls.Add(btnSave);
        Controls.Add(lblTotal);
        Controls.Add(dgvLines);
        Controls.Add(numLoyaltyPoints);
        Controls.Add(lblRedeemPoints);
        Controls.Add(numTaxRate);
        Controls.Add(lblTax);
        Controls.Add(numOrderDiscount);
        Controls.Add(cboOrderDiscountType);
        Controls.Add(lblOrderDiscount);
        Controls.Add(numLineDiscount);
        Controls.Add(cboLineDiscountType);
        Controls.Add(lblLineDiscount);
        Controls.Add(btnRemoveLine);
        Controls.Add(btnAddLine);
        Controls.Add(cboPaymentMethod);
        Controls.Add(lblMethod);
        Controls.Add(numQuantity);
        Controls.Add(lblQuantity);
        Controls.Add(cboBook);
        Controls.Add(lblBook);
        Controls.Add(cboPaymentStatus);
        Controls.Add(lblStatus);
        Controls.Add(cboEmployee);
        Controls.Add(lblEmployee);
        Controls.Add(cboCustomer);
        Controls.Add(lblCustomer);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "PosForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Point of Sale";
        ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
        ((System.ComponentModel.ISupportInitialize)numLineDiscount).EndInit();
        ((System.ComponentModel.ISupportInitialize)numOrderDiscount).EndInit();
        ((System.ComponentModel.ISupportInitialize)numTaxRate).EndInit();
        ((System.ComponentModel.ISupportInitialize)numLoyaltyPoints).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvLines).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblCustomer;
    private ComboBox cboCustomer;
    private Label lblEmployee;
    private ComboBox cboEmployee;
    private Label lblStatus;
    private ComboBox cboPaymentStatus;
    private Label lblBook;
    private ComboBox cboBook;
    private Label lblQuantity;
    private NumericUpDown numQuantity;
    private Label lblMethod;
    private ComboBox cboPaymentMethod;
    private Button btnAddLine;
    private Button btnRemoveLine;
    private Label lblLineDiscount;
    private ComboBox cboLineDiscountType;
    private NumericUpDown numLineDiscount;
    private Label lblOrderDiscount;
    private ComboBox cboOrderDiscountType;
    private NumericUpDown numOrderDiscount;
    private Label lblTax;
    private NumericUpDown numTaxRate;
    private Label lblRedeemPoints;
    private NumericUpDown numLoyaltyPoints;
    private DataGridView dgvLines;
    private Label lblTotal;
    private Button btnSave;
    private Button btnCancel;
    private Button btnPayWithQr;
}
