using AccountManagementAPI.Configuration;
using AccountManagementAPI.Domains.Persons.Services;
using AccountManagment.Libraries.Shared.Constants;
using AccountManagment.Libraries.Shared.Domains.Persons.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace AccountManagementAPI.Domains.Persons.Repository;

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

            await sqlConnection.ExecuteAsync(
            sql: storedProcedures.Value.InsertNewPerson,
            param: param,
            commandType: CommandType.StoredProcedure);

            logger.LogInformation("{Announcement}: Attempt to create a person was successful.",
                                    LoggerConstants.Succeeded);

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Announcement}: Attempt to create a person was unsuccessful.",
                             LoggerConstants.Failed);
            return false;
        }
    }

    public async Task<List<PersonsModel>?> RetreiveAllAsync() 
    {
        logger.LogInformation("Repository => Attempting to retrieve all persons details");

        try
        {
            await using var sqlConnection = new SqlConnection(connectionStrings.Value.DbConnection);

            var persons = await sqlConnection.QueryAsync<PersonsModel>(
                sql: storedProcedures.Value.GetAllPersons,
                commandType: CommandType.StoredProcedure);

            logger.LogInformation("{Announcement}: Attempt to retrieve all persons details was successful.",
                                    LoggerConstants.Succeeded);

            return persons.ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Announcement}: Attempt to retrieve all persons details was unsuccessful.",
                            LoggerConstants.Failed);
            return null;
        }
    }

    public async Task<bool> UpdateAsync(PersonsModel personModel)
    {
        logger.LogInformation("Repository => Attempting to update a person with code: {personCode}", personModel.code);

        try
        {
            var param = new DynamicParameters();

            param.Add(name: "@code", value: personModel.code, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(name: "@name", value: personModel.name, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(name: "@surname", value: personModel.surname, dbType: DbType.String, direction: ParameterDirection.Input);
            param.Add(name: "@id_number", value: personModel.id_number, dbType: DbType.String, direction: ParameterDirection.Input);

            await using var sqlConnection = new SqlConnection(connectionStrings.Value.DbConnection);

            await sqlConnection.ExecuteAsync(
            sql: storedProcedures.Value.UpdatePerson,
            param: param,
            commandType: CommandType.StoredProcedure);

            logger.LogInformation("{Announcement}: Attempt to update a person with code: {personCode} was successful.",
                                    LoggerConstants.Succeeded,
                                    personModel.code);

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Announcement}: Attempt to update a person with code: {personCode} was unsuccessful.",
                            LoggerConstants.Failed,
                            personModel.code);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int personCode)
    {
        logger.LogInformation("Repository => Attempting to delete a person with code: {personCode}", personCode);

        try
        {
            var param = new DynamicParameters();

            param.Add(name: "@person_code", value: personCode, dbType: DbType.Int64, direction: ParameterDirection.Input);

            await using var sqlConnection = new SqlConnection(connectionStrings.Value.DbConnection);

            await sqlConnection.ExecuteAsync(
            sql: storedProcedures.Value.DeletePerson,
            param: param,
            commandType: CommandType.StoredProcedure);

            logger.LogInformation("{Announcement}: Attempt to delete a person with code: {personCode} was successful.",
                                    LoggerConstants.Succeeded,
                                    personCode);

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Announcement}: Attempt to delete a person with code: {personCode} was unsuccessful.",
                            LoggerConstants.Failed,
                            personCode);
            return false;
        }
    }

}
