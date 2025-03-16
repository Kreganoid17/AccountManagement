using AccountManagement.Domains.Persons.Models;
using AccountManagement.Domains.Persons.Repository;
using AccountManagement.Domains.Persons.Services;
using AccountManagement.Domains.Transactions.Models;
using AccountManagement.Domains.Transactions.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.Domains.Transactions.Controllers
{

    public class TransactionsController(ITransactionsRepository transactionsRepository) : Controller
    {
        [HttpGet("transactions/{accountCode}")]
        public async Task<IActionResult> Transactions(int accountCode)
        {
            ViewData["AccountCode"] = accountCode;
            return View(await transactionsRepository.RetrieveAllTransactionsByAccountCode(accountCode));
        }

        [HttpPost("create/transaction")]
        public async Task<IActionResult> CreateAsync(TransactionsModel transactionsModel)
        {
            if (ModelState.IsValid)
            {
                var isCreated = await transactionsRepository.CreateAsync(transactionsModel);

                if (isCreated)
                {
                    TempData["ToastMessage"] = "Transaction has been created";
                    TempData["ToastType"] = "success";
                    return RedirectToAction(nameof(Persons));
                }
                else
                {
                    TempData["ToastMessage"] = "Unable to create transaction";
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
        [HttpPost("update/transaction")]
        public async Task<IActionResult> UpdateAsync(TransactionsModel transactionsModel)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = await transactionsRepository.UpdateAsync(transactionsModel);

                if (isUpdated)
                {
                    TempData["ToastMessage"] = "Transaction has been updated";
                    TempData["ToastType"] = "success";
                    return RedirectToAction(nameof(Persons));
                }
                else
                {
                    TempData["ToastMessage"] = "Unable to update transaction";
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


        [HttpPost("delete/transaction/{transactionCode}")]
        public async Task<IActionResult> DeleteAsync(int transactionCode)
        {
            var isDeleted = await transactionsRepository.DeleteAsync(transactionCode);

            if (isDeleted)
            {
                TempData["ToastMessage"] = "Transaction has been deleted";
                TempData["ToastType"] = "success";
                return RedirectToAction(nameof(Persons));
            }
            else
            {
                TempData["ToastMessage"] = "Unable to delete transaction";
                TempData["ToastType"] = "error";
                return RedirectToAction(nameof(Persons));
            }
        }
    }
}
