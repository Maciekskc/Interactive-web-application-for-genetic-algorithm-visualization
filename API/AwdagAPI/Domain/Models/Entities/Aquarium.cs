using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.Entities
{
    public class Aquarium
    {
        public Guid Id { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
        
        public virtual ICollection<Fish> Fishes { get; set; }
    }
}
