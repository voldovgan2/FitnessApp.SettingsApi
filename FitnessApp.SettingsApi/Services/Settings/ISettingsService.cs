using System.Threading.Tasks;
using FitnessApp.SettingsApi.Models.Input;
using FitnessApp.SettingsApi.Models.Output;

namespace FitnessApp.SettingsApi.Services.Settings;

public interface ISettingsService
{
    Task<SettingsGenericModel> GetSettingsByUserId(string userId);
    Task<SettingsGenericModel> CreateSettings(CreateSettingsGenericModel model);
    Task<SettingsGenericModel> UpdateSettings(UpdateSettingsGenericModel model);
    Task<string> DeleteSettings(string userId);
}