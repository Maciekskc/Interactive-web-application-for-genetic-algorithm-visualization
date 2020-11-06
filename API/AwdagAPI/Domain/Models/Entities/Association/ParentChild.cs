using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities.Association
{
    public class ParentChild
    {
        public int ParentId { get; set; }
        public virtual Fish Parent { get; set; }

        public int ChildId { get; set; }
        public virtual Fish Child { get; set; }
    }
}
