using FitnessApp.SettingsApi.Models.Input;
using FitnessApp.IntegrationEvents;
using FitnessApp.NatsServiceBus;
using FitnessApp.Serializer.JsonSerializer;
using FitnessApp.SettingsApi.Services.Settings;
using FitnessApp.SettingsApi.Models.Output;

namespace FitnessApp.SettingsApi.Services.MessageBus
{
    public class SettingsMessageBusService : MessageBusService
    {
        private readonly ISettingsService<Data.Entities.Settings, SettingsModel, GetUsersSettingsModel, CreateSettingsModel, UpdateSettingsModel> _settingsService;
        
        public SettingsMessageBusService
        (
            IServiceBus serviceBus,
            ISettingsService<Data.Entities.Settings, SettingsModel, GetUsersSettingsModel, CreateSettingsModel, UpdateSettingsModel> settingsService,
            IJsonSerializer serializer
        )
            : base(serviceBus, serializer)
        {
            _settingsService = settingsService;
        }

        protected override void HandleNewUserRegisteredEvent(NewUserRegisteredEvent integrationEvent)
        {
            _settingsService.CreateItemAsync(CreateSettingsModel.Default(integrationEvent.UserId));
        }
    }
}