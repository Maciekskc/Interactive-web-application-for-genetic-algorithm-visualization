using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities
{
    /// <summary>
    /// Gotowe zdjęcie do produktu (po wszelkich przetworzeniach użytkownika, tj. kadrowanie, filtry)
    /// </summary>
    public class ProductImage
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }


        public Guid ImageId { get; set; }
        [ForeignKey("ImageId")]
        public virtual Image Image { get; set; }
    }
}