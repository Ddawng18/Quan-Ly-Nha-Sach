namespace BookStoreApp.DTO;

public class RecentOrderDto
{
    public int OrderID { get; set; }
    public string Customer { get; set; } = string.Empty;
    public decimal Total { get; set; }
}
