using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities
{
    /// <summary>
    /// Dane do faktury
    /// </summary>
    public class InvoiceData
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Nazwa/kod faktury
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Data wystawienia faktury
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Typ płatności, np przelew, karta kredytowa, gotówka
        /// </summary>
        public PaymentType PaymentType { get; set; }

        public int InvoiceCustomerId { get; set; }
        public virtual InvoiceCustomer InvoiceCustomer { get; set; }

        public int InvoiceVendorId { get; set; }
        public virtual InvoiceVendor InvoiceVendor { get; set; }


        //Albo Order albo AnonymousOrder - nie mogą być dwa na raz null albo być dwa na raz przypisane
        public Guid? OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        public Guid? AnonymousOrderId { get; set; }
        [ForeignKey("AnonymousOrderId")]
        public virtual AnonymousOrder AnonymousOrder { get; set; }
    }
}