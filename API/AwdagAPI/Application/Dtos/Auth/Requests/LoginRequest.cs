using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Auth.Requests
{
    public class LoginRequest
    {
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(255)]
        public string Password { get; set; }
    }
}