using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using FitnessApp.Abstractions.Models.Base;
using FitnessApp.Abstractions.Db.Repository.Base;
using FitnessApp.Abstractions.Db.Configuration;
using FitnessApp.Serializer.JsonMapper;
using FitnessApp.Abstractions.Db.Entities.Base;

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
            IOptions<MongoDbSettings> settings, 
            IJsonMapper mapper, 
            ILogger<SettingsRepository<BaseEntity, Model, CreateModel, UpdateModel>> log
        )
            : base(settings, mapper, log)
        {
            
        }
     }
}