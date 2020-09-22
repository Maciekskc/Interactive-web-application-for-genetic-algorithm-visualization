using Validation.Models;

namespace Validation.Resources
{
    /// <summary>
    ///     Błędy typowe dla operacji na bazie danych
    ///     Kody z prefixem "03"
    /// </summary>
    public class DatabaseErrors
    {
        public static readonly string ErrorCodePrefix = "03";

        /// <summary>
        ///     Błąd ogólny
        ///     Wystąpił błąd podczas zapisu do bazy
        /// </summary>
        public DetailedError ErrorOccuredWhileSaving = new DetailedError
        {
            ErrorCode = $"{ErrorCodePrefix}-01",
            DescriptionFormatter = "Wystąpił błąd podczas zapisu do bazy"
        };

        /// <summary>
        ///     Nieprawidłowy identyfikator zasobu
        /// </summary>
        public DetailedError InvalidResourceIdentifier = new DetailedError
        {
            ErrorCode = $"{ErrorCodePrefix}-02",
            DescriptionFormatter = "Nieprawidłowy identyfikator zasobu"
        };
    }
}