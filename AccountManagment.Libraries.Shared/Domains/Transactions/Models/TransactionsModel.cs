using AccountManagment.Libraries.Shared.DataValidations;
using System.ComponentModel.DataAnnotations;

namespace AccountManagment.Libraries.Shared.Domains.Transactions.Models
{
    public class TransactionsModel
    {
        public int code { get; set; }

        [Required]
        public int account_code { get; set; }

        [Required]
        [DateNotInFuture]
        public DateTime transaction_date { get; set; } = DateTime.Now;

        public DateTime capture_date { get; set; } = DateTime.Now;

        public int transaction_type { get; set; }

        [Required]
        [TransactionAmount]
        public double amount { get; set; }

        [Required]
        public string description { get; set; } = string.Empty;
    }
}
