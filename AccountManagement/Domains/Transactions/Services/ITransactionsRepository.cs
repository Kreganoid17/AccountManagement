using AccountManagment.Libraries.Shared.Domains.Transactions.Models;

namespace AccountManagement.Domains.Transactions.Services;

public interface ITransactionsRepository
{
    Task<List<TransactionsModel>?> RetrieveAllTransactionsByAccountCode(int accountCode);

    Task<TransactionsModel?> RetrieveTransactionByTransactionCodeAsync(int transactionCode);

    Task<bool> CreateTransactionAsync(TransactionsModel transactionModel);

    Task<bool> UpdateTransactionAsync(TransactionsModel transactionModel);

    Task<bool> DeleteTransactionAsync(int transactionCode);
}
