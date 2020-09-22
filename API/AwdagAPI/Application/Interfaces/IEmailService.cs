using Domain.Models.Entities;
using System.Threading.Tasks;
using Application.Utilities;

namespace Application.Interfaces
{
    public interface IEmailService
    {
        /// <summary>
        /// Wysyła emaila po rejestracji użytkownika.
        /// Email zawiera link do potwierdzenia konta oraz powitanie.
        /// </summary>
        /// <param name="userToRegister"></param>
        /// <param name="generatedEmailConfirmationToken"></param>
        /// <param name="baseUrl"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        Task<ServiceResponse> SendEmailAfterRegistrationAsync(ApplicationUser userToRegister, string generatedEmailConfirmationToken, string baseUrl, string language);

        /// <summary>
        /// Wysyła emaila z linkiem resetującym hasło użytkownika.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordResetToken"></param>
        /// <param name="baseUrl"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        Task<ServiceResponse> SendPasswordResetEmailAsync(ApplicationUser user, string passwordResetToken, string baseUrl, string language);
    }
}