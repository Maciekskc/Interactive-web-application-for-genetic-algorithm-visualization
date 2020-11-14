using Application.Dtos.Admin.Requests;
using Application.Dtos.Admin.Responses;
using System.Threading.Tasks;
using Application.Utilities;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        /// <summary>
        /// Metoda zwracająca listę użytkowników
        /// Obsługuje paginację, wyszukiwanie i sortowanie
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ServiceResponse<GetUsersResponse>> GetUsersAsync(GetUsersRequest request);

        /// <summary>
        /// Metoda zwracająca szczegóły o pojedynczym użytkowniku
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ServiceResponse<GetUserResponse>> GetUserAsync(string userId);

        /// <summary>
        /// Metoda do tworzenia nowego użytkownika z poziomu panelu administratora
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ServiceResponse<CreateUserResponse>> CreateUserAsync(CreateUserRequest request);

        /// <summary>
        /// Metoda do aktualizowania informacji o użytkowniku
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ServiceResponse<UpdateUserResponse>> UpdateUserAsync(string userId, UpdateUserRequest request);

        /// <summary>
        /// Metoda do usuwania użytkownika
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ServiceResponse> DeleteUserAsync(string userId);

        /// <summary>
        /// Metoda do ustalania hasła użytkownikowi przez administratora
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ServiceResponse> SetUserPasswordAsync(string userId, SetUserPasswordRequest request);

        /// <summary>
        /// Metoda Do tworzenia rybki pozwalająca edytować wszystkie statystyki
        /// </summary>
        /// <param name="fish"></param>
        /// <returns></returns>
        Task<ServiceResponse> CreateExtraordinaryFish(Fish fish);
    }
}