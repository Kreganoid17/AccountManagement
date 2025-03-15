using AccountManagement.Domains.Persons.Models;
using AccountManagement.Domains.Persons.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Domains.Persons.Controllers;

public class PersonsController(IPersonsRepository personsRepository) : Controller
{
    [HttpGet("persons")]
    public async Task<IActionResult> Persons()
    {
        return View(await personsRepository.RetrieveAllAsync());
    }

    [HttpPost("create/persons")]
    public async Task<IActionResult> CreateAsync(PersonsModel personsModel) 
    {
        if (ModelState.IsValid)
        {
            var isCreated = await personsRepository.CreateAsync(personsModel);

            if (isCreated)
            {
                return RedirectToAction(nameof(Persons));
            }
            else
            {
                return BadRequest();
            }
        }
        else 
        {
            return RedirectToAction(nameof(Persons));
        }
    }
}
