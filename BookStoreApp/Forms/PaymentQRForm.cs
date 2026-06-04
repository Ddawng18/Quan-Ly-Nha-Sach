using BookStoreApp.BLL.Payments;
using BookStoreApp.DTO.Payments;
using BookStoreApp.Utilities;

namespace BookStoreApp.Forms;

/// <summary>
/// Displays a QR code for payment, polls the provider for status,
/// and returns the transaction ID on successful payment.
/// </summary>
public partial class PaymentQRForm : Form
{
    private readonly IPaymentProvider _provider;
    private readonly string _orderId;
    private readonly decimal _amount;
    private readonly string _description;
    private readonly int _timeoutSeconds;
    private readonly int _pollingIntervalSeconds;
    private readonly string _providerName;

    private System.Windows.Forms.Timer? _countdownTimer;
    private System.Windows.Forms.Timer? _pollingTimer;
    private CancellationTokenSource? _cancellationSource;
    private int _secondsRemaining;
    private string? _currentTransactionId;
    private string? _finalTransactionId;

    /// <summary>
    /// The confirmed transaction ID after successful payment.
    /// </summary>
    public string TransactionId => _finalTransactionId ?? string.Empty;

    public PaymentQRForm(
        IPaymentProvider provider,
        string orderId,
        decimal amount,
        string description,
        int timeoutSeconds = 300,
        int pollingIntervalSeconds = 5,
        string providerName = "QR Payment")
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        _orderId = orderId ?? throw new ArgumentNullException(nameof(orderId));
        _amount = amount;
        _description = description ?? string.Empty;
        _timeoutSeconds = Math.Max(30, timeoutSeconds);
        _pollingIntervalSeconds = Math.Max(1, pollingIntervalSeconds);
        _providerName = providerName;
        _secondsRemaining = _timeoutSeconds;

        InitializeComponent();
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        await CreatePaymentAndStartPolling();
    }

    private async Task CreatePaymentAndStartPolling()
    {
        _cancellationSource?.Cancel();
        _cancellationSource?.Dispose();
        _cancellationSource = new CancellationTokenSource();
        var ct = _cancellationSource.Token;

        SetStatus("Creating payment request...", System.Drawing.Color.Gray);

        var result = await _provider.CreatePaymentAsync(_orderId, _amount, _description, ct);

        if (!result.Success)
        {
            SetStatus($"Payment error: {result.ErrorMessage ?? "Unknown"}", System.Drawing.Color.Red);
            return;
        }

        _currentTransactionId = result.TransactionId;

        FileLogger.Info(
            $"Payment created: transaction={_currentTransactionId}, order={_orderId}, amount={_amount:N0} VND, provider={_providerName}");

        // Display QR code
        if (!string.IsNullOrWhiteSpace(result.QrCodeData))
        {
            try
            {
                var bytes = Convert.FromBase64String(result.QrCodeData);
                using var ms = new System.IO.MemoryStream(bytes);
                this.Invoke(() =>
                {
                    picQrCode.Image?.Dispose();
                    picQrCode.Image = System.Drawing.Image.FromStream(ms);
                });
            }
            catch
            {
                SetStatus("Could not display QR code.", System.Drawing.Color.Red);
            }
        }

        // Update display
        this.Invoke(() =>
        {
            lblOrderId.Text = $"Order: {_orderId}";
            lblProvider.Text = $"Provider: {_providerName}";
            lblAmount.Text = $"{_amount:N0} ₫";
        });

        // Reset countdown
        _secondsRemaining = _timeoutSeconds;
        UpdateCountdownLabel();

        // Start countdown timer (ticks every 1 second)
        _countdownTimer?.Dispose();
        _countdownTimer = new System.Windows.Forms.Timer { Interval = 1000 };
        _countdownTimer.Tick += CountdownTimer_Tick;
        _countdownTimer.Start();

        // Start polling timer
        _pollingTimer?.Dispose();
        _pollingTimer = new System.Windows.Forms.Timer { Interval = _pollingIntervalSeconds * 1000 };
        _pollingTimer.Tick += async (_, _) => await PollStatusAsync();
        _pollingTimer.Start();

        // Immediate first poll
        await PollStatusAsync();

        SetStatus("Waiting for payment...", System.Drawing.Color.FromArgb(33, 150, 243));
    }

    private async Task PollStatusAsync()
    {
        if (_cancellationSource?.IsCancellationRequested != false)
            return;

        if (string.IsNullOrWhiteSpace(_currentTransactionId))
            return;

        try
        {
            var result = await _provider.QueryStatusAsync(_currentTransactionId, _cancellationSource.Token);

            switch (result.Status)
            {
                case PaymentStatus.Paid:
                    FileLogger.Info($"Payment confirmed: transaction={_currentTransactionId}");
                    OnPaymentConfirmed();
                    break;
                case PaymentStatus.Cancelled:
                    SetStatus("Payment was cancelled.", System.Drawing.Color.Red);
                    StopTimers();
                    break;
                case PaymentStatus.Failed:
                    SetStatus($"Payment failed: {result.ErrorMessage ?? "Unknown error"}", System.Drawing.Color.Red);
                    StopTimers();
                    break;
                case PaymentStatus.Expired:
                    SetStatus("Payment timed out.", System.Drawing.Color.Red);
                    StopTimers();
                    break;
                // Pending: do nothing, keep polling
            }
        }
        catch (OperationCanceledException)
        {
            // Expected when user clicks Cancel
        }
        catch (Exception ex)
        {
            FileLogger.Error($"Polling error for transaction {_currentTransactionId}", ex);
            SetStatus($"Polling error: {ex.Message}", System.Drawing.Color.Red);
        }
    }

    private void OnPaymentConfirmed()
    {
        _finalTransactionId = _currentTransactionId;
        StopTimers();

        this.Invoke(() =>
        {
            lblStatus.Text = "Status: ✓ Confirmed";
            lblStatus.ForeColor = System.Drawing.Color.Green;
            btnCancel.Enabled = false;
            btnRefresh.Enabled = false;

            Task.Delay(800).ContinueWith(_ =>
            {
                this.Invoke(() =>
                {
                    DialogResult = DialogResult.OK;
                    Close();
                });
            });
        });
    }

    private void CountdownTimer_Tick(object? sender, EventArgs e)
    {
        _secondsRemaining--;
        UpdateCountdownLabel();

        if (_secondsRemaining <= 0)
        {
            StopTimers();
            SetStatus("Payment expired.", System.Drawing.Color.Red);
        }
    }

    private void UpdateCountdownLabel()
    {
        this.Invoke(() =>
        {
            var mins = _secondsRemaining / 60;
            var secs = _secondsRemaining % 60;
            lblCountdown.Text = $"Time remaining: {mins:D2}:{secs:D2}";

            if (_secondsRemaining <= 60)
            {
                lblCountdown.ForeColor = System.Drawing.Color.Red;
            }
        });
    }

    private void SetStatus(string text, System.Drawing.Color color)
    {
        this.Invoke(() =>
        {
            lblStatus.Text = $"Status: {text}";
            lblStatus.ForeColor = color;
        });
    }

    private void StopTimers()
    {
        _countdownTimer?.Stop();
        _pollingTimer?.Stop();
    }

    private void btnRefresh_Click(object? sender, EventArgs e)
    {
        StopTimers();
        _ = CreatePaymentAndStartPolling();
    }

    private void btnCancel_Click(object? sender, EventArgs e)
    {
        StopTimers();
        _cancellationSource?.Cancel();
        DialogResult = DialogResult.Cancel;
        Close();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        StopTimers();
        _cancellationSource?.Cancel();
        base.OnFormClosing(e);
    }
}
