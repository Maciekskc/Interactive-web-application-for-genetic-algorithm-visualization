using Application.Dtos.Utilities;
using Persistence.Enums;

namespace Application.Dtos.Fish.Request
{
    public class GetUserFishesRequest : PaginationQuery
    {
        public GetFishesOrderBy? OrderBy { get; set; }
    }
}
