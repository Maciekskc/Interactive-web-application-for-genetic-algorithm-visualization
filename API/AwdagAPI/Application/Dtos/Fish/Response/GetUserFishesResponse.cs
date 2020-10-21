using Application.Dtos.Utilities;
using System.Collections.Generic;
using Persistence.Enums;

namespace Application.Dtos.Fish.Response
{
    public class GetUserFishesResponse : PagedResponse<FishForGetUserFishesResponse>
    {
        public GetUserFishesResponse(PaginationQuery request, IEnumerable<FishForGetUserFishesResponse> data,
            int totalNumberOfItems, GetFishesOrderBy? orderBy) : base(request, data, totalNumberOfItems)
        {
            OrderBy = orderBy;
        }

        public GetFishesOrderBy? OrderBy { get; set; }
    }

    public class FishForGetUserFishesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAlive { get; set; }
    }
}
