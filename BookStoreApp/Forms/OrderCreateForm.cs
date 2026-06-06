using BookStoreApp.BLL;
using BookStoreApp.DTO;

namespace BookStoreApp.Forms;

public partial class OrderCreateForm : Form
{
    private readonly IOrderService _orderService = ServiceLocator.OrderService;
    private readonly IBookService _bookService = ServiceLocator.BookService;
    private readonly ICustomerService _customerService = ServiceLocator.CustomerService;
    private readonly IEmployeeService _employeeService = ServiceLocator.EmployeeService;
    private readonly List<OrderDetail> _lines = [];

    public OrderCreateForm()
    {
        InitializeComponent();
        LoadLookups();
    }

    private void LoadLookups()
    {
        cboCustomer.DisplayMember = nameof(Customer.FullName);
        cboCustomer.ValueMember = nameof(Customer.CustomerID);
        cboCustomer.DataSource = _customerService.GetCustomers().ToList();

        cboEmployee.DisplayMember = nameof(Employee.FullName);
        cboEmployee.ValueMember = nameof(Employee.EmployeeID);
        cboEmployee.DataSource = _employeeService.GetEmployees().ToList();

        cboPaymentStatus.Items.AddRange(OrderStatus.All.Cast<object>().ToArray());
        cboPaymentStatus.SelectedItem = OrderStatus.Pending;

        var books = _bookService.GetBooks().Where(b => !b.IsDeleted && b.QuantityInStock > 0).ToList();
        cboBook.DisplayMember = nameof(Book.Title);
        cboBook.ValueMember = nameof(Book.BookID);
        cboBook.DataSource = books;
    }

    private void btnAddLine_Click(object sender, EventArgs e)
    {
        if (cboBook.SelectedItem is not Book book)
        {
            MessageBox.Show("Select a book.", "Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var qty = (int)numQuantity.Value;
        if (qty <= 0)
        {
            MessageBox.Show("Quantity must be greater than zero.", "Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var existing = _lines.FirstOrDefault(l => l.BookID == book.BookID);
        if (existing != null)
        {
            existing.Quantity += qty;
            existing.Subtotal = existing.UnitPrice * existing.Quantity;
        }
        else
        {
            _lines.Add(new OrderDetail
            {
                BookID = book.BookID,
                Quantity = qty,
                UnitPrice = book.SellPrice,
                Subtotal = book.SellPrice * qty
            });
        }

        RefreshLines();
    }

    private void btnRemoveLine_Click(object sender, EventArgs e)
    {
        if (dgvLines.CurrentRow?.DataBoundItem is not OrderDetail line)
        {
            return;
        }

        _lines.Remove(line);
        RefreshLines();
    }

    private void RefreshLines()
    {
        dgvLines.DataSource = null;
        dgvLines.DataSource = _lines.ToList();
        var total = _lines.Sum(l => l.Subtotal);
        lblTotal.Text = $"Total: {total:N2}";
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (cboCustomer.SelectedValue is not int customerId ||
            cboEmployee.SelectedValue is not int employeeId)
        {
            MessageBox.Show("Select customer and employee.", "Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (_lines.Count == 0)
        {
            MessageBox.Show("Add at least one line item.", "Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var order = new Order
        {
            CustomerID = customerId,
            EmployeeID = employeeId,
            OrderDate = DateTime.Now,
            PaymentStatus = cboPaymentStatus.SelectedItem?.ToString() ?? OrderStatus.Pending,
            PaymentMethod = "Cash"
        };

        var result = _orderService.CreateOrder(order, _lines);
        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage, "Create Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
