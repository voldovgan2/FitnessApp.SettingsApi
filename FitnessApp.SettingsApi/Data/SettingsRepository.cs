using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using FitnessApp.Common.Abstractions.Db.Repository.Base;
using FitnessApp.Common.Abstractions.Db.Entities.Base;
using FitnessApp.Common.Abstractions.Models.Base;
using FitnessApp.Common.Abstractions.Db.Configuration;
using AutoMapper;

namespace FitnessApp.SettingsApi.Data
{
    public class SettingsRepository<BaseEntity, Model, CreateModel, UpdateModel> 
        : GenericRepository<BaseEntity, Model, CreateModel, UpdateModel>
        , ISettingsRepository<BaseEntity, Model, CreateModel, UpdateModel>
        where BaseEntity : IEntity
        where Model : ISearchableModel
        where CreateModel : ICreateModel
        where UpdateModel : IUpdateModel
    {
        public SettingsRepository
        (
            IOptions<CosmosDbSettings> settings, 
            IMapper mapper,
            ILogger<SettingsRepository<BaseEntity, Model, CreateModel, UpdateModel>> log
        )
            : base(settings, mapper, log)
        {
            
        }
     }
}