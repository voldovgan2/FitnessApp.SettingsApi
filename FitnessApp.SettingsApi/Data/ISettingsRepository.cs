﻿using FitnessApp.Common.Abstractions.Db;
using FitnessApp.SettingsApi.Models.Input;
using FitnessApp.SettingsApi.Models.Output;

namespace FitnessApp.SettingsApi.Data;

public interface ISettingsRepository :
    IGenericRepository<
        SettingsGenericModel,
        CreateSettingsGenericModel,
        UpdateSettingsGenericModel>;