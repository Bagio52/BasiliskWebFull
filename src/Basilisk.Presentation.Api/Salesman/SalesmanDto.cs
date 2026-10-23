using System;
using Basilisk.DataAccess.Models.Enums;

using System.ComponentModel.DataAnnotations;
using Basilisk.Presentation.Api.Shared;

namespace Basilisk.Presentation.Api.Salesman;

public class SalesmanDto
{
    [StringLength(maximumLength: 5, MinimumLength = 4)]
    [UniqueEmployeeNumber]              //<--contoh validasi yang mengambil dari database
    public string EmployeeNumber { get; set; }

    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    [Required(ErrorMessage = "The Last Name is required, if you dont have one then fill it with your First Name.")]
    public string LastName { get; set; }

    [Required]
    public Level? Level { get; set; }

    [Required]
    [BeforeDate("HiredDate")]
    public DateTime? BirthDate { get; set; } = null;

    public DateTime HiredDate { get; set; } = DateTime.Now;

    public string Address { get; set; }
    
    public string City { get; set; }
    
    [Phone]
    public string Phone { get; set; }
    
    public string? SuperiorEmployeeNumber { get; set; }
}
