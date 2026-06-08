using BookStoreApp.BLL;
using BookStoreApp.DTO;

namespace BookStoreApp.Forms;

public partial class BookEditForm : Form
{
    private readonly Book? _existingBook;
    private readonly ErrorProvider _errorProvider = new();
    private readonly IBookService _bookService = ServiceLocator.BookService;
    private readonly ICategoryService _categoryService = ServiceLocator.CategoryService;

    public Book Book { get; private set; } = new();

    public BookEditForm()
    {
        InitializeComponent();
        LoadLookups();
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
        numSellPrice.Value = ClampNumeric(book.SellPrice, numSellPrice.Minimum, numSellPrice.Maximum);

        if (book.CategoryID > 0)
        {
            cboCategory.SelectedValue = book.CategoryID;
        }
    }

    private void LoadLookups()
    {
        cboCategory.DisplayMember = nameof(Category.CategoryName);
        cboCategory.ValueMember = nameof(Category.CategoryID);
        cboCategory.DataSource = _categoryService.GetCategories().ToList();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        _errorProvider.Clear();

        if (!ValidateFields(out var publishYear))
        {
            return;
        }

        var categoryId = cboCategory.SelectedValue is int catId ? catId : 0;

        Book = new Book
        {
            BookID      = _existingBook?.BookID ?? 0,
            CategoryID  = categoryId,
            Title       = txtTitle.Text.Trim(),
            Author      = txtAuthor.Text.Trim(),
            ISBN        = txtISBN.Text.Trim(),
            Publisher   = txtPublisher.Text.Trim(),
            PublishYear = publishYear,
            SellPrice   = numSellPrice.Value,
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
