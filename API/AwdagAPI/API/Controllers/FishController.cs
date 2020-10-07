using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.DataStorage;
using Application.Dtos.Admin.Requests;
using Application.Dtos.Admin.Responses;
using Application.Dtos.NewFolder.Response;
using Application.HubConfig;
using Application.HubConfig.TimerManager;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [AllowAnonymous]
    public class FishController : BaseController
    {
        private readonly IFishService _fishService;
        private readonly IHubContext<AquariumHub> _hub;

        public FishController(IFishService fishService, IHubContext<AquariumHub> hub)
        {
            _fishService = fishService;
            _hub = hub;
        }

        [Produces(typeof(GetFishFromAquariumResponse))]
        [HttpGet("aquarium/{aquariumId}")]
        public async Task<IActionResult> GetFishesFromAquarium([FromRoute] Guid aquariumId)
        {
            var response = await _fishService.GetFishesFromAquarium(aquariumId);
            return SendResponse(response);
        }

        [Produces(typeof(GetFishFromAquariumResponse))]
        [HttpGet("aquarium/{aquariumId}/hub")]
        public async Task<IActionResult> GetFishesFromAquariumHUB([FromRoute] Guid aquariumId)
        {
            var response = await _fishService.GetFishesFromAquarium(aquariumId);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var timerManager =
                    new TimerManager(() => _hub.Clients.All.SendAsync("transferfishes", response.Payload));
                return Ok(response.Payload);
            }
            else
                return Ok();
        }
    }
}