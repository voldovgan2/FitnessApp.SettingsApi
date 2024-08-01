using System.Net.Http;
using System.Net.Http.Headers;
using FitnessApp.Common.IntegrationTests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;

namespace FitnessApp.SettingsApi.IntegrationTests;
public class TestWebApplicationFactory(MongoDbFixture fixture) : TestWebApplicationFactoryBase<Program, MockAuthenticationHandler>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder
            .ConfigureTestServices(services =>
            {
                services.RemoveAll<IMongoClient>();
                services.AddSingleton<IMongoClient>((_) => fixture.Client);
            });
    }

    public HttpClient CreateHttpClient()
    {
        var httpClient = CreateClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: MockConstants.Scheme);
        return httpClient;
    }
}
