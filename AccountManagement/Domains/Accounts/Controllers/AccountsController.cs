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
                    return RedirectToAction(nameof(Accounts), new { personCode = accountModel.person_code});
                }
                else 
                {
                    return BadRequest();
                }
            }

            return BadRequest();
        }
    }
}
