using Application.Dtos.Utilities;
using Persistence.Enums;
using System;
using System.Collections.Generic;

namespace Application.Dtos.Maintenance.Responses
{
    public class GetUpcomingMessagesResponse : PagedResponse<MessageForGetUpcomingMessagesResponse>
    {
        public GetUpcomingMessagesResponse(PaginationQuery request, IEnumerable<MessageForGetUpcomingMessagesResponse> data, int totalNumberOfItems, GetMessagesOrderBy? orderBy) : base(request, data, totalNumberOfItems)
        {
            OrderBy = orderBy;
        }

        public GetMessagesOrderBy? OrderBy { get; set; }
    }

    public class MessageForGetUpcomingMessagesResponse
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }
    }
}