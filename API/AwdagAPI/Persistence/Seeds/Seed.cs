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
        public static async Task SeedIdentity(DataContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager, context);
            await SeedMaintenanceMessages(context);
            await SeedAquariums(context);
            await SeedFishes(context);
            await SeedPhysicalStatistic(context);
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

        private static async Task SeedMaintenanceMessages(DataContext context)
        {
            if (!context.MaintenanceMessages.Any())
            {
                var maintenanceMessages = new List<MaintenanceMessage>()
                {
                    new MaintenanceMessage
                    {
                        StartDate = DateTime.Now.AddDays(-5).AddHours(+1),
                        EndDate = DateTime.Now.AddDays(-2).AddHours(-5),
                        Description =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                    },
                    new MaintenanceMessage
                    {
                        StartDate = DateTime.Now.AddDays(-4).AddHours(+5),
                        EndDate = DateTime.Now.AddDays(-2).AddHours(-5),
                        Description =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                    },
                    new MaintenanceMessage
                    {
                        StartDate = DateTime.Now.AddDays(-5).AddHours(+1),
                        EndDate = DateTime.Now.AddDays(-1).AddHours(-1),
                        Description =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                    },
                    new MaintenanceMessage
                    {
                        StartDate = DateTime.Now.AddDays(-7).AddHours(+9),
                        EndDate = DateTime.Now.AddDays(-1).AddHours(-5),
                        Description =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                    },
                    new MaintenanceMessage
                    {
                        StartDate = DateTime.Now.AddDays(-7).AddHours(+2),
                        EndDate = DateTime.Now.AddDays(-2).AddHours(-4),
                        Description =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                    },
                    new MaintenanceMessage
                    {
                        StartDate = DateTime.Now.AddDays(-12).AddHours(+1),
                        EndDate = DateTime.Now.AddDays(-1).AddHours(-2),
                        Description =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                    },
                    new MaintenanceMessage
                    {
                        StartDate = DateTime.Now.AddDays(-13).AddHours(+1),
                        EndDate = DateTime.Now.AddDays(-2).AddHours(-5),
                        Description =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                    },
                    new MaintenanceMessage
                    {
                        StartDate = DateTime.Now.AddDays(-3).AddHours(+1),
                        EndDate = DateTime.Now.AddDays(-1).AddHours(-1),
                        Description =
                            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                    },
                };
                context.MaintenanceMessages.AddRange(maintenanceMessages);
                await context.SaveChangesAsync();
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
                        Id = Guid.Parse("4577ac01-8376-45d1-aaee-3dcfbd9aaaaa"),
                        Width = 1080,
                        Height = 720
                    },
                    new Aquarium()
                    {
                        Id = Guid.Parse("c14fb210-d853-4935-ae3c-13434e9bbbbb"),
                        Width = 1080,
                        Height = 720
                    },
                };
                context.Aquariums.AddRange(aquariums);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedPhysicalStatistic(DataContext context)
        {
            if (!context.PhysicalStatistics.Any())
            {
                var physicalStats = new List<PhysicalStatistic>()
                {
                    new PhysicalStatistic()
                    {
                        Id = Guid.Parse("4577ac01-8376-45d1-aaee-3dcfbd9aeb83"),
                        X = 640,
                        Y = 640,
                        Vx = 1,
                        Vy = 1,
                        V = 1,
                        Color = "#FFFFFFF",
                        FishId = Guid.Parse("4577ac01-8376-45d1-aaee-3dcfbd9aeb86")
                    },
                    new PhysicalStatistic()
                    {
                        Id = Guid.Parse("4577ac01-8376-45d1-aaee-3dcfbd9aeb84"),
                        X = 640,
                        Y = 640,
                        Vx = -1,
                        Vy = 1,
                        V = 1,
                        Color = "#FFFFFFD",
                        FishId = Guid.Parse("c14fb210-d853-4935-ae3c-13434e9b3197")
            },
                    new PhysicalStatistic()
                    {
                        Id = Guid.Parse("4577ac01-8376-45d1-aaee-3dcfbd9aeb85"),
                        X = 640,
                        Y = 640,
                        Vx = 1,
                        Vy = -1,
                        V = 1,
                        Color = "#FFFFFFE",
                        FishId = Guid.Parse("c14fb210-d853-4935-ae3c-13434e9b3198")
            },
                };
                context.PhysicalStatistics.AddRange(physicalStats);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedFishes(DataContext context)
        {
            if (!context.Fishes.Any())
            {
                var fishes = new List<Fish>()
                {
                    new Fish()
                    {
                        Id = Guid.Parse("4577ac01-8376-45d1-aaee-3dcfbd9aeb86"),
                        AquariumId = Guid.Parse("4577ac01-8376-45d1-aaee-3dcfbd9aaaaa"),
                        Name = "Adam"
                    },
                    new Fish()
                    {
                        Id = Guid.Parse("c14fb210-d853-4935-ae3c-13434e9b3197"), 
                        AquariumId = Guid.Parse("4577ac01-8376-45d1-aaee-3dcfbd9aaaaa"),
                        Name = "Ewa",
                    },
                    new Fish()
                    {
                        Id = Guid.Parse("c14fb210-d853-4935-ae3c-13434e9b3198"),
                        AquariumId = Guid.Parse("c14fb210-d853-4935-ae3c-13434e9bbbbb"),
                        Name = "Pietrek",
                    },
                };
                context.Fishes.AddRange(fishes);
                await context.SaveChangesAsync();
            }
        }
    }
}