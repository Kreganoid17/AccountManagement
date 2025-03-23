using AccountManagment.Libraries.Shared.Domains.Accounts;

namespace AccountManagement.Domains.Accounts.Services;

public interface IAccountsRepository
{
    Task<List<AccountsModel>?> RetrieveAllAccountsByPersonsCode(int personId);

    Task<AccountsModel?> RetrieveAccountByAccountCodeAsync(int accountCode);

    Task<bool> CreateAccountAsync(AccountsModel account);

    Task<bool> DeleteAccountByAccountCodeAsync(int accountCode);
}
