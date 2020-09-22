using System;
using System.Collections.Generic;
using Domain.Models.Entities.Association;

namespace Domain.Models.Entities
{
    /// <summary>
    /// Kupony przeznaczone dla wybranych użytkowników
    /// </summary>
    public class PersonalizedVoucher
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
        /// Maksymalna liczba użyć kodu rabatowego dla każdego z przypisanych użytkowników
        /// </summary>
        public int UsesPerUser { get; set; }


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
        /// Flaga definiująca czy dany kupon jest aktywny (na podstawie tylko i wyłączenie dat DateFrom-DateTo!)
        /// </summary>
        public bool IsActive { get; set; }


        public virtual ICollection<PersonalizedVoucherUser> PersonalizedVoucherUsers { get; set; }
        public virtual ICollection<UsedPersonalizedVoucher> UsedPersonalizedVouchers { get; set; }

    }
}