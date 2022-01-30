using FitnessApp.SettingsApi.Models.Input;
using FitnessApp.SettingsApi.Services.Settings;
using FitnessApp.SettingsApi.Models.Output;
using FitnessApp.ServiceBus.AzzureServiceBus.TopicSubscribers;
using FitnessApp.Common.Serializer.JsonSerializer;

namespace FitnessApp.SettingsApi.Services.MessageBus
{
    public class SettingsMessageTopicSubscribersService : GenericServiceNewUserRegisteredSubscriberService<Data.Entities.Settings, SettingsModel, GetUsersSettingsModel, CreateSettingsModel, UpdateSettingsModel>
    {
        public SettingsMessageTopicSubscribersService
       (
           ISettingsService<Data.Entities.Settings, SettingsModel, GetUsersSettingsModel, CreateSettingsModel, UpdateSettingsModel> service,
           string subscription,
           IJsonSerializer serializer
       )
           : base(service, subscription, serializer)
        { }
    }
}