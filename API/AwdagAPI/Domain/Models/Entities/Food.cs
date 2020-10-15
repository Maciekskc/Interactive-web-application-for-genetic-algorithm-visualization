namespace Domain.Models.Entities
{
    public class Food
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public virtual Aquarium Aquarium { get; set; }
        public int AquariumId { get; set; }
    }
}
