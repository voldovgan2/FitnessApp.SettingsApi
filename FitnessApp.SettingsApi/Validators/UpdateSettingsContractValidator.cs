using System;
using System.Linq;
using FitnessApp.SettingsApi.Contracts.Input;
using FitnessApp.SettingsApi.Enums;
using FluentValidation;

namespace FitnessApp.SettingsApi.Validators
{
    public class UpdateSettingsContractValidator : AbstractValidator<UpdateSettingsContract>
    {
        public UpdateSettingsContractValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId can't be empty");

            RuleFor(x => x.CanFollow)
                .IsInEnum()
                .When(x => x.CanFollow.HasValue)
                .WithMessage(x => GetPrivacyTypeValidationError(nameof(x.CanFollow), x.CanFollow.Value));

            RuleFor(x => x.CanViewFollowers)
                .IsInEnum()
                .When(x => x.CanViewFollowers.HasValue)
                .WithMessage(x => GetPrivacyTypeValidationError(nameof(x.CanViewFollowers), x.CanFollow.Value));

            RuleFor(x => x.CanViewFollowings)
                .IsInEnum()
                .When(x => x.CanViewFollowings.HasValue)
                .WithMessage(x => GetPrivacyTypeValidationError(nameof(x.CanViewFollowings), x.CanFollow.Value));

            RuleFor(x => x.CanViewFood)
                .IsInEnum()
                .When(x => x.CanViewFood.HasValue)
                .WithMessage(x => GetPrivacyTypeValidationError(nameof(x.CanViewFood), x.CanFollow.Value));

            RuleFor(x => x.CanViewExercises)
                .IsInEnum()
                .When(x => x.CanViewExercises.HasValue)
                .WithMessage(x => GetPrivacyTypeValidationError(nameof(x.CanViewExercises), x.CanFollow.Value));

            RuleFor(x => x.CanViewJournal)
                .IsInEnum()
                .When(x => x.CanViewJournal.HasValue)
                .WithMessage(x => GetPrivacyTypeValidationError(nameof(x.CanViewJournal), x.CanFollow.Value));

            RuleFor(x => x.CanViewProgress)
                .IsInEnum()
                .When(x => x.CanViewProgress.HasValue)
                .WithMessage(x => GetPrivacyTypeValidationError(nameof(x.CanViewProgress), x.CanFollow.Value));
        }

        private string GetPrivacyTypeValidationError(string fieldname, PrivacyType value)
        {
            var enumValues = (PrivacyType[])Enum.GetValues(typeof(PrivacyType));
            var enumDescriptions = enumValues.Select(v => $"{Enum.GetName(typeof(PrivacyType), v)}: {(int)v}");
            return $"Invalid {fieldname} value: {value}. Value should be: {string.Join(", ", enumDescriptions)}";
        }
    }
}
