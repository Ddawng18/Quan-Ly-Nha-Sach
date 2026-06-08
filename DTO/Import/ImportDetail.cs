namespace BookStoreApp.DTO;

public class ImportDetail
{
    public int ImportDetailID { get; set; }
    public int ImportID { get; set; }
    public int BookID { get; set; }
    public int Quantity { get; set; }
    public decimal ImportPrice { get; set; }
    public decimal Subtotal { get; set; }
}
