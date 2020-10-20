namespace Domain.Models.Entities
{
    public class Food
    {
        public int Id { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public virtual Aquarium Aquarium { get; set; }
        public int AquariumId { get; set; }
    }
}
