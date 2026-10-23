using Basilisk.Presentation.Api.Salesman;
using System.ComponentModel.DataAnnotations;

namespace Basilisk.Presentation.Api.Shared;

[AttributeUsage(AttributeTargets.Property)]
public class BirthDateBeforeHiredDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime birthDate)
        {
            var student = (SalesmanDto)validationContext.ObjectInstance;
            var type = validationContext.ObjectType;
            var propertyName = validationContext.MemberName;
            var displayName = validationContext.DisplayName;

            if (birthDate < student.HiredDate) return ValidationResult.Success;

            return new ValidationResult(ErrorMessage);
        }
        else
        {
            return new ValidationResult("This validation is only for Date Time ");
        }

    }
}
