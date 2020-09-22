using API.Utilities;
using Application.Dtos.Account.Requests;
using Application.Dtos.Account.Responses;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Produces(typeof(Response<GetAccountDetailsResponse>))]
        [HttpGet("details")]
        public async Task<IActionResult> GetAccountDetails()
        {
            var response = await _accountService.GetAccountDetailsAsync();
            return SendResponse(response);
        }

        [Produces(typeof(Response<UpdateAccountDetailsResponse>))]
        [HttpPut("details")]
        public async Task<IActionResult> UpdateAccountDetails([FromBody] UpdateAccountDetailsRequest request)
        {
            var response = await _accountService.UpdateAccountDetailsAsync(request);
            return SendResponse(response);
        }

        [AllowAnonymous]
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string confirmationCode)
        {
            var response = await _accountService.ConfirmEmailAsync(userId, confirmationCode);
            return SendResponse(response);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var response = await _accountService.ChangePasswordAsync(request);
            return SendResponse(response);
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var response = await _accountService.ForgotPasswordAsync(request);
            return SendResponse(response);
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var response = await _accountService.ResetPasswordAsync(request);
            return SendResponse(response);
        }

        [AllowAnonymous]
        [HttpPost("resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendConfirmationEmailRequest request)
        {
            var response = await _accountService.ResendConfirmationEmailAsync(request);
            return SendResponse(response);
        }
    }
}