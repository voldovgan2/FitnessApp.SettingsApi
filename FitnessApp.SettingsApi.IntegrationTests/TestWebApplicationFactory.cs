using FitnessApp.Common.IntegrationTests;
using FitnessApp.SettingsApi.Data.Entities;

namespace FitnessApp.SettingsApi.IntegrationTests;
public class TestWebApplicationFactory(MongoDbFixture fixture) :
    TestAbstractWebApplicationFactoryBase<
        Program,
        MockAuthenticationHandler,
        SettingsGenericEntity>(fixture);
