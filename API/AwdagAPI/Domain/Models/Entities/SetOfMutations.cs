using System.ComponentModel;

namespace Domain.Models.Entities
{
    public class SetOfMutations
    {
        public int Id { get; set; }

        /// <summary>
        /// Variable that allow fish to be a predator whitch allow object attack other
        /// </summary>
        [DefaultValue(false)]
        public bool Predator { get; set; }

        /// <summary>
        /// Variable that allow fish to fastest its speed whitch will decrease its hunger time iterval
        /// </summary>
        [DefaultValue(false)]
        public bool HungryCharge { get; set; }

        public virtual Fish Fish { get; set; }
        public int FishId { get; set; }
    }
}
