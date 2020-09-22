using Validation.Models;

namespace Validation.Resources
{
    /// <summary>
    ///     Błędy typowe dla serwisu AccountService
    ///     Kody z prefixem "01"
    /// </summary>
    public class AccountErrors
    {
        public static readonly string ErrorCodePrefix = "01";

        /// <summary>
        ///     Nie znaleziono użytkownika
        /// </summary>
        public DetailedError UserNotFound = new DetailedError
        {
            ErrorCode = $"{ErrorCodePrefix}-01",
            DescriptionFormatter = "Nie znaleziono użytkownika"
        };

        /// <summary>
        ///     Nie znaleziono użytkownika o podanym emailu \"{0}\"
        /// </summary>
        public DetailedError CouldNotFindUserWithGivenEmail = new DetailedError
        {
            ErrorCode = $"{ErrorCodePrefix}-02",
            DescriptionFormatter = "Nie znaleziono użytkownika o podanym emailu \"{0}\""
        };

        /// <summary>
        ///     Wystąpił błąd podczas aktualizowania konta użytkownika
        /// </summary>
        public DetailedError ErrorOccuredWhileUpdatingUser = new DetailedError
        {
            ErrorCode = $"{ErrorCodePrefix}-03",
            DescriptionFormatter = "Wystąpił błąd podczas aktualizowania konta użytkownika"
        };

        /// <summary>
        ///     Wystąpił błąd podczas potwierdzania emaila
        /// </summary>
        public DetailedError ErrorOccuredWhileConfirmingEmail = new DetailedError
        {
            ErrorCode = $"{ErrorCodePrefix}-04",
            DescriptionFormatter = "Wystąpił błąd podczas potwierdzania emaila"
        };

        /// <summary>
        ///     Wystąpił błąd podczas zmiany hasła
        /// </summary>
        public DetailedError ErrorOccuredWhileChangingPassword = new DetailedError
        {
            ErrorCode = $"{ErrorCodePrefix}-05",
            DescriptionFormatter = "Wystąpił błąd podczas zmiany hasła"
        };

        /// <summary>
        ///     Wystąpił błąd podczas resetowania hasła
        /// </summary>
        public DetailedError ErrorOccuredWhileResettingPassword = new DetailedError
        {
            ErrorCode = $"{ErrorCodePrefix}-06",
            DescriptionFormatter = "Wystąpił błąd podczas resetowania hasła"
        };

        /// <summary>
        ///     Wystąpił błąd podczas tworzenia użytkownika
        /// </summary>
        public DetailedError ErrorOccuredWhileCreatingUser = new DetailedError
        {
            ErrorCode = $"{ErrorCodePrefix}-07",
            DescriptionFormatter = "Wystąpił błąd podczas tworzenia użytkownika"
        };

        /// <summary>
        ///     Wystąpił błąd podczas usuwania użytkownika
        /// </summary>
        public DetailedError ErrorOccuredWhileDeletingUser = new DetailedError
        {
            ErrorCode = $"{ErrorCodePrefix}-08",
            DescriptionFormatter = "Wystąpił błąd podczas usuwania użytkownika"
        };

        /// <summary>
        ///     Wystąpił błąd podczas ustawiania hasła użytkownika
        /// </summary>
        public DetailedError ErrorOccuredWhileSettingPassword = new DetailedError
        {
            ErrorCode = $"{ErrorCodePrefix}-09",
            DescriptionFormatter = "Wystąpił błąd podczas ustawiania hasła użytkownika"
        };

        /// <summary>
        ///     Usunięte konto nie może zostać potwierdzone
        /// </summary>
        public DetailedError DeletedAccountCanNotBeConfirmed = new DetailedError
        {
            ErrorCode = $"{ErrorCodePrefix}-10",
            DescriptionFormatter = "Usunięte konto nie może zostać potwierdzone"
        };

        /// <summary>
        /// Konto z adresem email {0} jest już potwierdzone
        /// </summary>
        public DetailedError EmailIsAlreadyConfirmed = new DetailedError
        {
            ErrorCode = $"{ErrorCodePrefix}-11",
            DescriptionFormatter = "Konto z adresem email {0} jest już potwierdzone"
        };
    }
}