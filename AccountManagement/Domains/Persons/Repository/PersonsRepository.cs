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
    public async Task<bool> CreateAsync(PersonsModel personModel)
    {
        logger.LogInformation("Repository => Attempting to create a person");

        try
        {
            var param = new DynamicParameters();

            param.Add(name: "@name", value: personModel.name, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(name: "@surname", value: personModel.surname, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(name: "@id_number", value: personModel.id_number, dbType: DbType.String, direction: ParameterDirection.Input);

            await using var sqlConnection = new SqlConnection(connectionStrings.Value.DbConnection);

            var persons = await sqlConnection.ExecuteAsync(
                sql: storedProcedures.Value.InsertNewPerson,
                param: param,
                commandType: CommandType.StoredProcedure);

            logger.LogInformation("{Announcement}: Attempt to create a person was successful.",
                                    Constants.Succeeded);

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Announcement}: Attempt to create a person was unsuccessful.",
                            Constants.Failed);
            return false;
        }
    }

    public async Task<List<PersonsModel>?> RetrieveAllAsync()
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
