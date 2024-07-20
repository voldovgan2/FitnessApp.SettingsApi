using FitnessApp.Common.Abstractions.Models.Generic;
using FitnessApp.SettingsApi.Enums;

namespace FitnessApp.SettingsApi.Models.Output;

public class SettingsGenericModel : IGenericModel
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public PrivacyType CanFollow { get; set; }
    public PrivacyType CanViewFollowers { get; set; }
    public PrivacyType CanViewFollowings { get; set; }
    public PrivacyType CanViewFood { get; set; }
    public PrivacyType CanViewExercises { get; set; }
    public PrivacyType CanViewJournal { get; set; }
    public PrivacyType CanViewProgress { get; set; }
}