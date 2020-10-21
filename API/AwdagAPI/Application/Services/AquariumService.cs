using System;
using System.Threading.Tasks;
using Application.Dtos.Aquarium.Requests;
using Application.Dtos.Aquarium.Responses;
using Application.Interfaces;
using Application.Utilities;

namespace Application.Services
{
    public class AquariumService : Service, IAquariumService
    {
        public AquariumService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<ServiceResponse<GetAquariumResponse>> CreateAquariumAsync(CreateAquariumRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<GetAquariumResponse>> GetAquariumAsync(int aquariumId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<GetAquariumResponse>> EditAquariumAsync(int aquariumId, EditAquariumRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse> RemoveAquariumAsync(int aquariumId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<GetAllAquariumsResponse>> GetAllAquariumsAsync(GetAllAquariumsRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
