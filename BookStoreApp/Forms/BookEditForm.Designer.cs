namespace BookStoreApp.Forms;

partial class BookEditForm
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
        lblTitle = new Label();
        txtTitle = new TextBox();
        lblAuthor = new Label();
        txtAuthor = new TextBox();
        lblISBN = new Label();
        txtISBN = new TextBox();
        lblPublisher = new Label();
        txtPublisher = new TextBox();
        lblPublishYear = new Label();
        numPublishYear = new NumericUpDown();
        lblImportPrice = new Label();
        numImportPrice = new NumericUpDown();
        lblSellPrice = new Label();
        numSellPrice = new NumericUpDown();
        lblCategory = new Label();
        cboCategory = new ComboBox();
        lblSupplier = new Label();
        cboSupplier = new ComboBox();
        lblQuantity = new Label();
        numQuantity = new NumericUpDown();
        btnSave = new Button();
        btnCancel = new Button();
        ((System.ComponentModel.ISupportInitialize)numPublishYear).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numImportPrice).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numSellPrice).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
        SuspendLayout();
        //
        // lblTitle
        //
        lblTitle.AutoSize = true;
        lblTitle.Location = new Point(24, 24);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(39, 20);
        lblTitle.Text = "Title";
        //
        // txtTitle
        //
        txtTitle.Location = new Point(24, 48);
        txtTitle.Name = "txtTitle";
        txtTitle.Size = new Size(400, 27);
        //
        // lblAuthor
        //
        lblAuthor.AutoSize = true;
        lblAuthor.Location = new Point(24, 88);
        lblAuthor.Name = "lblAuthor";
        lblAuthor.Size = new Size(52, 20);
        lblAuthor.Text = "Author";
        //
        // txtAuthor
        //
        txtAuthor.Location = new Point(24, 112);
        txtAuthor.Name = "txtAuthor";
        txtAuthor.Size = new Size(400, 27);
        //
        // lblISBN
        //
        lblISBN.AutoSize = true;
        lblISBN.Location = new Point(24, 152);
        lblISBN.Name = "lblISBN";
        lblISBN.Size = new Size(38, 20);
        lblISBN.Text = "ISBN";
        //
        // txtISBN
        //
        txtISBN.Location = new Point(24, 176);
        txtISBN.Name = "txtISBN";
        txtISBN.Size = new Size(400, 27);
        //
        // lblPublisher
        //
        lblPublisher.AutoSize = true;
        lblPublisher.Location = new Point(24, 216);
        lblPublisher.Name = "lblPublisher";
        lblPublisher.Size = new Size(68, 20);
        lblPublisher.Text = "Publisher";
        //
        // txtPublisher
        //
        txtPublisher.Location = new Point(24, 240);
        txtPublisher.Name = "txtPublisher";
        txtPublisher.Size = new Size(400, 27);
        //
        // lblPublishYear
        //
        lblPublishYear.AutoSize = true;
        lblPublishYear.Location = new Point(24, 340);
        lblPublishYear.Name = "lblPublishYear";
        lblPublishYear.Size = new Size(91, 20);
        lblPublishYear.Text = "Publish Year";
        //
        // numPublishYear
        //
        numPublishYear.Location = new Point(24, 364);
        numPublishYear.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
        numPublishYear.Minimum = new decimal(new int[] { 1900, 0, 0, 0 });
        numPublishYear.Name = "numPublishYear";
        numPublishYear.Size = new Size(120, 27);
        numPublishYear.Value = new decimal(new int[] { 2024, 0, 0, 0 });
        //
        // lblImportPrice
        //
        lblImportPrice.AutoSize = true;
        lblImportPrice.Location = new Point(168, 340);
        lblImportPrice.Name = "lblImportPrice";
        lblImportPrice.Size = new Size(89, 20);
        lblImportPrice.Text = "Import Price";
        //
        // numImportPrice
        //
        numImportPrice.DecimalPlaces = 2;
        numImportPrice.Location = new Point(168, 364);
        numImportPrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        numImportPrice.Name = "numImportPrice";
        numImportPrice.Size = new Size(120, 27);
        //
        // lblSellPrice
        //
        lblSellPrice.AutoSize = true;
        lblSellPrice.Location = new Point(304, 340);
        lblSellPrice.Name = "lblSellPrice";
        lblSellPrice.Size = new Size(70, 20);
        lblSellPrice.Text = "Sell Price";
        //
        // numSellPrice
        //
        numSellPrice.DecimalPlaces = 2;
        numSellPrice.Location = new Point(304, 364);
        numSellPrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        numSellPrice.Name = "numSellPrice";
        numSellPrice.Size = new Size(120, 27);
        //
        // lblCategory
        //
        lblCategory.AutoSize = true;
        lblCategory.Location = new Point(24, 276);
        lblCategory.Name = "lblCategory";
        lblCategory.Size = new Size(69, 20);
        lblCategory.Text = "Category";
        //
        // cboCategory
        //
        cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCategory.Location = new Point(24, 300);
        cboCategory.Name = "cboCategory";
        cboCategory.Size = new Size(190, 28);
        //
        // lblSupplier
        //
        lblSupplier.AutoSize = true;
        lblSupplier.Location = new Point(234, 276);
        lblSupplier.Name = "lblSupplier";
        lblSupplier.Size = new Size(64, 20);
        lblSupplier.Text = "Supplier";
        //
        // cboSupplier
        //
        cboSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
        cboSupplier.Location = new Point(234, 300);
        cboSupplier.Name = "cboSupplier";
        cboSupplier.Size = new Size(190, 28);
        //
        // lblQuantity
        //
        lblQuantity.AutoSize = true;
        lblQuantity.Location = new Point(24, 408);
        lblQuantity.Name = "lblQuantity";
        lblQuantity.Size = new Size(65, 20);
        lblQuantity.Text = "Quantity";
        //
        // numQuantity
        //
        numQuantity.Location = new Point(24, 432);
        numQuantity.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
        numQuantity.Name = "numQuantity";
        numQuantity.Size = new Size(120, 27);
        //
        // btnSave
        //
        btnSave.BackColor = Color.FromArgb(33, 150, 243);
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(24, 480);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(120, 40);
        btnSave.Text = "Save";
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += btnSave_Click;
        //
        // btnCancel
        //
        btnCancel.Location = new Point(160, 480);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(120, 40);
        btnCancel.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;
        //
        // BookEditForm
        //
        AcceptButton = btnSave;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(454, 540);
        Controls.Add(btnCancel);
        Controls.Add(btnSave);
        Controls.Add(numQuantity);
        Controls.Add(lblQuantity);
        Controls.Add(numSellPrice);
        Controls.Add(lblSellPrice);
        Controls.Add(numImportPrice);
        Controls.Add(lblImportPrice);
        Controls.Add(numPublishYear);
        Controls.Add(lblPublishYear);
        Controls.Add(cboSupplier);
        Controls.Add(lblSupplier);
        Controls.Add(cboCategory);
        Controls.Add(lblCategory);
        Controls.Add(txtPublisher);
        Controls.Add(lblPublisher);
        Controls.Add(txtISBN);
        Controls.Add(lblISBN);
        Controls.Add(txtAuthor);
        Controls.Add(lblAuthor);
        Controls.Add(txtTitle);
        Controls.Add(lblTitle);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "BookEditForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Book";
        ((System.ComponentModel.ISupportInitialize)numPublishYear).EndInit();
        ((System.ComponentModel.ISupportInitialize)numImportPrice).EndInit();
        ((System.ComponentModel.ISupportInitialize)numSellPrice).EndInit();
        ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblTitle;
    private TextBox txtTitle;
    private Label lblAuthor;
    private TextBox txtAuthor;
    private Label lblISBN;
    private TextBox txtISBN;
    private Label lblPublisher;
    private TextBox txtPublisher;
    private Label lblPublishYear;
    private NumericUpDown numPublishYear;
    private Label lblImportPrice;
    private NumericUpDown numImportPrice;
    private Label lblSellPrice;
    private NumericUpDown numSellPrice;
    private Label lblCategory;
    private ComboBox cboCategory;
    private Label lblSupplier;
    private ComboBox cboSupplier;
    private Label lblQuantity;
    private NumericUpDown numQuantity;
    private Button btnSave;
    private Button btnCancel;
}
