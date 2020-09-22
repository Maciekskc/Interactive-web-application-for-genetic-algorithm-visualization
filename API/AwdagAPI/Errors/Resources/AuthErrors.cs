using Validation.Models;

namespace Validation.Resources
{
    /// <summary>
    ///     Błędy typowe dla serwisu AuthService
    ///     Kody z prefixem "06"
    /// </summary>
    public class AuthErrors
    {
        public static readonly string ErrorCodePrefix = "06";

        /// <summary>
        ///     Twoje konto zostało usunięte
        /// </summary>
        public DetailedError YourAccountWasDeleted = new DetailedError
        {
            ErrorCode = $"{ErrorCodePrefix}-01",
            DescriptionFormatter = "Twoje konto zostało usunięte"
        };

        /// <summary>
        ///     Wystąpił błąd uwierzytelniania
        /// </summary>
        public DetailedError AnErrorOccuredWhileAuthenticating = new DetailedError
        {
            ErrorCode = $"{ErrorCodePrefix}-02",
            DescriptionFormatter = "Wystąpił błąd uwierzytelniania"
        };
    }
}