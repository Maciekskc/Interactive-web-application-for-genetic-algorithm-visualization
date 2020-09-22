namespace Domain.Models.Entities
{
    /// <summary>
    /// Dane sprzedawcy/wystawiającego fakturę
    /// </summary>
    public class InvoiceVendor
    {
        public int Id { get; set; }

        /// <summary>
        /// Nazwa firmy
        /// </summary>
        /// <value></value>

        /// <summary>
        /// Odpowiednik NIPu
        /// </summary>
        public string TaxNumber { get; set; }
        public string Email { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        /// <summary>
        /// Flaga wskazująca na to, czy są to najnowsze, najbardziej aktualne dane sprzedającego
        /// </summary>
        public bool IsActive { get; set; }
    }
}