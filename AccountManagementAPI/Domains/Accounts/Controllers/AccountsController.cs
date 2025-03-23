using AccountManagementAPI.Domains.Accounts.Services;
using AccountManagment.Libraries.Shared.Domains.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagementAPI.Domains.Accounts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "OpenApiSpecificationForAccounts")]
    public class AccountsController(IAccountsRepository accountsRepository) : Controller
    {
        [HttpGet("{personCode:int}")]
        public async Task<ActionResult<List<AccountsModel>?>> RetreiveAllAsync(int personCode)
        {
            var accounts = await accountsRepository.RetrieveAllAsync(personCode);

            return Ok(accounts);
        }
        
        [HttpGet("account/{accountCode:int}")]
        public async Task<ActionResult<AccountsModel>> RetreiveSingleAsync(int accountCode)
        {
            var account = await accountsRepository.RetrieveSingleAsync(accountCode);

            return Ok(account);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateAsync(AccountsModel accountModel)
        {
            var isCreated = await accountsRepository.CreateAsync(accountModel);

            if (isCreated) 
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{accountCode:int}")]
        public async Task<IActionResult> DeleteAsync(int accountCode)
        {
            var isDeleted = await accountsRepository.DeleteAsync(accountCode);

            if (isDeleted) 
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
