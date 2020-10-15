using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Entities;
using Domain.Models.Entities.Association;

namespace Domain.Models
{
    public class Fish
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

        [InverseProperty("Parent")]
        public virtual ICollection<ParentChild> Childs { get; set; }
    }
}
