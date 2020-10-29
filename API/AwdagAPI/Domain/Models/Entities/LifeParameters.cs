using System;
using System.ComponentModel;

namespace Domain.Models.Entities
{
    public class LifeParameters
    {
        public static float MAX_HUNGER = 5.0F;
        public static float MAX_VITALITY = 5.0F;

        public int Id { get; set; }

        /// <summary>
        /// Variable thats describe the hunger level of object
        /// </summary>
        [DefaultValue(3.0F)]
        public float Hunger { get; set; }

        /// <summary>
        /// Time interval that set the period of time after whitch fish hungry level will increase
        /// </summary>
        public TimeSpan HungerInterval { get; set; }

        /// <summary>
        /// Last hunger update time to count if planed period of time passed
        /// </summary>
        public DateTime LastHungerUpdate { get; set; }

        /// <summary>
        /// Vitality of fish whitch is his strength in posible attack/defence
        /// </summary>
        [DefaultValue(5.0F)]
        public float Vitality { get; set; }

        /// <summary>
        /// Time interval that set the period of time after whitch fish vitality will regenerate
        /// </summary>
        public TimeSpan VitalityInterval { get; set; }

        /// <summary>
        /// Last vitality update time to count if planed period of time passed
        /// </summary>
        public DateTime LastVitalityUpdate { get; set; }

        public bool ReadyToProcreate { get; set; }

        public virtual Fish Fish { get; set; }
        public int FishId { get; set; }
    }
}
