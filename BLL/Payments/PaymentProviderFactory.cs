using BookStoreApp.DTO.Payments;

namespace BookStoreApp.BLL.Payments;

/// <summary>
/// Factory that selects the appropriate IPaymentProvider based on configuration.
/// Falls back to DemoPaymentProvider when no real credentials are configured.
/// </summary>
public static class PaymentProviderFactory
{
    public static IPaymentProvider Create(PaymentConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

        return config.DefaultProvider?.ToLowerInvariant() switch
        {
            "momo" when IsConfigured(config.MoMo?.SecretKey)
                => new MomoPaymentProvider(config.MoMo!),
            "vnpay" when IsConfigured(config.VNPay?.HashSecret)
                => new VNPayPaymentProvider(config.VNPay!),
            _ => new DemoPaymentProvider()
        };
    }

    private static bool IsConfigured(string? value) =>
        !string.IsNullOrWhiteSpace(value);
}
