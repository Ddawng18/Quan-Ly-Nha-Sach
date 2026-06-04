namespace BookStoreApp.DTO;

public class OrderDetailViewDto
{
    public int OrderDetailID { get; set; }
    public int OrderID { get; set; }
    public int BookID { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal Subtotal { get; set; }
}
