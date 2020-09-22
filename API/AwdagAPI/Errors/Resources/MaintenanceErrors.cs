using Validation.Models;

namespace Validation.Resources
{
    /// <summary>
    ///     Błędy typowe dla serwisu MaintenanceService
    ///     Kody z prefixem "02"
    /// </summary>
    public class MaintenanceErrors
    {
        public static readonly string ErrorCodePrefix = "02";

        /// <summary>
        ///     Data rozpoczęcia nie może być większa, niż data zakończenia
        /// </summary>
        // todo: przenieść do validatora
        public DetailedError StartDateMustBeNotGreaterThanEndDate = new DetailedError
        {
            ErrorCode = $"{ErrorCodePrefix}-01",
            DescriptionFormatter = "Data rozpoczęcia nie może być większa, niż data zakończenia"
        };
    }
}