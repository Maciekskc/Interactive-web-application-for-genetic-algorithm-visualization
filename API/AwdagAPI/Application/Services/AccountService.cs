using Application.Dtos.Account.Requests;
using Application.Dtos.Account.Responses;
using Application.Infrastructure.Errors;
using Application.Interfaces;
using Domain.Models.Entities;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Utilities;
using Validation;
using Validation.Models;

namespace Application.Services
{
    public class AccountService : Service, IAccountService
    {
        private readonly IEmailService _emailService;

        public AccountService(IServiceProvider serviceProvider, IEmailService emailService) : base(serviceProvider)
        {
            _emailService = emailService;
        }

        public async Task<ServiceResponse<GetAccountDetailsResponse>> GetAccountDetailsAsync()
        {
            if (CurrentlyLoggedUser == null)
                throw new RestException(HttpStatusCode.Unauthorized);

            var details = Mapper.Map<ApplicationUser, GetAccountDetailsResponse>(CurrentlyLoggedUser);
            var roles = await UserManager.GetRolesAsync(CurrentlyLoggedUser);

            details.Roles = roles.ToList();

            return new ServiceResponse<GetAccountDetailsResponse>(HttpStatusCode.OK, payload: details);
        }

        public async Task<ServiceResponse<UpdateAccountDetailsResponse>> UpdateAccountDetailsAsync(UpdateAccountDetailsRequest request)
        {
            if (CurrentlyLoggedUser == null)
                throw new RestException(HttpStatusCode.Unauthorized);

            Mapper.Map(request, CurrentlyLoggedUser);
            var result = await UserManager.UpdateAsync(CurrentlyLoggedUser);

            if (result.Succeeded)
            {
                var response = Mapper.Map<ApplicationUser, UpdateAccountDetailsResponse>(CurrentlyLoggedUser);
                var roles = await UserManager.GetRolesAsync(CurrentlyLoggedUser);
                response.Roles = roles.ToList();
                return new ServiceResponse<UpdateAccountDetailsResponse>(HttpStatusCode.OK, response);
            }

            var resultErrors = result.Errors.Select(e => e.Description);
            ErrorResultToReturn = new ErrorResult(Errors.AccountErrors.ErrorOccuredWhileUpdatingUser, resultErrors);

            throw new RestException(HttpStatusCode.BadRequest, ErrorResultToReturn);
        }

        public async Task<ServiceResponse> ConfirmEmailAsync(string userId, string confirmationCode)
        {
            var user = await GetEntityByIdAsync<ApplicationUser>(userId, HttpStatusCode.BadRequest);

            var result = await UserManager.ConfirmEmailAsync(user, confirmationCode);

            if (result.Succeeded)
                return new ServiceResponse(HttpStatusCode.OK);

            var resultErrors = result.Errors.Select(e => e.Description);
            ErrorResultToReturn = new ErrorResult(Errors.AccountErrors.ErrorOccuredWhileConfirmingEmail, resultErrors);

            throw new RestException(HttpStatusCode.BadRequest, ErrorResultToReturn);
        }

        public async Task<ServiceResponse> ChangePasswordAsync(ChangePasswordRequest request)
        {
            if (CurrentlyLoggedUser == null)
                throw new RestException(HttpStatusCode.Unauthorized);

            var changePasswordResult = await UserManager.ChangePasswordAsync(CurrentlyLoggedUser, request.CurrentPassword, request.NewPassword);
            if (changePasswordResult.Succeeded)
                return new ServiceResponse(HttpStatusCode.OK);

            var resultErrors = changePasswordResult.Errors.Select(e => e.Description);
            ErrorResultToReturn = new ErrorResult(Errors.AccountErrors.ErrorOccuredWhileChangingPassword, resultErrors);

            throw new RestException(HttpStatusCode.BadRequest, ErrorResultToReturn);
        }

        public async Task<ServiceResponse> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var user = await UserManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                ErrorResultToReturn = new ErrorResult(Errors.AccountErrors.CouldNotFindUserWithGivenEmail.SetParams(request.Email));
                throw new RestException(HttpStatusCode.NotFound, ErrorResultToReturn);
            }

            var passwordResetToken = await UserManager.GeneratePasswordResetTokenAsync(user);
            var emailServiceResponse = await _emailService.SendPasswordResetEmailAsync(user, passwordResetToken, request.UrlToIncludeInEmail, request.Language);

            return emailServiceResponse;
        }

        public async Task<ServiceResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await GetEntityByIdAsync<ApplicationUser>(request.UserId, HttpStatusCode.BadRequest);

            var result = await UserManager.ResetPasswordAsync(user, request.PasswordResetCode, request.NewPassword);

            if (result.Succeeded)
                return new ServiceResponse(HttpStatusCode.OK);

            var resultErrors = result.Errors.Select(e => e.Description);
            ErrorResultToReturn = new ErrorResult(Errors.AccountErrors.ErrorOccuredWhileResettingPassword, resultErrors);

            throw new RestException(HttpStatusCode.BadRequest, ErrorResultToReturn);
        }

        public async Task<ServiceResponse> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request)
        {
            var user = await UserManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                ErrorResultToReturn = new ErrorResult(Errors.AccountErrors.CouldNotFindUserWithGivenEmail.SetParams(request.Email));
                throw new RestException(HttpStatusCode.BadRequest, ErrorResultToReturn);
            }

            if (user.IsDeleted)
            {
                ErrorResultToReturn = new ErrorResult(Errors.AccountErrors.DeletedAccountCanNotBeConfirmed);
                throw new RestException(HttpStatusCode.BadRequest, ErrorResultToReturn);
            }

            if (user.EmailConfirmed)
            {
                ErrorResultToReturn = new ErrorResult(Errors.AccountErrors.EmailIsAlreadyConfirmed.SetParams(request.Email));
                throw new RestException(HttpStatusCode.BadRequest, ErrorResultToReturn);
            }

            var generatedEmailConfirmationToken = await UserManager.GenerateEmailConfirmationTokenAsync(user);
            var sendEmailResponse = await _emailService.SendEmailAfterRegistrationAsync(user, generatedEmailConfirmationToken,
                request.UrlToIncludeInEmail, request.Language);

            if (sendEmailResponse.ResponseType == HttpStatusCode.OK)
            {
                return new ServiceResponse(HttpStatusCode.OK);
            }

            ErrorResultToReturn = new ErrorResult(Errors.EmailErrors.ErrorOccuredWhileSendingEmailWithConfirmationLink.SetParams(request.Email));
            throw new RestException(HttpStatusCode.BadRequest, ErrorResultToReturn);
        }
    }
}