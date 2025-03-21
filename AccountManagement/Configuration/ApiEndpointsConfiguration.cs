namespace AccountManagement.Configuration;

public class ApiEndpointsConfiguration
{

    public string GetPersonsEndpoint { get; set; } = string.Empty;
    public string CreatePersonEndpoint { get; set; } = string.Empty;
    public string UpdatePersonEndpoint { get; set; } = string.Empty;
    public string DeletePersonEndpoint { get; set; } = string.Empty;
}
