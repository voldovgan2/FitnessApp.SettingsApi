using FitnessApp.Common.IntegrationTests;
using FitnessApp.SettingsApi.Data.Entities;

namespace FitnessApp.SettingsApi.IntegrationTests;
public class TestWebApplicationFactory(MongoDbFixture fixture) :
    TestGenericWebApplicationFactoryBase<
        Program,
        MockAuthenticationHandler,
        SettingsGenericEntity>(fixture);
