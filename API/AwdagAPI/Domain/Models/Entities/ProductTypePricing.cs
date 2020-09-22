using System;

namespace Domain.Models.Entities
{
    /// <summary>
    /// Cena wyświetlana jako "przed promocją"
    /// </summary>
    public class ProductTypePricing
    {
        public int Id { get; set; }

        /// <summary>
        /// Data od kiedy ma obowiazywać "promocja"
        /// </summary>
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// Data do kiedy ma obowiązywać "promocja"
        /// </summary>
        public DateTime DateTo { get; set; }

        /// <summary>
        /// Mnożnik ceny podstawowej, wyrażany w liczbie > 1, aby pokazać, że coś jest "tańsze", niż było
        /// </summary>
        public float Multiplier { get; set; }

        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}