using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.NewFolder.Response;
using Application.Interfaces;
using Application.Utilities;
using Domain.Models;

namespace Application.Services
{
    public class FishService : Service, IFishService
    {
        public FishService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<ServiceResponse<List<GetFishFromAquariumResponse>>> GetFishesFromAquarium(Guid aquariumId)
        {
            if(aquariumId == null)
                return new ServiceResponse<List<GetFishFromAquariumResponse>>(HttpStatusCode.BadRequest,new []{ "Zły argument"});

            var listOfFishes = Context.Fishes.Where(f => f.Aquarium.Id == aquariumId).ToList();
            var response = Mapper.Map<List<Fish>, List<GetFishFromAquariumResponse>>(listOfFishes);

            return new ServiceResponse<List<GetFishFromAquariumResponse>>(HttpStatusCode.OK,response);
        }
    }
}
