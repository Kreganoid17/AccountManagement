using AccountManagment.Libraries.Shared.Domains.Accounts.Models;

namespace AccountManagementAPI.Domains.Accounts.Services;

public interface IAccountsRepository
{
    Task<List<AccountsModel>?> RetrieveAllAsync(int personCode);

    Task<bool> CreateAsync(AccountsModel model);

    Task<bool> DeleteAsync(int accountCode);

    Task<AccountsModel?> RetrieveSingleAsync(int accountCode);
}
