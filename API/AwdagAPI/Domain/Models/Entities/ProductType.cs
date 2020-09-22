using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Entities.Association;

namespace Domain.Models.Entities
{
    /// <summary>
    /// Typ produktu, np naklejka, fototapeta, obraz
    /// </summary>
    public class ProductType
    {
        public int Id { get; set; }

        /// <summary>
        /// Nazwa typu produktu, np naklejka, fototapeta, obraz
        /// </summary>
        [Required, MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Opodatkowanie wyrażone w procentach
        /// </summary>
        public TaxAmount TaxAmount { get; set; }

        /// <summary>
        /// Typ podatku, np VAT, GST
        /// </summary>
        /// <value></value>
        public TaxType TaxType { get; set; }

        public virtual ICollection<ProductTypeParameterProductType> ProductTypeParameterProductTypes { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductTypePricing> ProductTypePricings { get; set; }
    }
}