using FluentValidation;
using RpgStats.Dto;
using RpgStats.Repo;

namespace RpgStats.Services.Validations.StatValue;

public class StatValueForCreationValidator : AbstractValidator<StatValueForCreationDto>
{
    private readonly RpgStatsContext _rpgStatsContext;

    public StatValueForCreationValidator(RpgStatsContext rpgStatsContext)
    {
        _rpgStatsContext = rpgStatsContext;

        RuleFor(statValue => statValue.Value).NotEmpty().WithMessage("You must enter a value");
        RuleFor(statValue => statValue.Value).GreaterThan(0).WithMessage("You must enter a value");
        RuleFor(statValue => statValue.Value)
            .MustAsync(BeMoreOrEqualThanPreviousLevel).WithMessage("Stat value must be greater than or equal to the previous level's value.");
    }

    private async Task<bool> BeMoreOrEqualThanPreviousLevel(StatValueForCreationDto statValueForCreationDto,
        int newValue, CancellationToken cancellationToken)
    {
        var isMoreOrEqual = await StatValueValidationRules.BeMoreOrEqualThanPreviousLevel(_rpgStatsContext, statValueForCreationDto, newValue, cancellationToken);
        return isMoreOrEqual;
    }
}