using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Validation.DataAnnotations;

namespace Application.Dtos.Admin.Requests
{
    public class UpdateUserRequest
    {
        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Roles]
        public List<string> Roles { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneConfirmed { get; set; }
        public bool IsDeleted { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}