using System.Collections.Generic;
using Domain.Models.Entities.Association;

namespace Domain.Models.Entities
{
    /// <summary>
    /// Kategoria obrazu
    /// </summary>
    public class Category
    {
        public int Id { get; set; }

        /// <summary>
        /// Nazwa kategorii, np architecture, nature
        /// </summary>
        public string Name { get; set; }

        public virtual ICollection<ImageCategory> ImageCategories { get; set; }
    }
}