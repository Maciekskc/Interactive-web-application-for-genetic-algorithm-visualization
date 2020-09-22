using Application.Dtos.Admin.Requests;
using Application.Dtos.Admin.Responses;
using Application.Infrastructure.Errors;
using Application.Interfaces;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Utilities;
using Application.Utilities.QuerySorters;
using Validation;
using Validation.Models;

namespace Application.Services
{
    public class AdminService : Service, IAdminService
    {
        private readonly IEmailService _emailService;

        public AdminService(IServiceProvider serviceProvider, IEmailService emailService) : base(serviceProvider)
        {
            _emailService = emailService;
        }

        public async Task<ServiceResponse<GetUsersResponse>> GetUsersAsync(GetUsersRequest request)
        {
            bool isQueryIncluded = !string.IsNullOrWhiteSpace(request.Query);

            var dbQuery = Context.Users.Where(u => u.IsDeleted == false);

            if (isQueryIncluded)
            {
                string queryToLower = request.Query.ToLower();
                dbQuery = dbQuery.Where(u => u.Email.ToLower().Contains(queryToLower)
                                             || u.LastName.ToLower().Contains(queryToLower)
                                             || u.FirstName.ToLower().Contains(queryToLower)
                                             || u.UserName.ToLower().Contains(queryToLower));
            }

            dbQuery = AdminQuerySorter.GetUsersSortQuery(dbQuery, request.OrderBy);

            var totalNumberOfItems = await dbQuery.CountAsync();
            var users = await dbQuery.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            var usersWithRoles = Mapper.Map<List<ApplicationUser>, List<UserForGetUsersResponse>>(users);
            foreach (var user in users)
            {
                var currentUserDto = usersWithRoles.Single(u => u.Id == user.Id);
                var roles = await UserManager.GetRolesAsync(user);
                currentUserDto.Roles = roles.ToList();
            }

            var response = new GetUsersResponse(request, usersWithRoles, totalNumberOfItems, request.OrderBy ?? GetUsersOrderBy.LastNameAsc);
            return new ServiceResponse<GetUsersResponse>(HttpStatusCode.OK, response);
        }

        public async Task<ServiceResponse<GetUserResponse>> GetUserAsync(string userId)
        {
            var user = await GetEntityByIdAsync<ApplicationUser>(userId);

            var userRoles = await UserManager.GetRolesAsync(user);

            var response = Mapper.Map<ApplicationUser, GetUserResponse>(user);
            response.Roles = userRoles.ToList();

            return new ServiceResponse<GetUserResponse>(HttpStatusCode.OK, response);
        }

        public async Task<ServiceResponse<CreateUserResponse>> CreateUserAsync(CreateUserRequest request)
        {
            var userToRegister = new ApplicationUser()
            {
                LastName = request.LastName,
                FirstName = request.FirstName,
                Email = request.Email,
                UserName = request.Email
            };

            await UserCreator.CreateUserAsync(UserManager, userToRegister, request.Password, request.Roles);

            var generatedEmailConfirmationToken =
                await UserManager.GenerateEmailConfirmationTokenAsync(userToRegister);

            var sendEmailResult = await _emailService.SendEmailAfterRegistrationAsync(userToRegister, generatedEmailConfirmationToken, request.UrlToIncludeInEmail, request.Language);

            var emailErrors = new List<string>();

            if (sendEmailResult.ResponseType != HttpStatusCode.OK)
                emailErrors = sendEmailResult.Errors.ToList();

            var userDtoToReturn = Mapper.Map<ApplicationUser, CreateUserResponse>(userToRegister);
            userDtoToReturn.Roles = request.Roles;

            return emailErrors.Any() ?
                new ServiceResponse<CreateUserResponse>(HttpStatusCode.Created, emailErrors, userDtoToReturn)
                : new ServiceResponse<CreateUserResponse>(HttpStatusCode.Created, userDtoToReturn);
        }

        public async Task<ServiceResponse<UpdateUserResponse>> UpdateUserAsync(string userId, UpdateUserRequest request)
        {
            var user = await GetEntityByIdAsync<ApplicationUser>(userId);

            Mapper.Map(request, user);

            var updatedInfoResult = await UserManager.UpdateAsync(user);
            if (!updatedInfoResult.Succeeded)
            {
                ErrorResultToReturn = new ErrorResult(Errors.AccountErrors.ErrorOccuredWhileUpdatingUser,
                    updatedInfoResult.Errors.Select(e => e.Description));
                throw new RestException(HttpStatusCode.BadRequest, ErrorResultToReturn);
            }

            var currentUserRoles = await UserManager.GetRolesAsync(user);

            var removeFromRolesResult = await UserManager.RemoveFromRolesAsync(user, currentUserRoles);
            if (!removeFromRolesResult.Succeeded)
                throw new RestException(HttpStatusCode.BadRequest, new ErrorResult(Errors.AccountErrors.ErrorOccuredWhileUpdatingUser, removeFromRolesResult.Errors.Select(e => e.Description)));

            var addToRolesResult = await UserManager.AddToRolesAsync(user, request.Roles);
            if (!addToRolesResult.Succeeded)
                throw new RestException(HttpStatusCode.BadRequest, new ErrorResult(Errors.AccountErrors.ErrorOccuredWhileUpdatingUser, addToRolesResult.Errors.Select(e => e.Description)));

            var response = Mapper.Map<ApplicationUser, UpdateUserResponse>(user);
            response.Roles = request.Roles;

            return new ServiceResponse<UpdateUserResponse>(HttpStatusCode.OK, response);
        }

        public async Task<ServiceResponse> DeleteUserAsync(string userId)
        {
            var user = await GetEntityByIdAsync<ApplicationUser>(userId);

            user.IsDeleted = true;
            var updateResult = await UserManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                throw new RestException(HttpStatusCode.BadRequest, new ErrorResult(Errors.AccountErrors.ErrorOccuredWhileDeletingUser, updateResult.Errors.Select(e => e.Description)));

            return new ServiceResponse(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResponse> SetUserPasswordAsync(string userId, SetUserPasswordRequest request)
        {
            var user = await GetEntityByIdAsync<ApplicationUser>(userId);

            var resetPasswordToken = await UserManager.GeneratePasswordResetTokenAsync(user);

            var resetPasswordResult = await UserManager.ResetPasswordAsync(user, resetPasswordToken, request.NewPassword);
            if (!resetPasswordResult.Succeeded)
                throw new RestException(HttpStatusCode.BadRequest, new ErrorResult(Errors.AccountErrors.ErrorOccuredWhileSettingPassword, resetPasswordResult.Errors.Select(e => e.Description)));

            return new ServiceResponse(HttpStatusCode.OK);
        }
    }
}