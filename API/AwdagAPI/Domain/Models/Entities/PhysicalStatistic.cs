using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class PhysicalStatistic
    {
        public int Id { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        /// <summary>
        /// speed of fish, the movement of fidh will be described by vectors vx and vy this variable will be used to specify second vector to optimize speed of fish
        /// </summary>
        public float V { get; set; }
        public float Vx { get; set; }
        public float Vy { get; set; }
        public string Color { get; set; }

        /// <summary>
        /// Describes angle of sight to both side from axis of symetry of object expressed in degrees
        /// </summary>
        [Range(0,90), DefaultValue(15)]
        public int VisionAngle { get; set; }
        /// <summary>
        /// Describes maximal range of sight of object
        /// </summary>
        public int VisionRange { get; set; }

        public virtual Fish Fish { get; set; }
        public int FishId { get; set; }
    }
}
