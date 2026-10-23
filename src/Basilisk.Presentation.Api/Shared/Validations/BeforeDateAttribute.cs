using System;
using System.ComponentModel.DataAnnotations;

namespace Basilisk.Presentation.Api.Shared;

public class BeforeDateAttribute : ValidationAttribute
{
    //[BirthDateBefore("HiredDate")]
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
            var instance = validationContext.ObjectInstance;

            var objectType = validationContext.ObjectInstance.GetType();
            var propertyDariType = objectType.GetProperty(DatePropertyName);

            var laterDate = (DateTime)propertyDariType.GetValue(instance);

            if (earlierDate < laterDate) return ValidationResult.Success;

            return new ValidationResult(ErrorMessage);
        }
        else
        {
            return new ValidationResult("This validation is only for Date Time ");
        }
    }
}
