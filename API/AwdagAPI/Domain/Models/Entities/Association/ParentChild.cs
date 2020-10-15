using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.Entities.Association
{
    public class ParentChild
    {
        [Key, Column(Order = 1)]
        public int ParentId { get; set; }
        public virtual Fish Parent { get; set; }

        [Key, Column(Order = 2)]
        public int ChildId { get; set; }
        public virtual Fish Child { get; set; }
    }
}
