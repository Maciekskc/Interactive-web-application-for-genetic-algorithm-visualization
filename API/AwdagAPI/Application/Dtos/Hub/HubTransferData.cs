using System.Collections.Generic;

namespace Application.Dtos.Hub
{
    public class HubTransferData
    {
        public int AquariumWidth { get; set; }
        public int AquariumHeight { get; set; }
        public IList<FishForHubTransferData> Fishes { get; set; }
        public IList<FoodForHubTransferData> Foods { get; set; }
    }

    public class FishForHubTransferData
    {
        public string Name { get; set; }
        public virtual PhysicalStatsForFishForHubTransferData PhysicalStatistic { get; set; }
        public bool Predator { get; set; }
        public bool HungryCharge { get; set; }
    }

    public class PhysicalStatsForFishForHubTransferData
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Vx { get; set; }
        public float Vy { get; set; }
        public string Color { get; set; }
    }

    public class FoodForHubTransferData
    {
        public float X { get; set; }
        public float Y { get; set; }
    }
}
