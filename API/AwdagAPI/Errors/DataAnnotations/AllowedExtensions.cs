using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Validation.DataAnnotations
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(params string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            var extension = Path.GetExtension(file?.FileName);
            if (file != null && !_extensions.Contains(extension.ToLower()))
                return new ValidationResult($"Only {string.Join(" ", _extensions)} extensions allowed.");

            return ValidationResult.Success;
        }
    }
}
