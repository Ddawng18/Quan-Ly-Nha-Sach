using System.Globalization;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web;
using BookStoreApp.DTO.Payments;
using BookStoreApp.Utilities;

namespace BookStoreApp.BLL.Payments;

/// <summary>
/// VNPay payment provider using VNPay sandbox API.
/// HMAC-SHA512 signature per VNPay specification.
/// </summary>
public class VNPayPaymentProvider : IPaymentProvider
{
    private readonly VNPayConfig _config;
    private readonly HttpClient _httpClient;

    public VNPayPaymentProvider(VNPayConfig config) : this(config, new HttpClient())
    {
    }

    public VNPayPaymentProvider(VNPayConfig config, HttpClient httpClient)
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
        // VNPay requires amount × 100 (VND, no decimals)
        var amountCents = ((long)Math.Round(amount, MidpointRounding.AwayFromZero)) * 100;
        var createDate = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
        var expireDate = DateTime.UtcNow.AddHours(7).AddMinutes(15).ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
        var txnRef = orderId;
        var orderInfo = string.IsNullOrWhiteSpace(description) ? $"Order #{orderId}" : description;
        var ipAddr = "127.0.0.1";
        var locale = "vn";

        // Build sorted parameter dictionary
        var parameters = new SortedDictionary<string, string>
        {
            ["vnp_Version"] = "2.1.0",
            ["vnp_Command"] = "pay",
            ["vnp_TmnCode"] = _config.TmnCode,
            ["vnp_Amount"] = amountCents.ToString(CultureInfo.InvariantCulture),
            ["vnp_BankCode"] = "",
            ["vnp_CreateDate"] = createDate,
            ["vnp_CurrCode"] = "VND",
            ["vnp_IpAddr"] = ipAddr,
            ["vnp_Locale"] = locale,
            ["vnp_OrderInfo"] = orderInfo,
            ["vnp_OrderType"] = "other",
            ["vnp_ReturnUrl"] = "https://localhost/return",
            ["vnp_TxnRef"] = txnRef,
            ["vnp_ExpireDate"] = expireDate
        };

        // Build query string and sign
        var queryString = string.Join("&",
            parameters.Select(kvp =>
                $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));

        var signature = ComputeHmacSha512(queryString, _config.HashSecret);
        var paymentUrl = $"{(_config.BaseUrl ?? "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html").TrimEnd('/')}?{queryString}&vnp_SecureHash={signature}";

        try
        {
            // Post to VNPay to create transaction
            var content = new FormUrlEncodedContent(parameters.Concat(
                new[] { new KeyValuePair<string, string>("vnp_SecureHash", signature) }));

            var response = await _httpClient.PostAsync(
                _config.BaseUrl?.TrimEnd('/') ?? "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html",
                content,
                cancellationToken);

            var responseText = await response.Content.ReadAsStringAsync(cancellationToken);

            // Try to parse response as query string
            var responseParams = System.Web.HttpUtility.ParseQueryString(responseText);
            var responseCode = responseParams["vnp_ResponseCode"] ?? "";

            if (responseCode == "00")
            {
                var transactionId = responseParams["vnp_TransactionNo"] ?? txnRef;
                var qrData = responseParams["vnp_QrCode"] ?? paymentUrl;

                // VNPay QR may be raw data or a URL; try downloading if it's a URL
                string? qrBase64 = null;
                if (Uri.TryCreate(qrData, UriKind.Absolute, out var qrUri) &&
                    (qrUri.Scheme == "http" || qrUri.Scheme == "https"))
                {
                    qrBase64 = await DownloadQrImageAsync(qrData, cancellationToken);
                }

                return new PaymentCreationResult(true, transactionId, qrBase64 ?? qrData, null);
            }

            var message = responseParams["vnp_Message"] ?? $"VNPay returned response code {responseCode}";
            return new PaymentCreationResult(false, txnRef, string.Empty, message);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            FileLogger.Error($"VNPay CreatePayment failed: orderId={orderId}", ex);
            return new PaymentCreationResult(false, txnRef, string.Empty, ex.Message);
        }
    }

    public async Task<PaymentStatusResult> QueryStatusAsync(
        string transactionId,
        CancellationToken cancellationToken = default)
    {
        var createDate = DateTime.UtcNow.AddHours(7).ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
        var ipAddr = "127.0.0.1";

        var parameters = new SortedDictionary<string, string>
        {
            ["vnp_RequestId"] = Guid.NewGuid().ToString(),
            ["vnp_Version"] = "2.1.0",
            ["vnp_Command"] = "querydr",
            ["vnp_TmnCode"] = _config.TmnCode,
            ["vnp_TxnRef"] = transactionId,
            ["vnp_OrderInfo"] = transactionId,
            ["vnp_TransactionDate"] = createDate,
            ["vnp_CreateDate"] = createDate,
            ["vnp_IpAddr"] = ipAddr
        };

        var queryString = string.Join("&",
            parameters.Select(kvp =>
                $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));

        var signature = ComputeHmacSha512(queryString, _config.HashSecret);

        try
        {
            var content = new FormUrlEncodedContent(parameters.Concat(
                new[] { new KeyValuePair<string, string>("vnp_SecureHash", signature) }));

            var response = await _httpClient.PostAsync(
                "https://sandbox.vnpayment.vn/merchant_webapi/api/transaction",
                content,
                cancellationToken);

            var responseText = await response.Content.ReadAsStringAsync(cancellationToken);
            var responseParams = System.Web.HttpUtility.ParseQueryString(responseText);

            var responseCode = responseParams["vnp_ResponseCode"] ?? "";
            var transactionStatus = responseParams["vnp_TransactionStatus"] ?? "";

            var status = (responseCode, transactionStatus) switch
            {
                ("00", "00") => PaymentStatus.Paid,
                ("00", "01") => PaymentStatus.Pending,
                ("00", "02") => PaymentStatus.Failed,
                _ => PaymentStatus.Failed
            };

            var errorMessage = status == PaymentStatus.Paid ? null
                : responseParams["vnp_Message"];

            return new PaymentStatusResult(status, errorMessage);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            FileLogger.Error($"VNPay QueryStatus failed: transactionId={transactionId}", ex);
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
        catch
        {
            return null;
        }
    }

    private static string ComputeHmacSha512(string data, string key)
    {
        using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
    }
}
