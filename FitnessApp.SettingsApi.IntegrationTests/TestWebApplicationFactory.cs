using System.Linq;
using FitnessApp.SettingsApi.Services.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessApp.SettingsApi.IntegrationTests
{
    public class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .ConfigureServices((ctx, services) =>
                {
                    var settingsServiceDescriptor = services.SingleOrDefault(s => s.ServiceType == typeof(ISettingsService));
                    services.Remove(settingsServiceDescriptor);
                    services.AddTransient<ISettingsService, SettingsServiceMock>();
                })
                .ConfigureTestServices(services =>
                {
                    services
                        .AddAuthentication(defaultScheme: "TestScheme")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestScheme", options => { });
                })
                .UseEnvironment("Development");
        }
    }
}