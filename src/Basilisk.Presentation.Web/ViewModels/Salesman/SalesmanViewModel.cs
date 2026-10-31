using System;

using System.ComponentModel.DataAnnotations;
using Basilisk.DataAccess.Models.Enums;
using Basilisk.Presentation.Web.Validations;

namespace Basilisk.Presentation.Web.ViewModels;

public class SalesmanViewModel
{
    [Display(Name = "Employee Number")]
    [StringLength(maximumLength:5, MinimumLength = 4)]
    [UniqueEmployeeNumber]          // <---Contoh Validasi yang mengambil dari database
    public string EmployeeNumber { get; set; }

    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    [Required (ErrorMessage = "The Last Name is required, if you dont have one then fill it with your First Name.")]
    public string LastName { get; set; }
    
    [Required]
    public Level? Level { get; set; }
    
    [Display(Name = "Birth Date")]
    [Required]
    // [BirthDateBeforeHireDate]        // <--- Contoh Validasi yang spesifik
    [BeforeDate("Hire Date")]           // <--- Contoh Validasi yang umum, namun untuk isntasiasinya yang sama
    public DateTime? BirthDate { get; set; } = null;
    
    [Display(Name = "Hire Date")]
    public DateTime HiredDate { get; set; } = DateTime.Now;
    
    public string Address { get; set; }
    
    public string City { get; set; }
    
    [Phone]
    public string Phone { get; set; }
    
    [Display(Name = "Supperior")]
    public string? SuperiorEmployeeNumber { get; set; }
    public string FullName => $"{FirstName} {LastName}".Trim();
    public string? SuperiorFullName { get; set; }
}
