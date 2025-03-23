namespace AccountManagement.Configuration;

public class StoredProcedureOptions
{
    public string GetAllTransactionsByAccountCode { get; set; } = string.Empty;
    public string GetTransactionById { get; set; } = string.Empty;
    public string InsertNewTransaction { get; set; } = string.Empty;
    public string DeleteTransaction { get; set; } = string.Empty;
    public string UpdateTransaction { get; set; } = string.Empty;
}
