namespace BookStoreApp.DTO.Payments;

/// <summary>
/// Result returned by IPaymentProvider.QueryStatusAsync.
/// </summary>
public record PaymentStatusResult(
    PaymentStatus Status,
    string? ErrorMessage);
