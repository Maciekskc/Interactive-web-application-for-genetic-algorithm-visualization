using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.NewFolder.Response;
using Application.HubConfig;
using Application.Interfaces;
using Application.Services;
using Domain.Models;

namespace Application.DataStorage
{
    public class AquariumDataManager
    {
        private readonly IFishService _fishService;

        public AquariumDataManager(IFishService fishService)
        {
            _fishService = fishService;
        }

        public async Task<List<GetFishFromAquariumResponse>> GetFishes(Guid aquariumId)
        {
            var list = await _fishService.GetFishesFromAquarium(aquariumId);

            return list.Payload;
        }
    }
}
