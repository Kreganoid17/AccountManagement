namespace AccountManagement.Domains.Transactions.Models
{
    public class TransactionsModel
    {
        public DateTime transaction_date { get; set; }

        public DateTime capture_date { get; set; }

        public double amount { get; set; }

        public string description { get; set; } = string.Empty;
    }
}
