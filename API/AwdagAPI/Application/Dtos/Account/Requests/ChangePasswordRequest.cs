using System.ComponentModel.DataAnnotations;
using Validation.DataAnnotations;

namespace Application.Dtos.Account.Requests
{
    public class ChangePasswordRequest
    {
        [Required]
        [MaxLength(255)]
        public string CurrentPassword { get; set; }

        [Password]
        [MaxLength(255)]
        public string NewPassword { get; set; }

        [Required]
        [Compare(nameof(NewPassword))]
        [MaxLength(255)]
        public string ConfirmNewPassword { get; set; }
    }
}