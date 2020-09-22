using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Dtos.Maintenance.Requests;
using Application.Dtos.Maintenance.Responses;
using Application.Infrastructure;
using Application.Infrastructure.Errors;
using Application.Interfaces;
using Application.Utilities;
using Application.Utilities.QuerySorters;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Enums;
using Validation;
using Validation.Models;

namespace Application.Services
{
    public class MaintenanceService : Service, IMaintenanceService
    {
        public MaintenanceService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<ServiceResponse<GetAllMessagesResponse>> GetAllMessagesAsync(GetAllMessagesRequest request)
        {
            bool isQueryIncluded = !string.IsNullOrWhiteSpace(request.Query);

            var dbQuery = Context.MaintenanceMessages.AsQueryable();

            if (isQueryIncluded)
            {
                string queryToLower = request.Query.ToLower();
                dbQuery = dbQuery.Where(m => m.Description.ToLower().Contains(queryToLower));
            }

            dbQuery = MaintenanceMessageQuerySorter.GetMessagesSortQuery(dbQuery, request.OrderBy);

            var totalNumberOfItems = dbQuery.Count();
            var messages = await dbQuery.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            var messagesToReturn = Mapper.Map<IEnumerable<MaintenanceMessage>, IEnumerable<MessageForGetAllMessagesResponse>>(messages);

            var response = new GetAllMessagesResponse(request, messagesToReturn, totalNumberOfItems, request.OrderBy ?? GetMessagesOrderBy.StartDateAsc);
            return new ServiceResponse<GetAllMessagesResponse>(HttpStatusCode.OK, response);
        }

        public async Task<ServiceResponse<GetUpcomingMessagesResponse>> GetUpcomingMessagesAsync(GetUpcomingMessagesRequest request)
        {
            bool isQueryIncluded = !string.IsNullOrWhiteSpace(request.Query);

            var dbQuery = Context.MaintenanceMessages.Where(m => m.StartDate > DateTime.UtcNow);

            if (isQueryIncluded)
            {
                string queryToLower = request.Query.ToLower();
                dbQuery = dbQuery.Where(m => m.Description.ToLower().Contains(queryToLower));
            }

            dbQuery = MaintenanceMessageQuerySorter.GetMessagesSortQuery(dbQuery, request.OrderBy);

            var totalNumberOfItems = dbQuery.Count();
            var messages = await dbQuery.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            var messagesToReturn = Mapper.Map<IEnumerable<MaintenanceMessage>, IEnumerable<MessageForGetUpcomingMessagesResponse>>(messages);

            var response = new GetUpcomingMessagesResponse(request, messagesToReturn, totalNumberOfItems, request.OrderBy ?? GetMessagesOrderBy.StartDateAsc);
            return new ServiceResponse<GetUpcomingMessagesResponse>(HttpStatusCode.OK, response);
        }

        public async Task<ServiceResponse<GetCurrentMessagesResponse>> GetCurrentMessagesAsync(GetCurrentMessagesRequest request)
        {
            bool isQueryIncluded = !string.IsNullOrWhiteSpace(request.Query);

            var dbQuery = Context.MaintenanceMessages.Where(m => m.StartDate <= DateTime.UtcNow && m.EndDate >= DateTime.UtcNow);

            if (isQueryIncluded)
            {
                string queryToLower = request.Query.ToLower();
                dbQuery = dbQuery.Where(m => m.Description.ToLower().Contains(queryToLower));
            }

            dbQuery = MaintenanceMessageQuerySorter.GetMessagesSortQuery(dbQuery, request.OrderBy);

            var totalNumberOfItems = dbQuery.Count();
            var messages = await dbQuery.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

            var messagesToReturn = Mapper.Map<IEnumerable<MaintenanceMessage>, IEnumerable<MessageForGetCurrentMessagesResponse>>(messages);

            var response = new GetCurrentMessagesResponse(request, messagesToReturn, totalNumberOfItems, request.OrderBy ?? GetMessagesOrderBy.StartDateAsc);
            return new ServiceResponse<GetCurrentMessagesResponse>(HttpStatusCode.OK, response);
        }

        public async Task<ServiceResponse<GetMessageResponse>> GetMessageAsync(int messageId)
        {
            var message = await GetEntityByIdAsync<MaintenanceMessage>(messageId);
            return new ServiceResponse<GetMessageResponse>(HttpStatusCode.OK, Mapper.Map<GetMessageResponse>(message));
        }

        public async Task<ServiceResponse<CreateMessageResponse>> CreateMessageAsync(CreateMessageRequest request)
        {
            if (request.StartDate > request.EndDate)
            {
                ErrorResultToReturn = new ErrorResult(Errors.MaintenanceErrors.StartDateMustBeNotGreaterThanEndDate);
                throw new RestException(HttpStatusCode.BadRequest, ErrorResultToReturn);
            }

            var message = Mapper.Map<MaintenanceMessage>(request);
            Context.MaintenanceMessages.Add(message);
            await SaveChangesAsync(new[] { "Wystąpił błąd podczas tworzenia wiadomości" });
            return new ServiceResponse<CreateMessageResponse>(HttpStatusCode.Created, Mapper.Map<CreateMessageResponse>(message));
        }

        public async Task<ServiceResponse<UpdateMessageResponse>> UpdateMessageAsync(int messageId, UpdateMessageRequest request)
        {
            var message = await GetEntityByIdAsync<MaintenanceMessage>(messageId);

            if (!WillMessageChange(message, request))
                return new ServiceResponse<UpdateMessageResponse>(HttpStatusCode.OK, Mapper.Map<UpdateMessageResponse>(message));

            if (request.StartDate > request.EndDate)
            {
                ErrorResultToReturn = new ErrorResult(Errors.MaintenanceErrors.StartDateMustBeNotGreaterThanEndDate);
                throw new RestException(HttpStatusCode.BadRequest, ErrorResultToReturn);
            }

            Mapper.Map(request, message);

            await SaveChangesAsync(new[] { "Wystąpił błąd podczas aktualizowania wiadomości" });

            return new ServiceResponse<UpdateMessageResponse>(HttpStatusCode.OK, Mapper.Map<UpdateMessageResponse>(message));
        }

        public async Task<ServiceResponse> DeleteMessageAsync(int messageId)
        {
            var message = await GetEntityByIdAsync<MaintenanceMessage>(messageId);

            Context.MaintenanceMessages.Remove(message);
            await SaveChangesAsync(new[] { "Wystąpił błąd podczas usuwania wiadomości" });

            return new ServiceResponse(HttpStatusCode.NoContent);
        }

        private static bool WillMessageChange(MaintenanceMessage message, UpdateMessageRequest request)
        {
            if (message.EndDate != request.EndDate ||
                message.StartDate != request.StartDate ||
                message.Description != request.Description)
                return true;

            return false;
        }
    }
}