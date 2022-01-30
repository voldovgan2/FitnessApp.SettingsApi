using Microsoft.Extensions.Logging;
using FitnessApp.SettingsApi.Data;
using FitnessApp.Common.Abstractions.Services.Base;
using FitnessApp.Common.Abstractions.Db.Entities.Base;
using FitnessApp.Common.Abstractions.Models.Base;
using FitnessApp.Common.Abstractions.Services.Cache;

namespace FitnessApp.SettingsApi.Services.Settings
{
    public class SettingsService<Entity, Model, GetItemsModel, CreateModel, UpdateModel>
        : GenericService<Entity, Model, GetItemsModel, CreateModel, UpdateModel>
        , ISettingsService<Entity, Model, GetItemsModel, CreateModel, UpdateModel>
        where Entity : IEntity
        where Model : ISearchableModel
        where GetItemsModel : IGetItemsModel
        where CreateModel : ICreateModel
        where UpdateModel : IUpdateModel
    {        
        public SettingsService
        (
            ISettingsRepository<Entity, Model, CreateModel, UpdateModel> repository, 
            ICacheService<Model> cacheService,
            ILogger<SettingsService<Entity, Model, GetItemsModel, CreateModel, UpdateModel>> log
        )
            : base(repository, cacheService, log)
        {
        }
    }
}