using System;
using System.ComponentModel;

namespace Domain.Models
{
    public class LifeTimeStatistic
    {
        public int Id { get; set; }

        public DateTime BirthDate { get; set; }

        [DefaultValue(null)]
        public DateTime DeathDate { get; set; }

        [DefaultValue(0)]
        public int FoodCollected { get; set; }

        [DefaultValue(0.0)]
        public double DistanceSwimmed { get; set; }

        [DefaultValue(0)]
        public int Descendants { get; set; }


        public virtual Fish Fish { get; set; }
        public int FishId { get; set; }
    }
}
