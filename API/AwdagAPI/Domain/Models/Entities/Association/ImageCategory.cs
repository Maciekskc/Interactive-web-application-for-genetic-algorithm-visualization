using System;

namespace Domain.Models.Entities.Association
{
    public class ImageCategory
    {
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public Guid ImageId { get; set; }
        public virtual Image Image { get; set; }
    }
}