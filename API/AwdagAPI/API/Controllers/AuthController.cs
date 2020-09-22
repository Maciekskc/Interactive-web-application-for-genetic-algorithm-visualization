using API.Utilities;
using Application.Dtos.Auth.Requests;
using Application.Dtos.Auth.Responses;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [Produces(typeof(Response<LoginResponse>))]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest dto)
        {
            var response = await _authService.LoginAsync(dto);

            if (response.StatusCode == HttpStatusCode.OK)
                AssignTokenCookiesToResponse(response.Payload.Token, response.Payload.RefreshToken);

            return SendResponse(response);
        }

        [Produces(typeof(Response<RegisterResponse>))]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest dto)
        {
            var response = await _authService.RegisterAsync(dto);
            return SendResponse(response);
        }

        [Produces(typeof(Response<RefreshTokenResponse>))]
        [HttpGet("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            HttpContext.Request.Cookies.TryGetValue("refresh-token", out var refreshToken);
            HttpContext.Request.Cookies.TryGetValue("access-token", out var accessToken);

            var response = await _authService.RefreshTokenAsync(accessToken, refreshToken);

            if (response.StatusCode == HttpStatusCode.OK)
                AssignTokenCookiesToResponse(response.Payload.Token, response.Payload.RefreshToken);

            return SendResponse(response);
        }

        private void AssignTokenCookiesToResponse(string accessToken, string refreshToken)
        {
            var expiryOffset = DateTimeOffset.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:RefreshTokenExpiry"));

            HttpContext.Response.Cookies.Append("access-token", accessToken, new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                Expires = expiryOffset
            });

            HttpContext.Response.Cookies.Append("refresh-token", refreshToken, new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                Expires = expiryOffset
            });
        }
    }
}