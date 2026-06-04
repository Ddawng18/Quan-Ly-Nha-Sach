namespace BookStoreApp.DTO.Payments;

/// <summary>
/// Represents the current state of a payment transaction.
/// </summary>
public enum PaymentStatus
{
    Pending,
    Paid,
    Cancelled,
    Failed,
    Expired
}
