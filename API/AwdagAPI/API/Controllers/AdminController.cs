using API.Utilities;
using Application.Dtos.Admin.Requests;
using Application.Dtos.Admin.Responses;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize(Roles = Role.Administrator)]
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Produces(typeof(GetUsersResponse))]
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersRequest request)
        {
            var response = await _adminService.GetUsersAsync(request);
            return SendResponse(response);
        }

        [Produces(typeof(Response<GetUserResponse>))]
        [HttpGet("users/{userId}")]
        public async Task<IActionResult> GetUser([FromRoute] string userId)
        {
            var response = await _adminService.GetUserAsync(userId);
            return SendResponse(response);
        }

        [Produces(typeof(Response<CreateUserResponse>))]
        [HttpPost("users")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var response = await _adminService.CreateUserAsync(request);

            if (response.StatusCode == HttpStatusCode.Created)
                response.CreatedUrlLocation = Url.Action("GetUser", new { userId = response.Payload.Id });
            return SendResponse(response);
        }

        [Produces(typeof(Response<UpdateUserResponse>))]
        [HttpPut("users/{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string userId, [FromBody] UpdateUserRequest request)
        {
            var response = await _adminService.UpdateUserAsync(userId, request);
            return SendResponse(response);
        }

        [HttpDelete("users/{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string userId)
        {
            var response = await _adminService.DeleteUserAsync(userId);
            return SendResponse(response);
        }

        [HttpPost("users/{userId}/set-password")]
        public async Task<IActionResult> SetUserPassword([FromRoute] string userId,
            [FromBody] SetUserPasswordRequest request)
        {
            var response = await _adminService.SetUserPasswordAsync(userId, request);
            return SendResponse(response);
        }

        [HttpPost("createFish")]
        public async Task<IActionResult> CreateExtraordinaryFish([FromBody] Fish fish)
        {
            var response = await _adminService.CreateExtraordinaryFish(fish);
            return SendResponse(response);
        }
    }
}