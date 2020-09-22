using System;

namespace Domain.Models.Entities.Association
{
    public class PersonalizedVoucherUser
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Guid PersonalizedVoucherId { get; set; }
        public virtual PersonalizedVoucher PersonalizedVoucher { get; set; }
    }
}
