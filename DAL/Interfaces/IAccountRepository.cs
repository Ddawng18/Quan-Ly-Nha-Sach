using BookStoreApp.DTO;

namespace BookStoreApp.DAL.Interfaces;

public interface IAccountRepository
{
    Account? GetByUsername(string username);
}
