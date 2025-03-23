using System.ComponentModel.DataAnnotations;

namespace AccountManagment.Libraries.Shared.Domains.Accounts.Models;

public class AccountsModel
{
    public int code { get; set; }

    [Required]
    public int person_code { get; set; }

    [Required]
    [RegularExpression(@"^\d+$")]
    public string account_number { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^\d+(\.\d+)?$")]
    public double outstanding_balance { get; set; }
}
