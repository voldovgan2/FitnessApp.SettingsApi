using System;
using System.Threading.Tasks;
using FitnessApp.Common.Serializer.JsonSerializer;
using FitnessApp.Common.ServiceBus;
using FitnessApp.SettingsApi.Models.Input;
using FitnessApp.SettingsApi.Models.Output;

namespace FitnessApp.SettingsApi.Services.MessageBus
{
    public class SettingsMessageTopicSubscribersService : GenericServiceNewUserRegisteredSubscriberService<SettingsGenericModel, CreateSettingsGenericModel>
    {
        public SettingsMessageTopicSubscribersService(
            Func<CreateSettingsGenericModel, Task<SettingsGenericModel>> createItemMethod,
            string subscription,
            IJsonSerializer serializer
        ) : base(createItemMethod, subscription, serializer)
        { }
    }
}