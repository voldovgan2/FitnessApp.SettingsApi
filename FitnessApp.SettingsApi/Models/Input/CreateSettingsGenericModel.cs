using FitnessApp.Common.Abstractions.Models.Generic;
using FitnessApp.SettingsApi.Enums;

namespace FitnessApp.SettingsApi.Models.Input
{
    public class CreateSettingsGenericModel : ICreateGenericModel
    {
        public string UserId { get; set; }
        public PrivacyType CanFollow { get; set; }
        public PrivacyType CanViewFollowers { get; set; }
        public PrivacyType CanViewFollowings { get; set; }
        public PrivacyType CanViewFood { get; set; }
        public PrivacyType CanViewExercises { get; set; }
        public PrivacyType CanViewJournal { get; set; }
        public PrivacyType CanViewProgress { get; set; }

        public static CreateSettingsGenericModel Default(string userId)
        {
            return new CreateSettingsGenericModel
            {
                UserId = userId,
                CanFollow = PrivacyType.All,
                CanViewFollowers = PrivacyType.All,
                CanViewFollowings = PrivacyType.All,
                CanViewFood = PrivacyType.All,
                CanViewExercises = PrivacyType.All,
                CanViewJournal = PrivacyType.All,
                CanViewProgress = PrivacyType.All
            };
        }
    }
}
