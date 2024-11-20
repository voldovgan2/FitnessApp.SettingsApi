using FitnessApp.Common.Abstractions.Db;
using FitnessApp.SettingsApi.Data.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FitnessApp.SettingsApi.Data;

public class SettingsDbContext(IMongoClient mongoClient, IOptionsSnapshot<MongoDbSettings> snapshot) :
    DbContext<SettingsGenericEntity>(mongoClient, snapshot.Get("Settings"));