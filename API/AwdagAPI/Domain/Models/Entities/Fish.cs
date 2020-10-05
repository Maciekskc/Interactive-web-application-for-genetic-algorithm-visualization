using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Entities;

namespace Domain.Models
{
    public class Fish
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual Aquarium  Aquarium { get; set; }
        public Guid AquariumId { get; set; }

        public virtual PhysicalStatistic PhysicalStatistic { get; set; }
    }
}
