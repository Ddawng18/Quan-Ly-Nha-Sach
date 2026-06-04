namespace BookStoreApp.DTO;

public class Employee
{
    public int EmployeeID { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public string Position { get; set; } = string.Empty;
    public string Role { get; set; } = "Staff";
    public DateTime CreatedDate { get; set; }
}
