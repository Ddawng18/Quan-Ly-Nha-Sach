using BookStoreApp.BLL;
using BookStoreApp.DTO;
using BookStoreApp.Forms;
using BookStoreApp.Theme;

namespace BookStoreApp.UserControls;

public partial class BookControl : UserControl
{
    private readonly IBookService _bookService = ServiceLocator.BookService;
    private readonly ICategoryService _categoryService = ServiceLocator.CategoryService;

    private readonly bool _canAdd;
    private readonly bool _canEdit;
    private readonly bool _canDelete;

    // Constructor mặc định: full quyền (Admin)
    public BookControl()
        : this(canAdd: true, canEdit: true, canDelete: true)
    {
    }

    // Constructor cũ giữ lại để không break code khác
    // readOnly = true => tất cả đều false
    public BookControl(bool readOnly)
        : this(canAdd: !readOnly, canEdit: !readOnly, canDelete: !readOnly)
    {
    }

    // Constructor mới: kiểm soát từng quyền riêng
    public BookControl(bool canAdd, bool canEdit, bool canDelete)
    {
        _canAdd    = canAdd;
        _canEdit   = canEdit;
        _canDelete = canDelete;
        InitializeComponent();
        LoadFilters();
        ApplyTheme();
        LoadBooks();
    }

    private void LoadFilters()
    {
        txtSearch.PlaceholderText = "Search title, author, ISBN...";

        cboCategoryFilter.DisplayMember = "Text";
        cboCategoryFilter.ValueMember = "Value";
        var categories = new List<object> { new { Text = "All categories", Value = (int?)null } };
        categories.AddRange(_categoryService.GetCategories().Select(c => new { Text = c.CategoryName, Value = (int?)c.CategoryID }));
        cboCategoryFilter.DataSource = categories;
        cboCategoryFilter.SelectedIndexChanged += (_, _) => LoadBooks();

        cboPublisherFilter.Items.Add("All publishers");
        cboPublisherFilter.Items.AddRange(_bookService.GetPublishers().Cast<object>().ToArray());
        cboPublisherFilter.SelectedIndex = 0;
        cboPublisherFilter.SelectedIndexChanged += (_, _) => LoadBooks();

        cboStockFilter.Items.AddRange(Enum.GetNames<StockLevelFilter>().Cast<object>().ToArray());
        cboStockFilter.SelectedItem = StockLevelFilter.All.ToString();
        cboStockFilter.SelectedIndexChanged += (_, _) => LoadBooks();

        btnAdd.Enabled    = _canAdd;
        btnEdit.Enabled   = _canEdit;
        btnDelete.Enabled = _canDelete;
    }

    private void ApplyTheme()
    {
        BackColor = AppTheme.MainBackground;
        panelToolbar.BackColor = AppTheme.MainBackground;
        AppTheme.StyleActionButton(btnAdd, AppTheme.Add);
        AppTheme.StyleActionButton(btnEdit, AppTheme.Edit);
        AppTheme.StyleActionButton(btnDelete, AppTheme.Delete);
        AppTheme.StyleRefreshButton(btnRefresh);
        AppTheme.StyleActionButton(btnSearch, AppTheme.Add);
        AppTheme.ApplyGridStyle(dgvBooks);
    }

    private void LoadBooks()
    {
        dgvBooks.DataSource = null;
        dgvBooks.DataSource = _bookService.GetFilteredBookViews(CreateFilter()).ToList();
        ConfigureGridColumns();
    }

    private BookFilter CreateFilter()
    {
        var categoryId = cboCategoryFilter.SelectedValue is int selectedCategory ? selectedCategory : (int?)null;
        var publisher = cboPublisherFilter.SelectedIndex > 0 ? cboPublisherFilter.SelectedItem?.ToString() ?? string.Empty : string.Empty;
        var stock = Enum.TryParse<StockLevelFilter>(cboStockFilter.SelectedItem?.ToString(), out var stockLevel)
            ? stockLevel
            : StockLevelFilter.All;

        return new BookFilter
        {
            SearchText = txtSearch.Text,
            CategoryId = categoryId,
            Publisher  = publisher,
            StockLevel = stock
        };
    }

    private void btnSearch_Click(object sender, EventArgs e) => LoadBooks();

    private void txtSearch_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            LoadBooks();
        }
    }

    private void ConfigureGridColumns()
    {
        if (dgvBooks.Columns.Count == 0) return;

        HideColumn("CategoryID");
        HideColumn("SupplierID");

        var columns = new (string Property, string Header, int Width)[]
        {
            ("BookID",          "BookID",        70),
            ("CategoryName",    "CategoryName",  120),
            ("Title",           "Title",         160),
            ("Author",          "Author",        130),
            ("ISBN",            "ISBN",          130),
            ("Publisher",       "Publisher",     130),
            ("PublishYear",     "PublishYear",   95),
            ("ImportPrice",     "ImportPrice",   95),
            ("SellPrice",       "SellPrice",     95),
            ("QuantityInStock", "QuantityStock", 105)
        };

        var displayIndex = 0;
        foreach (var (property, header, width) in columns)
        {
            if (dgvBooks.Columns[property] is not DataGridViewColumn column) continue;
            column.Visible      = true;
            column.HeaderText   = header;
            column.Width        = width;
            column.DisplayIndex = displayIndex++;
        }

        if (dgvBooks.Columns["ImportPrice"] is DataGridViewColumn importPrice)
        {
            importPrice.DefaultCellStyle.Format    = "N2";
            importPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        if (dgvBooks.Columns["SellPrice"] is DataGridViewColumn sellPrice)
        {
            sellPrice.DefaultCellStyle.Format    = "N2";
            sellPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        if (dgvBooks.Columns["QuantityInStock"] is DataGridViewColumn quantity)
        {
            quantity.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
    }

    private void HideColumn(string name)
    {
        if (dgvBooks.Columns[name] is DataGridViewColumn column)
            column.Visible = false;
    }

    private Book? GetSelectedBook()
    {
        if (dgvBooks.CurrentRow?.DataBoundItem is BookViewDto view)
            return _bookService.GetBook(view.BookID);
        return null;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        using var form = new BookEditForm();
        if (form.ShowDialog(FindForm()) != DialogResult.OK) return;

        var result = _bookService.AddBook(form.Book);
        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage, "Add Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        LoadBooks();
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        var selected = GetSelectedBook();
        if (selected is null)
        {
            MessageBox.Show("Please select a book to edit.", "Edit Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var book = _bookService.GetBook(selected.BookID);
        if (book is null)
        {
            MessageBox.Show("Book not found.", "Edit Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            LoadBooks();
            return;
        }

        using var form = new BookEditForm(book);
        if (form.ShowDialog(FindForm()) != DialogResult.OK) return;

        var result = _bookService.UpdateBook(form.Book);
        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage, "Edit Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        LoadBooks();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        var selected = GetSelectedBook();
        if (selected is null)
        {
            MessageBox.Show("Please select a book to delete.", "Delete Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var confirm = MessageBox.Show(
            $"Delete \"{selected.Title}\"?",
            "Confirm Delete",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes) return;

        var result = _bookService.DeleteBook(selected.BookID);
        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage, "Delete Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        LoadBooks();
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        cboCategoryFilter.SelectedIndex  = 0;
        cboPublisherFilter.SelectedIndex = 0;
        cboStockFilter.SelectedItem      = StockLevelFilter.All.ToString();
        LoadBooks();
    }
}
