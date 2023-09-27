using System;
using FitnessApp.Common.Serializer.JsonSerializer;
using FitnessApp.ServiceBus.AzureServiceBus.TopicSubscribers;
using FitnessApp.SettingsApi.Services.MessageBus;
using FitnessApp.SettingsApi.Services.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessApp.SettingsApi.DependencyInjection
{
    public static class SettingsMessageTopicSubscribersServiceExtension
    {
        public static IServiceCollection AddSettingsMessageTopicSubscribersService(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddTransient<ITopicSubscribers, SettingsMessageTopicSubscribersService>(
                sp =>
                {
                    var configuration = sp.GetRequiredService<IConfiguration>();
                    var subscription = configuration.GetValue<string>("ServiceBusSubscriptionName");
                    return new SettingsMessageTopicSubscribersService(
                        sp.GetRequiredService<ISettingsService>().CreateSettings,
                        subscription,
                        sp.GetRequiredService<IJsonSerializer>()
                    );
                }
            );

            return services;
        }
    }
}
