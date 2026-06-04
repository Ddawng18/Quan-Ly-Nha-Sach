using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DTO;
using BookStoreApp.Utilities;

namespace BookStoreApp.DAL.Repositories;

public class AccountRepository : IAccountRepository
{
    public Account? GetByUsername(string username) =>
        FakeDatabase.Accounts.FirstOrDefault(a =>
            a.IsActive &&
            string.Equals(a.Username, username.Trim(), StringComparison.OrdinalIgnoreCase));
}
