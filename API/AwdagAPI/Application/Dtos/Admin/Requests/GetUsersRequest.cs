using Application.Dtos.Utilities;
using Persistence.Enums;

namespace Application.Dtos.Admin.Requests
{
    public class GetUsersRequest : PaginationQuery
    {
        public GetUsersOrderBy? OrderBy { get; set; }
    }
}