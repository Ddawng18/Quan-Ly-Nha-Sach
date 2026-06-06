using Microsoft.Data.SqlClient;

namespace BookStoreApp.DAL;

/// <summary>
/// Cung cấp SqlConnection từ connection string trong appsettings.json.
/// Thêm NuGet: Microsoft.Data.SqlClient
/// </summary>
public static class DbConnectionFactory
{
    // Connection string cho SQL Server LocalDB
    // (localdb)\MSSQLLocalDB là instance mặc định khi cài Visual Studio
    private static string _connectionString =
        @"Server=localhost;Database=QuanLyNhaSach;Trusted_Connection=True;TrustServerCertificate=True;";

    public static void Configure(string connectionString)
    {
        _connectionString = connectionString;
    }

    public static SqlConnection Create()
    {
        return new SqlConnection(_connectionString);
    }
}
