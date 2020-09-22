using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Validation.DataAnnotations;

namespace Application.Dtos.Admin.Requests
{
    public class CreateUserRequest
    {
        [Required]
        [MaxLength(255)]
        [Url]
        public string UrlToIncludeInEmail { get; set; }

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Password]
        [MaxLength(255)]
        public string Password { get; set; }

        [Roles]
        public List<string> Roles { get; set; }

        [Required]
        public string Language { get; set; }
    }
}