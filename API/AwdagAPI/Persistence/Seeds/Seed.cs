using Domain.Models;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Seeds
{
    public static class Seed
    {
        public class SeedIdHelper
        {
            public int aquariumId = 3;
            public int fishId = 0;
            public int lifeParameterId = 0;
            public int lifeTimeStatisticId = 0;
            public int setOfMutationsId = 0;
            public int physicalStatisticId = 0;
        }

        public static SeedIdHelper seed = new SeedIdHelper();

        public static async Task SeedIdentity(DataContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager, context);
            await SeedAquariums(context);
            await SeedFishes(context);
            await SeedPhysicalStatistic(context);
            await SeedLifeParameters(context);
            await SeedLifeTimeStatistic(context);
            await SeedSetOfMutations(context);
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Role.Administrator))
            {
                var adminRole = new IdentityRole(Role.Administrator);
                await roleManager.CreateAsync(adminRole);
            }

            if (!await roleManager.RoleExistsAsync(Role.User))
            {
                var userRole = new IdentityRole(Role.User);
                await roleManager.CreateAsync(userRole);
            }
        }

        private static async Task SeedUsers(UserManager<ApplicationUser> userManager, DataContext context)
        {
            if (!userManager.Users.Any())
            {
                const string adminEmail = "admin@test.com";
                const string adminLastName = "Admin";
                const string adminFirstName = "Admin";
                const string adminPassword = "Admin123!";

                var adminAccount = await userManager.FindByEmailAsync(adminEmail);
                if (adminAccount == null)
                {
                    var admin = new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        FirstName = adminFirstName,
                        LastName = adminLastName,
                        UserName = adminEmail,
                        Email = adminEmail,
                    };
                    var result = await userManager.CreateAsync(admin, adminPassword);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(admin, Role.Administrator);
                    }
                }

                var users = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        Id = Guid.Parse("5ffe2032-6c7c-48c6-950f-e47976b2389a").ToString(),
                        FirstName = "Jan",
                        LastName = "Nowak",
                        UserName = "nowak@test.com",
                        Email = "nowak@test.com",
                    },
                    new ApplicationUser
                    {
                        Id = Guid.Parse("9b348741-db54-4471-aa48-7284b11cab4b").ToString(),
                        FirstName = "Adam",
                        LastName = "Kowalski",
                        UserName = "kowalski@test.com",
                        Email = "kowalski@test.com",
                    },
                    new ApplicationUser
                    {
                        Id = Guid.Parse("651799e0-fccf-4e6d-a5d2-1c153ae77f72").ToString(),
                        FirstName = "Marek",
                        LastName = "Góra",
                        UserName = "gora@test.com",
                        Email = "gora@test.com",
                    },
                    new ApplicationUser
                    {
                        Id = Guid.Parse("4a15e2f7-52dd-4e22-b0f4-241944216775").ToString(),
                        FirstName = "Dawid",
                        LastName = "Cichy",
                        UserName = "cichy@test.com",
                        Email = "cichy@test.com",
                    }
                };

                foreach (var user in users)
                {
                    var result = await userManager.CreateAsync(user, "Password123!");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, Role.User);
                    }
                }
            }
        }

        
        private static async Task SeedAquariums(DataContext context)
        {
            if (!context.Aquariums.Any())
            {
                var aquariums = new List<Aquarium>()
                {
                    new Aquarium()
                    {
                        //Id = ++seed.aquariumId,
                    },
                    new Aquarium()
                    {
                        //Id = ++seed.aquariumId,
                        Width = 1100,
                        Height = 720
                    },
                    new Aquarium()
                    {
                        //Id = ++seed.aquariumId,
                        Width = 1080,
                        Height = 720
                    },
                };
                seed.aquariumId = 3;
                context.Aquariums.AddRange(aquariums);
                await context.SaveChangesAsync();
            }
        }
        
        private static async Task SeedFishes(DataContext context)
        {
            Random random = new Random();
            if (!context.Fishes.Any())
            {
                var fishes = new List<Fish>();
                for (int i = 0; i < 20; i++)
                    fishes.Add(
                    new Fish()
                        {
                            //Id = ++seed.fishId,
                            AquariumId = random.Next(1,seed.aquariumId),
                            Name = $"Object#{i}"
                        }
                    );
                context.Fishes.AddRange(fishes);
                await context.SaveChangesAsync();
            }
        }
        private static async Task SeedPhysicalStatistic(DataContext context)
        {
            Random random = new Random();
            if (!context.PhysicalStatistics.Any())
            {
                var physicalStats = new List<PhysicalStatistic>();
                for (int i = 0; i < 20; i++)
                    physicalStats.Add(
                        new PhysicalStatistic()
                        {
                            //Id = ++seed.physicalStatisticId,
                            X = random.Next(300,700),
                            Y = random.Next(300, 700),
                            Vx = random.Next(-3, 3),
                            Vy = random.Next(-3, 3),
                            V = random.Next(1, 3),
                            Color = String.Format("#{0:X6}", random.Next(0x1000000)),
                            FishId = ++seed.physicalStatisticId
                        }
                    );
                
                context.PhysicalStatistics.AddRange(physicalStats);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedLifeParameters(DataContext context)
        {
            Random random = new Random();
            if (!context.LifeParameters.Any())
            {
                var lifeParameters = new List<LifeParameters>();
                for (int i = 0; i < 20; i++)
                    lifeParameters.Add(
                        new LifeParameters()
                        {
                            //Id = ++seed.lifeParameterId,
                            HungerInterval = new TimeSpan(0,0,random.Next(30,60)),
                            LastHungerUpdate = DateTime.UtcNow,
                            VitalityInterval = new TimeSpan(0, 0, random.Next(55, 60)),
                            LastVitalityUpdate = DateTime.UtcNow,
                            FishId = ++seed.lifeParameterId
                        }
                    );

                context.LifeParameters.AddRange(lifeParameters);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedLifeTimeStatistic(DataContext context)
        {
            Random random = new Random();
            if (!context.LifeTimeStatistic.Any())
            {
                var lifeTimeStatistics = new List<LifeTimeStatistic>();
                for (int i = 0; i < 20; i++)
                    lifeTimeStatistics.Add(
                        new LifeTimeStatistic()
                        {
                            //Id = ++seed.lifeTimeStatisticId,
                            BirthDate = DateTime.UtcNow,
                            FishId = ++seed.lifeTimeStatisticId
                        }
                    );

                context.LifeTimeStatistic.AddRange(lifeTimeStatistics);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedSetOfMutations(DataContext context)
        {
            Random random = new Random();
            if (!context.SetOfMutations.Any())
            {
                var lifeTimeStatistics = new List<SetOfMutations>();
                for (int i = 0; i < 20; i++)
                    lifeTimeStatistics.Add(
                        new SetOfMutations()
                        {
                            //Id = ++seed.setOfMutationsId,
                            FishId = ++seed.setOfMutationsId
                        }
                    );

                context.SetOfMutations.AddRange(lifeTimeStatistics);
                await context.SaveChangesAsync();
            }
        }
    }
}