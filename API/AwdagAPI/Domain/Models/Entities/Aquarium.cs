using System.Collections.Generic;
using System.ComponentModel;

namespace Domain.Models.Entities
{
    public class Aquarium
    {
        public int Id { get; set; }

        [DefaultValue(1080)]
        public int Width { get; set; }

        [DefaultValue(720)]
        public int Height { get; set; }

        /// <summary>
        /// Maximum population size in one aquarium
        /// </summary>
        [DefaultValue(20)]
        public int Capacity { get; set; }

        [DefaultValue(10)]
        public int FoodMaximalAmount { get; set; }

        public virtual ICollection<Fish> Fishes { get; set; }
        public virtual ICollection<Food> Foods { get; set; }
    }
}
