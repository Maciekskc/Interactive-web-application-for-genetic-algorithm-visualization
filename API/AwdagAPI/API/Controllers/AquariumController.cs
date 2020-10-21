using System.Threading.Tasks;
using Application.Dtos.Aquarium.Requests;
using Application.Dtos.Aquarium.Responses;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = Role.Administrator)]
    public class AquariumController : BaseController
    {
        private readonly IAquariumService _aquariumService;

        public AquariumController(IAquariumService aquariumService)
        {
            _aquariumService = aquariumService;
        }

        [Produces(typeof(GetAquariumResponse))]
        [HttpPost("create")]
        public async Task<IActionResult> CreateAquarium([FromBody] CreateAquariumRequest request)
        {
            var response = await _aquariumService.CreateAquariumAsync(request);
            return SendResponse(response);
        }
        
        [Produces(typeof(GetAquariumResponse))]
        [HttpPost("{aquariumId}")]
        public async Task<IActionResult> GetAquarium([FromRoute] int aquariumId)
        {
            var response = await _aquariumService.GetAquariumAsync(aquariumId);
            return SendResponse(response);
        }
        
        [Produces(typeof(GetAquariumResponse))]
        [HttpPut("{aquariumId/edit}")]
        public async Task<IActionResult> EditAquarium([FromRoute] int aquariumId,[FromBody] EditAquariumRequest request)
        {
            var response = await _aquariumService.EditAquariumAsync(aquariumId, request);
            return SendResponse(response);
        }

        [HttpPut("{aquariumId/delete}")]
        public async Task<IActionResult> RemoveAquarium([FromRoute] int aquariumId)
        {
            var response = await _aquariumService.RemoveAquariumAsync(aquariumId);
            return SendResponse(response);
        }

        [Produces(typeof(GetAllAquariumsResponse))]
        [HttpPut("get-all-aquariums")]
        public async Task<IActionResult> GetListOfAquarium([FromQuery] GetAllAquariumsRequest request)
        {
            var response = await _aquariumService.GetAllAquariumsAsync(request);
            return SendResponse(response);
        }
    }
}