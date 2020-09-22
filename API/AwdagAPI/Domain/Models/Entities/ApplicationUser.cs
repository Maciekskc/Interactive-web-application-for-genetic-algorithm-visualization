using Domain.Models.Interfaces;

using Microsoft.AspNetCore.Identity;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Entities.Association;

namespace Domain.Models.Entities
{
    public class ApplicationUser : IdentityUser, IHasStringId
    {
        [Required, MaxLength(255)]
        public string FirstName { get; set; }

        [Required, MaxLength(255)]
        public string LastName { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<ArchivedPersonalData> ArchivedPersonalData { get; set; }
        public virtual ICollection<PersonalizedVoucherUser> UsedVouchers { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}