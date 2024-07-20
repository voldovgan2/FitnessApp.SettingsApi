using System;
using FitnessApp.Common.Abstractions.Db.DbContext;
using FitnessApp.SettingsApi.Data;
using FitnessApp.SettingsApi.Data.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessApp.SettingsApi.DependencyInjection;

public static class SettingsRepositoryExtension
{
    public static IServiceCollection ConfigureSettingsRepository(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddTransient<IDbContext<SettingsGenericEntity>, DbContext<SettingsGenericEntity>>();
        services.AddTransient<ISettingsRepository, SettingsRepository>();

        return services;
    }
}
