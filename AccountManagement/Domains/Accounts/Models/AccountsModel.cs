namespace AccountManagement.Domains.Accounts.Models
{
    public class AccountsModel
    {
        public int code { get; set; }

        public int person_code { get; set; }

        public string account_number { get; set; } = string.Empty;

        public decimal outstanding_balance { get; set; }
    }
}
