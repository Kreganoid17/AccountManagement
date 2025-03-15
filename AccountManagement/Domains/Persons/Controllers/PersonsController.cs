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

    [HttpPost("update/persons")]
    public async Task<IActionResult> UpdateAsync(PersonsModel personsModel)
    {
        if (ModelState.IsValid)
        {
            var isCreated = await personsRepository.UpdateAsync(personsModel);

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

    [HttpPost("delete/persons/{personCode}")]
    public async Task<IActionResult> DeleteAsync(int personCode) 
    {
        var isDeleted = await personsRepository.DeleteAsync(personCode);

        if (isDeleted)
        {
            return RedirectToAction(nameof(Persons));
        }
        else 
        {
            return BadRequest();
        }
    }

}
