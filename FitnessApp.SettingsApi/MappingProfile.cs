using AutoMapper;
using FitnessApp.SettingsApi.Contracts.Input;
using FitnessApp.SettingsApi.Contracts.Output;
using FitnessApp.SettingsApi.Data.Entities;
using FitnessApp.SettingsApi.Models.Input;
using FitnessApp.SettingsApi.Models.Output;

namespace FitnessApp.SettingsApi
{
    public class MappingProfile : Profile
    {    
        public MappingProfile()
        {
            CreateMap<Settings, SettingsModel>();
            CreateMap<SettingsModel, Settings>();
            CreateMap<CreateSettingsModel, Settings>();

            CreateMap<SettingsModel, SettingsContract>();
            CreateMap<CreateSettingsContract, CreateSettingsModel>();
            CreateMap<UpdateSettingsContract, UpdateSettingsModel>();
        }
    }
}
