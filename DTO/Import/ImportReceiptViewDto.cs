namespace BookStoreApp.DTO;

public class ImportReceiptViewDto
{
    public int ImportID { get; set; }
    public int SupplierID { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public int EmployeeID { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public DateTime ImportDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Note { get; set; }
}

public class ImportDetailViewDto
{
    public int ImportDetailID { get; set; }
    public int ImportID { get; set; }
    public int BookID { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal ImportPrice { get; set; }
    public decimal Subtotal { get; set; }
}
