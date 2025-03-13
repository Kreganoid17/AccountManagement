﻿namespace AccountManagement.Configuration;

public class StoredProcedureOptions
{
    public string GetAllPersons { get; set; } = string.Empty;
    public string GetAllAccountsByPersonCode { get; set; } = string.Empty;
    public string GetAllTransactionsByAccountCode { get; set; } = string.Empty;
}
