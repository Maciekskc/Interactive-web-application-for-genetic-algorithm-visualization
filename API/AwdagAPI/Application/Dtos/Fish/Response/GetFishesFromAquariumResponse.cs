using System;
using System.Collections.Generic;
using Application.Dtos.Utilities;
using Persistence.Enums;

namespace Application.Dtos.Fish.Response
{
    public class GetFishesFromAquariumResponse : PagedResponse<FishForGetFishesFromAquariumResponse>
    {
        public GetFishesFromAquariumResponse(PaginationQuery request, IEnumerable<FishForGetFishesFromAquariumResponse> data,
            int totalNumberOfItems, GetFishesOrderBy? orderBy) : base(request, data, totalNumberOfItems)
        {
            OrderBy = orderBy;
        }

        public GetFishesOrderBy? OrderBy { get; set; }
    }

    public class FishForGetFishesFromAquariumResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PhysicalStatsForFishForGetFishFromAquariumResponse PhysicalStatistic { get; set; }
        public LifeParametersForFishForGetFishFromAquariumResponse LifeParameters { get; set; }
        public LifeTimeStatisticForFishForGetFishFromAquariumResponse LifeTimeStatistic { get; set; }
    }

    public class LifeTimeStatisticForFishForGetFishFromAquariumResponse
    {
        public DateTime BirthDate  { get; set; }
    }

    public class LifeParametersForFishForGetFishFromAquariumResponse
    {
        public float Hunger { get; set; }
    }

    public class PhysicalStatsForFishForGetFishFromAquariumResponse
    {
        public float V { get; set; }
    }
}
