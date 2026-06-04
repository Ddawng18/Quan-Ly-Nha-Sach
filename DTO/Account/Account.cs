namespace BookStoreApp.DTO;

public class Account
{
    public int AccountID { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public int EmployeeID { get; set; }
    public bool IsActive { get; set; } = true;
}
