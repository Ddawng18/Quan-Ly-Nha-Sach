using System.Text.Json;
using BookStoreApp.DTO.Payments;
using BookStoreApp.Forms;

namespace BookStoreApp;

static class Program
{
    /// <summary>
    /// Payment configuration loaded from appsettings.json at startup.
    /// </summary>
    public static PaymentConfig PaymentConfig { get; } = LoadPaymentConfig();

    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new LoginForm());
    }

    private static PaymentConfig LoadPaymentConfig()
    {
        try
        {
            var path = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            if (!File.Exists(path))
            {
                return new PaymentConfig();
            }

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
            // If config file is missing or malformed, use defaults (Demo provider)
            return new PaymentConfig();
        }
    }
}
