using System;

namespace Domain.Models.Entities
{
    public class ArchivedPersonalData
    {
        public int Id { get; set; }
        public UserData UserData { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public DateTime ChangedAt { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}