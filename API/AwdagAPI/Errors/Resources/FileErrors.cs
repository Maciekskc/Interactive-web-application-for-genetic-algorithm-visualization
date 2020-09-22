using Validation.Models;

namespace Validation.Resources
{
    /// <summary>
    ///  Błędy typowe dla plików
    ///  Kody z prefixem "07"
    /// </summary>
    public class FileErrors
    {
        public static readonly string ErrorCodePrefix = "07";

        /// <summary>
        /// "\"{0}\" - Nieobsługiwany typ pliku. Dozwolone rozszerzenie to \"{1}\""
        /// </summary>
        public DetailedError NotSupportedFileType = new DetailedError
        {
            ErrorCode = $"{ErrorCodePrefix}-01",
            DescriptionFormatter = "\"{0}\" - Nieobsługiwany typ pliku. Dozwolone rozszerzenie to \"{1}\""
        };
    }
}
