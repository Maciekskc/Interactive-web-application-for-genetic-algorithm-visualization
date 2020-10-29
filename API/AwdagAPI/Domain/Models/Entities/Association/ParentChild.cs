using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities.Association
{
    public class ParentChild
    {
        [Key]
        public int Id { get; set; }

        public int ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Fish Parent { get; set; }

        public int ChildId { get; set; }
        [ForeignKey("ChildId")]
        public virtual Fish Child { get; set; }
    }
}
