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
                    //pętla organizująca algorytm
                    var stats = _context.PhysicalStatistics.ToList();
                    foreach (var fish in _context.Fishes.Where(f=>f.IsAlive).ToList())
                    {
                        if(await CheckAndUpdateLifeParametersAsync(fish))
                            await MakeAMoveAsync(fish);
                    }

                    //przesyłanie danych poprzez hub
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

        private async Task MakeAMoveAsync(Fish fish)
        {
            float fishOldX = fish.PhysicalStatistic.X;
            float fishOldY = fish.PhysicalStatistic.Y;

            //określamy 4.0 jako pułap najedzenia, jeżeli jest poniżej to szukają jedzenia, jeżeli nie to próbują się rozmnażać
            if (fish.LifeParameters.Hunger < 4.0)
            {
                if (fish.SetOfMutations.Predator)
                {
                    await CheckIfSeeFoodAndChangeDirectionIfNeedForPredatorAsync(fish);
                }
                else
                {
                    await CheckIfSeeFoodAndChangeDirectionIfNeedAsync(fish);
                }
            }
            else
            {
                //tutaj wykonamy krzyżowanie
            }

            fish.PhysicalStatistic.X += fish.PhysicalStatistic.Vx;
            fish.PhysicalStatistic.Y += fish.PhysicalStatistic.Vy;

            await CheckIfHitBorderAndChangeDirectionIdNeed(fish, fishOldX, fishOldY);
            //aktualizujemy statystyki po ruchu
            UpdateLifeTimeStatisticOfFishAfterMove(fish);
            var val = await _context.SaveChangesAsync();
        }

        private async Task CheckIfSeeFoodAndChangeDirectionIfNeedAsync(Fish fish)
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
                    //zostawimy ten update pod zapisaem ponieważ po ruchu również odbywa się aktualizacja
                    UpdateLifeTimeStatisticOfFishAfterEat(fish);
                }
                break;
            }
        }

        private async Task CheckIfSeeFoodAndChangeDirectionIfNeedForPredatorAsync(Fish fish)
        {
            //tutaj sprawdzamy czy w zasięgu wzroku jest jakaś rybka
            var listOfTargetInAquarium = fish.Aquarium.Fishes.Where(f=>f.IsAlive);
            foreach (var target in listOfTargetInAquarium)
            {
                //pomijamy opcje zaatakowania samej siebie
                if(target == fish)
                    continue;

                //sprawdzamy czy dystans do celu zgadza się z zakresem widzenia
                var distance = (int)Math.Sqrt(Math.Pow(target.PhysicalStatistic.X - fish.PhysicalStatistic.X, 2.0) +
                                Math.Pow(target.PhysicalStatistic.Y - fish.PhysicalStatistic.Y, 2.0));
                if (distance > fish.PhysicalStatistic.VisionRange)
                    continue;

                //sprawdzamy czy znajduje się w przedziale kątu widzenia
                var angleDif = Math.Abs(Math.Atan2(target.PhysicalStatistic.Y - fish.PhysicalStatistic.Y, target.PhysicalStatistic.X - fish.PhysicalStatistic.X) -
                                        Math.Atan2(fish.PhysicalStatistic.Vy, fish.PhysicalStatistic.Vx));

                if (angleDif > (double)(fish.PhysicalStatistic.VisionAngle * Math.PI / 180))
                    continue;

                if (angleDif == 0)
                    break;

                //nie może zaatakować silniejszego od siebie osobnika
                if (target.LifeParameters.Vitality < fish.LifeParameters.Vitality)
                    continue;

                if (!fish.SetOfMutations.HungryCharge)
                {
                    fish.SetOfMutations.HungryCharge = true;
                    fish.PhysicalStatistic.V *= 2;
                }

                //wyznaczamy wektor kierunkowy do obiektu
                var a = (double)(target.PhysicalStatistic.X - fish.PhysicalStatistic.X);
                var b = (double)(target.PhysicalStatistic.Y - fish.PhysicalStatistic.Y);
                var length = (double)Math.Sqrt(Math.Pow(a, 2) - Math.Pow(b, 2));

                if (length > 0)
                {
                    fish.PhysicalStatistic.Vx = (float)(a * fish.PhysicalStatistic.V / length);
                    fish.PhysicalStatistic.Vy = (float)(b * fish.PhysicalStatistic.V / length);
                }

                if (length <= fish.PhysicalStatistic.V)
                {
                    //atakowany cel ginie a rybka vitalność rybki zostaje zwiększona
                    var attackedTarget = _context.Fishes.Single(t => t.Id == target.Id);
                    attackedTarget.IsAlive = false;
                    attackedTarget.LifeTimeStatistic.DeathDate = DateTime.UtcNow;
                    //specjalna aktualizacja statystyk predatora
                    fish.LifeParameters.Vitality /= 2;
                    fish.SetOfMutations.HungryCharge = false;
                    //szarża zostaje wyłączona jednak prędkości zmienią się dopiero po uderzeniu w ścianę, przymyślic czy to zostawić
                    fish.PhysicalStatistic.V /= 2;

                    await _context.SaveChangesAsync();
                    UpdateLifeTimeStatisticOfFishAfterEat(fish);
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
                fish.PhysicalStatistic.Vy = (float)Math.Sqrt(Math.Pow(fish.PhysicalStatistic.V, 2.0) + Math.Pow(fish.PhysicalStatistic.Vx, 2.0)) * (random.Next(0, 100) > 50 ? 1 : -1);
            }

            //if hit right wall set random negative value on Vx and fill Vy (force to turn left)
            if (fish.PhysicalStatistic.X > fish.Aquarium.Width)
            {
                hit = true;
                fish.PhysicalStatistic.Vx = -(float)random.NextDouble() * fish.PhysicalStatistic.V;
                fish.PhysicalStatistic.Vy = (float)Math.Sqrt(Math.Pow(fish.PhysicalStatistic.V, 2.0) + Math.Pow(fish.PhysicalStatistic.Vx, 2.0)) * (random.Next(0, 100) > 50 ? 1 : -1);
            }

            //if hit upper wall set random positive value on Vy and fill Vx (force to turn down)
            if (fish.PhysicalStatistic.Y < 0)
            {
                hit = true;
                fish.PhysicalStatistic.Vy = (float)random.NextDouble() * fish.PhysicalStatistic.V;
                fish.PhysicalStatistic.Vx = (float)Math.Sqrt(Math.Pow(fish.PhysicalStatistic.V, 2.0) + Math.Pow(fish.PhysicalStatistic.Vy, 2.0)) * (random.Next(0,100) > 50 ? 1 : -1);
            }

            //if hit bottem wall set random negative value on Vy and fill Vx (force to turn up)
            if (fish.PhysicalStatistic.Y > fish.Aquarium.Height)
            {
                hit = true;
                fish.PhysicalStatistic.Vy = -(float)random.NextDouble() * fish.PhysicalStatistic.V;
                fish.PhysicalStatistic.Vx = (float)Math.Sqrt(Math.Pow(fish.PhysicalStatistic.V, 2.0) + Math.Pow(fish.PhysicalStatistic.Vy, 2.0)) * (random.Next(0, 100) > 50 ? 1 : -1);
            }

            //and finally if hit turn position back to dont swim above walls
            if (hit)
            {
                fish.PhysicalStatistic.X = fishOldX;
                fish.PhysicalStatistic.Y = fishOldY;
            }
        }

        /// <summary>
        /// aktualizuje statystyke rybki po każdym ruchu, np przepłynięty dystans
        /// </summary>
        /// <param name="fish"></param>
        private void UpdateLifeTimeStatisticOfFishAfterMove(Fish fish)
        {
            fish.LifeTimeStatistic.DistanceSwimmed += fish.PhysicalStatistic.V;
        }

        /// <summary>
        /// aktualizuje statystyki związane z jedzeniem i zapisuje stan w bazie danych, nie zapisuje zmian w bazie
        /// </summary>
        /// <param name="fish"></param>
        /// <returns></returns>
        private void UpdateLifeTimeStatisticOfFishAfterEat(Fish fish)
        {
            //aktualizujemy współczynnik głodu dodajemy 0.5 lub róznice między maksymalnym możliwym poziomem a aktualnym głodem
            fish.LifeParameters.Hunger += LifeParameters.MAX_HUNGER - fish.LifeParameters.Hunger > 0.5F
                    ? 0.5F
                    : LifeParameters.MAX_HUNGER - fish.LifeParameters.Hunger;
            fish.LifeParameters.LastHungerUpdate = DateTime.UtcNow;
            fish.LifeTimeStatistic.FoodCollected++;
        }

        /// <summary>
        /// aktualizowanie  parametrów życiowych obiektu
        /// </summary>
        /// <param name="fish"></param>
        /// <returns>zwraca True jeśli po aktualizacji rybka dalej żyje, w przeciwnym wypadku false</returns>
        private async Task<bool> CheckAndUpdateLifeParametersAsync(Fish fish)
        {
            //sprawdzamy czy minął interwał regeneracji i dodajemy 0.5 lub róznice między maksymalną witalnością
            var diference = fish.LifeParameters.LastVitalityUpdate - DateTime.UtcNow;
            var diferenceOtherSiide = DateTime.UtcNow- fish.LifeParameters.LastVitalityUpdate;

            var span = fish.LifeParameters.VitalityInterval;
            if (DateTime.UtcNow - fish.LifeParameters.LastVitalityUpdate > fish.LifeParameters.VitalityInterval)
            {

                fish.LifeParameters.Vitality += LifeParameters.MAX_VITALITY - fish.LifeParameters.Vitality > 0.5F
                    ? 0.5F
                    : LifeParameters.MAX_VITALITY - fish.LifeParameters.Vitality;
                fish.LifeParameters.LastVitalityUpdate = DateTime.UtcNow;
            }

            if (DateTime.UtcNow - fish.LifeParameters.LastHungerUpdate > fish.LifeParameters.HungerInterval)
            {
                fish.LifeParameters.Hunger -= 0.5F;
                fish.LifeParameters.LastHungerUpdate = DateTime.UtcNow;
            }

            if (fish.LifeParameters.Hunger <= 0)
            {
                fish.LifeTimeStatistic.DeathDate = DateTime.UtcNow;
                fish.IsAlive = false;
            }

            await _context.SaveChangesAsync();

            if(fish.IsAlive)
                return true;

            return false;
        }
    }
}
