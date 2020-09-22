using System.Collections.Generic;
using Domain.Models.Entities.Association;

namespace Domain.Models.Entities
{
    public class ProductTypeParameter
    {
        public int Id { get; set; }

        // todo: co tak właściwie ma być w product type parameter? 

        /// <summary>
        /// Nazwa parametru 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Wartosć parametru
        /// </summary>
        public double Value { get; set; }

        public virtual ICollection<ProductTypeParameterProductType> ProductTypeParameterProductTypes { get; set; }
    }
}