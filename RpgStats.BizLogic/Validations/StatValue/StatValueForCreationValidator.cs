using FluentValidation;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;

namespace RpgStats.BizLogic.Validations.StatValue;

public class StatValueForCreationValidator : AbstractValidator<StatValueForCreationDto>
{
    private readonly IStatValueService _statValueService;
    public StatValueForCreationValidator(IStatValueService statValueService)
    {
        _statValueService = statValueService;

        RuleFor(statValue => statValue).Custom((sv, context) =>
        {
            if (sv.Value == 0)
                context.AddFailure(sv.GameStatShortName, $"{sv.GameStatShortName} - Value must not be empty.");
            var isMoreOrEqual = BeMoreOrEqualThanPreviousLevel(sv).Result;
            if (isMoreOrEqual) return;
            context.AddFailure(sv.GameStatShortName,
                $"{sv.GameStatShortName} - The value ({sv.Value}) for level {sv.Level} must be more or equal than the value of level {sv.Level - 1}.");
        });
    }

    private async Task<bool> BeMoreOrEqualThanPreviousLevel(StatValueForCreationDto statValueForCreationDto)
    {
        var isMoreOrEqual = await StatValueValidationRules.BeMoreOrEqualThanPreviousLevel(_statValueService,
            statValueForCreationDto);
        return isMoreOrEqual;
    }
}