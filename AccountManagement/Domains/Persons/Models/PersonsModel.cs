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
    [MinLength(13, ErrorMessage = "Min length of 13 numbers required")]
    [MaxLength(13, ErrorMessage = "Max length of 13 numbers required")]
    public string id_number { get; set; } = string.Empty;

}
