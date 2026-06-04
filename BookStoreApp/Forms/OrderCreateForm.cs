using BookStoreApp.BLL;
using BookStoreApp.BLL.Payments;
using BookStoreApp.DTO;

namespace BookStoreApp.Forms;

public partial class OrderCreateForm : Form
{
    private readonly IBookService _bookService = ServiceLocator.BookService;
    private readonly ICustomerService _customerService = ServiceLocator.CustomerService;
    private readonly IEmployeeService _employeeService = ServiceLocator.EmployeeService;
    private readonly IPosService _posService = ServiceLocator.PosService;
    private readonly List<CartLine> _lines = [];
    private ComboBox cboLineDiscountType = new();
    private NumericUpDown numLineDiscount = new();
    private ComboBox cboOrderDiscountType = new();
    private NumericUpDown numOrderDiscount = new();
    private NumericUpDown numTaxRate = new();
    private NumericUpDown numLoyaltyPoints = new();
    private ComboBox cboPaymentMethod = new();

    public OrderCreateForm()
    {
        InitializeComponent();
        InitializePosControls();
        LoadLookups();
    }

    private void InitializePosControls()
    {
        Text = "Point of Sale";
        lblPayment.Text = "Status";
        cboPaymentStatus.Items.Clear();
        cboPaymentStatus.Items.AddRange(OrderStatus.All.Cast<object>().ToArray());
        cboPaymentStatus.SelectedItem = OrderStatus.Pending;

        AddLabel("Line Discount", 16, 140);
        cboLineDiscountType = CreateDiscountTypeCombo(16, 164);
        numLineDiscount = CreateMoneyInput(160, 164, 100);

        AddLabel("Order Discount", 276, 140);
        cboOrderDiscountType = CreateDiscountTypeCombo(276, 164);
        numOrderDiscount = CreateMoneyInput(420, 164, 100);

        AddLabel("Tax %", 536, 140);
        numTaxRate = CreateMoneyInput(536, 164, 72);
        numTaxRate.DecimalPlaces = 2;
        numTaxRate.Maximum = 100;

        AddLabel("Redeem Points", 16, 204);
        numLoyaltyPoints = CreateMoneyInput(16, 228, 120);
        numLoyaltyPoints.DecimalPlaces = 0;
        numLoyaltyPoints.ValueChanged += (_, _) => RefreshLines();

        AddLabel("Method", 488, 80);
        cboPaymentMethod = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Location = new Point(488, 104),
            Size = new Size(136, 28)
        };
        cboPaymentMethod.Items.Add("Cash");
        cboPaymentMethod.SelectedIndex = 0;
        Controls.Add(cboPaymentMethod);

        var btnPayWithQr = new Button
        {
            Text = "Pay with QR",
            Location = new Point(280, 584),
            Size = new Size(130, 40),
            FlatStyle = FlatStyle.Flat,
            UseVisualStyleBackColor = true,
            BackColor = Color.White
        };
        btnPayWithQr.Click += btnPayWithQr_Click;
        Controls.Add(btnPayWithQr);

        numOrderDiscount.ValueChanged += (_, _) => RefreshLines();
        cboOrderDiscountType.SelectedIndexChanged += (_, _) => RefreshLines();
        numTaxRate.ValueChanged += (_, _) => RefreshLines();
        cboCustomer.SelectedIndexChanged += (_, _) => UpdateLoyaltyLimit();

        dgvLines.Location = new Point(16, 272);
        dgvLines.Size = new Size(608, 220);
        lblTotal.Location = new Point(16, 500);
        lblTotal.Size = new Size(608, 72);
        btnSave.Location = new Point(16, 584);
        btnCancel.Location = new Point(148, 584);
        ClientSize = new Size(644, 640);
    }

    private void AddLabel(string text, int x, int y)
    {
        Controls.Add(new Label { AutoSize = true, Text = text, Location = new Point(x, y) });
    }

    private ComboBox CreateDiscountTypeCombo(int x, int y)
    {
        var combo = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Location = new Point(x, y),
            Size = new Size(132, 28)
        };
        combo.Items.AddRange(Enum.GetNames<DiscountType>().Cast<object>().ToArray());
        combo.SelectedItem = DiscountType.None.ToString();
        Controls.Add(combo);
        return combo;
    }

    private NumericUpDown CreateMoneyInput(int x, int y, int width) =>
        new()
        {
            Location = new Point(x, y),
            Size = new Size(width, 27),
            DecimalPlaces = 2,
            Maximum = 1000000000,
            ThousandsSeparator = true
        };

    private void LoadLookups()
    {
        cboCustomer.DisplayMember = nameof(Customer.FullName);
        cboCustomer.ValueMember = nameof(Customer.CustomerID);
        cboCustomer.DataSource = _customerService.GetCustomers().ToList();

        cboEmployee.DisplayMember = nameof(Employee.FullName);
        cboEmployee.ValueMember = nameof(Employee.EmployeeID);
        cboEmployee.DataSource = _employeeService.GetEmployees().ToList();

        if (cboPaymentStatus.SelectedIndex < 0)
        {
            cboPaymentStatus.SelectedItem = OrderStatus.Pending;
        }

        var books = _bookService.GetBooks().Where(b => !b.IsDeleted && b.QuantityInStock > 0).ToList();
        cboBook.DisplayMember = nameof(Book.Title);
        cboBook.ValueMember = nameof(Book.BookID);
        cboBook.DataSource = books;
        Controls.Add(numLineDiscount);
        Controls.Add(numOrderDiscount);
        Controls.Add(numTaxRate);
        Controls.Add(numLoyaltyPoints);
        UpdateLoyaltyLimit();
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

        var result = _posService.AddOrUpdateLine(
            _lines,
            book,
            qty,
            ParseDiscountType(cboLineDiscountType),
            numLineDiscount.Value);

        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage, "Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        RefreshLines();
    }

    private void btnRemoveLine_Click(object sender, EventArgs e)
    {
        if (dgvLines.CurrentRow?.DataBoundItem is not CartLine line)
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
        var loyaltyDiscount = Math.Min(
            numLoyaltyPoints.Value * LoyaltySettings.RedemptionValuePerPoint,
            _lines.Sum(l => l.Subtotal));
        var totals = _posService.CalculateTotals(
            _lines,
            ParseDiscountType(cboOrderDiscountType),
            numOrderDiscount.Value,
            numTaxRate.Value,
            loyaltyDiscount);
        lblTotal.Text =
            $"Subtotal: {totals.Subtotal:N2} | Discount: {(totals.Discount + totals.LoyaltyDiscount):N2}\r\n" +
            $"Tax: {totals.Tax:N2} | Grand Total: {totals.GrandTotal:N2}";
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        var checkout = PrepareAndValidateCheckout();
        if (checkout is null) return;

        checkout.Order!.PaymentMethod = "Cash";
        checkout.Order.PaymentStatus = OrderStatus.Paid;

        var result = _posService.CompleteCheckout(checkout);
        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage, "Create Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnPayWithQr_Click(object? sender, EventArgs e)
    {
        var checkout = PrepareAndValidateCheckout();
        if (checkout is null) return;

        var config = Program.PaymentConfig;
        var provider = PaymentProviderFactory.Create(config);
        var providerName = GetProviderDisplayName(config);
        var orderRef = $"POS-{DateTime.Now:yyyyMMddHHmmss}";

        using var paymentForm = new PaymentQRForm(
            provider,
            orderRef,
            checkout.Totals.GrandTotal,
            $"Bookstore order",
            config.QrTimeoutSeconds,
            config.PollingIntervalSeconds,
            providerName);

        var paymentResult = paymentForm.ShowDialog(this);

        if (paymentResult != DialogResult.OK)
        {
            return;
        }

        checkout.Order!.PaymentTransactionId = paymentForm.TransactionId;
        checkout.Order.PaymentMethod = "QR Payment";
        checkout.Order.PaymentStatus = OrderStatus.Paid;

        var result = _posService.CompleteCheckout(checkout);
        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage, "Create Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    private CheckoutResult? PrepareAndValidateCheckout()
    {
        if (cboCustomer.SelectedValue is not int customerId ||
            cboEmployee.SelectedValue is not int employeeId)
        {
            MessageBox.Show("Select customer and employee.", "Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }

        var checkout = _posService.PrepareCheckout(new CheckoutRequest
        {
            CustomerID = customerId,
            EmployeeID = employeeId,
            PaymentStatus = cboPaymentStatus.SelectedItem?.ToString() ?? OrderStatus.Pending,
            PaymentMethod = "Cash",
            OrderDiscountType = ParseDiscountType(cboOrderDiscountType),
            OrderDiscountValue = numOrderDiscount.Value,
            TaxRate = numTaxRate.Value,
            LoyaltyPointsToRedeem = (int)numLoyaltyPoints.Value,
            Lines = _lines
        });

        if (!checkout.Validation.IsValid)
        {
            MessageBox.Show(checkout.Validation.ErrorMessage, "Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }

        return checkout;
    }

    private void UpdateLoyaltyLimit()
    {
        if (cboCustomer.SelectedItem is Customer customer)
        {
            numLoyaltyPoints.Maximum = Math.Max(0, customer.LoyaltyPoints);
        }
    }

    private static DiscountType ParseDiscountType(ComboBox combo) =>
        Enum.TryParse<DiscountType>(combo.SelectedItem?.ToString(), out var type) ? type : DiscountType.None;

    private static string GetProviderDisplayName(DTO.Payments.PaymentConfig config) =>
        config.DefaultProvider?.ToLowerInvariant() switch
        {
            "momo" => "MoMo",
            "vnpay" => "VNPay",
            _ => "Demo"
        };

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

}
