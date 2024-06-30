using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FitnessApp.SettingsApi.Contracts.Input;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;
using Xunit;

namespace FitnessApp.SettingsApi.IntegrationTests
{
    public class SettingsControllerTest : IClassFixture<MongoDbFixture>
    {
        private readonly MongoDbFixture _fixture;
        private readonly HttpClient _httpClient;

        public SettingsControllerTest(MongoDbFixture fixture)
        {
            _fixture = fixture;
            var appFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.RemoveAll<IMongoClient>();
                        services.AddSingleton<IMongoClient>((_) => _fixture.Client);
                        services
                            .AddAuthentication(defaultScheme: "TestScheme")
                            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestScheme", options => { });
                    })
                    .UseEnvironment("Development");
                });
            _httpClient = appFactory.CreateClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "TestScheme");
        }

        [Fact]
        public async Task GetSettings_ReturnsOk()
        {
            // Act
            var response = await _httpClient.GetAsync("api/Settings/GetSettings/EntityIdToGet");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateSettings_ReturnsOk()
        {
            // Arrange
            var createSettingsContract = new CreateSettingsContract
            {
                UserId = "EntityIdToCreate",
                CanFollow = Enums.PrivacyType.All,
                CanViewExercises = Enums.PrivacyType.Followers,
                CanViewFollowers = Enums.PrivacyType.FollowerssOfFollowers,
                CanViewFollowings = Enums.PrivacyType.JustMe,
                CanViewFood = Enums.PrivacyType.All,
                CanViewJournal = Enums.PrivacyType.Followers,
                CanViewProgress = Enums.PrivacyType.FollowerssOfFollowers
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("api/Settings/CreateSettings", createSettingsContract);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UpdateSettings_ReturnsOk()
        {
            // Arrange
            var createSettingsContract = new UpdateSettingsContract
            {
                UserId = "EntityIdToUpdate",
                CanFollow = Enums.PrivacyType.All,
                CanViewExercises = Enums.PrivacyType.Followers,
                CanViewFollowers = Enums.PrivacyType.FollowerssOfFollowers,
                CanViewFollowings = Enums.PrivacyType.JustMe,
                CanViewFood = Enums.PrivacyType.All,
                CanViewJournal = Enums.PrivacyType.Followers,
                CanViewProgress = Enums.PrivacyType.FollowerssOfFollowers
            };

            // Act
            var response = await _httpClient.PutAsJsonAsync("api/Settings/UpdateSettings", createSettingsContract);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteSettings_ReturnsOk()
        {
            // Act
            var response = await _httpClient.DeleteAsync("api/Settings/DeleteSettings/EntityIdToDelete");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
