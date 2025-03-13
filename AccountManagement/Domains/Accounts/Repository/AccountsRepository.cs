using AccountManagement.Configuration;
using AccountManagement.Domains.Accounts.Models;
using AccountManagement.Domains.Accounts.Services;
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
        public async Task<List<AccountsModel>?> GetAllAccountsByPersonsId(int personCode)
        {
            logger.LogInformation("Repository => Attempting to retrieve all account data for person with code: {personCode}", personCode);

            try 
            {
                await using var sqlConnection = new SqlConnection(connectionStrings.Value.DbConnection);

                var param = new DynamicParameters();

                param.Add("@person_code", personCode, DbType.Int64, ParameterDirection.Input);

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
