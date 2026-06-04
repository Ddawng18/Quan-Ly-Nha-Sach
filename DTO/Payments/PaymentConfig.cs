namespace BookStoreApp.DTO.Payments;

/// <summary>
/// Configuration model for payment providers, deserialized from appsettings.json.
/// </summary>
public class PaymentConfig
{
    public string DefaultProvider { get; set; } = "Demo";
    public int QrTimeoutSeconds { get; set; } = 300;
    public int PollingIntervalSeconds { get; set; } = 5;
    public MomoConfig? MoMo { get; set; }
    public VNPayConfig? VNPay { get; set; }
}

public class MomoConfig
{
    public string PartnerCode { get; set; } = string.Empty;
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = "https://test-payment.momo.vn/v2/gateway/api/";
}

public class VNPayConfig
{
    public string TmnCode { get; set; } = string.Empty;
    public string HashSecret { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
}
