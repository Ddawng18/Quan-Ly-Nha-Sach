namespace BookStoreApp.DTO;

public class CheckoutResult
{
    public ValidationResult Validation { get; set; } = ValidationResult.Ok();
    public Order? Order { get; set; }
    public IReadOnlyList<OrderDetail> Details { get; set; } = [];
    public CartTotals Totals { get; set; } = new();
}
