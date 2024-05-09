using System.Reflection;
using FitnessApp.Common.Configuration;
using FitnessApp.Common.Middleware;
using FitnessApp.Common.Serializer.JsonSerializer;
using FitnessApp.SettingsApi;
using FitnessApp.SettingsApi.Contracts.Input;
using FitnessApp.SettingsApi.Data;
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
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<CreateSettingsContract>, CreateSettingsContractValidator>();

builder.Services.ConfigureMapper(new MappingProfile());
builder.Services.AddTransient<IJsonSerializer, JsonSerializer>();
builder.Services.ConfigureMongo(builder.Configuration);
builder.Services.ConfigureVault(builder.Configuration);
builder.Services.ConfigureSettingsRepository();
builder.Services.ConfigureNats(builder.Configuration);
builder.Services.AddSettingsMessageTopicSubscribersService();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureSwagger(Assembly.GetExecutingAssembly().GetName().Name);
builder.Services.AddTransient<ISettingsService, SettingsService>();
if ("false".Contains("true"))
    builder.Services.AddHostedService<SettingsMessageTopicSubscribersService>();

builder.Host.ConfigureAppConfiguration();

if ("test".Length == 0)
{
    builder.Services
        .AddHealthChecks()
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
    app.UseSwaggerAndUi();
}

app.UseHttpsRedirection();
app.UseMiddleware<RequestResponseLoggingMiddleware>();
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