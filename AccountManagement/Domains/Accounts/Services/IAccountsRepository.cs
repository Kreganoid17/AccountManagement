using AccountManagement.Domains.Accounts.Models;

namespace AccountManagement.Domains.Accounts.Services
{
    public interface IAccountsRepository
    {
        Task<List<AccountsModel>?> GetAllAccountsByPersonsId(int personId);
    }
}
