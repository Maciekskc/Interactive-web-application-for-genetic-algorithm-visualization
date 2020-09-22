using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities
{
    public class AnonymousOrder
    {
        public Guid Id { get; set; }

        [Column(TypeName = "Money")]
        public decimal TotalPrice { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public string Email { get; set; }

        public int AnonymousAddressId { get; set; }
        public virtual AnonymousAddress AnonymousAddress { get; set; }

        public Guid ShipmentId { get; set; }
        public virtual Shipment Shipment { get; set; }

        public Guid PaymentId { get; set; }
        public virtual Payment Payment { get; set; }

        public Guid? InvoiceDataId { get; set; }
        public virtual InvoiceData InvoiceData { get; set; }

        public Guid? UsedPublicVoucherId { get; set; }
        public virtual UsedPublicVoucher UsedPublicVoucher { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}