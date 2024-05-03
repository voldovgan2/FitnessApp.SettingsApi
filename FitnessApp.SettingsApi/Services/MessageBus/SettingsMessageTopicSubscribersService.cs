﻿using System;
using System.Threading.Tasks;
using FitnessApp.Common.Serializer.JsonSerializer;
using FitnessApp.Common.ServiceBus;
using FitnessApp.Common.ServiceBus.Nats.Services;
using FitnessApp.SettingsApi.Models.Input;
using FitnessApp.SettingsApi.Models.Output;

namespace FitnessApp.SettingsApi.Services.MessageBus
{
    public class SettingsMessageTopicSubscribersService(
        IServiceBus serviceBus,
        Func<CreateSettingsGenericModel, Task<SettingsGenericModel>> createItemMethod,
        IJsonSerializer serializer
        ) : GenericServiceNewUserRegisteredSubscriberService<SettingsGenericModel, CreateSettingsGenericModel>(serviceBus, createItemMethod, serializer);
}