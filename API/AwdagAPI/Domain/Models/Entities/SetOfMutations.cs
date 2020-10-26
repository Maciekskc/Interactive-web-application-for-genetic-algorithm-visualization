using System.ComponentModel;

namespace Domain.Models.Entities
{
    public class SetOfMutations
    {
        public int Id { get; set; }

        /// <summary>
        /// Zmienna opisująca czy dany osobnik jest drapieżnikiem
        /// </summary>
        [DefaultValue(false)]
        public bool Predator { get; set; }

        /// <summary>
        /// Zmienna opisującza czy drapieżnik szarżuje, domyślna umieętność drapieżnika pozwalająca dogonić ofiarę
        /// </summary>
        [DefaultValue(false)]
        public bool PredatorAttackCharge { get; set; }

        /// <summary>
        /// Variable that allow fish to fastest its speed whitch will decrease its hunger time iterval
        /// </summary>
        [DefaultValue(false)]
        public bool HungryCharge { get; set; }

        public virtual Fish Fish { get; set; }
        public int FishId { get; set; }
    }
}
