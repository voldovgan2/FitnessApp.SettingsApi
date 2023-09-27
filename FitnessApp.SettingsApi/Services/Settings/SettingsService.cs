using System.Threading.Tasks;
using AutoMapper;
using FitnessApp.Common.Abstractions.Services.Generic;
using FitnessApp.SettingsApi.Data;
using FitnessApp.SettingsApi.Data.Entities;
using FitnessApp.SettingsApi.Models.Input;
using FitnessApp.SettingsApi.Models.Output;

namespace FitnessApp.SettingsApi.Services.Settings
{
    public class SettingsService
        : GenericService<SettingsGenericEntity, SettingsGenericModel, CreateSettingsGenericModel, UpdateSettingsGenericModel>,
        ISettingsService
    {
        public SettingsService(
            ISettingsRepository repository,
            IMapper mapper
        ) : base(repository, mapper)
        {
        }

        public Task<SettingsGenericModel> GetSettingsByUserId(string userId)
        {
            return GetItemByUserId(userId);
        }

        public Task<SettingsGenericModel> CreateSettings(CreateSettingsGenericModel model)
        {
            return CreateItem(model);
        }

        public Task<SettingsGenericModel> UpdateSettings(UpdateSettingsGenericModel model)
        {
            return UpdateItem(model);
        }

        public Task<string> DeleteSettings(string userId)
        {
            return DeleteItem(userId);
        }
    }
}