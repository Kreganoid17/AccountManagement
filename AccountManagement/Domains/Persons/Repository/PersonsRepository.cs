using AccountManagement.Configuration;
using AccountManagement.Domains.Persons.Services;
using AccountManagment.Libraries.Shared.Constants;
using AccountManagment.Libraries.Shared.Domains.Persons.Models;
using HttpClientLibrary.HttpClientService;
using Microsoft.Extensions.Options;

namespace AccountManagement.Domains.Persons.Repository;

public class PersonsRepository(IOptionsSnapshot<ApiEndpointsConfiguration> apiEndpoints,
                               ILogger<PersonsRepository> logger,
                               IHttpClientHelper client) : IPersonsRepository
{
    public async Task<bool> CreatePersonAsync(PersonsModel personModel)
    {
        logger.LogInformation("Repository => Attempting to create a person");

        try
        {
            var url = apiEndpoints.Value.CreatePersonEndpoint;

            var response = await client.HttpPostAsync(url, personModel);

            if (response.IsSuccessStatusCode) 
            {
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Announcement}: Attempt to create a person was unsuccessful.",
                            LoggerConstants.Failed);
            return false;
        }
    }

    public async Task<bool> DeletePersonByPersonCodeAsync(int personCode)
    {
        logger.LogInformation("Repository => Attempting to delete a person with code: {code}",
            personCode);

        try
        {
            var url = apiEndpoints.Value.DeletePersonEndpoint;

            var response = await client.HttpDeleteAsync(url.Replace("{personCode}", personCode.ToString()));

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Announcement}: Attempt to delete a person with code: {code} was unsuccessful.",
                            LoggerConstants.Failed,
                            personCode);
            return false;
        }
    }

    public async Task<List<PersonsModel>?> GetPersonsAsync()
    {
        var url = apiEndpoints.Value.GetPersonsEndpoint;

        var result = await client.HttpGetAllAsync<PersonsModel>(url);

        return result;
    }

    public async Task<bool> UpdatePersonAsync(PersonsModel personModel)
    {
        logger.LogInformation("Repository => Attempting to update a person with code: {code}",
            personModel.code);

        try
        {
            var url = apiEndpoints.Value.UpdatePersonEndpoint;

            var response = await client.HttpPutAsync(url, personModel);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Announcement}: Attempt to update a person with code: {code} was unsuccessful.",
                            LoggerConstants.Failed,
                            personModel.code);
            return false;
        }
    }
}
