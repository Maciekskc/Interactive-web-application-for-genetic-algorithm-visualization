namespace Application.Dtos.Aquarium.Requests
{
    public class CreateAquariumRequest
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Capacity { get; set; }
        public int FoodMaximalAmount { get; set; }
    }
}
