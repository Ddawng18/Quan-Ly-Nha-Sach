using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using BookStoreApp.DTO.Payments;

namespace BookStoreApp.BLL.Payments;

/// <summary>
/// MoMo (Mobile Money) payment provider using MoMo v2 sandbox API.
/// HMAC-SHA256 signature per MoMo specification.
/// </summary>
public class MomoPaymentProvider : IPaymentProvider
{
    private readonly MomoConfig _config;
    private readonly HttpClient _httpClient;

    public MomoPaymentProvider(MomoConfig config) : this(config, new HttpClient()) { }

    public MomoPaymentProvider(MomoConfig config, HttpClient httpClient)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<PaymentCreationResult> CreatePaymentAsync(
        string orderId,
        decimal amount,
        string description,
        CancellationToken cancellationToken = default)
    {
        var requestId = Guid.NewGuid().ToString();
        var orderInfo = string.IsNullOrWhiteSpace(description) ? $"Order #{orderId}" : description;
        var extraData = "";

        var amountInt = (long)Math.Round(amount, MidpointRounding.AwayFromZero);

        var rawSignature =
            $"accessKey={_config.AccessKey}" +
            $"&amount={amountInt}" +
            $"&extraData={extraData}" +
            $"&ipnUrl={Uri.EscapeDataString("https://localhost/ipn")}" +
            $"&orderId={orderId}" +
            $"&orderInfo={Uri.EscapeDataString(orderInfo)}" +
            $"&partnerCode={_config.PartnerCode}" +
            $"&redirectUrl={Uri.EscapeDataString("https://localhost/redirect")}" +
            $"&requestId={requestId}" +
            $"&requestType=captureWallet";

        var signature = ComputeHmacSha256(rawSignature, _config.SecretKey);

        var payload = new
        {
            partnerCode = _config.PartnerCode,
            accessKey   = _config.AccessKey,
            requestId,
            amount      = amountInt.ToString(),
            orderId,
            orderInfo,
            redirectUrl = "https://localhost/redirect",
            ipnUrl      = "https://localhost/ipn",
            extraData,
            requestType = "captureWallet",
            signature,
            lang        = "vi"
        };

        try
        {
            var baseUrl = (_config.BaseUrl ?? "https://test-payment.momo.vn/v2/gateway/api/").TrimEnd('/');
            var response = await _httpClient.PostAsJsonAsync(
                $"{baseUrl}/create", payload, cancellationToken);

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            using var doc = JsonDocument.Parse(content);
            var root = doc.RootElement;

            var resultCode = root.TryGetProperty("resultCode", out var rc) ? rc.GetInt32() : -1;

            if (resultCode == 0)
            {
                var transId = root.TryGetProperty("transId", out var tid)
                    ? tid.GetString() ?? requestId : requestId;
                var qrCodeUrl = root.TryGetProperty("qrCodeUrl", out var qr)
                    ? qr.GetString() : null;

                string? qrBase64 = null;
                if (!string.IsNullOrWhiteSpace(qrCodeUrl))
                    qrBase64 = await DownloadQrImageAsync(qrCodeUrl, cancellationToken);

                return new PaymentCreationResult(true, transId, qrBase64 ?? string.Empty, null);
            }

            var message = root.TryGetProperty("message", out var msg)
                ? msg.GetString()
                : $"MoMo returned resultCode={resultCode}";
            return new PaymentCreationResult(false, requestId, string.Empty, message);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            FileLogger.Error($"MoMo CreatePayment failed: orderId={orderId}", ex);
            return new PaymentCreationResult(false, requestId, string.Empty, ex.Message);
        }
    }

    public async Task<PaymentStatusResult> QueryStatusAsync(
        string transactionId,
        CancellationToken cancellationToken = default)
    {
        var requestId = Guid.NewGuid().ToString();

        var rawSignature =
            $"accessKey={_config.AccessKey}" +
            $"&orderId={transactionId}" +
            $"&partnerCode={_config.PartnerCode}" +
            $"&requestId={requestId}";

        var signature = ComputeHmacSha256(rawSignature, _config.SecretKey);

        var payload = new
        {
            partnerCode = _config.PartnerCode,
            accessKey   = _config.AccessKey,
            requestId,
            orderId     = transactionId,
            signature,
            lang        = "vi"
        };

        try
        {
            var baseUrl = (_config.BaseUrl ?? "https://test-payment.momo.vn/v2/gateway/api/").TrimEnd('/');
            var response = await _httpClient.PostAsJsonAsync(
                $"{baseUrl}/query", payload, cancellationToken);

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            using var doc = JsonDocument.Parse(content);
            var root = doc.RootElement;

            var resultCode = root.TryGetProperty("resultCode", out var rc) ? rc.GetInt32() : -1;

            var status = resultCode switch
            {
                0    => PaymentStatus.Paid,
                1000 => PaymentStatus.Pending,
                1001 => PaymentStatus.Pending,
                1002 => PaymentStatus.Pending,
                1003 => PaymentStatus.Cancelled,
                1004 => PaymentStatus.Failed,
                1005 => PaymentStatus.Expired,
                _    => PaymentStatus.Failed
            };

            var errorMessage = status == PaymentStatus.Paid ? null
                : root.TryGetProperty("message", out var msg) ? msg.GetString() : null;

            return new PaymentStatusResult(status, errorMessage);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            FileLogger.Error($"MoMo QueryStatus failed: transactionId={transactionId}", ex);
            return new PaymentStatusResult(PaymentStatus.Failed, ex.Message);
        }
    }

    private async Task<string?> DownloadQrImageAsync(string url, CancellationToken cancellationToken)
    {
        try
        {
            var bytes = await _httpClient.GetByteArrayAsync(url, cancellationToken);
            return Convert.ToBase64String(bytes);
        }
        catch { return null; }
    }

    private static string ComputeHmacSha256(string data, string key)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }
}
