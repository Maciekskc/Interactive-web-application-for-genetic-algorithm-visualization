using Application.Dtos.Utilities;
using Persistence.Enums;
using System;
using System.Collections.Generic;

namespace Application.Dtos.Maintenance.Responses
{
    public class GetAllMessagesResponse : PagedResponse<MessageForGetAllMessagesResponse>
    {
        public GetAllMessagesResponse(PaginationQuery request, IEnumerable<MessageForGetAllMessagesResponse> data, int totalNumberOfItems, GetMessagesOrderBy? orderBy) : base(request, data, totalNumberOfItems)
        {
            OrderBy = orderBy;
        }

        public GetMessagesOrderBy? OrderBy { get; set; }
    }

    public class MessageForGetAllMessagesResponse
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }
    }
}