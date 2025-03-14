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
            return View(await accountsRepository.RetrieveAllAccountsByPersonsId(personCode));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(AccountsModel accountModel)
        {
            if (ModelState.IsValid)
            {
                var isCreated = await accountsRepository.CreateAsync(accountModel);

                if (isCreated)
                {
                    return Ok();
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
