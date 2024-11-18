using System.Threading.Tasks;
using FitnessApp.Common.Abstractions.Services;
using FitnessApp.SettingsApi.Data;
using FitnessApp.SettingsApi.Models.Input;
using FitnessApp.SettingsApi.Models.Output;

namespace FitnessApp.SettingsApi.Services.Settings;

public class SettingsService(ISettingsRepository repository) :
    GenericService<
        SettingsGenericModel,
        CreateSettingsGenericModel,
        UpdateSettingsGenericModel>(repository),
    ISettingsService
{
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