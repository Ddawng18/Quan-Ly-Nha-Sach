using BookStoreApp.DTO.Payments;

namespace BookStoreApp.BLL.Payments;

/// <summary>
/// Abstraction for QR-based payment providers (MoMo, VNPay, Demo).
/// Implementations handle provider-specific API calls and signature generation.
/// </summary>
public interface IPaymentProvider
{
    /// <summary>Creates a payment request and returns a QR payload.</summary>
    Task<PaymentCreationResult> CreatePaymentAsync(
        string orderId,
        decimal amount,
        string description,
        CancellationToken cancellationToken = default);

    /// <summary>Polls or verifies current payment status.</summary>
    Task<PaymentStatusResult> QueryStatusAsync(
        string transactionId,
        CancellationToken cancellationToken = default);
}
