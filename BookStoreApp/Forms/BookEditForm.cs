using BookStoreApp.DTO;

namespace BookStoreApp.Forms;

public partial class BookEditForm : Form
{
    private readonly Book? _existingBook;
    private readonly ErrorProvider _errorProvider = new();

    public Book Book { get; private set; } = new();

    public BookEditForm()
    {
        InitializeComponent();
        Text = "Add Book";
    }

    public BookEditForm(Book book)
        : this()
    {
        _existingBook = book;
        Text = "Edit Book";
        txtTitle.Text = book.Title;
        txtAuthor.Text = book.Author;
        txtISBN.Text = book.ISBN;
        txtPublisher.Text = book.Publisher;
        numPublishYear.Value = ClampNumeric(book.PublishYear, numPublishYear.Minimum, numPublishYear.Maximum);
        numImportPrice.Value = ClampNumeric(book.ImportPrice, numImportPrice.Minimum, numImportPrice.Maximum);
        numSellPrice.Value = ClampNumeric(book.SellPrice, numSellPrice.Minimum, numSellPrice.Maximum);
        numQuantity.Value = ClampNumeric(book.QuantityInStock, numQuantity.Minimum, numQuantity.Maximum);
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        _errorProvider.Clear();

        if (!ValidateFields(out var publishYear))
        {
            return;
        }

        Book = new Book
        {
            BookID = _existingBook?.BookID ?? 0,
            CategoryID = _existingBook?.CategoryID ?? 0,
            SupplierID = _existingBook?.SupplierID ?? 0,
            Title = txtTitle.Text.Trim(),
            Author = txtAuthor.Text.Trim(),
            ISBN = txtISBN.Text.Trim(),
            Publisher = txtPublisher.Text.Trim(),
            PublishYear = publishYear,
            ImportPrice = numImportPrice.Value,
            SellPrice = numSellPrice.Value,
            QuantityInStock = (int)numQuantity.Value,
            LastImportDate = _existingBook?.LastImportDate,
            LastSoldDate = _existingBook?.LastSoldDate,
            IsDeleted = _existingBook?.IsDeleted ?? false
        };

        DialogResult = DialogResult.OK;
        Close();
    }

    private bool ValidateFields(out int publishYear)
    {
        publishYear = 0;
        var valid = true;

        if (string.IsNullOrWhiteSpace(txtTitle.Text))
        {
            _errorProvider.SetError(txtTitle, "Title is required.");
            valid = false;
        }

        if (string.IsNullOrWhiteSpace(txtAuthor.Text))
        {
            _errorProvider.SetError(txtAuthor, "Author is required.");
            valid = false;
        }

        if (string.IsNullOrWhiteSpace(txtISBN.Text))
        {
            _errorProvider.SetError(txtISBN, "ISBN is required.");
            valid = false;
        }

        if (!int.TryParse(numPublishYear.Text, out publishYear))
        {
            _errorProvider.SetError(numPublishYear, "Publish year is invalid.");
            valid = false;
        }

        return valid;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private static decimal ClampNumeric(decimal value, decimal min, decimal max) =>
        value < min ? min : value > max ? max : value;
}
