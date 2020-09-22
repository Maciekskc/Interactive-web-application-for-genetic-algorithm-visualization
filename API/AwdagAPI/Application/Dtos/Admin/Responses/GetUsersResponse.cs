using Application.Dtos.Utilities;
using Persistence.Enums;
using System;
using System.Collections.Generic;

namespace Application.Dtos.Admin.Responses
{
    public class GetUsersResponse : PagedResponse<UserForGetUsersResponse>
    {
        public GetUsersResponse(PaginationQuery request, IEnumerable<UserForGetUsersResponse> data,
            int totalNumberOfItems, GetUsersOrderBy? orderBy) : base(request, data, totalNumberOfItems)
        {
            OrderBy = orderBy;
        }

        public GetUsersOrderBy? OrderBy { get; set; }
    }

    public class UserForGetUsersResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool IsDeleted { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset LockoutEnd { get; set; }
        public int AccessFailedCount { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}