using Application.Dtos.Maintenance.Requests;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using API.Utilities;
using Application.Dtos.Maintenance.Responses;

namespace API.Controllers
{
    [Authorize(Roles = Role.Administrator)]
    public class MaintenanceController : BaseController
    {
        private readonly IMaintenanceService _maintenanceService;

        public MaintenanceController(IMaintenanceService maintenanceService)
        {
            _maintenanceService = maintenanceService;
        }

        [AllowAnonymous]
        [Produces(typeof(GetCurrentMessagesResponse))]
        [HttpGet("messages/current")]
        public async Task<IActionResult> GetCurrentMessages([FromQuery] GetCurrentMessagesRequest request)
        {
            var response = await _maintenanceService.GetCurrentMessagesAsync(request);
            return SendResponse(response);
        }

        [Produces(typeof(GetAllMessagesResponse))]
        [HttpGet("messages/all")]
        public async Task<IActionResult> GetAllMessages([FromQuery] GetAllMessagesRequest request)
        {
            var response = await _maintenanceService.GetAllMessagesAsync(request);
            return SendResponse(response);
        }

        [Produces(typeof(GetUpcomingMessagesResponse))]
        [HttpGet("messages/upcoming")]
        public async Task<IActionResult> GetUpcomingMessages([FromQuery] GetUpcomingMessagesRequest request)
        {
            var response = await _maintenanceService.GetUpcomingMessagesAsync(request);
            return SendResponse(response);
        }

        [Produces(typeof(Response<GetMessageResponse>))]
        [HttpGet("messages/{messageId}")]
        public async Task<IActionResult> GetMessage([FromRoute] int messageId)
        {
            var response = await _maintenanceService.GetMessageAsync(messageId);
            return SendResponse(response);
        }

        [Produces(typeof(Response<CreateMessageResponse>))]
        [HttpPost("messages")]
        public async Task<IActionResult> CreateMessage([FromBody] CreateMessageRequest request)
        {
            var response = await _maintenanceService.CreateMessageAsync(request);

            if (response.StatusCode == HttpStatusCode.Created)
                response.CreatedUrlLocation = Url.Action("GetMessage", new { messageId = response.Payload.Id });

            return SendResponse(response);
        }

        [Produces(typeof(Response<UpdateMessageResponse>))]
        [HttpPut("messages/{messageId}")]
        public async Task<IActionResult> UpdateMessage([FromRoute] int messageId, [FromBody] UpdateMessageRequest request)
        {
            var response = await _maintenanceService.UpdateMessageAsync(messageId, request);
            return SendResponse(response);
        }

        [HttpDelete("messages/{messageId}")]
        public async Task<IActionResult> DeleteMessage([FromRoute] int messageId)
        {
            var response = await _maintenanceService.DeleteMessageAsync(messageId);
            return SendResponse(response);
        }
    }
}