using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Dtos.NewFolder.Response;
using Application.HubConfig;
using Application.HubConfig.TimerManager;
using Application.Infrastructure;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
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
                    var fishList =  _context.Fishes.Where(f=>f.AquariumId==1).ToList();
                    var response = Mapper.Map<List<Fish>, List<GetFishFromAquariumResponse>>(fishList);

                    await _hub.Clients.Group("aq-1").SendAquariumData(response);

                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
                catch (Exception e)
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            };
        }
    }
}
