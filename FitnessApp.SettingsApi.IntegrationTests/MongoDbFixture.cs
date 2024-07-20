using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitnessApp.Common.Abstractions.Db.Configuration;
using FitnessApp.Common.Abstractions.Db.DbContext;
using FitnessApp.SettingsApi.Data.Entities;
using Microsoft.Extensions.Options;
using Mongo2Go;
using MongoDB.Driver;

namespace FitnessApp.SettingsApi.IntegrationTests;

public class MongoDbFixture : IDisposable
{
    private readonly MongoDbRunner _runner;
    public MongoClient Client { get; }

    public MongoDbFixture()
    {
        _runner = MongoDbRunner.Start();
        Client = new MongoClient(_runner.ConnectionString);
        CreateMockData().GetAwaiter().GetResult();
    }

    private async Task CreateMockData()
    {
        var mongoDbSettings = new MongoDbSettings
        {
            DatabaseName = "FitnessSettings",
            CollecttionName = "Settings",
        };
        var dbContext = new DbContext<SettingsGenericEntity>(Client, Options.Create(mongoDbSettings));
        IEnumerable<string> itemIds =
        [
            "EntityIdToGet",
            "EntityIdToDelete",
            "EntityIdToUpdate"
        ];
        var createdItemsTasks = itemIds.Select(userId => dbContext.CreateItem(new SettingsGenericEntity
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

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
            _runner.Dispose();
    }
}
