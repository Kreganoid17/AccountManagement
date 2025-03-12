namespace AccountManagement.Domains.Persons.Models;

public class PersonsModel
{
    public int PersonsId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string IdNumber { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;
}
