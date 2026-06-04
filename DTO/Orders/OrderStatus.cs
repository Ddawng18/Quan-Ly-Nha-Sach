namespace BookStoreApp.DTO;

public static class OrderStatus
{
    public const string Pending = "Pending";
    public const string Paid = "Paid";
    public const string Cancelled = "Cancelled";

    public static readonly IReadOnlyList<string> All = [Pending, Paid, Cancelled];
}
