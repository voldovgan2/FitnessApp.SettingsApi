using FitnessApp.Common.Abstractions.Db.Repository.Generic;
using FitnessApp.SettingsApi.Data.Entities;
using FitnessApp.SettingsApi.Models.Input;
using FitnessApp.SettingsApi.Models.Output;

namespace FitnessApp.SettingsApi.Data
{
    public interface ISettingsRepository
        : IGenericRepository<
            SettingsGenericEntity,
            SettingsGenericModel,
            CreateSettingsGenericModel,
            UpdateSettingsGenericModel>
    { }
}