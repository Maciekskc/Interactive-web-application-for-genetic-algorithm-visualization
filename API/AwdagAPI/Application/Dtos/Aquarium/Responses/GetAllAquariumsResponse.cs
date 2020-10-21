using System.Collections.Generic;
using Application.Dtos.Utilities;
using Persistence.Enums;

namespace Application.Dtos.Aquarium.Responses
{
    public class GetAllAquariumsResponse : PagedResponse<AquariumForGetAllAquariumsResponse>
    {
        public GetAllAquariumsResponse(PaginationQuery request, IEnumerable<AquariumForGetAllAquariumsResponse> data,
            int totalNumberOfItems, GetAquariumsOrderBy? orderBy) : base(request, data, totalNumberOfItems)
        {
            OrderBy = orderBy;
        }

        public GetAquariumsOrderBy? OrderBy { get; set; }
    }

    public class AquariumForGetAllAquariumsResponse
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Capacity { get; set; }
        public int FoodMaximalAmount { get; set; }

        public int CurrentPopulationCount { get; set; }
        public int CurrentFoodsAmount { get; set; }
    }
}
