using Validation.Resources;

namespace Validation
{
    public static class Errors
    {
        /// <summary>
        ///     Błędy typowe dla serwisu AccountService
        ///     Kody z prefixem "01"
        /// </summary>
        public static AccountErrors AccountErrors { get; set; }

        /// <summary>
        ///     Błędy typowe dla serwisu MaintenanceService
        ///     Kody z prefixem "02"
        /// </summary>
        public static MaintenanceErrors MaintenanceErrors { get; set; }

        /// <summary>
        ///     Błędy typowe dla operacji na bazie danych
        ///     Kody z prefixem "03"
        /// </summary>
        public static DatabaseErrors DatabaseErrors { get; set; }

        /// <summary>
        ///     Błędy typowe dla serwisu AdminService
        ///     Kody z prefixem "05"
        /// </summary>
        public static UserCreatorErrors UserCreatorErrors { get; set; }

        /// <summary>
        ///     Błędy typowe dla serwisu AuthService
        ///     Kody z prefixem "06"
        /// </summary>
        public static AuthErrors AuthErrors { get; set; }

        /// <summary>
        ///     Błędy typowe dla serwisu EmailService
        ///     Kody z prefixem "04"
        /// </summary>
        public static EmailErrors EmailErrors { get; set; }

        /// <summary>
        ///  Błędy typowe dla plików
        ///  Kody z prefixem "10"
        /// </summary>
        public static FileErrors FileErrors { get; set; }

        /// <summary>
        ///  Błędy typowe dla serwisu LogService
        ///  Kody z prefixem "11"
        /// </summary>
        public static LogErrors LogErrors { get; set; }

        static Errors()
        {
            AccountErrors = new AccountErrors();
            MaintenanceErrors = new MaintenanceErrors();
            DatabaseErrors = new DatabaseErrors();
            UserCreatorErrors = new UserCreatorErrors();
            AuthErrors = new AuthErrors();
            EmailErrors = new EmailErrors();
            LogErrors = new LogErrors();
        }
    }
}