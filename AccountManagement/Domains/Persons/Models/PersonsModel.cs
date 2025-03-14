using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Domains.Persons.Models;

public class PersonsModel
{
    public int code { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string? name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Surname is required")]
    public string? surname { get; set; } = string.Empty;

    [Required(ErrorMessage = "ID Number is required")]
    [RegularExpression("([0-9]+)", ErrorMessage = "Invalid ID Number")]
    [StringLength(13, ErrorMessage = "Max Length of 13 digits is required")]
    public string id_number { get; set; } = string.Empty;

}
