using BookStoreApp.DAL.Interfaces;
using BookStoreApp.DAL.Repositories;
using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public class AuthService : IAuthService
{
    private readonly IAccountRepository _accountRepository;

    public AuthService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public Account? Login(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return null;
        }

        var account = _accountRepository.GetByUsername(username);
        if (account is null)
        {
            return null;
        }

        return string.Equals(account.Password, password, StringComparison.Ordinal)
            ? account
            : null;
    }
}
