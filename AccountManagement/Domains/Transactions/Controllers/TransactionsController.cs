using AccountManagement.Domains.Transactions.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Domains.Transactions.Controllers
{

    public class TransactionsController(ITransactionsRepository transactionsRepository) : Controller
    {
        [HttpGet("transactions/{accountCode}")]
        public async Task<IActionResult> Transactions(int accountCode)
        {
            return View(await transactionsRepository.GetAllTransactionsByAccountCode(accountCode));
        }
    }
}
