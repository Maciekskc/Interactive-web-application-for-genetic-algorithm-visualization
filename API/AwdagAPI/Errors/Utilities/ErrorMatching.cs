using System.Collections.Generic;
using System.Text.RegularExpressions;
using Validation.Models;
using Validation.Resources;

namespace Validation.Utilities
{
    public class ErrorMatching
    {
        public static List<DetailedError> TranslateErrors(IEnumerable<string> errors)
        {
            var detailedErrors = new List<DetailedError>();
            foreach (var error in errors)
            {
                var detailedErrorWithTemplate = GetType(error);
                var parameters = ReverseFormat(detailedErrorWithTemplate.Template, error);
                var detailedError = detailedErrorWithTemplate.SetParams(parameters.ToArray());
                detailedErrors.Add(detailedError);
            }
            return detailedErrors;
        }

        /// <summary>
        /// Metoda zwracająca listę wartości parametrów na podstawie wzoru stringa (ValidationErrora) i
        /// tego samego stringa ale z wartościami
        /// </summary>
        /// <param name="template"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        private static List<string> ReverseFormat(string template, string str)
        {
            template = Regex.Replace(template, @"[\\\^\$\.\|\?\*\+\(\)]", m => "\\"
                                                                               + m.Value);

            string pattern = "^" + Regex.Replace(template, @"\{[0-9]+\}", "(.*?)") + "$";

            Regex regex = new Regex(pattern);
            Match match = regex.Match(str);
            List<string> parameters = new List<string>();

            for (int i = 1; i < match.Groups.Count; i++)
            {
                parameters.Add(match.Groups[i].Value);
            }

            return parameters;
        }

        public static DetailedErrorWithTemplate GetType(string message)
        {
            if (IsInvalidValueForField(message))
                return FieldValidationError.InvalidValueForField;

            if (NotValidValue(message))
                return FieldValidationError.NotValidValue;

            if (InvalidValue(message))
                return FieldValidationError.InvalidValue;

            if (IsRequired(message))
                return FieldValidationError.FieldIsRequired;

            if (IsMaxLength(message))
                return FieldValidationError.FieldLengthMustNotBeGreaterThan;

            if (IsMinLength(message))
                return FieldValidationError.FieldLengthMustNotBeLessThan;

            if (IsRange(message))
                return FieldValidationError.FieldValueMustBeBetween;

            if (IsEmail(message))
                return FieldValidationError.InvalidEmail;

            if (IsUrl(message))
                return FieldValidationError.InvalidUrl;

            if (IsPasswordAtLeastOneLowercase(message))
                return FieldValidationError.PasswordMustContainAtLeastOneLowercaseLetter;

            if (IsPasswordAtLeastOneUppercase(message))
                return FieldValidationError.PasswordMustContainUppercaseLetter;

            if (IsPasswordAtLeastOneNumber(message))
                return FieldValidationError.PasswordMustContainNumber;

            if (IsPasswordAtLeastOneSpecialCharacter(message))
                return FieldValidationError.PasswordMustContainSpecialCharacter;

            if (IsPasswordLongEnough(message))
                return FieldValidationError.PasswordMustContainAtLeastXCharacters;

            if (IsRoles(message))
                return FieldValidationError.FieldContainsRoleThatDoesNotExist;

            if (IsPhone(message))
                return FieldValidationError.InvalidPhoneNumber;

            if (IsFieldAndOtherFieldEqual(message))
                return FieldValidationError.FieldAndOtherFieldAreNotEqual;

            if (IsMaxFileSize(message))
                return FieldValidationError.FileIsTooBig;

            if (IsAllowedExtensions(message))
                return FieldValidationError.NotAllowedFileExtension;

            return FieldValidationError.UnknownError;
        }

        private static bool IsAllowedExtensions(string message)
        {
            return message.Contains("Only") & message.Contains("extensions allowed.");
        }

        private static bool IsMaxFileSize(string message)
        {
            return message.Contains("File") && message.Contains("larger than") && message.Contains("MB.");
        }

        private static bool InvalidValue(string message)
        {
            return message.Contains("The value") && message.Contains("is invalid.");
        }

        private static bool NotValidValue(string message)
        {
            return message.Contains("The value") && message.Contains("is not valid.");
        }

        private static bool IsInvalidValueForField(string message)
        {
            return message.Contains("The value") && message.Contains("is not valid for");
        }

        private static bool IsRequired(string message)
        {
            return message.Contains("The") && message.Contains("field is required.");
        }

        private static bool IsRange(string message)
        {
            return message.Contains("The field") && message.Contains("must be between") && message.Contains("and");
        }

        private static bool IsMinLength(string message)
        {
            return message.Contains("The field") && message.Contains("must be a string or array type with a minimum length of");
        }

        private static bool IsMaxLength(string message)
        {
            return message.Contains("The field") && message.Contains("must be a string or array type with a maximum length of");
        }

        private static bool IsEmail(string message)
        {
            return message.Contains("The ") && message.Contains("field is not a valid e-mail address.");
        }

        private static bool IsUrl(string message)
        {
            return message.Contains("The ") && message.Contains("field is not a valid fully-qualified http, https, or ftp URL.");
        }

        private static bool IsPasswordAtLeastOneLowercase(string message)
        {
            return message.Contains("The ") && message.Contains("must contain at least one lowercase letter.");
        }

        private static bool IsPasswordAtLeastOneUppercase(string message)
        {
            return message.Contains("The ") && message.Contains("must contain at least one uppercase letter.");
        }

        private static bool IsPasswordAtLeastOneSpecialCharacter(string message)
        {
            return message.Contains("The ") && message.Contains("must contain at least one special character.");
        }

        private static bool IsPasswordAtLeastOneNumber(string message)
        {
            return message.Contains("The ") && message.Contains("must contain at least one number.");
        }

        private static bool IsPasswordLongEnough(string message)
        {
            return message.Contains("The ") && message.Contains("must be at least") && message.Contains("characters long.");
        }

        private static bool IsRoles(string message)
        {
            return message.Contains("The ") && message.Contains("field contains invalid role names.");
        }

        private static bool IsPhone(string message)
        {
            return message.Contains("The ") && message.Contains("field is not a valid phone number.");
        }

        private static bool IsFieldAndOtherFieldEqual(string message)
        {
            return message.Contains("and") && message.Contains("do not match.");
        }
    }
}
