using System.Threading.Tasks;
using FitnessApp.SettingsApi.Models.Input;
using FitnessApp.SettingsApi.Models.Output;
using FitnessApp.SettingsApi.Services.Settings;

namespace FitnessApp.SettingsApi.IntegrationTests
{
    public class SettingsServiceMock : ISettingsService
    {
        public Task<SettingsGenericModel> GetSettingsByUserId(string userId)
        {
            return Task.FromResult(new SettingsGenericModel
            {
                Id = $"Id: {userId}",
                UserId = userId,
                CanFollow = Enums.PrivacyType.All,
                CanViewExercises = Enums.PrivacyType.Followers,
                CanViewFollowers = Enums.PrivacyType.FollowerssOfFollowers,
                CanViewFollowings = Enums.PrivacyType.JustMe,
                CanViewFood = Enums.PrivacyType.All,
                CanViewJournal = Enums.PrivacyType.Followers,
                CanViewProgress = Enums.PrivacyType.FollowerssOfFollowers
            });
        }

        public Task<SettingsGenericModel> CreateSettings(CreateSettingsGenericModel model)
        {
            return Task.FromResult(new SettingsGenericModel
            {
                Id = $"Id: {model.UserId}",
                UserId = model.UserId,
                CanFollow = model.CanFollow,
                CanViewExercises = model.CanViewExercises,
                CanViewFollowers = model.CanViewFollowers,
                CanViewFollowings = model.CanViewFollowings,
                CanViewFood = model.CanViewFood,
                CanViewJournal = model.CanViewJournal,
                CanViewProgress = model.CanViewProgress
            });
        }

        public Task<string> DeleteSettings(string userId)
        {
            return Task.FromResult(userId);
        }

        public Task<SettingsGenericModel> UpdateSettings(UpdateSettingsGenericModel model)
        {
            return Task.FromResult(new SettingsGenericModel
            {
                Id = $"Id: {model.UserId}",
                UserId = model.UserId,
                CanFollow = model.CanFollow,
                CanViewExercises = model.CanViewExercises,
                CanViewFollowers = model.CanViewFollowers,
                CanViewFollowings = model.CanViewFollowings,
                CanViewFood = model.CanViewFood,
                CanViewJournal = model.CanViewJournal,
                CanViewProgress = model.CanViewProgress
            });
        }
    }
}
