using System.Threading.Tasks;
using Application.Dtos.Fish.Request;
using Application.Dtos.Fish.Response;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class FishController : BaseController
    {
        private readonly IFishService _fishService;

        public FishController(IFishService fishService)
        {
            _fishService = fishService;
        }

        [AllowAnonymous]
        [Produces(typeof(GetFishResponse))]
        [HttpGet("{fishId}")]
        public async Task<IActionResult> GetFish([FromRoute] int fishId)
        {
            var response = await _fishService.GetFish(fishId);
            return SendResponse(response);
        }

        [AllowAnonymous]
        [Produces(typeof(GetFishesFromAquariumResponse))]
        [HttpGet("aquarium/{aquariumId}")]
        public async Task<IActionResult> GetFishesFromAquarium([FromRoute] int aquariumId, [FromQuery] GetFishesFromAquariumRequest request)
        {
            var response = await _fishService.GetFishesFromAquarium(aquariumId, request);
            return SendResponse(response);
        }

        [Authorize(Roles = Role.User)]
        [Produces(typeof(GetUserFishesResponse))]
        [HttpGet("get-user-fishes")]
        public async Task<IActionResult> GetUserFishes([FromQuery] GetUserFishesRequest request)
        {
            var response = await _fishService.GetUserFishes(request);
            return SendResponse(response);
        }

        /*[Produces(typeof(GetFishResponse))]
        [HttpPut()]
        public async Task<IActionResult> EditFish([FromBody] EditFishRequest request)
        {
            var response = await _fishService.EditFish(request);
            return SendResponse(response);
        }
*/
        [Produces(typeof(GetFishResponse))]
        [HttpPost("create")]
        public async Task<IActionResult> CreateFish([FromBody] CreateFishRequest request)
        {
            var response = await _fishService.CreateFish(request);
            return SendResponse(response);
        }

        [HttpPost("kill/{fishId}")]
        public async Task<IActionResult> KillFish([FromRoute] int fishId)
        {
            var response = await _fishService.KillFish(fishId);
            return SendResponse(response);
        }
    }
}