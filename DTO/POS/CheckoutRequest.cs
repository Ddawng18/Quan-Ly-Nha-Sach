namespace BookStoreApp.DTO;

public class CheckoutRequest
{
    public int CustomerID { get; set; }
    public int EmployeeID { get; set; }
    public string PaymentStatus { get; set; } = OrderStatus.Pending;
    public string PaymentMethod { get; set; } = "Cash";
    public DiscountType OrderDiscountType { get; set; }
    public decimal OrderDiscountValue { get; set; }
    public decimal TaxRate { get; set; }
    public int LoyaltyPointsToRedeem { get; set; }
    public IReadOnlyList<CartLine> Lines { get; set; } = [];
}
