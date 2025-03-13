using AccountManagement.Domains.Accounts.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Domains.Accounts.Controllers
{
    public class AccountsController(IAccountsRepository accountsRepository) : Controller
    {
        [HttpGet("{personCode}")]
        public async Task<IActionResult> Accounts(int personCode)
        {
            return View(await accountsRepository.GetAllAccountsByPersonsId(personCode));
        }
    }
}
