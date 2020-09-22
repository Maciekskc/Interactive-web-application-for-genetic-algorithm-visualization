using Validation.Models;

namespace Validation.Resources
{
    /// <summary>
    ///     Błędy typowe dla walidacji pól
    ///     Należy pamiętać, że zawsze jako {0} podawana jest nazwa pola!
    ///     Kody z prefixem "00"
    /// </summary>
    public static class FieldValidationError
    {
        public static readonly string ErrorCodePrefix = "00";

        /// <summary>
        ///     Długość pola {0} nie może być większa, niż {1}
        /// </summary>
        public static readonly DetailedErrorWithTemplate FieldLengthMustNotBeGreaterThan = new DetailedErrorWithTemplate
        {
            Template = "The field {0} must be a string or array type with a maximum length of '{1}'.",
            ErrorCode = $"{ErrorCodePrefix}-01",
            DescriptionFormatter = "Długość pola {0} nie może być większa, niż {1}"
        };

        /// <summary>
        ///     Długość pola {0} nie może być mniejsza, niż {1}
        /// </summary>
        public static readonly DetailedErrorWithTemplate FieldLengthMustNotBeLessThan = new DetailedErrorWithTemplate
        {
            Template = "The field {0} must be a string or array type with a minimum length of '{1}'.",
            ErrorCode = $"{ErrorCodePrefix}-02",
            DescriptionFormatter = "Długość pola {0} nie może być mniejsza, niż {1}"
        };

        /// <summary>
        ///     Pole {0} jest wymagane
        /// </summary>
        public static readonly DetailedErrorWithTemplate FieldIsRequired = new DetailedErrorWithTemplate
        {
            Template = "The {0} field is required.",
            ErrorCode = $"{ErrorCodePrefix}-03",
            DescriptionFormatter = "Pole {0} jest wymagane"
        };

        /// <summary>
        ///     Wartość pola {0} musi być z przedziału [{1}, {2}]
        /// </summary>
        public static readonly DetailedErrorWithTemplate FieldValueMustBeBetween = new DetailedErrorWithTemplate
        {
            Template = "The field {0} must be between {1} and {2}.",
            ErrorCode = $"{ErrorCodePrefix}-04",
            DescriptionFormatter = "Wartość pola {0} musi być z przedziału [{1}, {2}]"
        };

        /// <summary>
        ///     Pola {0} oraz {1} nie są sobie równe
        /// </summary>
        public static readonly DetailedErrorWithTemplate FieldAndOtherFieldAreNotEqual = new DetailedErrorWithTemplate
        {
            Template = "'{0}' and '{1}' do not match.",
            ErrorCode = $"{ErrorCodePrefix}-05",
            DescriptionFormatter = "Pola {0} oraz {1} nie są sobie równe"
        };

        /// <summary>
        ///     Pole {0} musi zawierać małą literę
        /// </summary>
        public static readonly DetailedErrorWithTemplate PasswordMustContainAtLeastOneLowercaseLetter = new DetailedErrorWithTemplate
        {
            Template = "The {0} must contain at least one lowercase letter.",
            ErrorCode = $"{ErrorCodePrefix}-06",
            DescriptionFormatter = "Pole {0} musi zawierać małą literę"
        };

        /// <summary>
        ///     Pole {0} musi zawierać wielką literę
        /// </summary>
        public static readonly DetailedErrorWithTemplate PasswordMustContainUppercaseLetter = new DetailedErrorWithTemplate
        {
            Template = "The {0} must contain at least one uppercase letter.",
            ErrorCode = $"{ErrorCodePrefix}-07",
            DescriptionFormatter = "Pole {0} musi zawierać wielką literę"
        };

        /// <summary>
        ///     Pole {0} musi zawierać cyfrę
        /// </summary>
        public static readonly DetailedErrorWithTemplate PasswordMustContainNumber = new DetailedErrorWithTemplate
        {
            Template = "The {0} must contain at least one number.",
            ErrorCode = $"{ErrorCodePrefix}-08",
            DescriptionFormatter = "Pole {0} musi zawierać cyfrę"
        };

        /// <summary>
        ///     Pole {0} musi zawierać znak specjalny np. #$^+=!*()@%&
        /// </summary>
        public static readonly DetailedErrorWithTemplate PasswordMustContainSpecialCharacter = new DetailedErrorWithTemplate
        {
            Template = "The {0} must contain at least one special character.",
            ErrorCode = $"{ErrorCodePrefix}-09",
            DescriptionFormatter = "Pole {0} musi zawierać znak specjalny np. #$^+=!*()@%&"
        };

        /// <summary>
        ///     Pole {0} musi mieć co najmniej {1} znaków
        /// </summary>
        public static readonly DetailedErrorWithTemplate PasswordMustContainAtLeastXCharacters = new DetailedErrorWithTemplate
        {
            Template = "The {0} must be at least {0} characters long.",
            ErrorCode = $"{ErrorCodePrefix}-10",
            DescriptionFormatter = "Pole {0} musi mieć co najmniej {1} znaków"
        };

        /// <summary>
        ///     Pole {0} zawiera niepoprawną nazwę roli
        /// </summary>
        public static readonly DetailedErrorWithTemplate FieldContainsRoleThatDoesNotExist = new DetailedErrorWithTemplate
        {
            Template = "The {0} field contains invalid role names.",
            ErrorCode = $"{ErrorCodePrefix}-11",
            DescriptionFormatter = "Pole {0} zawiera niepoprawną nazwę roli"
        };

        /// <summary>
        ///     Pole {0} jest niepoprawnym adresem URL
        /// </summary>
        public static readonly DetailedErrorWithTemplate InvalidUrl = new DetailedErrorWithTemplate
        {
            Template = "The {0} field is not a valid fully-qualified http, https, or ftp URL.",
            ErrorCode = $"{ErrorCodePrefix}-12",
            DescriptionFormatter = "Pole {0} jest niepoprawnym adresem URL"
        };

        /// <summary>
        ///     Pole {0} jest niepoprawnym adresem email
        /// </summary>
        public static readonly DetailedErrorWithTemplate InvalidEmail = new DetailedErrorWithTemplate
        {
            Template = "The {0} field is not a valid e-mail address.",
            ErrorCode = $"{ErrorCodePrefix}-13",
            DescriptionFormatter = "Pole {0} jest niepoprawnym adresem email"
        };

        /// <summary>
        ///     Pole {0} jest niepoprawnym numerem telefonu
        /// </summary>
        public static readonly DetailedErrorWithTemplate InvalidPhoneNumber = new DetailedErrorWithTemplate
        {
            Template = "The {0} field is not a valid phone number.",
            ErrorCode = $"{ErrorCodePrefix}-14",
            DescriptionFormatter = "Pole {0} jest niepoprawnym numerem telefonu"
        };

        /// <summary>
        ///     Wartość {0} dla pola {1} jest niepoprawna
        /// </summary>
        public static readonly DetailedErrorWithTemplate InvalidValueForField = new DetailedErrorWithTemplate
        {
            Template = "The value {0} is not valid for {1}.",
            ErrorCode = $"{ErrorCodePrefix}-15",
            DescriptionFormatter = "Wartość {0} dla pola {1} jest niepoprawna"
        };

        /// <summary>
        ///     Wartość {0} jest niepoprawna
        /// </summary>
        public static readonly DetailedErrorWithTemplate NotValidValue = new DetailedErrorWithTemplate
        {
            Template = "The value {0} is not valid.",
            ErrorCode = $"{ErrorCodePrefix}-16",
            DescriptionFormatter = "Wartość {0} jest niepoprawna"
        };

        /// <summary>
        ///     Wartość {0} jest niepoprawna
        /// </summary>
        public static readonly DetailedErrorWithTemplate InvalidValue = new DetailedErrorWithTemplate
        {
            Template = "The value {0} is invalid.",
            ErrorCode = $"{ErrorCodePrefix}-17",
            DescriptionFormatter = "Wartość {0} jest niepoprawna"
        };



        /// <summary>
        ///     Wystąpił błąd
        /// </summary>
        public static readonly DetailedErrorWithTemplate UnknownError = new DetailedErrorWithTemplate
        {
            Template = "Unknown error occured.",
            ErrorCode = $"{ErrorCodePrefix}-18",
            DescriptionFormatter = "Wystąpił błąd"
        };

        /// <summary>
        ///     Plik {0} jest większy niż {1}  MB.
        /// </summary>
        public static readonly DetailedErrorWithTemplate FileIsTooBig = new DetailedErrorWithTemplate
        {
            Template = "File {0} larger than {1} MB.",
            ErrorCode = $"{ErrorCodePrefix}-19",
            DescriptionFormatter = "Plik {0} jest większy niż {1} MB."
        };

        /// <summary>
        ///     Tylko {0} rozszerzenia są dozwolone.
        /// </summary>
        public static readonly DetailedErrorWithTemplate NotAllowedFileExtension = new DetailedErrorWithTemplate
        {
            Template = "Only {0} extensions allowed.",
            ErrorCode = $"{ErrorCodePrefix}-20",
            DescriptionFormatter = "Tylko {0} rozszerzenia są dozwolone."
        };
    }
}