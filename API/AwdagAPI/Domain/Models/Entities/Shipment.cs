using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities
{
    public class Shipment
    {
        public Guid Id { get; set; }

        // todo: właściwie to nie wiemy jakie dane dostaniemy / potrzebujemy do API FedEx, trzeba zrobić research i uzupełnić

        //Albo Order albo AnonymousOrder - nie mogą być dwa na raz null albo być dwa na raz przypisane
        public Guid? OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        public Guid? AnonymousOrderId { get; set; }
        [ForeignKey("AnonymousOrderId")]
        public virtual AnonymousOrder AnonymousOrder { get; set; }

    }
}