using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Dtos.Fish.Request;
using Application.Dtos.Fish.Response;
using Application.Infrastructure.Errors;
using Application.Interfaces;
using Application.Utilities;
using Application.Utilities.QuerySorters;
using Domain.Models;
using Domain.Models.Entities;
using Persistence.Enums;

namespace Application.Services
{
    public class FishService : Service, IFishService
    {
        public FishService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<ServiceResponse<GetFishesFromAquariumResponse>> GetFishesFromAquarium(int aquariumId, GetFishesFromAquariumRequest request)
        {
            bool isQueryIncluded = !string.IsNullOrWhiteSpace(request.Query);

            var dbQuery = Context.Fishes.Where(f => f.IsAlive && f.AquariumId == aquariumId);

            if (isQueryIncluded)
            {
                string queryToLower = request.Query.ToLower();
                dbQuery = dbQuery.Where(f => f.Name.ToLower().Contains(queryToLower));
            }

            dbQuery = FishQuerySorter.GetFishesSortQuery(dbQuery, request.OrderBy);

            var totalNumberOfItems = dbQuery.Count();
            var fishes = dbQuery.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

            var fishesToResponse = Mapper.Map<List<Fish>, List<FishForGetFishesFromAquariumResponse>>(fishes);

            var response = new GetFishesFromAquariumResponse(request, fishesToResponse, totalNumberOfItems, request.OrderBy ?? GetFishesOrderBy.NameAsc);
            return new ServiceResponse<GetFishesFromAquariumResponse>(HttpStatusCode.OK, response);
        }

        public async Task<ServiceResponse<GetFishResponse>> GetFish(int fishId)
        {
            var fish = await GetEntityByIdAsync<Fish>(fishId);
            var response = Mapper.Map<Fish, GetFishResponse>(fish);

            //finding parents of child in asotiations table
            var assotiationsWithParent = Context.ParentChild.Where(x => x.ChildId == fishId).ToList();
            if (assotiationsWithParent.Count() == 2)
            {
                response.Parent1 = Mapper.Map<Fish, ParentOfFishForGetFishResponse>(assotiationsWithParent[0].Parent);
                response.Parent2 = Mapper.Map<Fish, ParentOfFishForGetFishResponse>(assotiationsWithParent[1].Parent);
            }

            return new ServiceResponse<GetFishResponse>(HttpStatusCode.OK,response);
        }

        public async Task<ServiceResponse<GetUserFishesResponse>> GetUserFishes(GetUserFishesRequest request)
        {
            bool isQueryIncluded = !string.IsNullOrWhiteSpace(request.Query);

            var dbQuery = Context.Fishes.Where(f => f.OwnerId == CurrentlyLoggedUser.Id);

            if (isQueryIncluded)
            {
                string queryToLower = request.Query.ToLower();
                dbQuery = dbQuery.Where(f => f.Name.ToLower().Contains(queryToLower));
            }

            dbQuery = FishQuerySorter.GetFishesSortQuery(dbQuery, request.OrderBy);

            var totalNumberOfItems = dbQuery.Count();
            var fishes = dbQuery.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

            var fishesToResponse = Mapper.Map<List<Fish>, List<FishForGetUserFishesResponse>>(fishes);

            var response = new GetUserFishesResponse(request, fishesToResponse, totalNumberOfItems, request.OrderBy ?? GetFishesOrderBy.NameAsc);
            return new ServiceResponse<GetUserFishesResponse>(HttpStatusCode.OK, response);
        }

        public Task<ServiceResponse> EditFish(EditFishRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse> CreateFish(CreateFishRequest request)
        {
            //TODO: TA METODA JEST BARDZO WAŻNA ALE JEJ PRZEROBIENIE WYMAGA DOGŁĘBNEGO SPRAWDZENIA
            var aquarium = await GetEntityByIdAsync<Aquarium>(request.AquariumId);
            if (aquarium.Fishes.Count>=aquarium.Capacity)
                throw new RestException(HttpStatusCode.BadRequest, "Cannot create another object. Aquarium has maximum capacity.");

            Random random = new Random();
            var fish = new Fish()
            {
                Name = request.Name,
                AquariumId = request.AquariumId,
                IsAlive = true,
                OwnerId = CurrentlyLoggedUser == null ? null : CurrentlyLoggedUser.Id,
                PhysicalStatistic = new PhysicalStatistic()
                {
                    X = random.Next(0,aquarium.Width),
                    Y = random.Next(0,aquarium.Height),
                    V = random.Next(2,4),
                    Vx = random.Next(2,4) * random.Next(0, 100) > 50 ? 1 : -1,
                    Vy = random.Next(2, 4) * random.Next(0, 100) > 50 ? 1 : -1,
                    Color = String.Format("#{0:X6}", random.Next(0x1000000)),
                },
                SetOfMutations = new SetOfMutations()
                {

                },
                LifeTimeStatistic = new LifeTimeStatistic()
                {
                    BirthDate = DateTime.UtcNow,
                    DeathDate = null,
                },
                LifeParameters = new LifeParameters()
                {
                    HungerInterval = new TimeSpan(0, 0, random.Next(30, 60)),
                    LastHungerUpdate = DateTime.UtcNow,
                    VitalityInterval = new TimeSpan(0, 0, random.Next(55, 60)),
                    LastVitalityUpdate = DateTime.UtcNow
                }
            };
            Context.Fishes.Add(fish);
            await SaveChangesAsync();
            return new ServiceResponse(HttpStatusCode.Created);
        }

        public async Task<ServiceResponse> KillFish(int fishId)
        {
            var fish = await GetEntityByIdAsync<Fish>(fishId);

            if(fish.OwnerId != CurrentlyLoggedUser.Id)
                throw new RestException(HttpStatusCode.Unauthorized, "Cannot kill fish that don't belongs to you.");

            fish.IsAlive = false;
            await SaveChangesAsync();
            return new ServiceResponse(HttpStatusCode.OK);
        }
    }
}
