using System;

namespace Application.Dtos.Fish.Response
{
    public class GetFishResponse
    {
        public string Name { get; set; }
        public bool IsAlive { get; set; }
        public int AquariumId { get; set; }

        public virtual PhysicalStatisticForGetFishResponse PhysicalStatistic { get; set; }
        public virtual LifeParametersForGetFishResponse LifeParameters { get; set; }
        public virtual SetOfMutationsForGetFishResponse SetOfMutations { get; set; }
        public virtual LifeTimeStatisticForGetFishResponse LifeTimeStatistic { get; set; }


        public ParentOfFishForGetFishResponse? Parent1 { get; set; }
        public ParentOfFishForGetFishResponse? Parent2 { get; set; }
    }

    public class PhysicalStatisticForGetFishResponse
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float V { get; set; }
        public float Vx { get; set; }
        public float Vy { get; set; }
        public string Color { get; set; }
        public int VisionAngle { get; set; }
        public int VisionRange { get; set; }
    }

    public class SetOfMutationsForGetFishResponse
    {
        public bool Predator { get; set; }
        public bool HungryCharge { get; set; }
    }

    public class LifeParametersForGetFishResponse
    {
        public float Hunger { get; set; }
        public DateTime LastHungerUpdate { get; set; }
        public float Vitality { get; set; }
    }

    public class LifeTimeStatisticForGetFishResponse
    {
        public DateTime BirthDate { get; set; }
        public DateTime DeathDate { get; set; }

        public int FoodCollected { get; set; }
        public double DistanceSwimmed { get; set; }
        public int Descendants { get; set; }
    }

    public class ParentOfFishForGetFishResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAlive { get; set; }
        public string Color { get; set; }
    }
}
