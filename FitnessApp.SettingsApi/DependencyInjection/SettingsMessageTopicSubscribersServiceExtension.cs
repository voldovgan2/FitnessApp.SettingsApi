using System;
using FitnessApp.Common.Serializer.JsonSerializer;
using FitnessApp.Common.ServiceBus.Nats.Services;
using FitnessApp.SettingsApi.Services.MessageBus;
using FitnessApp.SettingsApi.Services.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessApp.SettingsApi.DependencyInjection
{
    public static class SettingsMessageTopicSubscribersServiceExtension
    {
        public static IServiceCollection AddSettingsMessageTopicSubscribersService(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddTransient(
                sp =>
                {
                    return new SettingsMessageTopicSubscribersService(
                        sp.GetRequiredService<IServiceBus>(),
                        sp.GetRequiredService<ISettingsService>().CreateSettings,
                        sp.GetRequiredService<IJsonSerializer>()
                    );
                }
            );

            return services;
        }
    }
}
