using BookStoreApp.BLL;
using BookStoreApp.DTO;
using BookStoreApp.Forms;
using BookStoreApp.Theme;

namespace BookStoreApp.UserControls;

public partial class CategoryControl : UserControl
{
    private readonly ICategoryService _categoryService = ServiceLocator.CategoryService;

    public CategoryControl()
    {
        InitializeComponent();
        ApplyTheme();
        LoadCategories();
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
        AppTheme.ApplyGridStyle(dgvCategories);
    }

    private void LoadCategories()
    {
        dgvCategories.DataSource = null;
        dgvCategories.DataSource = _categoryService.SearchCategories(txtSearch.Text).ToList();
        ConfigureGridColumns();
    }

    private void ConfigureGridColumns()
    {
        if (dgvCategories.Columns.Count == 0)
        {
            return;
        }

        SetColumn("CategoryID", "CategoryID", 100, 0);
        SetColumn("CategoryName", "CategoryName", 280, 1);
    }

    private void SetColumn(string property, string header, int width, int displayIndex)
    {
        if (dgvCategories.Columns[property] is not DataGridViewColumn column)
        {
            return;
        }

        column.Visible = true;
        column.HeaderText = header;
        column.Width = width;
        column.DisplayIndex = displayIndex;
    }

    private Category? GetSelectedCategory()
    {
        if (dgvCategories.CurrentRow?.DataBoundItem is Category category)
        {
            return category;
        }

        return null;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        using var form = new CategoryEditForm();
        if (form.ShowDialog(FindForm()) != DialogResult.OK)
        {
            return;
        }

        var result = _categoryService.AddCategory(form.Category);
        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage, "Add Category", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        LoadCategories();
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        var selected = GetSelectedCategory();
        if (selected is null)
        {
            MessageBox.Show("Please select a category.", "Edit Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var category = _categoryService.GetCategory(selected.CategoryID);
        if (category is null)
        {
            MessageBox.Show("Category not found.", "Edit Category", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            LoadCategories();
            return;
        }

        using var form = new CategoryEditForm(category);
        if (form.ShowDialog(FindForm()) != DialogResult.OK)
        {
            return;
        }

        var result = _categoryService.UpdateCategory(form.Category);
        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage, "Edit Category", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        LoadCategories();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        var selected = GetSelectedCategory();
        if (selected is null)
        {
            MessageBox.Show("Please select a category.", "Delete Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (MessageBox.Show($"Delete \"{selected.CategoryName}\"?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }

        var result = _categoryService.DeleteCategory(selected.CategoryID);
        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage, "Delete Category", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        LoadCategories();
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        LoadCategories();
    }

    private void btnSearch_Click(object sender, EventArgs e) => LoadCategories();

    private void txtSearch_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            LoadCategories();
        }
    }
}
