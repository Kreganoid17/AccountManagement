using AccountManagement.Configuration;
using AccountManagement.Domains.Persons.Models;
using AccountManagement.Domains.Persons.Services;
using AccountManagement.Helpers;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace AccountManagement.Domains.Persons.Repository;

public class PersonsRepository(IOptionsSnapshot<ConnectionStringOptions> connectionStrings,
                               IOptionsSnapshot<StoredProcedureOptions> storedProcedures,
                               ILogger<PersonsRepository> logger) : IPersonsRepository
{
    public async Task<List<PersonsModel>?> GetAllPersons()
    {
        logger.LogInformation("Repository => Attempting to retrieve all persons details");

        try 
        {
            await using var sqlConnection = new SqlConnection(connectionStrings.Value.DbConnection);

            var persons = await sqlConnection.QueryAsync<PersonsModel>(
                sql: storedProcedures.Value.GetAllPersons,
                commandType: CommandType.StoredProcedure);

            logger.LogInformation("{Announcement}: Attempt to retrieve all persons details was successful.", 
                                    Constants.Succeeded);

            return persons.ToList();
        }
        catch (Exception ex) 
        {
            logger.LogError(ex, "{Announcement}: Attempt to retrieve all persons details was unsuccessful.", 
                            Constants.Failed);
            return null;
        }
    }
}
