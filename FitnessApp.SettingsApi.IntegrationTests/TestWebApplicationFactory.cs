using FitnessApp.Common.Tests.Fixtures;
using FitnessApp.SettingsApi.Data.Entities;

namespace FitnessApp.SettingsApi.IntegrationTests;
public class TestWebApplicationFactory(
    MongoDbFixture fixture,
    string databaseName,
    string collecttionName,
    string[] ids) :
    TestGenericWebApplicationFactoryBase<
        Program,
        MockAuthenticationHandler,
        SettingsGenericEntity>(
        fixture,
        databaseName,
        collecttionName,
        ids);
