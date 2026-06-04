namespace BookStoreApp.DTO;

public class CustomerPurchaseDto
{
    public int OrderID { get; set; }
    public DateTime OrderDate { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    public decimal OrderTotal { get; set; }
    public string PaymentStatus { get; set; } = string.Empty;
}
