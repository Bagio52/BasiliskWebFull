using System;
using System.ComponentModel.DataAnnotations;
using Basilisk.DataAccess.Models;

namespace Basilisk.Presentation.Web.Validations;

public class UniqueEmployeeNumberAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        string employeeNumber = (string)value;
        var context = (BasiliskTFContext)validationContext.GetService(typeof(BasiliskTFContext));

        bool ideExists = context.Salesmen.Any(s => s.EmployeeNumber.Equals(value));

        if (ideExists)
        {
            return new ValidationResult("Employee Number Already Taken");
        }
        return ValidationResult.Success;
    }
}
