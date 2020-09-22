using System;

namespace Domain.Models.Entities
{
    public class UsedPersonalizedVoucher
    {
        public int Id { get; set; }
        public Guid PersonalizedVoucherId { get; set; }
        public virtual PersonalizedVoucher PersonalizedVoucher { get; set; }

        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}