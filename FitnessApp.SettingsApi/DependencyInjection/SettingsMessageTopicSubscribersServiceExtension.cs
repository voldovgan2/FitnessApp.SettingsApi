using FitnessApp.Common.Serializer.JsonSerializer;
using FitnessApp.ServiceBus.AzzureServiceBus.TopicSubscribers;
using FitnessApp.SettingsApi.Data.Entities;
using FitnessApp.SettingsApi.Models.Input;
using FitnessApp.SettingsApi.Models.Output;
using FitnessApp.SettingsApi.Services.MessageBus;
using FitnessApp.SettingsApi.Services.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FitnessApp.SettingsApi.DependencyInjection
{
    public static class SettingsMessageTopicSubscribersServiceExtension
    {
        public static IServiceCollection AddSettingsMessageTopicSubscribersService(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddTransient<ITopicSubscribers, SettingsMessageTopicSubscribersService>
            (
                sp =>
                {
                    var configuration = sp.GetRequiredService<IConfiguration>();
                    var subscription = configuration.GetValue<string>("ServiceBusSubscriptionName");
                    return new SettingsMessageTopicSubscribersService
                    (
                        sp.GetRequiredService<ISettingsService<Settings, SettingsModel, GetUsersSettingsModel, CreateSettingsModel, UpdateSettingsModel>>(),
                        subscription,
                        sp.GetRequiredService<IJsonSerializer>()
                    );
                }
            );

            return services;
        }
    }
}
