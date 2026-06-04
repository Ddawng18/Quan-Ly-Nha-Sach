using System.Text;

namespace BookStoreApp.Utilities;

/// <summary>
/// Lightweight rolling file logger. Writes to logs/bookstore-{yyyy-MM-dd}.log.
/// Thread-safe via lock.
/// </summary>
public static class FileLogger
{
    private static readonly object Lock = new();
    private static string? _logDir;

    private static string LogDir
    {
        get
        {
            if (_logDir is null)
            {
                _logDir = Path.Combine(AppContext.BaseDirectory, "logs");
                Directory.CreateDirectory(_logDir);
            }

            return _logDir;
        }
    }

    private static string LogFile =>
        Path.Combine(LogDir, $"bookstore-{DateTime.Now:yyyy-MM-dd}.log");

    public static void Info(string message) => Write("INFO", message);

    public static void Warn(string message) => Write("WARN", message);

    public static void Error(string message, Exception? ex = null)
    {
        var fullMessage = ex is null
            ? message
            : $"{message} | Exception: {ex.GetType().Name}: {ex.Message}";

        Write("ERROR", fullMessage);
    }

    private static void Write(string level, string message)
    {
        var line = $"[{DateTime.Now:HH:mm:ss}] [{level}] {message}";

        lock (Lock)
        {
            try
            {
                File.AppendAllText(LogFile, line + Environment.NewLine, Encoding.UTF8);
            }
            catch
            {
                // Silently ignore logging failures — don't crash the app
            }
        }
    }
}
