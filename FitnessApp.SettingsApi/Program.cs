using System.Reflection;
using AutoMapper;
using FitnessApp.AzureServiceBus;
using FitnessApp.Common.Abstractions.Db.Configuration;
using FitnessApp.Common.Abstractions.Db.DbContext;
using FitnessApp.Common.Configuration.AppConfiguration;
using FitnessApp.Common.Configuration.Identity;
using FitnessApp.Common.Configuration.Mongo;
using FitnessApp.Common.Configuration.Swagger;
using FitnessApp.Common.Middleware;
using FitnessApp.Common.Serializer.JsonSerializer;
using FitnessApp.ServiceBus;
using FitnessApp.ServiceBus.AzureServiceBus.Configuration;
using FitnessApp.ServiceBus.AzureServiceBus.Consumer;
using FitnessApp.SettingsApi;
using FitnessApp.SettingsApi.Contracts.Input;
using FitnessApp.SettingsApi.Data;
using FitnessApp.SettingsApi.Data.Entities;
using FitnessApp.SettingsApi.DependencyInjection;
using FitnessApp.SettingsApi.Middleware;
using FitnessApp.SettingsApi.Services.Settings;
using FitnessApp.SettingsApi.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

builder.Services.Configure<AzureServiceBusSettings>(builder.Configuration.GetSection("AzureServiceBusSettings"));

builder.Services.ConfigureMongoClient(builder.Configuration);

builder.Services.AddTransient<IDbContext<SettingsGenericEntity>, DbContext<SettingsGenericEntity>>();

builder.Services.AddTransient<ISettingsRepository, SettingsRepository>();

builder.Services.AddTransient<ISettingsService, SettingsService>();

builder.Services.AddSettingsMessageTopicSubscribersService();

builder.Services.AddSingleton<IMessageConsumer, MessageConsumer>();

builder.Services.AddHostedService<MessageListenerService>();

builder.Services.ConfigureAzureAdAuthentication(builder.Configuration);

builder.Services.ConfigureSwaggerConfiguration(Assembly.GetExecutingAssembly().GetName().Name);

builder.Host.ConfigureAzureAppConfiguration();

builder.Services.AddHealthChecks()
        .AddMongoDb(builder.Configuration.GetValue<string>("MongoConnection:ConnectionString"), name: "MongoDb")
        .AddAzureServiceBusTopic(builder.Configuration.GetValue<string>("AzureServiceBusSettings:ConnectionString"), Topic.NEW_USER_REGISTERED)
        .AddCheck<SavaTestHealthCheck>(nameof(SavaTestHealthCheck));

builder.Services
    .AddHealthChecksUI(options =>
    {
        options.AddHealthCheckEndpoint("Healthcheck API", "/healthcheck");
    })
    .AddInMemoryStorage();

builder.Services.AddSingleton<SavaTestHealthCheck>();

builder.Services.AddApplicationInsightsTelemetry(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<CorrelationIdHeaderMiddleware>();

app.MapControllers();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger XML Api Demo v1");
});

app.MapHealthChecks("/healthcheck", new()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecksUI(options => options.UIPath = "/dashboard");

app.Run();

public partial class Program { }