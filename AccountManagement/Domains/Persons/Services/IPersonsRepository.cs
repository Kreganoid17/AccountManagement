using AccountManagement.Domains.Persons.Models;

namespace AccountManagement.Domains.Persons.Services;

public interface IPersonsRepository
{
    Task<List<PersonsModel>?> RetrieveAllAsync();

    Task<bool> CreateAsync(PersonsModel personModel);
    Task<bool> UpdateAsync(PersonsModel personModel);

    Task<bool> DeleteAsync(int personCode);   
}
