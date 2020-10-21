using System.Collections.Generic;
using Domain.Models.Interfaces;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities
{
    public class ApplicationUser : IdentityUser, IHasStringId
    {
        [Required, MaxLength(255)]
        public string FirstName { get; set; }

        [Required, MaxLength(255)]
        public string LastName { get; set; }

        public bool IsDeleted { get; set; }

        [InverseProperty("Owner")]
        public virtual ICollection<Fish> Fishes { get; set; }
    }
}