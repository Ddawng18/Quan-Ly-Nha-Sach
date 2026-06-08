namespace BookStoreApp.DTO;

public class BookViewDto
{
    public int BookID { get; set; }
    public int CategoryID { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public int PublishYear { get; set; }
    public decimal SellPrice { get; set; }
}
