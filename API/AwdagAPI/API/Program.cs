using Domain.Models.Entities;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Persistence;
using Persistence.Seeds;

using System;
using System.Threading.Tasks;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await SeedDatabase(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureLogging((hostingContext, logging) =>
                    {
                        logging.AddLog4Net();
                        logging.SetMinimumLevel(LogLevel.Debug);
                    });
                });

        private static async Task SeedDatabase(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DataContext>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await Seed.SeedIdentity(context, userManager, roleManager);
            }
            catch (Exception exception)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(exception, "An error occured during populating");
            }
        }
    }
}