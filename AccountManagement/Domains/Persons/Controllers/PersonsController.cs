using AccountManagement.Domains.Persons.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Domains.Persons.Controllers;
public class PersonsController(IPersonsRepository personsRepository) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Persons()
    {
        return View(await personsRepository.GetAllPersons());
    }
}
