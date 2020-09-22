using System.ComponentModel.DataAnnotations;
using Validation.DataAnnotations;

namespace Application.Dtos.Admin.Requests
{
    public class SetUserPasswordRequest
    {

        [Password]
        [MaxLength(255)]
        public string NewPassword { get; set; }

        [Required]
        [Compare(nameof(NewPassword))]
        [MaxLength(255)]
        public string ConfirmNewPassword { get; set; }
    }
}