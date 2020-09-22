using System.Threading.Tasks;
using Application.Dtos.Maintenance.Requests;
using Application.Dtos.Maintenance.Responses;
using Application.Utilities;

namespace Application.Interfaces
{
    public interface IMaintenanceService
    {
        /// <summary>
        /// Pobiera wszystkie wiadomości (zarówno archiwalne, obecne jak i nadchodzące)
        /// Obsługuje paginację, sortowanie i wyszukiwanie
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ServiceResponse<GetAllMessagesResponse>> GetAllMessagesAsync(GetAllMessagesRequest request);

        /// <summary>
        /// Pobiera nadchodzące wiadomości (z wyłączeniem obecnych/aktualnych)
        /// Obsługuje paginację, sortowanie i wyszukiwanie
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ServiceResponse<GetUpcomingMessagesResponse>> GetUpcomingMessagesAsync(GetUpcomingMessagesRequest request);

        /// <summary>
        /// Pobiera obecnie obowiązujące wiadomości
        /// Obsługuje paginację, sortowanie i wyszukiwanie
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ServiceResponse<GetCurrentMessagesResponse>> GetCurrentMessagesAsync(GetCurrentMessagesRequest request);

        /// <summary>
        /// Metoda do tworzenia nowej wiadomości
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ServiceResponse<CreateMessageResponse>> CreateMessageAsync(CreateMessageRequest request);

        /// <summary>
        /// Pobiera wiadomość o podanym id
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        Task<ServiceResponse<GetMessageResponse>> GetMessageAsync(int messageId);

        /// <summary>
        /// Aktualizuje wiadomość o podanym id
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ServiceResponse<UpdateMessageResponse>> UpdateMessageAsync(int messageId, UpdateMessageRequest request);

        /// <summary>
        /// Usuwa wiadomość o podanym id
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        Task<ServiceResponse> DeleteMessageAsync(int messageId);
    }
}