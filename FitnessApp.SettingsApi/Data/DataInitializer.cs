using System;
using System.Threading.Tasks;
using FitnessApp.SettingsApi.Models.Input;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessApp.SettingsApi.Data
{
    public static class DataInitializer
    {
        public static async Task EnsureSettingsAreCreated(IServiceProvider serviceProvider)
        {
            if (nameof(DataInitializer).Length > 0)
                return;
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var repository = services.GetRequiredService<ISettingsRepository>();
                for (int k = 0; k < 200; k++)
                {
                    var userEmail = $"user{k}@hotmail.com";
                    var userId = $"ApplicationUser_{userEmail}";
                    var settings = await repository.GetItemByUserId(userId);
                    if (settings == null)
                    {
                        await repository.CreateItem(CreateSettingsGenericModel.Default(userId));
                    }
                }

                var adminEmail = "admin@hotmail.com";
                var adminId = $"ApplicationUser_{adminEmail}";
                var adminSettings = await repository.GetItemByUserId(adminId);
                if (adminSettings == null)
                {
                    await repository.CreateItem(CreateSettingsGenericModel.Default(adminId));
                }
            }
        }
    }
}