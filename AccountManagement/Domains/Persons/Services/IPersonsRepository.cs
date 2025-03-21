using AccountManagment.Libraries.Shared.Domains.Persons.Models;

namespace AccountManagement.Domains.Persons.Services;

public interface IPersonsRepository
{
    Task<List<PersonsModel>?> GetPersonsAsync();

    Task<bool> CreatePersonAsync(PersonsModel personModel);

    Task<bool> UpdatePersonAsync(PersonsModel personModel);

    Task<bool> DeletePersonAsync(int personCode); 
}
