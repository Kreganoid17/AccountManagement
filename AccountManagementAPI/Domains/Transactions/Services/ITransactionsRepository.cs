using AccountManagment.Libraries.Shared.Domains.Transactions.Models;

namespace AccountManagementAPI.Domains.Transactions.Services;

public interface ITransactionsRepository
{
    Task<List<TransactionsModel>?> RetrieveAllAsync(int accountCode);

    Task<TransactionsModel?> RetrieveSingleAsync(int transactionCode);

    Task<bool> CreateAsync(TransactionsModel transactionModel);

    Task<bool> UpdateAsync(TransactionsModel transactionsModel);

    Task<bool> DeleteAsync(int personCode);
}
