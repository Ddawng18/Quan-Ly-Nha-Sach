using Microsoft.Data.SqlClient;

namespace BookStoreApp.DAL;

/// <summary>
/// Cung cấp SqlConnection từ connection string.
/// Thêm NuGet: Microsoft.Data.SqlClient
/// </summary>
public static class DbConnectionFactory
{
    // Tăng Connection Timeout lên 60s để tránh timeout khi server khởi động chậm.
    // Nếu dùng SQL Server Express/LocalDB thì giữ (localdb)\MSSQLLocalDB.
    // Nếu dùng SQL Server đầy đủ thì giữ localhost hoặc tên instance.
    private static string _connectionString =
    @"Server=localhost;Database=BookStoreDb;Trusted_Connection=True;TrustServerCertificate=True;";
    /// <summary>Command timeout (giây) áp dụng cho mọi SqlCommand tạo từ factory này.</summary>
    public static int CommandTimeout { get; set; } = 60;

    public static void Configure(string connectionString)
    {
        _connectionString = connectionString;
    }

    public static SqlConnection Create()
    {
        return new SqlConnection(_connectionString);
    }
}
