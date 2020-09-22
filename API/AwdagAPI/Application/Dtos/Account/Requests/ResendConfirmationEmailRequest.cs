using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Account.Requests
{
    public class ResendConfirmationEmailRequest
    {
        [Required]
        [MaxLength(255)]
        [Url]
        public string UrlToIncludeInEmail { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required]
        public string Language { get; set; }
    }
}