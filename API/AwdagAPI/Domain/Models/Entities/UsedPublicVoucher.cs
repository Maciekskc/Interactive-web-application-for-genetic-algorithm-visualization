using System;

namespace Domain.Models.Entities
{
    public class UsedPublicVoucher
    {
        public int Id { get; set; }

        public Guid PublicVoucherId { get; set; }
        public virtual PublicVoucher PublicVoucher { get; set; }


        //Albo Order albo AnonymousOrder - nie mogą być dwa na raz null albo być dwa na raz przypisane
        public Guid? OrderId { get; set; }
        public virtual Order Order { get; set; }

        public Guid? AnonymousOrderId { get; set; }
        public virtual AnonymousOrder AnonymousOrder { get; set; }
    }
}