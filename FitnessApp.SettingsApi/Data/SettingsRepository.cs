using AutoMapper;
using FitnessApp.Common.Abstractions.Db.DbContext;
using FitnessApp.Common.Abstractions.Db.Repository.Generic;
using FitnessApp.SettingsApi.Data.Entities;
using FitnessApp.SettingsApi.Models.Input;
using FitnessApp.SettingsApi.Models.Output;

namespace FitnessApp.SettingsApi.Data
{
    public class SettingsRepository(
        IDbContext<SettingsGenericEntity> dbContext,
        IMapper mapper
        )
        : GenericRepository<
            SettingsGenericEntity,
            SettingsGenericModel,
            CreateSettingsGenericModel,
            UpdateSettingsGenericModel>(dbContext, mapper),
        ISettingsRepository
    {
    }
}