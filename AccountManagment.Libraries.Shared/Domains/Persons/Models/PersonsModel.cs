using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagment.Libraries.Shared.Domains.Persons.Models;

public class PersonsModel
{
    public int code { get; set; }

    [Required]
    public string? name { get; set; } = string.Empty;

    [Required]
    public string? surname { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^\d{13}$")]
    public string id_number { get; set; } = string.Empty;
}
