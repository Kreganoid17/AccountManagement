using AccountManagement.Domains.Transactions.Models;

namespace AccountManagement.Domains.Transactions.Services
{
    public interface ITransactionsRepository
    {
        Task<List<TransactionsModel>?> RetrieveAllTransactionsByAccountCode(int accountCode);
    }
}
