﻿using System.Collections.Generic;

namespace Application.Dtos.Aquarium.Responses
{
    public class GetAquariumResponse
    {
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Capacity { get; set; }
        public int FoodMaximalAmount { get; set; }

        public int CurrentPopulationCount { get; set; }
        public int CurrentFoodsAmount { get; set; }

        public ICollection<int> HungaryHistogramData { get; set; }
        public float HungaryAvarage { get; set; }
    }
}
