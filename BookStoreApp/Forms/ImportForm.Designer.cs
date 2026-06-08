using BookStoreApp.Theme;

namespace BookStoreApp.Forms;

partial class ImportForm
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

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        lblSupplier = new Label();
        cboSupplier = new ComboBox();
        lblEmployee = new Label();
        cboEmployee = new ComboBox();
        lblBook = new Label();
        cboBook = new ComboBox();
        lblQty = new Label();
        numQuantity = new NumericUpDown();
        lblPrice = new Label();
        numImportPrice = new NumericUpDown();
        btnAddLine = new Button();
        btnRemoveLine = new Button();
        dgvLines = new DataGridView();
        lblTotal = new Label();
        lblNote = new Label();
        txtNote = new TextBox();
        btnSave = new Button();
        btnCancel = new Button();
        ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numImportPrice).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvLines).BeginInit();
        SuspendLayout();
        // 
        // lblSupplier
        // 
        lblSupplier.AutoSize = true;
        lblSupplier.Location = new Point(12, 16);
        lblSupplier.Name = "lblSupplier";
        lblSupplier.Size = new Size(67, 20);
        lblSupplier.TabIndex = 0;
        lblSupplier.Text = "Supplier:";
        // 
        // cboSupplier
        // 
        cboSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
        cboSupplier.Location = new Point(120, 13);
        cboSupplier.Name = "cboSupplier";
        cboSupplier.Size = new Size(200, 28);
        cboSupplier.TabIndex = 1;
        // 
        // lblEmployee
        // 
        lblEmployee.AutoSize = true;
        lblEmployee.Location = new Point(12, 48);
        lblEmployee.Name = "lblEmployee";
        lblEmployee.Size = new Size(78, 20);
        lblEmployee.TabIndex = 2;
        lblEmployee.Text = "Employee:";
        // 
        // cboEmployee
        // 
        cboEmployee.DropDownStyle = ComboBoxStyle.DropDownList;
        cboEmployee.Location = new Point(120, 45);
        cboEmployee.Name = "cboEmployee";
        cboEmployee.Size = new Size(200, 28);
        cboEmployee.TabIndex = 3;
        // 
        // lblBook
        // 
        lblBook.AutoSize = true;
        lblBook.Location = new Point(12, 80);
        lblBook.Name = "lblBook";
        lblBook.Size = new Size(46, 20);
        lblBook.TabIndex = 4;
        lblBook.Text = "Book:";
        // 
        // cboBook
        // 
        cboBook.DropDownStyle = ComboBoxStyle.DropDownList;
        cboBook.Location = new Point(120, 77);
        cboBook.Name = "cboBook";
        cboBook.Size = new Size(350, 28);
        cboBook.TabIndex = 5;
        // 
        // lblQty
        // 
        lblQty.AutoSize = true;
        lblQty.Location = new Point(12, 112);
        lblQty.Name = "lblQty";
        lblQty.Size = new Size(68, 20);
        lblQty.TabIndex = 6;
        lblQty.Text = "Quantity:";
        // 
        // numQuantity
        // 
        numQuantity.Location = new Point(120, 110);
        numQuantity.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        numQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        numQuantity.Name = "numQuantity";
        numQuantity.Size = new Size(80, 27);
        numQuantity.TabIndex = 7;
        numQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // lblPrice
        // 
        lblPrice.AutoSize = true;
        lblPrice.Location = new Point(12, 144);
        lblPrice.Name = "lblPrice";
        lblPrice.Size = new Size(138, 20);
        lblPrice.TabIndex = 8;
        lblPrice.Text = "Import Price (VND):";
        // 
        // numImportPrice
        // 
        numImportPrice.Location = new Point(156, 142);
        numImportPrice.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
        numImportPrice.Name = "numImportPrice";
        numImportPrice.Size = new Size(120, 27);
        numImportPrice.TabIndex = 9;
        numImportPrice.ThousandsSeparator = true;
        numImportPrice.ValueChanged += numImportPrice_ValueChanged;
        // 
        // btnAddLine
        //
        btnAddLine.Location = new Point(286, 142);
        btnAddLine.Name = "btnAddLine";
        btnAddLine.Size = new Size(130, 26);
        btnAddLine.TabIndex = 10;
        btnAddLine.Text = "+ Add to Receipt";
        btnAddLine.Click += btnAddLine_Click;
        AppTheme.StyleActionButton(btnAddLine, AppTheme.Add);
        //
        // btnRemoveLine
        //
        btnRemoveLine.Location = new Point(426, 142);
        btnRemoveLine.Name = "btnRemoveLine";
        btnRemoveLine.Size = new Size(90, 26);
        btnRemoveLine.TabIndex = 11;
        btnRemoveLine.Text = "Remove Line";
        btnRemoveLine.Click += btnRemoveLine_Click;
        AppTheme.StyleActionButton(btnRemoveLine, AppTheme.Delete);
        // 
        // dgvLines
        // 
        dgvLines.AllowUserToAddRows = false;
        dgvLines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvLines.ColumnHeadersHeight = 29;
        dgvLines.Location = new Point(12, 182);
        dgvLines.Name = "dgvLines";
        dgvLines.ReadOnly = true;
        dgvLines.RowHeadersWidth = 51;
        dgvLines.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvLines.Size = new Size(780, 250);
        dgvLines.TabIndex = 12;
        // 
        // lblTotal
        // 
        lblTotal.AutoSize = true;
        lblTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTotal.Location = new Point(12, 442);
        lblTotal.Name = "lblTotal";
        lblTotal.Size = new Size(111, 23);
        lblTotal.TabIndex = 13;
        lblTotal.Text = "Total: 0 VND";
        // 
        // lblNote
        // 
        lblNote.AutoSize = true;
        lblNote.Location = new Point(12, 470);
        lblNote.Name = "lblNote";
        lblNote.Size = new Size(45, 20);
        lblNote.TabIndex = 14;
        lblNote.Text = "Note:";
        // 
        // txtNote
        // 
        txtNote.Location = new Point(120, 467);
        txtNote.Name = "txtNote";
        txtNote.Size = new Size(400, 27);
        txtNote.TabIndex = 15;
        // 
        // btnSave
        //
        btnSave.Location = new Point(120, 508);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(120, 30);
        btnSave.TabIndex = 16;
        btnSave.Text = "Save Import Receipt";
        btnSave.Click += btnSave_Click;
        AppTheme.StyleActionButton(btnSave, AppTheme.Add);
        //
        // btnCancel
        //
        btnCancel.Location = new Point(250, 508);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(80, 30);
        btnCancel.TabIndex = 17;
        btnCancel.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;
        AppTheme.StyleRefreshButton(btnCancel);
        // 
        // ImportForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = AppTheme.MainBackground;
        ClientSize = new Size(820, 558);
        Controls.Add(lblSupplier);
        Controls.Add(cboSupplier);
        Controls.Add(lblEmployee);
        Controls.Add(cboEmployee);
        Controls.Add(lblBook);
        Controls.Add(cboBook);
        Controls.Add(lblQty);
        Controls.Add(numQuantity);
        Controls.Add(lblPrice);
        Controls.Add(numImportPrice);
        Controls.Add(btnAddLine);
        Controls.Add(btnRemoveLine);
        Controls.Add(dgvLines);
        Controls.Add(lblTotal);
        Controls.Add(lblNote);
        Controls.Add(txtNote);
        Controls.Add(btnSave);
        Controls.Add(btnCancel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        Name = "ImportForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Import Books";
        ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
        ((System.ComponentModel.ISupportInitialize)numImportPrice).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvLines).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblSupplier;
    private ComboBox cboSupplier;
    private Label lblEmployee;
    private ComboBox cboEmployee;
    private Label lblBook;
    private ComboBox cboBook;
    private Label lblQty;
    private NumericUpDown numQuantity;
    private Label lblPrice;
    private NumericUpDown numImportPrice;
    private Button btnAddLine;
    private Button btnRemoveLine;
    private DataGridView dgvLines;
    private Label lblTotal;
    private Label lblNote;
    private TextBox txtNote;
    private Button btnSave;
    private Button btnCancel;
}