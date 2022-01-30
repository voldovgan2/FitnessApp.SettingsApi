using FitnessApp.Common.Abstractions.Db.Entities.Base;
using FitnessApp.Common.Abstractions.Db.Repository.Base;
using FitnessApp.Common.Abstractions.Models.Base;

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