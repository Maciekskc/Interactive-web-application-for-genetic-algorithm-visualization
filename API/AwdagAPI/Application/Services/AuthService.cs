using Application.Dtos.Auth.Requests;
using Application.Dtos.Auth.Responses;
using Application.Infrastructure.Errors;
using Application.Interfaces;
using Domain.Models;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Utilities;
using Validation;
using Validation.Models;

namespace Application.Services
{
    public class AuthService : Service, IAuthService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IEmailService _emailService;

        public AuthService(IServiceProvider serviceProvider, SignInManager<ApplicationUser> signInManager, IJwtGenerator jwtGenerator, IEmailService emailService) : base(serviceProvider)
        {
            _jwtGenerator = jwtGenerator;
            _emailService = emailService;
            _signInManager = signInManager;
        }

        public async Task<ServiceResponse<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var user = await UserManager.FindByEmailAsync(request.Email);

            if (user == null)
                throw new RestException(HttpStatusCode.Unauthorized);

            if (user.IsDeleted)
            {
                ErrorResultToReturn = new ErrorResult(Errors.AuthErrors.YourAccountWasDeleted);
                throw new RestException(HttpStatusCode.BadRequest, ErrorResultToReturn);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
                throw new RestException(HttpStatusCode.Unauthorized);

            var generatedToken = await _jwtGenerator.CreateTokenAsync(user);

            var responseDto = new LoginResponse()
            {
                Token = generatedToken.Token,
                RefreshToken = generatedToken.RefreshToken,
            };

            return new ServiceResponse<LoginResponse>(HttpStatusCode.OK, responseDto);
        }

        public async Task<ServiceResponse<RegisterResponse>> RegisterAsync(RegisterRequest request)
        {
            var userToRegister = new ApplicationUser()
            {
                LastName = request.LastName,
                FirstName = request.FirstName,
                Email = request.Email,
                UserName = request.Email
            };

            if (request.Roles.Contains(Role.Administrator))
                throw new RestException(HttpStatusCode.Forbidden);
            await UserCreator.CreateUserAsync(UserManager, userToRegister, request.Password, request.Roles);

            var generatedEmailConfirmationToken =
                await UserManager.GenerateEmailConfirmationTokenAsync(userToRegister);

            var sendEmailResult = await _emailService.SendEmailAfterRegistrationAsync(userToRegister, generatedEmailConfirmationToken, request.UrlToIncludeInEmail, request.Language);

            var emailErrors = new List<string>();

            if (sendEmailResult.ResponseType != HttpStatusCode.OK)
                emailErrors = sendEmailResult.Errors.ToList();

            var token = await _jwtGenerator.CreateTokenAsync(userToRegister);
            var response = new RegisterResponse
            {
                Token = token.Token,
                RefreshToken = token.RefreshToken
            };

            if (emailErrors.Any())
                return new ServiceResponse<RegisterResponse>(HttpStatusCode.OK, emailErrors, response);

            return new ServiceResponse<RegisterResponse>(HttpStatusCode.OK, response);
        }

        public async Task<ServiceResponse<RefreshTokenResponse>> RefreshTokenAsync(string accessToken, string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                ErrorResultToReturn = new ErrorResult("Pusty access token");
                throw new RestException(HttpStatusCode.BadRequest, ErrorResultToReturn);
            }

            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                ErrorResultToReturn = new ErrorResult("Pusty refresh token");
                throw new RestException(HttpStatusCode.BadRequest, ErrorResultToReturn);
            }

            var validatedToken = _jwtGenerator.GetPrincipalFromToken(accessToken);

            if (validatedToken == null)
            {
                ErrorResultToReturn = new ErrorResult("Nieprawidłowy token");
                throw new RestException(HttpStatusCode.BadRequest, ErrorResultToReturn);
            }

            var storedRefreshToken =
                await Context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);

            var validationResult = _jwtGenerator.ValidateRefreshToken(storedRefreshToken, validatedToken);

            if (validationResult.Any())
            {
                ErrorResultToReturn = new ErrorResult(Errors.AuthErrors.AnErrorOccuredWhileAuthenticating, validationResult.ToArray());
                throw new RestException(HttpStatusCode.BadRequest, ErrorResultToReturn);
            }

            storedRefreshToken.Used = true;
            Context.RefreshTokens.Update(storedRefreshToken);

            await SaveChangesAsync(new[] { "Wystąpił bład podczas aktualizowania tokena" });

            var user = await UserManager.FindByIdAsync(_jwtGenerator.GetUserIdFromToken(validatedToken));

            if (user == null)
            {
                ErrorResultToReturn = new ErrorResult(Errors.AccountErrors.UserNotFound);
                throw new RestException(HttpStatusCode.BadRequest, ErrorResultToReturn);
            }

            var tokens = await _jwtGenerator.CreateTokenAsync(user);

            return new ServiceResponse<RefreshTokenResponse>(HttpStatusCode.OK, new RefreshTokenResponse { RefreshToken = tokens.RefreshToken, Token = tokens.Token });
        }
    }
}