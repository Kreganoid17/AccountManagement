using AccountManagementAPI.Configuration;
using AccountManagementAPI.Domains.Accounts.Services;
using AccountManagment.Libraries.Shared.Constants;
using AccountManagment.Libraries.Shared.Domains.Accounts;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace AccountManagementAPI.Domains.Accounts.Repository
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
                param.Add(name: "@outstanding_balance", value: accountModel.outstanding_balance, dbType: DbType.Decimal, direction: ParameterDirection.Input);

                await using var sqlConnection = new SqlConnection(connectionStrings.Value.DbConnection);

                await sqlConnection.ExecuteAsync(
                sql: storedProcedures.Value.InsertNewAccount,
                param: param,
                commandType: CommandType.StoredProcedure);

                logger.LogInformation("{Announcement}: Attempt to create a account for person with code: {personCode} was successful.",
                                        LoggerConstants.Succeeded,
                                        accountModel.person_code);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Announcement}: Attempt to create a account for person with code: {personCode} was unsuccessful.",
                                LoggerConstants.Failed,
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

                await sqlConnection.ExecuteAsync(
                sql: storedProcedures.Value.DeleteAccount,
                param: param,
                commandType: CommandType.StoredProcedure);

                logger.LogInformation("{Announcement}: Attempt to delete an account with code: {accountCode} was successful.",
                                        LoggerConstants.Succeeded,
                                        accountCode);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Announcement}: Attempt to delete an account with code: {accountCode} was unsuccessful.",
                                LoggerConstants.Failed,
                                accountCode);
                return false;
            }
        }

        public async Task<List<AccountsModel>?> RetrieveAllAsync(int personCode)
        {
            logger.LogInformation("Repository => Attempting to retrieve account with person code: {personCode}", personCode);

            try
            {
                var param = new DynamicParameters();

                param.Add(name: "@person_code", value: personCode, dbType: DbType.Int64, direction: ParameterDirection.Input);

                await using var sqlConnection = new SqlConnection(connectionStrings.Value.DbConnection);

                var accounts = await sqlConnection.QueryAsync<AccountsModel>(
                    param: param,
                    sql: storedProcedures.Value.GetAllAccountsByPersonCode,
                    commandType: CommandType.StoredProcedure);

                logger.LogInformation("{Announcement}: Attempt to retrieve account with person code: {personCode} was successful.",
                                        LoggerConstants.Succeeded,
                                        personCode);

                return accounts.ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Announcement}: Attempt to retrieve account with person code: {personCode} was unsuccessful.",
                                LoggerConstants.Failed,
                                personCode);
                return null;
            }
        }

        public async Task<AccountsModel?> RetrieveSingleAsync(int accountCode)
        {
            logger.LogInformation("Repository => Attempting to retrieve account with Code: {accontCode} details", accountCode);

            try
            {
                var param = new DynamicParameters();

                param.Add(name: "@account_code", value: accountCode, dbType: DbType.Int64, direction: ParameterDirection.Input);

                await using var sqlConnection = new SqlConnection(connectionStrings.Value.DbConnection);

                var account = await sqlConnection.QueryFirstOrDefaultAsync<AccountsModel>(
                    param: param,
                    sql: storedProcedures.Value.GetAccountById,
                    commandType: CommandType.StoredProcedure);

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
    }
}
