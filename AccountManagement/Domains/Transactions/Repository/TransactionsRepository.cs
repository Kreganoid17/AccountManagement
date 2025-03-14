using AccountManagement.Configuration;
using AccountManagement.Domains.Transactions.Models;
using AccountManagement.Domains.Transactions.Services;
using AccountManagement.Helpers;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace AccountManagement.Domains.Transactions.Repository
{
    public class TransactionsRepository(IOptionsSnapshot<ConnectionStringOptions> connectionStrings,
                                        IOptionsSnapshot<StoredProcedureOptions> storedProcedures,
                                        ILogger<TransactionsRepository> logger) : ITransactionsRepository
    {
        public async Task<List<TransactionsModel>?> RetrieveAllTransactionsByAccountCode(int accountCode)
        {
            logger.LogInformation("Repository => Attempt to retrieve all transactions with account code: {accountCode}", accountCode);

            try 
            {

                using var SqlConnection = new SqlConnection(connectionStrings.Value.DbConnection);

                var param = new DynamicParameters();

                param.Add("@account_Code", accountCode, DbType.Int64, ParameterDirection.Input);

                var transactions = await SqlConnection.QueryAsync<TransactionsModel>(
                    sql: storedProcedures.Value.GetAllTransactionsByAccountCode,
                    param: param,
                    commandType: CommandType.StoredProcedure);

                logger.LogInformation("{Announcement}: Attempt to retrieve all transactions with account code: {accountCode} was successful.",
                                        Constants.Succeeded,
                                        accountCode);

                return transactions.ToList();

            }catch (Exception ex) 
            {
                logger.LogWarning(ex, "{Announcement}: Attempt to retrieve all transactions with account code: {accountCode} was unsuccessful.",
                                    Constants.Failed,
                                    accountCode);
                return null;
            }
        }
    }
}
