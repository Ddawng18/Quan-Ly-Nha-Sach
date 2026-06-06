using System.Text.Json;
using BookStoreApp.DAL;
using BookStoreApp.DTO.Payments;
using BookStoreApp.Forms;

namespace BookStoreApp;

static class Program
{
    /// <summary>
    /// Payment configuration loaded from appsettings.json at startup.
    /// </summary>
    public static PaymentConfig PaymentConfig { get; } = LoadPaymentConfig();

    [STAThread]
    static void Main()
    {
        // Đọc connection string và cấu hình DbConnectionFactory
        // trước khi ServiceLocator khởi tạo bất kỳ Repository nào
        ConfigureDatabase();

        ApplicationConfiguration.Initialize();
        Application.Run(new LoginForm());
    }

    private static void ConfigureDatabase()
    {
        try
        {
            var path = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            if (!File.Exists(path)) return;

            var json = File.ReadAllText(path);
            using var doc = JsonDocument.Parse(json);

            if (doc.RootElement.TryGetProperty("ConnectionStrings", out var cs) &&
                cs.TryGetProperty("DefaultConnection", out var connStr))
            {
                DbConnectionFactory.Configure(connStr.GetString()!);
            }
        }
        catch
        {
            // Nếu đọc config thất bại, dùng connection string mặc định trong DbConnectionFactory
        }
    }

    private static PaymentConfig LoadPaymentConfig()
    {
        try
        {
            var path = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            if (!File.Exists(path)) return new PaymentConfig();

            var json = File.ReadAllText(path);
            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("Payment", out var paymentElement))
            {
                return paymentElement.Deserialize<PaymentConfig>(
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                    ?? new PaymentConfig();
            }

            return new PaymentConfig();
        }
        catch
        {
            return new PaymentConfig();
        }
    }
}
