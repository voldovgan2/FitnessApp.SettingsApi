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
            #region Contract 2 GenericModel
            CreateMap<CreateSettingsContract, CreateSettingsGenericModel>();
            CreateMap<UpdateSettingsContract, UpdateSettingsGenericModel>();
            #endregion

            #region GenericModel 2 GenericEntity
            CreateMap<SettingsGenericModel, SettingsGenericEntity>();
            CreateMap<CreateSettingsGenericModel, SettingsGenericEntity>();
            CreateMap<UpdateSettingsGenericModel, SettingsGenericEntity>();
            #endregion

            #region GenericEntity 2 GenericModel
            CreateMap<SettingsGenericEntity, SettingsGenericModel>();
            #endregion

            #region GenericModel 2 Contract
            CreateMap<SettingsGenericModel, SettingsContract>();
            #endregion
        }
    }
}
