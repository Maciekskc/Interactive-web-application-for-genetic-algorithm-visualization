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

        public static List<ChartModel> GetData()
        {
            var r = new Random();
            return new List<ChartModel>()
            {
                new ChartModel { Data = new List<int> { r.Next(1, 40) }, Label = "Data1" },
                new ChartModel { Data = new List<int> { r.Next(1, 40) }, Label = "Data2" },
                new ChartModel { Data = new List<int> { r.Next(1, 40) }, Label = "Data3" },
                new ChartModel { Data = new List<int> { r.Next(1, 40) }, Label = "Data4" }
            };
        }

        public async Task<List<GetFishFromAquariumResponse>> GetFishes(Guid aquariumId)
        {
            var list = await _fishService.GetFishesFromAquarium(aquariumId);

            return list.Payload;
        }
    }
}
