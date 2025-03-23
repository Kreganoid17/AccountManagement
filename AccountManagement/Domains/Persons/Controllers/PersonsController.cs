using AccountManagement.Domains.Persons.Services;
using AccountManagment.Libraries.Shared.Domains.Persons.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Domains.Persons.Controllers;

public class PersonsController(IPersonsRepository personsRepository) : Controller
{
    [HttpGet("persons")]
    public async Task<IActionResult> Persons()
    {
        return View(await personsRepository.GetPersonsAsync());
    }

    [HttpPost("create/person")]
    public async Task<IActionResult> CreateAsync(PersonsModel personsModel) 
    {
        if (ModelState.IsValid)
        {
            var isCreated = await personsRepository.CreatePersonAsync(personsModel);

            if (isCreated)
            {
                TempData["ToastMessage"] = "Person has been created";
                TempData["ToastType"] = "success";
                return RedirectToAction(nameof(Persons));
            }
            else
            {
                TempData["ToastMessage"] = "Unable to create person";
                TempData["ToastType"] = "error";
                return RedirectToAction(nameof(Persons));
            }
        }
        else 
        {
            TempData["ToastMessage"] = "Incorrect details entered";
            TempData["ToastType"] = "error";
            return RedirectToAction(nameof(Persons));
        }
    }

    [HttpPost("update/person")]
    public async Task<IActionResult> UpdateAsync(PersonsModel personsModel)
    {
        if (ModelState.IsValid)
        {
            var isUpdated = await personsRepository.UpdatePersonAsync(personsModel);

            if (isUpdated)
            {
                TempData["ToastMessage"] = "Person has been updated";
                TempData["ToastType"] = "success";
                return RedirectToAction(nameof(Persons));
            }
            else
            {
                TempData["ToastMessage"] = "Unable to update person";
                TempData["ToastType"] = "error";
                return RedirectToAction(nameof(Persons));
            }
        }
        else
        {
            TempData["ToastMessage"] = "Incorrect details entered";
            TempData["ToastType"] = "error";
            return RedirectToAction(nameof(Persons));
        }
    }

    [HttpPost("delete/person/{personCode}")]
    public async Task<IActionResult> DeleteAsync(int personCode) 
    {
        var isDeleted = await personsRepository.DeletePersonByPersonCodeAsync(personCode);

        if (isDeleted)
        {
            TempData["ToastMessage"] = "Person has been deleted";
            TempData["ToastType"] = "success";
            return RedirectToAction(nameof(Persons));
        }
        else 
        {
            TempData["ToastMessage"] = "Unable to delete person";
            TempData["ToastType"] = "error";
            return RedirectToAction(nameof(Persons));
        }
    }

}
