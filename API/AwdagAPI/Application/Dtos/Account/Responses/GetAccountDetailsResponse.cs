using System;
using System.Collections.Generic;

namespace Application.Dtos.Account.Responses
{
    public class GetAccountDetailsResponse
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