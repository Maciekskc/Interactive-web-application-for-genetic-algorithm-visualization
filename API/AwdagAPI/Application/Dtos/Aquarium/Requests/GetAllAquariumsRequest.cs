using Application.Dtos.Utilities;
using Persistence.Enums;

namespace Application.Dtos.Aquarium.Requests
{
    public class GetAllAquariumsRequest : PaginationQuery
    {
        public GetAquariumsOrderBy? OrderBy { get; set; }
    }
}
