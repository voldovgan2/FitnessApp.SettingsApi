using FitnessApp.SettingsApi.Enums;
using FitnessApp.Abstractions.Db.Entities.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace FitnessApp.SettingsApi.Data.Entities
{
    public class Settings : IEntity
    {
        [BsonId]
        public string UserId { get; set; }
        public PrivacyType CanFollow { get; set; }
        public PrivacyType CanViewFollowers { get; set; }
        public PrivacyType CanViewFollowings { get; set; }
        public PrivacyType CanViewFood { get; set; }
        public PrivacyType CanViewExercises { get; set; }
        public PrivacyType CanViewJournal { get; set; }
        public PrivacyType CanViewProgress { get; set; }

        public bool Matches(string search)
        {
            return true;
        }
    }
}