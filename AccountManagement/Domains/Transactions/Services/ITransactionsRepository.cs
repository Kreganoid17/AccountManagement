using AccountManagement.Domains.Transactions.Models;

namespace AccountManagement.Domains.Transactions.Services;

public interface ITransactionsRepository
{
    Task<List<TransactionsModel>?> RetrieveAllTransactionsByAccountCode(int accountCode);

    Task<TransactionsModel?> RetrieveSingleAsync(int transactionCode);

    Task<bool> CreateAsync(TransactionsModel transactionModel);

    Task<bool> UpdateAsync(TransactionsModel transactionsModel);

    Task<bool> DeleteAsync(int personCode);
}
