using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Models.Entities.Association;

namespace Domain.Models.Entities
{
    public class Image
    {
        public Guid Id { get; set; }

        // todo: czy tytuł w ogóle jest konieczny? 
        // bardziej tutaj myślę od strony wrzucania własnych obrazków, 
        // żeby go np. móc wyszukać wśród swoich wrzuconych zdjeć. 
        // Jeśli chodzi o AdobeStock, to z tytułem nie ma problemu, ponieważ można go wyciągnać z API.


        /// <summary>
        /// Tytuł zdjęcia
        /// </summary>
        [Required, MaxLength(512)]
        public string Title { get; set; }

        /// <summary>
        /// Id obrazu na AdobeStock - wyłącznie w przypadku, gdy ImageProvider jest typu "AdobeStock"
        /// </summary>
        public int? AdobeStockImageId { get; set; }

        /// <summary>
        /// Flaga określająca, czy zdjęcie jest "polecane", tzn czy ma się wyświetlać wyżej w wynikach wyszukiwania
        /// </summary>
        public bool IsFeatured { get; set; }

        /// <summary>
        /// Pochodzenie obrazka
        /// </summary>
        public ImageProvider ImageProvider { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ImageCategory> ImageCategories { get; set; }
    }
}