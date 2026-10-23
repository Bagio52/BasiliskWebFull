using System;
using System.ComponentModel.DataAnnotations;

namespace Basilisk.Presentation.Web.Validations;

public class BeforeDateAttribute : ValidationAttribute
{
    //[BirthDateBefore("HireDate")]
    //property... BirthDate
    public string DatePropertyName { get; set; }

    public BeforeDateAttribute(string datePropertyName)
    {
        DatePropertyName = datePropertyName;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime earlierDate)
        {
            //proses 1: ambil instansiasinya
            //menghindar : var instance = (SalesmanViewModel) validationContext.ObjectInstance
            var instance = validationContext.ObjectInstance;

            //proses 2: ambil property dari tipe data instansiasinya berdasarkan nama.
            var objectType = validationContext.ObjectInstance.GetType();        // SalesmanViewModel.clas
            var propertyDariType = objectType.GetProperty(DatePropertyName);    // HireDate

            //proses 3: cocokan property dengan instansiasinya
            var laterDate = (DateTime)propertyDariType.GetValue(instance);      // HireDate dari instansi

            if (earlierDate < laterDate) return ValidationResult.Success;

            return new ValidationResult(ErrorMessage);
        }
        else
        {
            return new ValidationResult("This validation is only for Date Time");
        }
    }
}
