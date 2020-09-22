using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Validation.Utilities;

namespace Validation.DataAnnotations
{
    public class RolesAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult($"The {validationContext.DisplayName} field is required.");
            if (value is string propertyAsString && !Role.List.Contains(propertyAsString))
                return new ValidationResult($"The {validationContext.DisplayName} field contains not valid role names.");
            if (value is IEnumerable<string> propertyAsEnumerableOfString)
            {
                foreach (var inputRole in propertyAsEnumerableOfString)
                {
                    if (!Role.List.Contains(inputRole))
                        return new ValidationResult($"The {validationContext.DisplayName} field contains invalid role names.");
                }
            }
            else
                throw new ArgumentException("Sprawdzenie poprawności ról nie może zostać przeprowadzone dla typu innego niż string lub lista stringów");

            return ValidationResult.Success;
        }
    }
}
