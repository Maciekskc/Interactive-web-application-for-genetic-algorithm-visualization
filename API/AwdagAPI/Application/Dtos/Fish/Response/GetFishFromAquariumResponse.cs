namespace Application.Dtos.NewFolder.Response
{
    public class GetFishFromAquariumResponse
    {
        public string Name { get; set; }
        public virtual PhysicalStatsForGetFishFromAquariumResponse PhysicalStatistic { get; set; }
    }

    public class PhysicalStatsForGetFishFromAquariumResponse
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Vx { get; set; }
        public float Vy { get; set; }
        public string Color { get; set; }
    }
}
