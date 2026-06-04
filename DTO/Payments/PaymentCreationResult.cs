namespace BookStoreApp.DTO.Payments;

/// <summary>
/// Result returned by IPaymentProvider.CreatePaymentAsync.
/// </summary>
public record PaymentCreationResult(
    bool Success,
    string TransactionId,
    string QrCodeData,
    string? ErrorMessage);
