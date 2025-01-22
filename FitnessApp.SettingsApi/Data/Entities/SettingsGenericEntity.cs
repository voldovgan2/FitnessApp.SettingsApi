using FitnessApp.Common.Abstractions.Db;
using FitnessApp.SettingsApi.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace FitnessApp.SettingsApi.Data.Entities;

public class SettingsGenericEntity : IWithUserIdEntity
{
    [BsonId]
    public string Id { get; init; }
    public string UserId { get; init; }
    public PrivacyType CanFollow { get; set; }
    public PrivacyType CanViewFollowers { get; set; }
    public PrivacyType CanViewFollowings { get; set; }
    public PrivacyType CanViewFood { get; set; }
    public PrivacyType CanViewExercises { get; set; }
    public PrivacyType CanViewJournal { get; set; }
    public PrivacyType CanViewProgress { get; set; }
    public string Partition { get; set; }
}