using AccountManagement.Configuration;
using AccountManagement.Domains.Accounts.Models;
using AccountManagement.Domains.Accounts.Services;
using AccountManagement.Domains.Persons.Models;
using AccountManagement.Helpers;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace AccountManagement.Domains.Accounts.Repository
{
    public class AccountsRepository(IOptionsSnapshot<ConnectionStringOptions> connectionStrings,
                                    IOptionsSnapshot<StoredProcedureOptions> storedProcedures,
                                    ILogger<AccountsRepository> logger) : IAccountsRepository
    {
        public async Task<bool> CreateAsync(AccountsModel accountModel)
        {
            logger.LogInformation("Repository => Attempting to create a account for person with code: {personCode}", accountModel.person_code);

            try
            {
                var param = new DynamicParameters();

                param.Add(name: "@person_code", value: accountModel.person_code, dbType: DbType.Int64, direction: ParameterDirection.Input);
                param.Add(name: "@account_number", value: accountModel.account_number, dbType: DbType.Int64, direction: ParameterDirection.Input);
                param.Add(name: "@outstanding_balance", value: accountModel.outstanding_balance, dbType: DbType.Double, direction: ParameterDirection.Input);

                await using var sqlConnection = new SqlConnection(connectionStrings.Value.DbConnection);

                var accounts = await sqlConnection.ExecuteAsync(
                    sql: storedProcedures.Value.InsertNewAccount,
                    param: param,
                    commandType: CommandType.StoredProcedure);

                logger.LogInformation("{Announcement}: Attempt to create a account for person with code: {personCode} was successful.",
                                        Constants.Succeeded,
                                        accountModel.person_code);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Announcement}: Attempt to create a account for person with code: {personCode} was unsuccessful.",
                                Constants.Failed,
                                accountModel.person_code);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int accountCode)
        {
            logger.LogInformation("Repository => Attempting to delete an account with code: {accountCode}", accountCode);

            try
            {
                var param = new DynamicParameters();

                param.Add(name: "@account_code", value: accountCode, dbType: DbType.Int64, direction: ParameterDirection.Input);

                await using var sqlConnection = new SqlConnection(connectionStrings.Value.DbConnection);

                var accounts = await sqlConnection.ExecuteAsync(
                    sql: storedProcedures.Value.DeleteAccount,
                    param: param,
                    commandType: CommandType.StoredProcedure);

                logger.LogInformation("{Announcement}: Attempt to delete an account with code: {accountCode} was successful.",
                                        Constants.Succeeded,
                                        accountCode);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Announcement}: Attempt to delete an account with code: {accountCode} was unsuccessful.",
                                Constants.Failed,
                                accountCode);
                return false;
            }
        }

        public async Task<List<AccountsModel>?> RetrieveAllAccountsByPersonsId(int personCode)
        {
            logger.LogInformation("Repository => Attempting to retrieve all account data for person with code: {personCode}", personCode);

            try 
            {
                await using var sqlConnection = new SqlConnection(connectionStrings.Value.DbConnection);

                var param = new DynamicParameters();

                param.Add(name: "@person_code", value: personCode, dbType: DbType.Int64, direction: ParameterDirection.Input);

                var accounts = await sqlConnection.QueryAsync<AccountsModel>(
                    sql: storedProcedures.Value.GetAllAccountsByPersonCode,
                    param: param,
                    commandType: CommandType.StoredProcedure);

                logger.LogInformation("{Announcement}: Attempt to retrieve all account data for person with code: {personCode} was successful.", 
                                        Constants.Succeeded, 
                                        personCode);

                return accounts.ToList();

            }catch (Exception ex) 
            {
                logger.LogError(ex, "{Announcement}: Attempt to retrieve all account data for person with code: {personCode} was unsuccessful.", 
                    Constants.Failed,
                    personCode);
                return null;
            }
        }
    }
}
