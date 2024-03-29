using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FitnessApp.SettingsApi.Contracts.Input;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace FitnessApp.SettingsApi.IntegrationTests
{
    public class SettingsControllerTest : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        public SettingsControllerTest(TestWebApplicationFactory factory)
        {
            _httpClient = factory
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false,
                });
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "TestScheme");
        }

        [Fact]
        public async Task GetSettings_ReturnsOk()
        {
            // Act
            var response = await _httpClient.GetAsync("api/Settings/GetSettings/test");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateSettings_ReturnsOk()
        {
            // Arrange
            var createSettingsContract = new CreateSettingsContract
            {
                UserId = "test",
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
                UserId = "test",
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
            var response = await _httpClient.DeleteAsync("api/Settings/DeleteSettings/test");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
