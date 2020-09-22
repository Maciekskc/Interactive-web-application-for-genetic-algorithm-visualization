using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Account.Requests
{
    public class UpdateAccountDetailsRequest
    {
        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        public bool IsDeleted { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
    }
}