using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos.Hub;
using Application.HubConfig;
using AutoMapper;
using Domain.Models;
using Domain.Models.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;

namespace Application.Services
{
    public class AlgorithmService : BackgroundService
    {
        private readonly IHubContext<AquariumHub,IAquariumHubInterface> _hub;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private DataContext _context;
        private IMapper Mapper;

        public AlgorithmService(IConfiguration configuration, IHubContext<AquariumHub, IAquariumHubInterface> hub, IServiceScopeFactory serviceScopeFactory)
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .Options;

            _serviceScopeFactory = serviceScopeFactory;
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                Mapper = scope.ServiceProvider.GetService<IMapper>();
            }
            _context = new DataContext(options);
            _hub = hub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var stats = _context.PhysicalStatistics.ToList();
                    foreach (var fish in _context.Fishes.Where(f=>f.IsAlive).ToList())
                    {
                        await MakeAMove(fish);
                    }

                    foreach (var aquarium in _context.Aquariums.ToList())
                    {
                        var dataToTransfer = Mapper.Map<Aquarium, HubTransferData>(aquarium);
                        await _hub.Clients.Group($"aq-{aquarium.Id}").TransferData(dataToTransfer);
                    }

                    await Task.Delay(TimeSpan.FromMilliseconds(100), stoppingToken);
                }
                catch (Exception e)
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            };
        }

        private async Task MakeAMove(Fish fish)
        {
            float fishOldX = fish.PhysicalStatistic.X;
            float fishOldY = fish.PhysicalStatistic.Y;

            await CheckIfSeeFoodAndChangeDirectionIfNeed(fish);

            fish.PhysicalStatistic.X += fish.PhysicalStatistic.Vx;
            fish.PhysicalStatistic.Y += fish.PhysicalStatistic.Vy;

            await CheckIfHitBorderAndChangeDirectionIdNeed(fish, fishOldX, fishOldY);
            var val = await _context.SaveChangesAsync();
        }

        private async Task CheckIfSeeFoodAndChangeDirectionIfNeed(Fish fish)
        {
            //tutaj sprawdzamy czy w zasięgu wzroku jest jakies jedzenie
            var listOfFoodInAquarium = fish.Aquarium.Foods;
            foreach (var food in listOfFoodInAquarium)
            {
                var distance = (int)Math.Sqrt(Math.Pow(food.X - fish.PhysicalStatistic.X, 2.0) +
                                Math.Pow(food.Y - fish.PhysicalStatistic.Y, 2.0));
                if(distance > fish.PhysicalStatistic.VisionRange )
                    continue;

                var angleDif = Math.Abs(Math.Atan2(food.Y - fish.PhysicalStatistic.Y, food.X - fish.PhysicalStatistic.X) -
                                        Math.Atan2(fish.PhysicalStatistic.Vy, fish.PhysicalStatistic.Vx));

                if (angleDif > (double)(fish.PhysicalStatistic.VisionAngle * Math.PI / 180))
                    continue;

                if (angleDif == 0)
                    break;

                var a = (double)(food.X - fish.PhysicalStatistic.X);
                var b = (double)(food.Y - fish.PhysicalStatistic.Y);
                var length = (double)Math.Sqrt(Math.Pow(a, 2) - Math.Pow(b, 2));

                if(length > 0 ){
                    fish.PhysicalStatistic.Vx = (float) (a  * fish.PhysicalStatistic.V / length);
                    fish.PhysicalStatistic.Vy = (float) (b  * fish.PhysicalStatistic.V / length);
                 }

                if (length <= fish.PhysicalStatistic.V)
                {
                    _context.Foods.Remove(food);
                    Random random = new Random();
                    _context.Foods.Add(new Food()
                    {
                        X = (float) random.Next(0, fish.Aquarium.Width),
                        Y = (float) random.Next(0, fish.Aquarium.Height),
                        AquariumId = fish.AquariumId,
                    });
                    await _context.SaveChangesAsync();
                    fish.LifeTimeStatistic.FoodCollected++;
                }
                break;
            }
        }

        private async Task CheckIfHitBorderAndChangeDirectionIdNeed(Fish fish, float fishOldX, float fishOldY)
        {
            Random random = new Random();
            bool hit = false;

            //if hit left wall set random positive value on Vx and fill Vy (force to turn right)
            if (fish.PhysicalStatistic.X < 0)
            {
                hit = true;
                fish.PhysicalStatistic.Vx = (float) random.NextDouble() * fish.PhysicalStatistic.V;
                fish.PhysicalStatistic.Vy = (float)Math.Sqrt(Math.Pow(fish.PhysicalStatistic.V, 2.0) - Math.Pow(fish.PhysicalStatistic.Vx, 2.0)) * random.Next(0, 100) > 50 ? 1 : -1;
            }

            //if hit right wall set random negative value on Vx and fill Vy (force to turn left)
            if (fish.PhysicalStatistic.X > fish.Aquarium.Width)
            {
                hit = true;
                fish.PhysicalStatistic.Vx = -(float)random.NextDouble() * fish.PhysicalStatistic.V;
                fish.PhysicalStatistic.Vy = (float)Math.Sqrt(Math.Pow(fish.PhysicalStatistic.V, 2.0) - Math.Pow(fish.PhysicalStatistic.Vx, 2.0)) * random.Next(0, 100) > 50 ? 1 : -1;
            }

            //if hit upper wall set random positive value on Vy and fill Vx (force to turn down)
            if (fish.PhysicalStatistic.Y < 0)
            {
                hit = true;
                fish.PhysicalStatistic.Vy = (float)random.NextDouble() * fish.PhysicalStatistic.V;
                fish.PhysicalStatistic.Vx = (float)Math.Sqrt(Math.Pow(fish.PhysicalStatistic.V, 2.0) - Math.Pow(fish.PhysicalStatistic.Vy, 2.0)) * random.Next(0,100) > 50 ? 1 : -1;
            }

            //if hit bottem wall set random negative value on Vy and fill Vx (force to turn up)
            if (fish.PhysicalStatistic.Y > fish.Aquarium.Height)
            {
                hit = true;
                fish.PhysicalStatistic.Vy = -(float)random.NextDouble() * fish.PhysicalStatistic.V;
                fish.PhysicalStatistic.Vx = (float)Math.Sqrt(Math.Pow(fish.PhysicalStatistic.V, 2.0) - Math.Pow(fish.PhysicalStatistic.Vy, 2.0)) * random.Next(0, 100) > 50 ? 1 : -1;
            }

            //and finally if hit turn position back to dont swim above walls
            if (hit)
            {
                fish.PhysicalStatistic.X = fishOldX;
                fish.PhysicalStatistic.Y = fishOldY;
            }
        }
    }
}
