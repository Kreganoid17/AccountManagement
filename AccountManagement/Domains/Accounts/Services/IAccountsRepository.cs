using AccountManagement.Domains.Accounts.Models;

namespace AccountManagement.Domains.Accounts.Services;

public interface IAccountsRepository
{
    Task<List<AccountsModel>?> RetrieveAllAccountsByPersonsId(int personId);

    Task<AccountsModel?> RetrieveSingleAsync(int accountCode);

    Task<bool> CreateAsync(AccountsModel account);

    Task<bool> DeleteAsync(int accountCode);
}
