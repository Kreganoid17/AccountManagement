using AccountManagment.Libraries.Shared.Domains.Persons.Models;

namespace AccountManagementAPI.Domains.Persons.Services;

public interface IPersonsRepository
{
    Task<bool> CreateAsync(PersonsModel personModel);

    Task<List<PersonsModel>?> RetrieveAllAsync();

    Task<bool> UpdateAsync(PersonsModel personModel);

    Task<bool> DeleteAsync(int personCode);
}
