using FitnessApp.Common.Abstractions.Db.Entities.Base;
using FitnessApp.Common.Abstractions.Models.Base;
using FitnessApp.Common.Abstractions.Services.Base;

namespace FitnessApp.SettingsApi.Services.Settings
{
    public interface ISettingsService<Entity, Model, GetItemsModel, CreateModel, UpdateModel>
        : IGenericService<Entity, Model, GetItemsModel, CreateModel, UpdateModel>
        where Entity : IEntity
        where Model : ISearchableModel
        where GetItemsModel : IGetItemsModel
        where CreateModel : ICreateModel
        where UpdateModel : IUpdateModel
    {
    }
}