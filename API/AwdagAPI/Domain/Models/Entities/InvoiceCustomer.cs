namespace Domain.Models.Entities
{
    /// <summary>
    /// todo: A może CustomerInvoice -> CustomerInvoices?
    /// Dane faktury klienta/nabywcy
    /// </summary>
    public class InvoiceCustomer
    {
        public int Id { get; set; }

        /// <summary>
        /// Domyślna będzie automatycznie się wypełniała
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Zarchiwizowane nie będą się wyświetlać
        /// </summary>
        public bool IsArchived { get; set; }

        /// <summary>
        /// Nazwa firmy
        /// </summary>
        public string CompanyName { get; set; }

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

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}