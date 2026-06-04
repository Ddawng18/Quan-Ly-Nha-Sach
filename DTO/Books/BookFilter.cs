namespace BookStoreApp.DTO;

public class BookFilter
{
    public string SearchText { get; set; } = string.Empty;
    public int? CategoryId { get; set; }
    public string Publisher { get; set; } = string.Empty;
    public StockLevelFilter StockLevel { get; set; } = StockLevelFilter.All;
}
