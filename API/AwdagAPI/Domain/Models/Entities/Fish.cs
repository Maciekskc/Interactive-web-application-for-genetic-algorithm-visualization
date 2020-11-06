using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Entities;
using Domain.Models.Entities.Association;
using Domain.Models.Interfaces;

namespace Domain.Models
{
    public class Fish : IHasIntId
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DefaultValue(true)]
        public bool IsAlive { get; set; }

        public virtual Aquarium  Aquarium { get; set; }
        public int AquariumId { get; set; }

        public virtual PhysicalStatistic PhysicalStatistic { get; set; }
        public virtual LifeParameters LifeParameters { get; set; }
        public virtual SetOfMutations SetOfMutations { get; set; }
        public virtual LifeTimeStatistic LifeTimeStatistic { get; set; }

        public virtual ICollection<ParentChild> Parents { get; set; }
        public virtual ICollection<ParentChild> Descendants { get; set; }

        public string? OwnerId { get; set; }
        public virtual ApplicationUser? Owner { get; set; }
    }
}
