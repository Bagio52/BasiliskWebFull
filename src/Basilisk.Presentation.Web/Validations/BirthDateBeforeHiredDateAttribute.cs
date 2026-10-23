using System;
using System.ComponentModel.DataAnnotations;
using Basilisk.DataAccess.Models;
using Basilisk.Presentation.Web.ViewModels;

namespace Basilisk.Presentation.Web.Validations;

public class BirthDateBeforeHiredDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime birthDate)
        {
            var student = (SalesmanViewModel) validationContext.ObjectInstance;
            var type = validationContext.ObjectInstance;
            var propertyName = validationContext.MemberName;
            var displayName = validationContext.DisplayName;

            if (birthDate < student.HiredDate) return ValidationResult.Success;

            return new ValidationResult(ErrorMessage);
        }
        else
        {
            return new ValidationResult("This validation is only for Date Time");
        }
    }
}
