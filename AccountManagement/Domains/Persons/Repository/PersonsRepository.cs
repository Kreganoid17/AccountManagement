using AccountManagement.Configuration;
using AccountManagement.Domains.Persons.Models;
using AccountManagement.Domains.Persons.Services;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace AccountManagement.Domains.Persons.Repository;

public class PersonsRepository(IOptionsSnapshot<ConnectionStringOptions> connectionStrings,
                               IOptionsSnapshot<StoredProcedureOptions> storedProcedures) : IPersonsRepository
{
    public async Task<List<PersonsModel>?> GetAllPersons()
    {
        try 
        {
            await using var sqlConnection = new SqlConnection(connectionStrings.Value.DbConnection);

            var persons = await sqlConnection.QueryAsync<PersonsModel>(
                sql: storedProcedures.Value.GetAllPersons,
                commandType: CommandType.StoredProcedure);

            return persons.ToList();
        }
        catch (Exception ex) 
        {
            return null;
        }
    }
}
