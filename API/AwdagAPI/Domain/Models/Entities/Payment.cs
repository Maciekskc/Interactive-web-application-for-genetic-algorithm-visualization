using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }

        // todo: sprawdzić API PayPal i na tego podstawie zdefiniować odpowiednie pola

        //Albo Order albo AnonymousOrder - nie mogą być dwa na raz null albo być dwa na raz przypisane
        public Guid? OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        public Guid? AnonymousOrderId { get; set; }
        [ForeignKey("AnonymousOrderId")]

        public virtual AnonymousOrder AnonymousOrder { get; set; }
    }
}