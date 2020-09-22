using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.Entities.Association
{
    public class FishAquarium
    {
        [Key, Column(Order = 1)]
        public Guid AquariumId { get; set; }
        public virtual Aquarium Aquarium { get; set; }

        [Key, Column(Order = 2)]
        public string FishId { get; set; }
        public virtual Fish Fish { get; set; }
    }
}
