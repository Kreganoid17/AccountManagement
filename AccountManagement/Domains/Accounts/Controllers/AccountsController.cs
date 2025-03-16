using AccountManagement.Domains.Accounts.Models;
using AccountManagement.Domains.Accounts.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Domains.Accounts.Controllers
{
    public class AccountsController(IAccountsRepository accountsRepository) : Controller
    {
        [HttpGet("accounts/{personCode}")]
        public async Task<IActionResult> Accounts(int personCode)
        {
            ViewData["PersonCode"] = personCode;
            return View(await accountsRepository.RetrieveAllAccountsByPersonsId(personCode));
        }

        [HttpPost("create/account")]
        public async Task<IActionResult> CreateAsync(AccountsModel accountModel)
        {
            if (ModelState.IsValid)
            {
                var isCreated = await accountsRepository.CreateAsync(accountModel);

                if (isCreated)
                {
                    TempData["ToastMessage"] = "Account has been created";
                    TempData["ToastType"] = "success";
                    return RedirectToAction(nameof(Accounts), new { personCode = accountModel.person_code});
                }
                else 
                {
                    TempData["ToastMessage"] = "Unable to create account";
                    TempData["ToastType"] = "error";
                    return RedirectToAction(nameof(Accounts), new { personCode = accountModel.person_code });
                }
            }

            TempData["ToastMessage"] = "Incorrect details entered";
            TempData["ToastType"] = "error";
            return RedirectToAction(nameof(Accounts), new { personCode = accountModel.person_code });
        }

        [HttpPost("delete/account/{accountCode}")]
        public async Task<IActionResult> DeleteAsync(int accountCode)
        {
            var isDeleted = await accountsRepository.DeleteAsync(accountCode);

            if (isDeleted)
            {
                TempData["ToastMessage"] = "Account has been deleted";
                TempData["ToastType"] = "success";
                return RedirectToAction(nameof(Accounts), new { personCode = accountCode});
            }
            else
            {
                TempData["ToastMessage"] = "Unable to delete account";
                TempData["ToastType"] = "error";
                return RedirectToAction(nameof(Accounts), new { personCode = accountCode });
            }
        }
    }
}
