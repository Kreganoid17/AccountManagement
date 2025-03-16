using AccountManagement.Helpers.DataValidations;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Domains.Transactions.Models;

public class TransactionsModel
{
    public int code { get; set; }

    [Required]
    public int account_code { get; set; }

    [Required]
    [DateNotInFuture]
    public DateTime transaction_date { get; set; } = DateTime.Now;

    public DateTime capture_date { get; set; } = DateTime.Now;

    [Required]
    public double amount { get; set; }

    [Required]
    public string description { get; set; } = string.Empty;
}
