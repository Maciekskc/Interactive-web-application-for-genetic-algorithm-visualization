using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities.Association
{
    public class FishPhysicalStatistic
    {
        [Key, Column(Order = 1)]
        public Guid PhysicalStatisticId { get; set; }
        public virtual PhysicalStatistic PhysicalStatistic { get; set; }
        [Key, Column(Order = 2)]
        public string FishId { get; set; }
        public virtual Fish Fish { get; set; }
    }
}
