using System.ComponentModel.DataAnnotations;
using Validation.DataAnnotations;

namespace Application.Dtos.Account.Requests
{
    public class ResetPasswordRequest
    {
        [Password]
        [MaxLength(255)]
        public string NewPassword { get; set; }

        [Required]
        [MaxLength(255)]
        [Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; }

        [Required]
        [MaxLength(255)]

        public string UserId { get; set; }

        [Required]
        [MaxLength(1023)]
        public string PasswordResetCode { get; set; }
    }
}