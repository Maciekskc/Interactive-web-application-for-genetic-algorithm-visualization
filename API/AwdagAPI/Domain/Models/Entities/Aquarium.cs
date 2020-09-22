using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Domain.Models.Entities.Association;

namespace Domain.Models.Entities
{
    public class Aquarium
    {
        public Guid Id { get; set; }

        public int Width { get; set; } = 1080;

        public int Heigth { get; set; } = 720;

        [InverseProperty("Aquarium")]
        public virtual ICollection<FishAquarium> FishAquarium { get; set; }
    }
}
