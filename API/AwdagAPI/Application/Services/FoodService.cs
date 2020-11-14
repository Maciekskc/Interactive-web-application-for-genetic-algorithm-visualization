using System;
using System.Net;
using System.Threading.Tasks;
using Application.Dtos.Food.Requests;
using Application.Interfaces;
using Application.Utilities;
using Domain.Models.Entities;

namespace Application.Services
{
    public class FoodService : Service, IFoodService
    {
        public FoodService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<ServiceResponse> CreateAdditionalFoodAsync(int aquariumId, CreateFoodRequest request)
        {
            Random random = new Random();
            var aquarium = await GetEntityByIdAsync<Aquarium>(aquariumId);

            Context.Foods.Add(new Food()
            {
                X = request.X == 0 ? (float)random.NextDouble()*aquarium.Width : request.X,
                Y = request.Y == 0 ? (float)random.NextDouble() * aquarium.Height : request.Y,
                AquariumId = aquariumId
            });

            await SaveChangesAsync();
            return new ServiceResponse(HttpStatusCode.Created);
        }
    }
}
