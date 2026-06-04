using BookStoreApp.BLL;
using BookStoreApp.DTO;
using BookStoreApp.Forms;
using BookStoreApp.Theme;

namespace BookStoreApp.UserControls;

public partial class SupplierControl : UserControl
{
    private readonly ISupplierService _supplierService = ServiceLocator.SupplierService;

    public SupplierControl()
    {
        InitializeComponent();
        ApplyTheme();
        LoadSuppliers();
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
        AppTheme.ApplyGridStyle(dgvSuppliers);
    }

    private void LoadSuppliers()
    {
        dgvSuppliers.DataSource = null;
        dgvSuppliers.DataSource = _supplierService.SearchSuppliers(txtSearch.Text).ToList();
        ConfigureGridColumns();
    }

    private void ConfigureGridColumns()
    {
        if (dgvSuppliers.Columns.Count == 0)
        {
            return;
        }

        SetColumn("SupplierID", "SupplierID", 90, 0);
        SetColumn("SupplierName", "SupplierName", 160, 1);
        SetColumn("Address", "Address", 180, 2);
        SetColumn("Email", "Email", 160, 3);
        SetColumn("Phone", "Phone", 120, 4);
    }

    private void SetColumn(string property, string header, int width, int displayIndex)
    {
        if (dgvSuppliers.Columns[property] is not DataGridViewColumn column)
        {
            return;
        }

        column.Visible = true;
        column.HeaderText = header;
        column.Width = width;
        column.DisplayIndex = displayIndex;
    }

    private Supplier? GetSelectedSupplier()
    {
        if (dgvSuppliers.CurrentRow?.DataBoundItem is Supplier supplier)
        {
            return supplier;
        }

        return null;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        using var form = new SupplierEditForm();
        if (form.ShowDialog(FindForm()) != DialogResult.OK)
        {
            return;
        }

        var result = _supplierService.AddSupplier(form.Supplier);
        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage, "Add Supplier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        LoadSuppliers();
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        var selected = GetSelectedSupplier();
        if (selected is null)
        {
            MessageBox.Show("Please select a supplier.", "Edit Supplier", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var supplier = _supplierService.GetSupplier(selected.SupplierID);
        if (supplier is null)
        {
            MessageBox.Show("Supplier not found.", "Edit Supplier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            LoadSuppliers();
            return;
        }

        using var form = new SupplierEditForm(supplier);
        if (form.ShowDialog(FindForm()) != DialogResult.OK)
        {
            return;
        }

        var result = _supplierService.UpdateSupplier(form.Supplier);
        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage, "Edit Supplier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        LoadSuppliers();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        var selected = GetSelectedSupplier();
        if (selected is null)
        {
            MessageBox.Show("Please select a supplier.", "Delete Supplier", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (MessageBox.Show($"Delete \"{selected.SupplierName}\"?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }

        var result = _supplierService.DeleteSupplier(selected.SupplierID);
        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage, "Delete Supplier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        LoadSuppliers();
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        LoadSuppliers();
    }

    private void btnSearch_Click(object sender, EventArgs e) => LoadSuppliers();

    private void txtSearch_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            LoadSuppliers();
        }
    }
}
