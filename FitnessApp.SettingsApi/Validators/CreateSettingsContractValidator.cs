using System;
using System.Linq;
using FitnessApp.SettingsApi.Contracts.Input;
using FitnessApp.SettingsApi.Enums;
using FluentValidation;

namespace FitnessApp.SettingsApi.Validators;

public class CreateSettingsContractValidator : AbstractValidator<CreateSettingsContract>
{
    public CreateSettingsContractValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId can't be empty");

        RuleFor(x => x.CanFollow)
            .IsInEnum()
            .WithMessage(x => GetPrivacyTypeValidationError(nameof(x.CanFollow), x.CanFollow));

        RuleFor(x => x.CanViewFollowers)
            .IsInEnum()
            .WithMessage(x => GetPrivacyTypeValidationError(nameof(x.CanViewFollowers), x.CanFollow));

        RuleFor(x => x.CanViewFollowings)
            .IsInEnum()
            .WithMessage(x => GetPrivacyTypeValidationError(nameof(x.CanViewFollowings), x.CanFollow));

        RuleFor(x => x.CanViewFood)
            .IsInEnum()
            .WithMessage(x => GetPrivacyTypeValidationError(nameof(x.CanViewFood), x.CanFollow));

        RuleFor(x => x.CanViewExercises)
            .IsInEnum()
            .WithMessage(x => GetPrivacyTypeValidationError(nameof(x.CanViewExercises), x.CanFollow));

        RuleFor(x => x.CanViewJournal)
            .IsInEnum()
            .WithMessage(x => GetPrivacyTypeValidationError(nameof(x.CanViewJournal), x.CanFollow));

        RuleFor(x => x.CanViewProgress)
            .IsInEnum()
            .WithMessage(x => GetPrivacyTypeValidationError(nameof(x.CanViewProgress), x.CanFollow));
    }

    private string GetPrivacyTypeValidationError(string fieldname, PrivacyType value)
    {
        var enumValues = (PrivacyType[])Enum.GetValues(typeof(PrivacyType));
        var enumDescriptions = enumValues.Select(v => $"{Enum.GetName(typeof(PrivacyType), v)}: {(int)v}");
        return $"Invalid {fieldname} value: {value}. Value should be: {string.Join(", ", enumDescriptions)}";
    }
}
