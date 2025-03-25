namespace AccountManagement.Configuration;

public class ApiEndpointsConfiguration
{

    public string GetPersonsEndpoint { get; set; } = string.Empty;
    public string CreatePersonEndpoint { get; set; } = string.Empty;
    public string UpdatePersonEndpoint { get; set; } = string.Empty;
    public string DeletePersonEndpoint { get; set; } = string.Empty;

    public string GetAccountsByPersonCodeEndpoint { get; set; } = string.Empty;
    public string CreateAccountEndpoint { get; set; } = string.Empty;
    public string DeleteAccountEndpoint { get; set; } = string.Empty;
    public string GetAccountByAccountCodeEndpoint { get; set; } = string.Empty;
    
    public string GetTransactionsByAccountCodeEndpoint { get; set; } = string.Empty;
    public string CreateTransactionEndpoint { get; set; } = string.Empty;
    public string GetTransactionByTransactionCodeEndpoint { get; set; } = string.Empty;
    public string UpdateTransactionEndpoint { get; set; } = string.Empty;
    public string DeleteTransactionEndpoint { get; set; } = string.Empty;
}
