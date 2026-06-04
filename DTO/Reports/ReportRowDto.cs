namespace BookStoreApp.DTO;

public class ReportRowDto
{
    public string ReportType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Count { get; set; }
    public decimal Amount { get; set; }
}
