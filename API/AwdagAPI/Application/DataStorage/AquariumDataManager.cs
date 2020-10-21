using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos.Fish.Response;
using Application.Interfaces;

namespace Application.DataStorage
{
    public class AquariumDataManager
    {
        private readonly IFishService _fishService;

        public AquariumDataManager(IFishService fishService)
        {
            _fishService = fishService;
        }

        public async Task<List<GetFishesFromAquariumResponse>> GetFishes(int aquariumId)
        {
            return null;
        }
    }
}
