namespace BookStoreApp.DTO;

public class CartTotals
{
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal LoyaltyDiscount { get; set; }
    public decimal Tax { get; set; }
    public decimal GrandTotal { get; set; }
}
