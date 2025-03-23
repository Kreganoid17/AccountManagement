using AccountManagement.Configuration;
using AccountManagement.Domains.Accounts.Services;
using AccountManagment.Libraries.Shared.Constants;
using AccountManagment.Libraries.Shared.Domains.Accounts.Models;
using HttpClientLibrary.HttpClientService;
using Microsoft.Extensions.Options;

namespace AccountManagement.Domains.Accounts.Repository;

public class AccountsRepository(IOptionsSnapshot<ApiEndpointsConfiguration> apiEndpoints,
                                IHttpClientHelper client,
                                ILogger<AccountsRepository> logger) : IAccountsRepository
{
    public async Task<bool> CreateAccountAsync(AccountsModel accountModel)
    {
        logger.LogInformation("Repository => Attempting to create a account for person with code: {personCode}", accountModel.person_code);

        try
        {
            var url = apiEndpoints.Value.CreateAccountEnpoint;

            var response = await client.HttpPostAsync(url, accountModel);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Announcement}: Attempt to create a account for person with code: {personCode} was unsuccessful.",
                            LoggerConstants.Failed,
                            accountModel.person_code);
            return false;
        }
    }

    public async Task<bool> DeleteAccountByAccountCodeAsync(int accountCode)
    {
        logger.LogInformation("Repository => Attempting to delete an account with code: {accountCode}", accountCode);

        try
        {
            var url = apiEndpoints.Value.DeleteAccountEndpoint.Replace("{accountCode}", accountCode.ToString());

            var response = await client.HttpDeleteAsync(url);

            if (response.IsSuccessStatusCode) 
            {
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Announcement}: Attempt to delete an account with code: {accountCode} was unsuccessful.",
                            LoggerConstants.Failed,
                            accountCode);
            return false;
        }
    }

    public async Task<AccountsModel?> RetrieveAccountByAccountCodeAsync(int accountCode)
    {
        logger.LogInformation("Repository => Attempting to retrieve account with Code: {accontCode} details", accountCode);

        try
        {
            var url = apiEndpoints.Value.GetAccountByAccountCodeEndpoint.Replace("{accountCode}", accountCode.ToString());

            var account = await client.HttpGetByIdAsync<AccountsModel>(url);

            logger.LogInformation("{Announcement}: Attempt to retrieve account with Code: {accontCode} was successful.",
                                    LoggerConstants.Succeeded,
                                    accountCode);

            return account;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Announcement}: Attempt to retrieve account with Code: {accontCode} was unsuccessful.",
                            LoggerConstants.Failed,
                            accountCode);
            return null;
        }
    }

    public async Task<List<AccountsModel>?> RetrieveAllAccountsByPersonsCode(int personCode)
    {
        logger.LogInformation("Repository => Attempting to retrieve all account data for person with code: {personCode}", personCode);

        try 
        {
            var url = apiEndpoints.Value.GetAccountsByPersonCodeEndpoint.Replace("{personCode}", personCode.ToString());

            var accounts = await client.HttpGetAllAsync<AccountsModel>(url);

            logger.LogInformation("{Announcement}: Attempt to retrieve all account data for person with code: {personCode} was successful.", 
                                    LoggerConstants.Succeeded, 
                                    personCode);

            return accounts;

        }catch (Exception ex) 
        {
            logger.LogError(ex, "{Announcement}: Attempt to retrieve all account data for person with code: {personCode} was unsuccessful.", 
                LoggerConstants.Failed,
                personCode);
            return null;
        }
    }
}
