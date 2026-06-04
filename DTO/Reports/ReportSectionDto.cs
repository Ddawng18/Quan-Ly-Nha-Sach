namespace BookStoreApp.DTO;

public class ReportSectionDto
{
    public string SectionName { get; set; } = string.Empty;
    public List<string> Headers { get; set; } = [];
    public List<List<string>> Rows { get; set; } = [];
}
