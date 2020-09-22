using Validation.Models;

namespace Validation.Resources
{
    /// <summary>
    ///     Błędy typowe dla UserCreator
    ///     Kody z prefixem "05"
    /// </summary>
    public class UserCreatorErrors
    {
        public static readonly string ErrorCodePrefix = "05";

        /// <summary>
        /// Rola \"{0}\" nie istnieje
        /// </summary>
        public DetailedError GivenRoleDoesNotExist = new DetailedError
        {
            ErrorCode = $"{ErrorCodePrefix}-01",
            DescriptionFormatter = "Rola \"{0}\" nie istnieje"
        };

        /// <summary>
        ///     Adres email \"{0}\" jest już zajęty
        /// </summary>
        public DetailedError EmailIsAlreadyTaken = new DetailedError
        {
            ErrorCode = $"{ErrorCodePrefix}-02",
            DescriptionFormatter = "Adres email \"{0}\" jest już zajęty"
        };
    }
}