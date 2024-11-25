﻿using System;
using FitnessApp.Common.Abstractions.Db;
using FitnessApp.SettingsApi.Data;
using FitnessApp.SettingsApi.Data.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessApp.SettingsApi.DependencyInjection;

public static class SettingsRepositoryExtension
{
    public static IServiceCollection ConfigureSettingsRepository(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddTransient<IDbContext<SettingsGenericEntity>, SettingsDbContext>();
        services.AddTransient<ISettingsRepository, SettingsRepository>();

        return services;
    }
}
