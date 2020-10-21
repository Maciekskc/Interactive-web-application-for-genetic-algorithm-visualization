using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Dtos.Aquarium.Requests;
using Application.Dtos.Aquarium.Responses;
using Application.Interfaces;
using Application.Utilities;
using Application.Utilities.QuerySorters;
using Domain.Models.Entities;
using Persistence.Enums;

namespace Application.Services
{
    public class AquariumService : Service, IAquariumService
    {
        public AquariumService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<ServiceResponse<GetAquariumResponse>> CreateAquariumAsync(CreateAquariumRequest request)
        {
            var aquarium = Mapper.Map<CreateAquariumRequest, Aquarium>(request);
            Context.Aquariums.Add(aquarium);
            await SaveChangesAsync();

            var recordForResponse = Context.Aquariums.SingleOrDefault(a => a == aquarium);
            var response = Mapper.Map<Aquarium, GetAquariumResponse>(recordForResponse);
            return new ServiceResponse<GetAquariumResponse>(HttpStatusCode.Created,response);
        }

        public async Task<ServiceResponse<GetAquariumResponse>> GetAquariumAsync(int aquariumId)
        {
            var recordForResponse = await GetEntityByIdAsync<Aquarium>(aquariumId);
            var response = Mapper.Map<Aquarium, GetAquariumResponse>(recordForResponse);
            return new ServiceResponse<GetAquariumResponse>(HttpStatusCode.OK, response);
        }

        public async Task<ServiceResponse<GetAquariumResponse>> EditAquariumAsync(int aquariumId, EditAquariumRequest request)
        {
            var aquarium = await GetEntityByIdAsync<Aquarium>(aquariumId);

            aquarium.Width = request.Width != 0 ? request.Width : aquarium.Width;
            aquarium.Height = request.Height != 0 ? request.Height : aquarium.Height;
            aquarium.Capacity = request.Capacity != 0 ? request.Capacity : aquarium.Capacity;
            aquarium.FoodMaximalAmount = request.FoodMaximalAmount != 0 ? request.FoodMaximalAmount : aquarium.FoodMaximalAmount;

            await SaveChangesAsync();
            var response = Mapper.Map<Aquarium, GetAquariumResponse>(aquarium);
            return new ServiceResponse<GetAquariumResponse>(HttpStatusCode.OK, response);
        }

        public async Task<ServiceResponse> RemoveAquariumAsync(int aquariumId)
        {
            var aquarium = await GetEntityByIdAsync<Aquarium>(aquariumId);
            Context.Aquariums.Remove(aquarium);
            await SaveChangesAsync();
            return new ServiceResponse(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResponse<GetAllAquariumsResponse>> GetAllAquariumsAsync(GetAllAquariumsRequest request)
        {
            bool isQueryIncluded = !string.IsNullOrWhiteSpace(request.Query);

            var dbQuery = Context.Aquariums.AsQueryable();

            if (isQueryIncluded)
            {
                string queryToLower = request.Query.ToLower();
                dbQuery = dbQuery.Where(a => a.Id.ToString().ToLower().Contains(queryToLower));
            }

            dbQuery = AquariumQuerySorter.GetAquariumsSortQuery(dbQuery, request.OrderBy);

            var totalNumberOfItems = dbQuery.Count();
            var fishes = dbQuery.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

            var fishesToResponse = Mapper.Map<List<Aquarium>, List<AquariumForGetAllAquariumsResponse>>(fishes);

            var response = new GetAllAquariumsResponse(request, fishesToResponse, totalNumberOfItems, request.OrderBy ?? GetAquariumsOrderBy.IdAsc);
            return new ServiceResponse<GetAllAquariumsResponse>(HttpStatusCode.OK, response);
        }
    }
}
