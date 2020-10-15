using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.HubConfig;
using Application.HubConfig.TimerManager;
using Application.Interfaces;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        private readonly IHubContext<AquariumHub> _hub;
        private DataContext _context;

        public AlgorithmService(IConfiguration configuration, IHubContext<AquariumHub> hub)
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .Options;

            _context = new DataContext(options);
            _hub = hub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var response =  _context.Fishes.Where(f=>f.AquariumId==1).ToList();
                    _hub.Clients.Group($"aq-{1}").SendAsync($"transferfishes-{1}",response);
                }
                catch (Exception e)
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            };
        }
    }
}
