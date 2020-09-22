using Application.Dtos.Utilities;
using Persistence.Enums;

namespace Application.Dtos.Maintenance.Requests
{
    public class GetCurrentMessagesRequest : PaginationQuery
    {
        public GetMessagesOrderBy? OrderBy { get; set; }
    }
}