using System.Collections.Generic;

namespace Domain.Models.Entities
{
    public class AnonymousAddress
    {
        public int Id { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<AnonymousOrder> AnonymousOrders { get; set; }
    }
}