using System.Linq;
using FitnessApp.Common.Tests.Fixtures;
using FitnessApp.SettingsApi.Data.Entities;

namespace FitnessApp.SettingsApi.IntegrationTests;

public class SettingsMongoDbFixture() :
    MongoDbFixture<SettingsGenericEntity>([.. IdsConstants.IdsToSeed.Select(id => new SettingsGenericEntity { Id = id, UserId = id, })]);
