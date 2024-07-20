using System;
using System.Threading.Tasks;
using FitnessApp.SettingsApi.Models.Input;
using FitnessApp.SettingsApi.Services.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessApp.SettingsApi.Data;

public static class DataInitializer
{
    public static async Task EnsureSettingsAreCreated(IServiceProvider serviceProvider)
    {
        if (nameof(DataInitializer).Length > 0)
            return;

        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        var service = services.GetRequiredService<ISettingsService>();
        for (int k = 0; k < 20; k++)
        {
            var userEmail = $"user{k}@hotmail.com";
            var userId = $"ApplicationUser_{userEmail}";
            await service.DeleteSettings(userId);
            await service.CreateSettings(CreateSettingsGenericModel.Default(userId));
        }

        var adminEmail = "admin@hotmail.com";
        var adminId = $"ApplicationUser_{adminEmail}";
        var adminSettings = await service.GetSettingsByUserId(adminId);
        if (adminSettings == null)
            await service.CreateSettings(CreateSettingsGenericModel.Default(adminId));
    }
}