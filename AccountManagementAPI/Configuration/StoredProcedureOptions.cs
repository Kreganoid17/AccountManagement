namespace AccountManagementAPI.Configuration;

public class StoredProcedureOptions
{
    public string GetAllPersons { get; set; } = string.Empty;
    public string InsertNewPerson { get; set; } = string.Empty;
    public string DeletePerson { get; set; } = string.Empty;
    public string UpdatePerson { get; set; } = string.Empty;

    public string GetAllAccountsByPersonCode { get; set; } = string.Empty;
    public string GetAccountById { get; set; } = string.Empty;
    public string InsertNewAccount { get; set; } = string.Empty;
    public string DeleteAccount { get; set; } = string.Empty;
}
