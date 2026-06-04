using System.Security.Cryptography;
using System.Text;

// System.Drawing is Windows-only; this provider is only used in the WinForms app
#pragma warning disable CA1416

namespace BookStoreApp.BLL.Payments;

/// <summary>
/// Demo payment provider that returns a programmatically generated QR image
/// and simulates payment confirmation after 10 seconds. Used when no real
/// payment credentials are configured.
/// </summary>
public class DemoPaymentProvider : IPaymentProvider
{
    private readonly Dictionary<string, DateTime> _transactions = new();
    private readonly object _lock = new();

    public Task<DTO.Payments.PaymentCreationResult> CreatePaymentAsync(
        string orderId,
        decimal amount,
        string description,
        CancellationToken cancellationToken = default)
    {
        var transactionId = $"DEMO-{Guid.NewGuid():N}"[..20];

        lock (_lock)
        {
            _transactions[transactionId] = DateTime.UtcNow;
        }

        var qrBase64 = GenerateDemoQrCode(orderId, amount);

        var result = new DTO.Payments.PaymentCreationResult(
            Success: true,
            TransactionId: transactionId,
            QrCodeData: qrBase64,
            ErrorMessage: null);

        return Task.FromResult(result);
    }

    public Task<DTO.Payments.PaymentStatusResult> QueryStatusAsync(
        string transactionId,
        CancellationToken cancellationToken = default)
    {
        DateTime created;
        lock (_lock)
        {
            if (!_transactions.TryGetValue(transactionId, out created))
            {
                return Task.FromResult(new DTO.Payments.PaymentStatusResult(
                    DTO.Payments.PaymentStatus.Failed,
                    "Transaction not found."));
            }
        }

        var elapsed = DateTime.UtcNow - created;
        var status = elapsed.TotalSeconds >= 10
            ? DTO.Payments.PaymentStatus.Paid
            : DTO.Payments.PaymentStatus.Pending;

        return Task.FromResult(new DTO.Payments.PaymentStatusResult(status, null));
    }

    /// <summary>
    /// Generates a simple QR-code-like bitmap as a base64-encoded PNG string.
    /// </summary>
    private static string GenerateDemoQrCode(string orderId, decimal amount)
    {
        const int size = 250;
        const int moduleCount = 21;
        const int moduleSize = size / moduleCount;
        const int offset = (size - moduleCount * moduleSize) / 2;

        using var bitmap = new System.Drawing.Bitmap(size, size);
        using var g = System.Drawing.Graphics.FromImage(bitmap);
        g.Clear(System.Drawing.Color.White);

        var rng = new Random(orderId.GetHashCode(StringComparison.Ordinal));
        using var blackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        using var blueBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(33, 150, 243));

        // Draw position detection patterns (top-left, top-right, bottom-left)
        DrawFinderPattern(g, offset, offset, moduleSize);
        DrawFinderPattern(g, offset + (moduleCount - 7) * moduleSize, offset, moduleSize);
        DrawFinderPattern(g, offset, offset + (moduleCount - 7) * moduleSize, moduleSize);

        // Draw random-looking data modules
        for (int row = 0; row < moduleCount; row++)
        {
            for (int col = 0; col < moduleCount; col++)
            {
                // Skip finder pattern areas
                if (IsInFinder(row, col, moduleCount))
                    continue;

                if (rng.Next(0, 100) < 45)
                {
                    var brush = rng.Next(0, 100) < 15 ? blueBrush : blackBrush;
                    g.FillRectangle(brush,
                        offset + col * moduleSize + 1,
                        offset + row * moduleSize + 1,
                        moduleSize - 2,
                        moduleSize - 2);
                }
            }
        }

        // Draw text overlay at bottom
        using var font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
        using var textBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(33, 150, 243));
        var text = $"BOOKSTORE DEMO";
        var textSize = g.MeasureString(text, font);
        var textX = (size - textSize.Width) / 2;

        // White background for text
        g.FillRectangle(System.Drawing.Brushes.White,
            textX - 4, size - 28, textSize.Width + 8, 24);
        g.DrawString(text, font, textBrush, textX, size - 26);

        using var ms = new System.IO.MemoryStream();
        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        return Convert.ToBase64String(ms.ToArray());
    }

    private static void DrawFinderPattern(System.Drawing.Graphics g, int x, int y, int moduleSize)
    {
        var total = 7 * moduleSize;
        using var outerBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        using var innerBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        using var centerBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        g.FillRectangle(outerBrush, x, y, total, total);
        g.FillRectangle(innerBrush, x + moduleSize, y + moduleSize, 5 * moduleSize, 5 * moduleSize);
        g.FillRectangle(centerBrush, x + 2 * moduleSize, y + 2 * moduleSize, 3 * moduleSize, 3 * moduleSize);
    }

    private static bool IsInFinder(int row, int col, int moduleCount)
    {
        // Top-left finder
        if (row < 7 && col < 7) return true;
        // Top-right finder
        if (row < 7 && col >= moduleCount - 7) return true;
        // Bottom-left finder
        if (row >= moduleCount - 7 && col < 7) return true;
        return false;
    }
}
