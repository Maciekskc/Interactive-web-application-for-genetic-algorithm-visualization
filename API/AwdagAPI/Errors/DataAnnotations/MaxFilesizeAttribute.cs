using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Validation.DataAnnotations
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSizeInMb;
        public MaxFileSizeAttribute(int maxFileSizeInMb)
        {
            _maxFileSizeInMb = maxFileSizeInMb;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > (_maxFileSizeInMb * 1000000))
                    return new ValidationResult($"File {validationContext.DisplayName} larger than {_maxFileSizeInMb} MB.");
            }

            return ValidationResult.Success;
        }
    }
}
