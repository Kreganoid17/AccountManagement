using AccountManagementAPI.Domains.Persons.Services;
using AccountManagment.Libraries.Shared.Domains.Persons.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagementAPI.Domains.Persons.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = "OpenApiSpecificationForPersons")]
public class PersonsController(IPersonsRepository personsRepository) : Controller
{
    [HttpGet]
    public async Task<ActionResult<List<PersonsModel>>> RetrieveAllAsync() 
    {
        var persons = await personsRepository.RetrieveAllAsync();

        return Ok(persons);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(PersonsModel personsModel)
    {
        var isCreated = await personsRepository.CreateAsync(personsModel);

        if (isCreated)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(PersonsModel personsModel)
    {
        var isUpdated = await personsRepository.UpdateAsync(personsModel);

        if (isUpdated)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpDelete("{personCode:int}")]
    public async Task<IActionResult> DeleteAsync(int personCode)
    {
        var isDeleted = await personsRepository.DeleteAsync(personCode);

        if (isDeleted)
        {
            return Ok();
        }

        return BadRequest();
    }
}
