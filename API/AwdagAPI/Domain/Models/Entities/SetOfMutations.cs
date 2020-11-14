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
        /// mutacja pozwalająca szarżować po zauważeniu jedzenia kosztem szybszej utraty głodu
        /// </summary>
        [DefaultValue(false)]
        public bool HungryCharge { get; set; }

        /// <summary>
        /// Zmienna do uzyskiwania informacji czy aktualnie wykorzystuje swoją mutacje szarży 
        /// </summary>
        [DefaultValue(false)]
        public bool HungryChargeEnabled { get; set; }

        public virtual Fish Fish { get; set; }
        public int FishId { get; set; }
    }
}
