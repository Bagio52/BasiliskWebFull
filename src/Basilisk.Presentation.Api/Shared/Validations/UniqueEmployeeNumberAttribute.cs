using System;
using System.ComponentModel.DataAnnotations;
using Basilisk.DataAccess.Models;

namespace Basilisk.Presentation.Api.Shared;
[AttributeUsage(AttributeTargets.Property)]
public class UniqueEmployeeNumberAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        string EmployeeNumber = (string) value;

        var context = (BasiliskTFContext)validationContext.GetService(typeof(BasiliskTFContext));

        bool idExists = context.Salesmen.Any(s => s.EmployeeNumber.Equals(value));

        if (idExists)
        {
            return new ValidationResult("Employee Number Already taken");
        }
        return ValidationResult.Success;
    }
}
