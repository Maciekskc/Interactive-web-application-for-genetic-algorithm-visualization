using Application.Dtos.Utilities;
using Persistence.Enums;

namespace Application.Dtos.Maintenance.Requests
{
    public class GetUpcomingMessagesRequest : PaginationQuery
    {
        public GetMessagesOrderBy? OrderBy { get; set; }
    }
}