﻿using System;
using FitnessApp.Common.ServiceBus.Nats.Services;
using FitnessApp.SettingsApi.Services.MessageBus;
using FitnessApp.SettingsApi.Services.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessApp.SettingsApi.DependencyInjection;

public static class SettingsMessageTopicSubscribersServiceExtension
{
    public static IServiceCollection AddSettingsMessageTopicSubscribersService(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddTransient(
            sp =>
            {
                return new SettingsMessageTopicSubscribersService(
                    sp.GetRequiredService<IServiceBus>(),
                    sp.GetRequiredService<ISettingsService>().CreateSettings);
            }
        );

        return services;
    }
}
