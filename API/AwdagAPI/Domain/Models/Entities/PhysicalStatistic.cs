using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Domain.Models
{
    public class PhysicalStatistic
    {
        public Guid Id { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float V { get; set; }
        public float Vx { get; set; }
        public float Vy { get; set; }
        public string Color { get; set; }

        public virtual Fish Fish { get; set; }
        public Guid FishId { get; set; }
    }
}
