using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Entities.Association;

namespace Domain.Models
{
    public class PhysicalStatistic
    {
        public Guid Id { get; set; }

        [InverseProperty("PhysicalStatistic")]
        public virtual FishPhysicalStatistic FishPhysicalStatistic { get; set; }
    }
}
