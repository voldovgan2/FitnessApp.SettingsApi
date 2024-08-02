using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessApp.Common.Abstractions.Db.Configuration;
using FitnessApp.Common.Abstractions.Db.DbContext;
using FitnessApp.Common.IntegrationTests;
using FitnessApp.SettingsApi.Data.Entities;

namespace FitnessApp.SettingsApi.IntegrationTests;

public class MongoDbFixture() : MongoDbFixtureBase<SettingsGenericEntity>(new MongoDbSettings
{
    DatabaseName = "FitnessSettings",
    CollecttionName = "Settings",
})
{
    protected override async Task CreateMockData(DbContext<SettingsGenericEntity> dbContext)
    {
        IEnumerable<string> userIds =
        [
            Constants.UserIdToGet,
            Constants.UserIdToUpdate,
            Constants.UserIdToDelete
        ];
        var createdItemsTasks = userIds.Select(userId => dbContext.CreateItem(new SettingsGenericEntity
        {
            UserId = userId,
            CanFollow = Enums.PrivacyType.All,
            CanViewExercises = Enums.PrivacyType.Followers,
            CanViewFollowers = Enums.PrivacyType.FollowerssOfFollowers,
            CanViewFollowings = Enums.PrivacyType.JustMe,
            CanViewFood = Enums.PrivacyType.All,
            CanViewJournal = Enums.PrivacyType.Followers,
            CanViewProgress = Enums.PrivacyType.FollowerssOfFollowers
        }));
        await Task.WhenAll(createdItemsTasks);
    }
}
