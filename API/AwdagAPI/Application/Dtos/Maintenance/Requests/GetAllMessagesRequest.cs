using Application.Dtos.Utilities;
using Persistence.Enums;

namespace Application.Dtos.Maintenance.Requests
{
    public class GetAllMessagesRequest : PaginationQuery
    {
        public GetMessagesOrderBy? OrderBy { get; set; }
    }
}