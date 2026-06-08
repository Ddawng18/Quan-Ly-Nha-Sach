using Microsoft.Data.SqlClient;

namespace BookStoreApp.DAL;

public static class DbConnectionFactory
{
    private static string _connectionString =
    @"Server=localhost;Database=BookStoreDb;Trusted_Connection=True;TrustServerCertificate=True;";

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
