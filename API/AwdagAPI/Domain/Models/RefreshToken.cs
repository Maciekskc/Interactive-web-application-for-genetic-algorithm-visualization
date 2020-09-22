using Domain.Models.Entities;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class RefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Token { get; set; }

        public string JwtId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool Used { get; set; }

        public bool Invalidated { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}