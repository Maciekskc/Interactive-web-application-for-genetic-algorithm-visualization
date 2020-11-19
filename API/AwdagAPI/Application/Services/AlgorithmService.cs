using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos.Hub;
using Application.HubConfig;
using AutoMapper;
using Domain.Models;
using Domain.Models.Entities;
using Domain.Models.Entities.Association;
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
                    var fishes = _context.Fishes.Where(f => f.IsAlive).ToList();
                    foreach (var fish in fishes)
                    {
                        if (await CheckAndUpdateLifeParametersAsync(fish))
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

        /// <summary>
        /// główna metoda odpowiedzialna za wykonanie ruchu przez rybkę
        /// </summary>
        /// <param name="fish"></param>
        /// <returns></returns>
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
                if(fish.LifeParameters.ReadyToProcreate && fish.Aquarium.Fishes.Where(f=>f.IsAlive).Count() < fish.Aquarium.Capacity * 2)
                    await ProcreationProcess(fish);
            }

            fish.PhysicalStatistic.X += fish.PhysicalStatistic.Vx;
            fish.PhysicalStatistic.Y += fish.PhysicalStatistic.Vy;

            await CheckIfHitBorderAndChangeDirectionIdNeed(fish, fishOldX, fishOldY);
            //aktualizujemy statystyki po ruchu
            UpdateLifeTimeStatisticOfFishAfterMove(fish);
            var val = await _context.SaveChangesAsync();
        }

        /// <summary>
        /// sprawdza czu rybka widzi jedzenie i ustawia ją w jego kierunku
        /// </summary>
        /// <param name="fish"></param>
        /// <returns></returns>
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

                //w tym momencie już widzi jedzenie i następuje aktualizacja ruchu i ewentualnych mutacji
                EnableHungryChargeMutationIfCould(fish);

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
                    //zostawimy ten update pod zapisaem ponieważ po ruchu również odbywa się aktualizacja
                    UpdateLifeTimeStatisticOfFishAfterEat(fish);
                    //jeżeli to pożywienie sprawiło że rybka osiągnęła stan najedzenia to ustawiamy flagi zdolności do prokreacji
                    if (fish.LifeParameters.Hunger >= 4.0F)
                    {
                        MakeFishReadyToProcreate(fish);
                    }
                    await _context.SaveChangesAsync();
                }
                break;
            }
        }

        /// <summary>
        /// sprawdza czu rybka widzi inną rybkę i ustawia ją w jej kierunku TYLKO DLA DRAPIEŻNIKA
        /// </summary>
        /// <param name="fish"></param>
        /// <returns></returns>
        private async Task CheckIfSeeFoodAndChangeDirectionIfNeedForPredatorAsync(Fish fish)
        {
            //tutaj sprawdzamy czy w zasięgu wzroku jest jakaś rybka
            var listOfTargetInAquarium = fish.Aquarium.Fishes.Where(f=>f.IsAlive);
            foreach (var target in listOfTargetInAquarium)
            {
                //pomijamy samą siebie
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

                //w tym momencie już widzi jedzenie i następuje aktualizacja ruchu i ewentualnych mutacji
                EnableHungryChargeMutationIfCould(fish);
                

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
                    //jeżeli to pożywienie sprawiło że rybka osiągnęła stan najedzenia to ustawiamy flagi zdolności do prokreacji
                    if (fish.LifeParameters.Hunger >= 4.0F)
                    {
                        MakeFishReadyToProcreate(fish);
                    }
                }
                break;
            }
        }

        /// <summary>
        /// Sprawdza czy rybka udeżyła w ściany akwarium i zmienia kierunek płynięcia
        /// </summary>
        /// <param name="fish"></param>
        /// <param name="fishOldX"></param>
        /// <param name="fishOldY"></param>
        /// <returns></returns>
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
            DisableHungryChargeMutationIfNeed(fish);
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

            var fishPrevHungerLevel = fish.LifeParameters.Hunger;

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
                if (fish.LifeParameters.Hunger < 4 && fishPrevHungerLevel >= 4)
                    fish.LifeParameters.ReadyToProcreate = false;
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

        /// <summary>
        /// zaczyna akcje związane z rozmnażaniem
        /// </summary>
        /// <param name="fish"></param>
        /// <returns></returns>
        private async Task ProcreationProcess(Fish fish)
        {
            Fish partner = await FindPartnerToProcreate(fish);
            if (partner != null)
            {
                //wyznaczamy wektor kierunkowy do obiektu
                var a = (double)(partner.PhysicalStatistic.X - fish.PhysicalStatistic.X);
                var b = (double)(partner.PhysicalStatistic.Y - fish.PhysicalStatistic.Y);
                var length = (double)Math.Sqrt(Math.Pow(a, 2) - Math.Pow(b, 2));

                if (length > 0)
                {
                    fish.PhysicalStatistic.Vx = (float)(a * fish.PhysicalStatistic.V / length);
                    fish.PhysicalStatistic.Vy = (float)(b * fish.PhysicalStatistic.V / length);
                }

                if (length <= fish.PhysicalStatistic.V)
                {
                    await Procreate(fish, partner);

                    //po krzyżowaniu należy zmienić u rodziców flage zdolności do prokreacji, chcemy żeby w stanie najedzenia mogli stworzyć tylko jednego potomka
                    fish.LifeParameters.ReadyToProcreate = false;
                    partner.LifeParameters.ReadyToProcreate = false;
                }
            }
        }

        /// <summary>
        /// przeszukuje akwarium i znajduje najbliższego partnera do prokreacji
        /// </summary>
        /// <param name="fish"></param>
        /// <returns></returns>
        private async Task<Fish> FindPartnerToProcreate(Fish fish)
        {
            var listOfTargetInAquarium = fish.Aquarium.Fishes.Where(f => f.IsAlive && f.LifeParameters.ReadyToProcreate);
            Fish partner = null;
            float nearestDistance = fish.Aquarium.Width;
            foreach (var target in listOfTargetInAquarium)
            {
                //pomijamy samą siebie
                if (target == fish)
                    continue;

                //szukamy najbliższego partnera 
                var distance = (float) Math.Sqrt(Math.Pow(target.PhysicalStatistic.X - fish.PhysicalStatistic.X, 2.0) +
                                               Math.Pow(target.PhysicalStatistic.Y - fish.PhysicalStatistic.Y, 2.0));
                if (distance < nearestDistance)
                    partner = target;
            }
            return partner;
        }

        /// <summary>
        /// rozmnaża 2 podane rybki na podstawie ich statystyk tworzy potomka który może okazać się odpowiednio lepszy luub gorszy od rodziców
        /// </summary>
        /// <param name="parent1"></param>
        /// <param name="parent2"></param>
        /// <returns></returns>
        private async Task Procreate(Fish parent1, Fish parent2)
        {
            //tutaj przeprowadzamy krzyzowanie w ściśle określonych warunkach
            Random random = new Random();
            //do najważniejsze parametry to prędkość, wzrok oraz kolor(ze względu na zauważalność)
            //krzyżowanie bedzie w przedziałach między statystykami rodziców + możliwy bonus w zależności od zebranego przez rodziców pożywenia
            var foodCollectedByParents =
                parent1.LifeTimeStatistic.FoodCollected + parent2.LifeTimeStatistic.FoodCollected;

            //podstawowe statystyki
            #region Velocity
            var parentSpeedDiference = Math.Abs(parent1.PhysicalStatistic.V - parent2.PhysicalStatistic.V);
            var minimalDescendantSpeed = Math.Min(parent1.PhysicalStatistic.V, parent2.PhysicalStatistic.V);
            const float speedBonusPerFoodCollected = 0.2F;
            //                         Minimalna prędkość   +   losowy procent różnicy                 +                      + bonus związany z jedzeniem LUB - 1, potomek może okazać się gorszy, raczejrybki nie mają prędkości mniejszej niż 2 więc może się okazać że  potometk bedzie miał prędkość 1 czyli duużo gorszą od rodzicó
            var childSpeed = (float) (minimalDescendantSpeed + random.NextDouble() * parentSpeedDiference  + (random.Next(0, 100) > 50 ? random.NextDouble() * foodCollectedByParents * speedBonusPerFoodCollected : -1));
            #endregion

            #region VisionAngle
            var parentVisionAngleDiference = Math.Abs(parent1.PhysicalStatistic.VisionAngle - parent2.PhysicalStatistic.VisionAngle);
            var minimalDescendantVisionAngle = Math.Min(parent1.PhysicalStatistic.VisionAngle, parent2.PhysicalStatistic.VisionAngle);
            const float visionAngleBonusPerFoodCollected = 0.34F;
            var childVisionAngle =
                (int) Math.Round(
                    minimalDescendantVisionAngle + random.NextDouble() * parentVisionAngleDiference +
                    random.NextDouble() * foodCollectedByParents * visionAngleBonusPerFoodCollected,
                    MidpointRounding.ToPositiveInfinity);
            #endregion

            #region VisionRange
            var parentVisionRangeDiference = Math.Abs(parent1.PhysicalStatistic.VisionRange - parent2.PhysicalStatistic.VisionRange);
            var minimalDescendantVisionRange = Math.Min(parent1.PhysicalStatistic.VisionRange, parent2.PhysicalStatistic.VisionRange);
            const float visionRangeBonusPerFoodCollected = 0.5F;
            var childVisionRange =
                (int) Math.Round(
                    minimalDescendantVisionRange + random.NextDouble() * parentVisionRangeDiference +
                    random.NextDouble() * foodCollectedByParents * visionRangeBonusPerFoodCollected,
                    MidpointRounding.ToPositiveInfinity);
            #endregion

            //interwały czasowe
            #region HungerInterval
            var parentHungaryIntervalDiference = Math.Abs(parent1.LifeParameters.HungerInterval.TotalSeconds - parent2.LifeParameters.HungerInterval.TotalSeconds);
            var minimalDescendantHungaryInterval = Math.Min(parent1.LifeParameters.HungerInterval.TotalSeconds, parent2.LifeParameters.HungerInterval.TotalSeconds);
            const int hungaryIntervalBonusPerFoodCollected = 1;
            var childHungaryInterval = new TimeSpan(0,0,0,
                (int)Math.Round(
                    minimalDescendantHungaryInterval + random.NextDouble() * parentHungaryIntervalDiference
                    + (random.Next(0, 100) > 50 ? random.NextDouble() * foodCollectedByParents * hungaryIntervalBonusPerFoodCollected : -1),
                    MidpointRounding.ToPositiveInfinity));
            //nie ma tu warunku granicznego zakładam że nie bedzie potrzebny
            #endregion

            #region VitalityInterval
            var parentVitalityIntervalDiference = Math.Abs(parent1.LifeParameters.HungerInterval.TotalSeconds - parent2.LifeParameters.HungerInterval.TotalSeconds);
            var maximalDescendantVitalityInterval = Math.Max(parent1.LifeParameters.HungerInterval.TotalSeconds, parent2.LifeParameters.HungerInterval.TotalSeconds);
            const int vitalityIntervaBonusPerFoodCollected = 1;
            var childVitalityIntervalInSeconds = 
                (int)Math.Round(
                    maximalDescendantVitalityInterval - random.NextDouble() * parentVitalityIntervalDiference +
                    random.NextDouble() - (random.Next(0, 100) > 50 ? random.NextDouble() * foodCollectedByParents * vitalityIntervaBonusPerFoodCollected : -1),
                    MidpointRounding.ToNegativeInfinity);
            var childVitalityInterval = new TimeSpan(0,0,0,childVitalityIntervalInSeconds < 5 ? 5 : childVitalityIntervalInSeconds);
            #endregion

            #region BoundaryConditions
            //warunki graniczne parametrów fizycznych, nie chcemy nie aby zmienne osiągneły zbyt duży poziom
            //prędkość nie większa niż 10
            childSpeed = childSpeed > 10 ? 10 : childSpeed;
            //kąt wzroku do 80 stopni
            childVisionAngle = childVisionAngle > 80 ? 80 : childVisionAngle;
            //zasięg wzroku na conajwyżej pół akwarium
            childVisionRange = childVisionRange > parent1.Aquarium.Width/2 ? parent1.Aquarium.Width / 2 : childVisionRange;
            #endregion

            //kolor rybki, będzie to prosta średnia arytmetyczna koliorów rodzica
            #region Color
            var parent1Color = ColorTranslator.FromHtml(parent1.PhysicalStatistic.Color);
            var parent2Color = ColorTranslator.FromHtml(parent1.PhysicalStatistic.Color);
            var childColor = Color.FromArgb((byte) (parent1Color.R + parent2Color.R) / 2,
                (byte) (parent1Color.G + parent2Color.G) / 2, (byte) (parent1Color.B + parent2Color.B) / 2);
            #endregion

            //Mutacje
            #region Predator
            var predatorParentsCount = (parent1.SetOfMutations.Predator ? 1 : 0) + (parent2.SetOfMutations.Predator ? 1 : 0);
            int percentageChanceForPredator = 10;
            switch (predatorParentsCount)
            {
                case 1:
                    percentageChanceForPredator += 30;
                    break;
                case 2:
                    percentageChanceForPredator += 65;
                    break;
                default:
                    percentageChanceForPredator += 0;
                    break;
            }
            bool IfChildIsPredator = random.Next(0, 100) <= percentageChanceForPredator;
            #endregion

            #region HungaryCharge
            var hungaryChargeParentsCount = (parent1.SetOfMutations.HungryCharge ? 1 : 0) + (parent2.SetOfMutations.HungryCharge ? 1 : 0);
            int percentageChanceForHungaryCharge = 10;
            switch (hungaryChargeParentsCount)
            {
                case 1:
                    percentageChanceForHungaryCharge += 30;
                    break;
                case 2:
                    percentageChanceForHungaryCharge += 65;
                    break;
                default:
                    percentageChanceForHungaryCharge += 0;
                    break;
            }
            bool IfChildGotHungaryCharge = random.Next(0, 100) <= percentageChanceForHungaryCharge;
            #endregion

            var fish = new Fish()
            {
                Name = $"DescId{parent1.Id}&Id{parent2.Id}",
                AquariumId = parent1.AquariumId,
                IsAlive = true,
                OwnerId = null,
                PhysicalStatistic = new PhysicalStatistic()
                {
                    //pojawi się w miejscu rodziców
                    X = parent1.PhysicalStatistic.X,
                    Y = parent1.PhysicalStatistic.Y,
                    //z obliczoną na podstawie 'krzyżowania' prędkością
                    V = childSpeed,
                    //popłynie z przeskalowaną prędkością w kierunku preciwnym do parent 1
                    Vx = -parent1.PhysicalStatistic.Vx * childSpeed / parent1.PhysicalStatistic.V,
                    Vy = -parent1.PhysicalStatistic.Vy * childSpeed / parent1.PhysicalStatistic.V,
                    //kolor przekonwertowany na hex
                    Color = "#" + childColor.R.ToString("X2")+ childColor.G.ToString("X2")+ childColor.B.ToString("X2"),
                    VisionAngle = childVisionAngle,
                    VisionRange = childVisionRange
                },
                SetOfMutations = new SetOfMutations()
                {
                    //zestaw mutacji został policzony ze względu na mutacje rodziców
                    Predator = IfChildIsPredator,
                    HungryCharge = IfChildGotHungaryCharge
                },
                LifeTimeStatistic = new LifeTimeStatistic()
                {
                    BirthDate = DateTime.UtcNow,
                    DeathDate = null,
                },
                LifeParameters = new LifeParameters()
                {
                    //nowy osobnik na start jest najedzony, to załatwi przykr przypadek atakowania rodzica w przypadku okazania się drapieżnikiem
                    Hunger = 4.0F,
                    //mimo najedzenia flaga prokreacji zostaje zdjęta
                    ReadyToProcreate = false,
                    HungerInterval = childHungaryInterval,
                    LastHungerUpdate = DateTime.UtcNow,
                    Vitality = LifeParameters.MAX_VITALITY,
                    VitalityInterval = childVitalityInterval,
                    LastVitalityUpdate = DateTime.UtcNow
                }
            };
            _context.Fishes.Add(fish);

            var parentChilds = new List<ParentChild>()
            {
                new ParentChild()
                {
                    Parent = parent1,
                    Child = fish
                },
                new ParentChild()
                {
                    Parent = parent2,
                    Child = fish
                }
            };
            _context.ParentChild.AddRange(parentChilds);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Odblokowuje flage zdolności do prokreacji, metoda powinna być wykorzystana w momencie zjadania jedzenia które przebija punkt najedzenia
        /// </summary>
        /// <param name="fish"></param>
        /// <returns></returns>
        private async Task MakeFishReadyToProcreate(Fish fish)
        {
            fish.LifeParameters.ReadyToProcreate = true;
        }

        /// <summary>
        /// włącza hungary charge jeśli rybka posiada takowa mutacje
        /// </summary>
        /// <param name="fish"></param>
        /// <returns></returns>
        private async Task EnableHungryChargeMutationIfCould(Fish fish)
        {
            if (fish.SetOfMutations.HungryCharge && !fish.SetOfMutations.HungryChargeEnabled)
            {
                
                fish.PhysicalStatistic.V = fish.PhysicalStatistic.V * (4.0F / 3.0F);
                fish.PhysicalStatistic.Vx = fish.PhysicalStatistic.Vx * (4.0F / 3.0F);
                fish.PhysicalStatistic.Vy = fish.PhysicalStatistic.Vy * (4.0F / 3.0F);
                fish.LifeParameters.HungerInterval = new TimeSpan((long) (fish.LifeParameters.HungerInterval.Ticks * (3.0F / 4.0F)));
                fish.SetOfMutations.HungryChargeEnabled = true;
            }
        }

        /// <summary>
        /// wyłącza hungary charge jeżeli rybka z taką muytacją zje jedzenie
        /// </summary>
        /// <param name="fish"></param>
        /// <returns></returns>
        private async Task DisableHungryChargeMutationIfNeed(Fish fish)
        {
            if (fish.SetOfMutations.HungryCharge && fish.SetOfMutations.HungryChargeEnabled)
            {
                fish.PhysicalStatistic.V = fish.PhysicalStatistic.V * (3.0F / 4.0F);
                fish.PhysicalStatistic.Vx = fish.PhysicalStatistic.Vx * (3.0F / 4.0F);
                fish.PhysicalStatistic.Vy = fish.PhysicalStatistic.Vy * (3.0F / 4.0F);
                fish.LifeParameters.HungerInterval = new TimeSpan((long) (fish.LifeParameters.HungerInterval.Ticks * (4.0F / 3.0F)));
                fish.SetOfMutations.HungryChargeEnabled = false;
            }
        }
    }
}
