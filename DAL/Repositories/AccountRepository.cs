using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DTO;
using Microsoft.Data.SqlClient;

namespace BookStoreApp.DAL.Repositories;

public class AccountRepository : IAccountRepository
{
    public Account? GetByUsername(string username)
    {
        using var conn = DbConnectionFactory.Create();
        conn.Open();
        using var cmd = new SqlCommand(
            "SELECT * FROM Accounts WHERE Username = @u AND IsActive = 1", conn);
        cmd.Parameters.AddWithValue("@u", username);
        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) return null;
        return new Account
        {
            AccountID  = (int)reader["AccountID"],
            EmployeeID = reader["EmployeeID"] == DBNull.Value ? 0 : (int)reader["EmployeeID"],
            Username   = reader["Username"].ToString()!,
            Password   = reader["Password"].ToString()!,
            Role       = reader["Role"].ToString()!,
            FullName   = reader["FullName"].ToString()!,
            IsActive   = (bool)reader["IsActive"]
        };
    }
}
