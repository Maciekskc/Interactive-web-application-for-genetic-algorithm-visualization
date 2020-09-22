using Application.Dtos.Account.Requests;
using Application.Dtos.Account.Responses;
using Application.Dtos.Admin.Requests;
using Application.Dtos.Admin.Responses;
using Application.Dtos.Logs.Responses;
using AutoMapper;
using Domain.Models.Entities;
using System;
using System.Globalization;
using System.IO;
using Application.Dtos.Auth.Responses;
using Application.Dtos.Maintenance.Requests;
using Application.Dtos.Maintenance.Responses;

namespace Application.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapsForAuth();
            MapsForAccount();
            MapsForAdmin();
            MapsForLogs();
            MapsForMaintenance();
        }

        /// <summary>
        /// Mapy dla kontrolera Maintenance
        /// </summary>
        private void MapsForMaintenance()
        {
            CreateMap<CreateMessageRequest, MaintenanceMessage>();
            CreateMap<MaintenanceMessage, CreateMessageResponse>();

            CreateMap<MaintenanceMessage, GetMessageResponse>();

            CreateMap<MaintenanceMessage, MessageForGetAllMessagesResponse>();

            CreateMap<MaintenanceMessage, MessageForGetUpcomingMessagesResponse>();

            CreateMap<MaintenanceMessage, MessageForGetCurrentMessagesResponse>();

            CreateMap<UpdateMessageRequest, MaintenanceMessage>();
            CreateMap<MaintenanceMessage, UpdateMessageResponse>();
        }

        /// <summary>
        /// Mapy dla kontrolera Admin
        /// </summary>
        private void MapsForAdmin()
        {
            CreateMap<ApplicationUser, UserForGetUsersResponse>();

            CreateMap<ApplicationUser, GetUserResponse>();

            CreateMap<ApplicationUser, CreateUserResponse>();

            CreateMap<UpdateUserRequest, ApplicationUser>();
            CreateMap<ApplicationUser, UpdateUserResponse>();
        }

        /// <summary>
        /// Mapy dla kontrolera Account
        /// </summary>
        private void MapsForAccount()
        {
            CreateMap<ApplicationUser, GetAccountDetailsResponse>();

            CreateMap<UpdateAccountDetailsRequest, ApplicationUser>();
            CreateMap<ApplicationUser, UpdateAccountDetailsResponse>();
        }

        /// <summary>
        /// Mapy dla kontrolera Auth
        /// </summary>
        private void MapsForAuth()
        {
            CreateMap<ApplicationUser, RegisterResponse>();
        }

        /// <summary>
        /// Mapy dla kontrolera Logs
        /// </summary>
        private void MapsForLogs()
        {
            CreateMap<FileInfo, LogForGetLogsFilesResponse>()
                .ForMember(log => log.SizeInKb, opt => opt.MapFrom(fileInfo => Math.Round((double)(fileInfo.Length / 1024), 2)))
                .ForMember(log => log.Date, opt => opt.MapFrom(fileInfo => DateTime.ParseExact(fileInfo.Name.Substring(3, 8), "yyyyMMdd", CultureInfo.InvariantCulture)))
                .ForMember(log => log.Name, opt => opt.MapFrom(fileInfo => Path.GetFileNameWithoutExtension(fileInfo.Name)));
        }
    }
}