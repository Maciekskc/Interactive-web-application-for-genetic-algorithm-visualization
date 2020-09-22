using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Validation.DataAnnotations
{
    public class PasswordAttribute : ValidationAttribute
    {
        private static readonly Regex AtLeastOneLowercaseLetterRegex = CreateRegex(AtLeastOneLowercaseLetterPattern);
        private static readonly Regex AtLeastOneUppercaseLetterRegex = CreateRegex(AtLeastOneUppercaseLetterPattern);
        private static readonly Regex AtLeastOneNumberRegex = CreateRegex(AtLeastOneNumberPattern);
        private static readonly Regex AtLeastOneSpecialCharacterRegex = CreateRegex(AtLeastOneSpecialCharacterPattern);

        private readonly int _minLength;
        private readonly bool _requireLowercase;
        private readonly bool _requireUppercase;
        private readonly bool _requireNumber;
        private readonly bool _requireSpecialCharacter;

        private const string AtLeastOneLowercaseLetterPattern = "(?=.*[a-z])";
        private const string AtLeastOneUppercaseLetterPattern = "(?=.*[A-Z])";
        private const string AtLeastOneNumberPattern = "\\d";
        private const string AtLeastOneSpecialCharacterPattern = "[*@!#%&()^~{}]+";

        public PasswordAttribute(int minLength = 6, bool requireLowercase = true, bool requireUppercase = true, bool requireNumber = true, bool requireSpecialCharacter = true)
        {
            _minLength = minLength;
            _requireLowercase = requireLowercase;
            _requireUppercase = requireUppercase;
            _requireNumber = requireNumber;
            _requireSpecialCharacter = requireSpecialCharacter;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string valueString)
            {
                if(_requireLowercase && !HasAtLeastOneLowercaseLetter(valueString))
                    return new ValidationResult($"The {validationContext.DisplayName} must contain at least one lowercase letter.");
                if(_requireUppercase && !HasAtLeastOneUppercaseLetter(valueString))
                    return new ValidationResult($"The {validationContext.DisplayName} must contain at least one uppercase letter.");
                if(_requireNumber && !HasAtLeastOneNumber(valueString))
                    return new ValidationResult($"The {validationContext.DisplayName} must contain at least one number.");
                if(_requireSpecialCharacter && !HasAtLeastOneSpecialCharacter(valueString))
                    return new ValidationResult($"The {validationContext.DisplayName} must contain at least one special character.");
                if(valueString.Length < _minLength)
                    return new ValidationResult($"The {validationContext.DisplayName} must be at least {_minLength} characters long.");
            }
            else if(value is null)
                return new ValidationResult($"The {validationContext.DisplayName} field is required.");
            else
                throw new ArgumentException("Sprawdzenie poprawności hasła nie może zostać przeprowadzone dla typu innego niż string");

            return ValidationResult.Success;
        }

        public static bool HasAtLeastOneLowercaseLetter(string password)
        {
            return AtLeastOneLowercaseLetterRegex.IsMatch(password);
        }

        public static bool HasAtLeastOneUppercaseLetter(string password)
        {
            return AtLeastOneUppercaseLetterRegex.IsMatch(password);
        }

        public static bool HasAtLeastOneNumber(string password)
        {
            return AtLeastOneNumberRegex.IsMatch(password);
        }

        public static bool HasAtLeastOneSpecialCharacter(string password)
        {
            return AtLeastOneSpecialCharacterRegex.IsMatch(password);
        }

        private static Regex CreateRegex(string pattern)
        {
            return new Regex(pattern);
        }
    }
}
