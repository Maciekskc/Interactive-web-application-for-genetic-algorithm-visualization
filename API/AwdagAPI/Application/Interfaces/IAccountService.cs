using Application.Dtos.Account.Requests;
using Application.Dtos.Account.Responses;
using System.Threading.Tasks;
using Application.Utilities;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        /// <summary>
        /// Zwraca szczegóły o koncie użytkownika, który wysyła request
        /// </summary>
        /// <returns></returns>
        Task<ServiceResponse<GetAccountDetailsResponse>> GetAccountDetailsAsync();

        /// <summary>
        /// Aktualizuje podstawowe informacje o profilu użytkownika
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ServiceResponse<UpdateAccountDetailsResponse>> UpdateAccountDetailsAsync(UpdateAccountDetailsRequest request);

        /// <summary>
        /// Metoda potwierdzająca email/konto użytkownika
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="confirmationCode"></param>
        /// <returns></returns>
        Task<ServiceResponse> ConfirmEmailAsync(string userId, string confirmationCode);

        /// <summary>
        /// Metoda do zmiany hasła użytkownika
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ServiceResponse> ChangePasswordAsync(ChangePasswordRequest request);

        /// <summary>
        /// Metoda do przypominania hasła
        /// Wysyła email z linkiem do resetowania hasła
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ServiceResponse> ForgotPasswordAsync(ForgotPasswordRequest request);

        /// <summary>
        /// Metoda do resetowania hasła i ustawiania nowego
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ServiceResponse> ResetPasswordAsync(ResetPasswordRequest request);

        /// <summary>
        /// Metoda służąca do ponownego wysłania emaila z potwierdzeniem konta
        /// Email zostaje wysłany w przypadku, gdy użytkownik nie potwierdził jeszcze swojego konta
        /// W przeciwnym razie zwraca błąd
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ServiceResponse> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request);
    }
}