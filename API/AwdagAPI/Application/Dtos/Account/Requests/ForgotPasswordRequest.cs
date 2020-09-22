using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Account.Requests
{
    public class ForgotPasswordRequest
    {
        [Required]
        [MaxLength(255)]
        [Url]
        public string UrlToIncludeInEmail { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Language { get; set; }
    }
}