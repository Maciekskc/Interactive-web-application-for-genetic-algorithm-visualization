using System.Threading.Tasks;
using Application.Dtos.Food.Requests;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class FoodController : BaseController
    {
        private IFoodService _iFoodService;

        public FoodController(IFoodService iFoodService)
        {
            _iFoodService = iFoodService;
        }

        [HttpPost("aquarium/{aquariumId}/create")]
        public async Task<IActionResult> CreateFood([FromRoute]int aquariumId,[FromBody]CreateFoodRequest request)
        {
            var response = await _iFoodService.CreateAdditionalFoodAsync(aquariumId,request);
            return SendResponse(response);
        }
    }
}