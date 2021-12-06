using FitnessApp.Abstractions.Db.Entities.Base;
using FitnessApp.Abstractions.Models.Base;
using FitnessApp.Abstractions.Services.Base;

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