namespace AccountManagement.Configuration;

public class StoredProcedureOptions
{
    public string GetAllAccountsByPersonCode { get; set; } = string.Empty;
    public string GetAccountById { get; set; } = string.Empty;
    public string InsertNewAccount { get; set; } = string.Empty;
    public string DeleteAccount { get; set; } = string.Empty;

    public string GetAllTransactionsByAccountCode { get; set; } = string.Empty;
    public string GetTransactionById { get; set; } = string.Empty;
    public string InsertNewTransaction { get; set; } = string.Empty;
    public string DeleteTransaction { get; set; } = string.Empty;
    public string UpdateTransaction { get; set; } = string.Empty;
}
