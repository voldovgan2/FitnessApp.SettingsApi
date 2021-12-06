using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FitnessApp.SettingsApi.Models.Input;
using FitnessApp.SettingsApi.Models.Output;
using FitnessApp.SettingsApi.Data.Entities;
using System.Linq;
using FitnessApp.Abstractions.Services.Cache;

namespace FitnessApp.SettingsApi.Data
{
    public class DataInitializer
    {
        public static async Task EnsureSettingsAreCreatedAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var repository = services.GetRequiredService<ISettingsRepository<Settings, SettingsModel, CreateSettingsModel, UpdateSettingsModel>>();
                for (int k = 0; k < 200; k++)
                {
                    var userEmail = $"user{k}@hotmail.com";
                    var userId = $"ApplicationUser_{userEmail}";
                    var settings = await repository.GetItemByUserIdAsync(userId);
                    if (settings == null)
                    {
                        await repository.CreateItemAsync(CreateSettingsModel.Default(userId));
                    }
                }
                var adminEmail = "admin@hotmail.com";
                var adminId = $"ApplicationUser_{adminEmail}";
                var adminSettings = await repository.GetItemByUserIdAsync(adminId);
                if (adminSettings == null)
                {
                    await repository.CreateItemAsync(CreateSettingsModel.Default(adminId));
                }
            }
        }

        public static async Task FillCacheWithSettingsAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var settingsRepository = services.GetRequiredService<ISettingsRepository<Settings, SettingsModel, CreateSettingsModel, UpdateSettingsModel>>();
                var cacheService = services.GetRequiredService<ICacheService<SettingsModel>>();
                var allSettings = await settingsRepository.GetAllItemsAsync();
                if (allSettings != null)
                {
                    foreach (var setting in allSettings.ToList())
                    {
                        await cacheService.SaveItem(setting.UserId, setting);
                    }
                }
            }
        }
    }
}