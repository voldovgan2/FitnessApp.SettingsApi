using FitnessApp.Abstractions.Db.Repository.Base;
using FitnessApp.Abstractions.Db.Entities.Base;
using FitnessApp.Abstractions.Models.Base;

namespace FitnessApp.SettingsApi.Data
{
    public interface ISettingsRepository<Entity, Model, CreateModel, UpdateModel> 
        : IGenericRepository<Entity, Model, CreateModel, UpdateModel>
        where Entity : IEntity
        where Model : ISearchableModel
        where CreateModel : ICreateModel
        where UpdateModel : IUpdateModel
    { 
    }
}