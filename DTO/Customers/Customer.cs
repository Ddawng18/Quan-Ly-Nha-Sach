namespace BookStoreApp.DTO;

public class Customer
{
    public int CustomerID { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int LoyaltyPoints { get; set; }
    public DateTime CreatedDate { get; set; }
}
