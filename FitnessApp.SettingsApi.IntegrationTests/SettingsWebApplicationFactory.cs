using FitnessApp.Common.Tests.Fixtures;
using FitnessApp.SettingsApi.Data.Entities;

namespace FitnessApp.SettingsApi.IntegrationTests;
public class SettingsWebApplicationFactory(SettingsMongoDbFixture fixture) :
    TestWebApplicationFactory<Program, MockAuthenticationHandler, SettingsGenericEntity>(fixture);
