using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Entities.Association;

namespace Domain.Models
{
    public class Fish
    {
        public Guid Id { get; set; }

        [InverseProperty("Fish")]
        public virtual FishAquarium FishAquarium { get; set; }

        [InverseProperty("Fish")]
        public virtual FishPhysicalStatistic FishPhysicalStatistic { get; set; }
    }
}
