using BookStoreApp.DTO;

namespace BookStoreApp.BLL;

public interface IAuthService
{
    Account? Login(string username, string password);
}
