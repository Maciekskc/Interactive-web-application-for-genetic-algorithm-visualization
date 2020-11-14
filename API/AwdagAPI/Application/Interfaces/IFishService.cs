using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos.Fish.Request;
using Application.Dtos.Fish.Response;
using Application.Utilities;

namespace Application.Interfaces
{
    public interface IFishService
    {
        /// <summary>
        /// Returns all fishes from aquarium
        /// </summary>
        /// <param name="aquariumId">Id of aquarium</param>
        /// <returns></returns>
        Task<ServiceResponse<GetFishesFromAquariumResponse>> GetFishesFromAquarium(int aquariumId, GetFishesFromAquariumRequest request);

        /// <summary>
        /// Get all informations about fish
        /// </summary>
        /// <param name="fishId"></param>
        /// <returns></returns>
        Task<ServiceResponse<GetFishResponse>> GetFish(int fishId);

        /// <summary>
        /// Return fishes of current logged user
        /// </summary>
        /// <param name="aquariumId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ServiceResponse<GetUserFishesResponse>> GetUserFishes(GetUserFishesRequest request);

        /// <summary>
        /// Edit fishes with data pasted to query
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ServiceResponse<GetFishResponse>> EditFish(int fishId, EditFishRequest request);

        /// <summary>
        /// Create new fish, statistic will be calculated by aquarium system to make object posibly competetive
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ServiceResponse> CreateFish(CreateFishRequest request);

        /// <summary>
        /// Kill fishes with given id number
        /// </summary>
        /// <param name="fishId"></param>
        /// <returns></returns>
        Task<ServiceResponse> KillFish(int fishId);
    }
}
