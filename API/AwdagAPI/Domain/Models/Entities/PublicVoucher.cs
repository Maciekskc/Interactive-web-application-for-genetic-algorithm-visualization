using System;
using System.Collections.Generic;

namespace Domain.Models.Entities
{
    /// <summary>
    /// Ogólnodostępne kupony
    /// </summary>
    public class PublicVoucher
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Opis kuponu, np "Kupon letni 2020"
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Kod rabatowy
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Maksymalna liczba użyć kodu rabatowego
        /// </summary>
        public int UseLimit { get; set; }


        /// <summary>
        /// Data od kiedy kupon obowiązuje
        /// </summary>
        public DateTime DateFrom { get; set; }


        /// <summary>
        /// Data do kiedy kupon obowiązuje
        /// </summary>
        public DateTime DateTo { get; set; }

        /// <summary>
        /// Zniżka wyrażona w procentach
        /// </summary>
        public int DiscountInPercents { get; set; }

        /// <summary>
        /// Flaga definiująca czy dany kupon jest aktywny (na podstawie dat DateFrom-DateTo oraz limitu użyć UseLimit!)
        /// </summary>
        public bool IsActive { get; set; }

        public virtual ICollection<UsedPublicVoucher> UsedPublicVouchers { get; set; }
    }
}