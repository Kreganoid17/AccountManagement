using AccountManagement.Configuration;
using AccountManagement.Domains.Accounts.Models;
using AccountManagement.Domains.Persons.Models;
using AccountManagement.Domains.Transactions.Models;
using AccountManagement.Domains.Transactions.Services;
using AccountManagement.Helpers;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace AccountManagement.Domains.Transactions.Repository;

public class TransactionsRepository(IOptionsSnapshot<ConnectionStringOptions> connectionStrings,
                                    IOptionsSnapshot<StoredProcedureOptions> storedProcedures,
                                    ILogger<TransactionsRepository> logger) : ITransactionsRepository
{
    public async Task<bool> CreateAsync(TransactionsModel transactionModel)
    {
        logger.LogInformation("Repository => Attempting to create a transaction for account with code: {accountCode}", transactionModel.account_code);

        try
        {
            var param = new DynamicParameters();

            param.Add(name: "@account_code", value: transactionModel.account_code, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(name: "@transaction_date", value: transactionModel.transaction_date, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            param.Add(name: "@capture_date", value: transactionModel.capture_date, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            param.Add(name: "@amount", value: transactionModel.amount, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add(name: "@description", value: transactionModel.description, dbType: DbType.String, direction: ParameterDirection.Input);

            await using var sqlConnection = new SqlConnection(connectionStrings.Value.DbConnection);

            await sqlConnection.ExecuteAsync(
            sql: storedProcedures.Value.InsertNewTransaction,
            param: param,
            commandType: CommandType.StoredProcedure);

            logger.LogInformation("{Announcement}: Attempt to create a transaction for account with code: {accountCode} was successful.",
                                    Constants.Succeeded,
                                    transactionModel.account_code);

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Announcement}: Attempt to create a transaction for account with code: {accountCode} unsuccessful.",
                            Constants.Failed,
                            transactionModel.account_code);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int transactionCode)
    {
        logger.LogInformation("Repository => Attempting to delete a transaction with code: {transactionCode}", transactionCode);

        try
        {
            var param = new DynamicParameters();

            param.Add(name: "@transaction_code", value: transactionCode, dbType: DbType.Int64, direction: ParameterDirection.Input);

            await using var sqlConnection = new SqlConnection(connectionStrings.Value.DbConnection);

            await sqlConnection.ExecuteAsync(
            sql: storedProcedures.Value.DeleteTransaction,
            param: param,
            commandType: CommandType.StoredProcedure);

            logger.LogInformation("{Announcement}: Attempt to delete a transaction with code: {transactionCode} was successful.",
                                    Constants.Succeeded,
                                    transactionCode);

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Announcement}: Attempt to delete a transaction with code: {transactionCode} was unsuccessful.",
                            Constants.Failed,
                            transactionCode);
            return false;
        }
    }

    public async Task<List<TransactionsModel>?> RetrieveAllTransactionsByAccountCode(int accountCode)
    {
        logger.LogInformation("Repository => Attempt to retrieve all transactions with account code: {accountCode}", accountCode);

        try 
        {

            using var SqlConnection = new SqlConnection(connectionStrings.Value.DbConnection);

            var param = new DynamicParameters();

            param.Add("@accountCode", accountCode, DbType.Int64, ParameterDirection.Input);

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

    public async Task<TransactionsModel?> RetrieveSingleAsync(int transactionCode)
    {
        logger.LogInformation("Repository => Attempting to retrieve transation with Code: {transactionCode} details", transactionCode);

        try
        {
            var param = new DynamicParameters();

            param.Add(name: "@transaction_code", value: transactionCode, dbType: DbType.Int64, direction: ParameterDirection.Input);

            await using var sqlConnection = new SqlConnection(connectionStrings.Value.DbConnection);

            var transaction = await sqlConnection.QueryFirstOrDefaultAsync<TransactionsModel>(
                param: param,
                sql: storedProcedures.Value.GetTransactionById,
                commandType: CommandType.StoredProcedure);

            logger.LogInformation("{Announcement}: Attempt to retrieve transation with Code: {transactionCode} was successful.",
                                    Constants.Succeeded,
                                    transactionCode);

            return transaction;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Announcement}: Attempt to retrieve transation with Code: {transactionCode} was unsuccessful.",
                            Constants.Failed,
                            transactionCode);
            return null;
        }
    }

    public async Task<bool> UpdateAsync(TransactionsModel transactionModel)
    {
        logger.LogInformation("Repository => Attempting to update a transaction for account with code: {accountCode}", transactionModel.account_code);

        try
        {
            var param = new DynamicParameters();

            param.Add(name: "@transaction_code", value: transactionModel.code, dbType: DbType.Int64, direction: ParameterDirection.Input);
            param.Add(name: "@transaction_date", value: transactionModel.transaction_date, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            param.Add(name: "@capture_date", value: transactionModel.capture_date, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            param.Add(name: "@amount", value: transactionModel.amount, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            param.Add(name: "@description", value: transactionModel.description, dbType: DbType.String, direction: ParameterDirection.Input);

            await using var sqlConnection = new SqlConnection(connectionStrings.Value.DbConnection);

            await sqlConnection.ExecuteAsync(
            sql: storedProcedures.Value.UpdateTransaction,
            param: param,
            commandType: CommandType.StoredProcedure);

            logger.LogInformation("{Announcement}: Attempt to update a transaction for account with code: {accountCode} was successful.",
                                    Constants.Succeeded,
                                    transactionModel.account_code);

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Announcement}: Attempt to update a transaction for account with code: {accountCode} unsuccessful.",
                            Constants.Failed,
                            transactionModel.account_code);
            return false;
        }
    }
}
