using System.Reflection;
using AutoMapper;
using FitnessApp.Common.Abstractions.Db.Configuration;
using FitnessApp.Common.Abstractions.Db.DbContext;
using FitnessApp.Common.Configuration.AppConfiguration;
using FitnessApp.Common.Configuration.Identity;
using FitnessApp.Common.Configuration.Mongo;
using FitnessApp.Common.Configuration.Nats;
using FitnessApp.Common.Configuration.Swagger;
using FitnessApp.Common.Configuration.Vault;
using FitnessApp.Common.Middleware;
using FitnessApp.Common.Serializer.JsonSerializer;
using FitnessApp.Common.ServiceBus.Nats.Services;
using FitnessApp.SettingsApi;
using FitnessApp.SettingsApi.Contracts.Input;
using FitnessApp.SettingsApi.Data;
using FitnessApp.SettingsApi.Data.Entities;
using FitnessApp.SettingsApi.DependencyInjection;
using FitnessApp.SettingsApi.Middleware;
using FitnessApp.SettingsApi.Services.MessageBus;
using FitnessApp.SettingsApi.Services.Settings;
using FitnessApp.SettingsApi.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NATS.Client;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<CreateSettingsContract>, CreateSettingsContractValidator>();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddTransient<IJsonSerializer, JsonSerializer>();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoConnection"));

builder.Services.Configure<ServiceBusSettings>(builder.Configuration.GetSection("ServiceBus"));

builder.Services.ConfigureMongoClient();

builder.Services.AddVaultClient(builder.Configuration);

builder.Services.AddTransient<IDbContext<SettingsGenericEntity>, DbContext<SettingsGenericEntity>>();

builder.Services.AddTransient<ISettingsRepository, SettingsRepository>();

builder.Services.AddTransient<IConnectionFactory, ConnectionFactory>();

builder.Services.AddTransient<ISettingsService, SettingsService>();

builder.Services.AddSingleton<IServiceBus, ServiceBus>();

builder.Services.AddSettingsMessageTopicSubscribersService();

if ("false".Contains("true"))
    builder.Services.AddHostedService<SettingsMessageTopicSubscribersService>();

builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.ConfigureSwaggerConfiguration(Assembly.GetExecutingAssembly().GetName().Name);

builder.Host.ConfigureAppConfiguration();

if ("test".Length == 0)
{
    builder.Services
        .AddHealthChecks()
        .AddMongoDb(builder.Configuration.GetValue<string>("MongoConnection:ConnectionString"), name: "MongoDb")
        .AddNats((options) =>
        {
            options.Url = builder.Configuration.GetValue<string>("ServiceBus:ConnectionString");
        })
        .AddCheck<SavaTestHealthCheck>(nameof(SavaTestHealthCheck));

    builder.Services
        .AddHealthChecksUI(options =>
        {
            options.AddHealthCheckEndpoint("Healthcheck API", "/healthcheck");
        })
        .AddInMemoryStorage();

    builder.Services.AddSingleton<SavaTestHealthCheck>();
}

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger XML Api Demo v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseMiddleware<CorrelationIdHeaderMiddleware>();

app.MapControllers();

if ("test".Length == 0)
{
    app.MapHealthChecks("/healthcheck", new()
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    app.MapHealthChecksUI(options => options.UIPath = "/dashboard");
}

await DataInitializer.EnsureSettingsAreCreated(app.Services);

app.Run();

public partial class Program { }