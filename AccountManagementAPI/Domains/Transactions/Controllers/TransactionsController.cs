using AccountManagementAPI.Domains.Transactions.Services;
using AccountManagment.Libraries.Shared.Domains.Transactions.Models;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagementAPI.Domains.Transactions.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(GroupName = "OpenApiSpecificationForTransactions")]
public class TransactionsController(ITransactionsRepository transactionsRepository) : Controller
{
    [HttpGet("{accountCode:int}")]
    public async Task<ActionResult<List<TransactionsModel>?>> RetreiveAllAsync(int accountCode)
    {
        var transactions = await transactionsRepository.RetrieveAllAsync(accountCode);

        return Ok(transactions);
    }

    [HttpGet("transaction/{transactionCode:int}")]
    public async Task<ActionResult<List<TransactionsModel>?>> RetreivesingleAsync(int transactionCode)
    {
        var transactions = await transactionsRepository.RetrieveSingleAsync(transactionCode);

        return Ok(transactions);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(TransactionsModel transactionsModel) 
    {
        var isCreated = await transactionsRepository.CreateAsync(transactionsModel);

        if (isCreated) 
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(TransactionsModel transactionModel) 
    {
        var isUpdated = await transactionsRepository.UpdateAsync(transactionModel);

        if (isUpdated) 
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpDelete("{transactionCode:int}")]
    public async Task<IActionResult> DeleteAsync(int transactionCode) 
    {
        var isDeleted = await transactionsRepository.DeleteAsync(transactionCode);

        if (isDeleted) 
        {
            return Ok();
        }

        return BadRequest();
    }

}
