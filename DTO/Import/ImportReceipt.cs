namespace BookStoreApp.DTO;

public class ImportReceipt
{
    public int ImportID { get; set; }
    public int SupplierID { get; set; }
    public int EmployeeID { get; set; }
    public DateTime ImportDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Note { get; set; }
}
