using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Entities
{
    public class ProductTransformation
    {
        public int Id { get; set; }
        public float CropX1 { get; set; }
        public float CropX2 { get; set; }
        public float CropY1 { get; set; }
        public float CropY2 { get; set; }
        public bool BlackAndWhite { get; set; }
        public bool Sepia { get; set; }
        public bool MirrorHorizontal { get; set; }
        public bool MirrorVertical { get; set; }

        [Range(0,100)]
        public byte Effect1 { get; set; }

        [Range(0, 100)]
        public byte Effect2 { get; set; }

        public bool Effect3 { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
