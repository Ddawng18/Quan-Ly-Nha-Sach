using BookStoreApp.DTO;

namespace BookStoreApp.Forms;

public partial class CategoryEditForm : Form
{
    private readonly Category? _existing;

    public Category Category { get; private set; } = new();

    public CategoryEditForm()
    {
        InitializeComponent();
        Text = "Add Category";
    }

    public CategoryEditForm(Category category)
        : this()
    {
        _existing = category;
        Text = "Edit Category";
        txtCategoryName.Text = category.CategoryName;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        Category = new Category
        {
            CategoryID = _existing?.CategoryID ?? 0,
            CategoryName = txtCategoryName.Text.Trim()
        };

        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
