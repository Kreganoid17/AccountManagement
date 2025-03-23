using AccountManagement.Configuration;
using AccountManagement.Constants;
using AccountManagement.Domains.Transactions.Services;
using AccountManagment.Libraries.Shared.Constants;
using AccountManagment.Libraries.Shared.Domains.Transactions.Models;
using HttpClientLibrary.HttpClientService;
using Microsoft.Extensions.Options;

namespace AccountManagement.Domains.Transactions.Repository;

public class TransactionsRepository(IOptionsSnapshot<ApiEndpointsConfiguration> apiEndpoints,
                                    IHttpClientHelper client,
                                    ILogger<TransactionsRepository> logger) : ITransactionsRepository
{
    public async Task<bool> CreateTransactionAsync(TransactionsModel transactionModel)
    {
        logger.LogInformation("Repository => Attempting to create a transaction for account with code: {accountCode}", transactionModel.account_code);

        try
        {
            switch ((TransactionType)transactionModel.transaction_type) 
            {
                case TransactionType.Debit:
                    transactionModel.amount = transactionModel.amount * -1;
                    break;

                case TransactionType.Credit:
                    break;

                default:
                    return false;
            }

            var url = apiEndpoints.Value.CreateTransactionEndpoint;

            var response = await client.HttpPostAsync(url, transactionModel);

            if (response.IsSuccessStatusCode) 
            {
                return true;
            }

            logger.LogInformation("{Announcement}: Attempt to create a transaction for account with code: {accountCode} was successful.",
                                    LoggerConstants.Succeeded,
                                    transactionModel.account_code);

            return false;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Announcement}: Attempt to create a transaction for account with code: {accountCode} unsuccessful.",
                            LoggerConstants.Failed,
                            transactionModel.account_code);
            return false;
        }
    }

    public async Task<bool> DeleteTransactionAsync(int transactionCode)
    {
        logger.LogInformation("Repository => Attempting to delete a transaction with code: {transactionCode}", transactionCode);

        try
        {
            var url = apiEndpoints.Value.DeleteTransactionEndpoint.Replace("{transactionCode}", transactionCode.ToString());

            var response = await client.HttpDeleteAsync(url);

            if (response.IsSuccessStatusCode) 
            {
                return true;
            }

            logger.LogInformation("{Announcement}: Attempt to delete a transaction with code: {transactionCode} was successful.",
                                    LoggerConstants.Succeeded,
                                    transactionCode);

            return false;
            
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Announcement}: Attempt to delete a transaction with code: {transactionCode} was unsuccessful.",
                            LoggerConstants.Failed,
                            transactionCode);
            return false;
        }
    }

    public async Task<List<TransactionsModel>?> RetrieveAllTransactionsByAccountCode(int accountCode)
    {
        logger.LogInformation("Repository => Attempt to retrieve all transactions with account code: {accountCode}", accountCode);

        try 
        {

            var url = apiEndpoints.Value.GetTransactionsByAccountCodeEndpoint.Replace("{accountCode}", accountCode.ToString());

            var transactions = await client.HttpGetAllAsync<TransactionsModel>(url);

            logger.LogInformation("{Announcement}: Attempt to retrieve all transactions with account code: {accountCode} was successful.",
                                    LoggerConstants.Succeeded,
                                    accountCode);

            return transactions;

        }catch (Exception ex) 
        {
            logger.LogWarning(ex, "{Announcement}: Attempt to retrieve all transactions with account code: {accountCode} was unsuccessful.",
                                LoggerConstants.Failed,
                                accountCode);
            return null;
        }
    }

    public async Task<TransactionsModel?> RetrieveTransactionByTransactionCodeAsync(int transactionCode)
    {
        logger.LogInformation("Repository => Attempting to retrieve transation with Code: {transactionCode} details", transactionCode);

        try
        {
            var url = apiEndpoints.Value.GetTransactionByTransactionCodeEndpoint.Replace("{transactionCode}", transactionCode.ToString());

            var transaction = await client.HttpGetByIdAsync<TransactionsModel>(url);

            logger.LogInformation("{Announcement}: Attempt to retrieve transation with Code: {transactionCode} was successful.",
                                    LoggerConstants.Succeeded,
                                    transactionCode);

            return transaction;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Announcement}: Attempt to retrieve transation with Code: {transactionCode} was unsuccessful.",
                            LoggerConstants.Failed,
                            transactionCode);
            return null;
        }
    }

    public async Task<bool> UpdateTransactionAsync(TransactionsModel transactionModel)
    {
        logger.LogInformation("Repository => Attempting to update a transaction for account with code: {accountCode}", transactionModel.account_code);

        try
        {
            switch ((TransactionType)transactionModel.transaction_type)
            {
                case TransactionType.Debit:
                    transactionModel.amount = transactionModel.amount * -1;
                    break;

                case TransactionType.Credit:
                    break;

                default:
                    return false;
            }

            var url = apiEndpoints.Value.UpdateTransactionEndpoint;

            var response = await client.HttpPutAsync(url, transactionModel);

            if (response.IsSuccessStatusCode) 
            {
                return true;
            }

            logger.LogInformation("{Announcement}: Attempt to update a transaction for account with code: {accountCode} was successful.",
                                    LoggerConstants.Succeeded,
                                    transactionModel.account_code);

            return false;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Announcement}: Attempt to update a transaction for account with code: {accountCode} unsuccessful.",
                            LoggerConstants.Failed,
                            transactionModel.account_code);
            return false;
        }
    }
}
