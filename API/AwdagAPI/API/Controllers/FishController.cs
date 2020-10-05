using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Admin.Requests;
using Application.Dtos.Admin.Responses;
using Application.Dtos.NewFolder.Response;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class FishController : BaseController
    {
        private readonly IFishService _fishService;
        public FishController(IFishService fishService)
        {
            _fishService = fishService;
        }

        [Produces(typeof(GetFishFromAquariumResponse))]
        [HttpGet("aquarium/{aquariumId}")]
        public async Task<IActionResult> GetFishesFromAquarium([FromRoute] Guid aquariumId)
        {
            var response = await _fishService.GetFishesFromAquarium(aquariumId);
            return SendResponse(response);
        }
    }
}