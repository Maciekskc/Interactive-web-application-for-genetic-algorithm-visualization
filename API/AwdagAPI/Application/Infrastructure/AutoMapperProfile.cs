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
using System.Linq;
using Application.Dtos.Aquarium.Requests;
using Application.Dtos.Aquarium.Responses;
using Application.Dtos.Auth.Responses;
using Application.Dtos.Fish.Response;
using Application.Dtos.Hub;
using Domain.Models;

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
            MapsForFishes();
            MapsForAquariums();
            MapsForHub();
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

        /// <summary>
        /// Mapy dla kontrolera Fishes
        /// </summary>
        private void MapsForFishes()
        {
            CreateMap<Fish, FishForGetFishesFromAquariumResponse>();
            CreateMap<PhysicalStatistic, PhysicalStatsForFishForGetFishFromAquariumResponse>();
            CreateMap<LifeTimeStatistic, LifeTimeStatisticForFishForGetFishFromAquariumResponse>();
            CreateMap<LifeParameters, LifeParametersForFishForGetFishFromAquariumResponse>();

            CreateMap<Fish, GetFishResponse>();
            CreateMap<PhysicalStatistic, PhysicalStatisticForGetFishResponse>();
            CreateMap<SetOfMutations, SetOfMutationsForGetFishResponse>();
            CreateMap<LifeTimeStatistic, LifeTimeStatisticForGetFishResponse>();
            CreateMap<LifeParameters, LifeParametersForGetFishResponse>();
            CreateMap<Fish, ParentOfFishForGetFishResponse>().ForMember(opt=>opt.Color,par=>par.MapFrom(src=>src.PhysicalStatistic.Color));

            CreateMap<Fish, FishForGetUserFishesResponse>();
        }

        /// <summary>
        /// Mapy dla kontrolera Aquarium
        /// </summary>
        private void MapsForAquariums()
        {
            CreateMap<Aquarium, GetAquariumResponse>()
                .ForMember(opt => opt.CurrentFoodsAmount,
                    par => par.MapFrom(src => src.Foods.Count))
                .ForMember(opt => opt.CurrentPopulationCount,
                    par => par.MapFrom(src => src.Fishes.Where(f=>f.IsAlive).Count()));

            CreateMap<Aquarium, AquariumForGetAllAquariumsResponse>()
                .ForMember(opt => opt.CurrentFoodsAmount,
                    par => par.MapFrom(src => src.Foods.Count))
                .ForMember(opt => opt.CurrentPopulationCount,
                    par => par.MapFrom(src => src.Fishes.Where(f => f.IsAlive).Count()));

            CreateMap<CreateAquariumRequest, Aquarium>();
        }

        private void MapsForHub()
        {
            CreateMap<Aquarium, HubTransferData>()
                .ForMember(opt => opt.AquariumWidth, src => src.MapFrom(aq => aq.Width))
                .ForMember(opt => opt.AquariumHeight, src => src.MapFrom(aq => aq.Height))
                .ForMember(opt => opt.Fishes, src => src.MapFrom(aq => aq.Fishes.Where(f => f.IsAlive)));
            CreateMap<Fish, FishForHubTransferData>()
                .ForMember(opt => opt.HungryCharge, src => src.MapFrom(f=>f.SetOfMutations.HungryCharge))
                .ForMember(opt => opt.Predator, src => src.MapFrom(f => f.SetOfMutations.Predator));
            CreateMap<PhysicalStatistic, PhysicalStatsForFishForHubTransferData>();
            CreateMap<Food, FoodForHubTransferData>();
        }
    }
}