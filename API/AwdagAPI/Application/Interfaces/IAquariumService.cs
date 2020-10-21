using System.Threading.Tasks;
using Application.Dtos.Aquarium.Requests;
using Application.Dtos.Aquarium.Responses;
using Application.Utilities;

namespace Application.Interfaces
{
    public interface IAquariumService
    {
        Task<ServiceResponse<GetAquariumResponse>> CreateAquariumAsync(CreateAquariumRequest request);
        Task<ServiceResponse<GetAquariumResponse>> GetAquariumAsync(int aquariumId);
        Task<ServiceResponse<GetAquariumResponse>> EditAquariumAsync(int aquariumId, EditAquariumRequest request);
        Task<ServiceResponse> RemoveAquariumAsync(int aquariumId);
        Task<ServiceResponse<GetAllAquariumsResponse>> GetAllAquariumsAsync(GetAllAquariumsRequest request);
    }
}
