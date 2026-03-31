using FluentValidation;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;

namespace RpgStats.BizLogic.Validations.StatValue;

public class StatValueForCreationListValidator : AbstractValidator<List<StatValueForCreationDto>>
{
    public StatValueForCreationListValidator(IStatValueService statValueService)
    {
        RuleFor(statValues => statValues)
            .NotEmpty()
            .WithMessage("The list of stat values must not be empty.");
        
        RuleFor(statValues => statValues)
            .Must(statValues => statValues.All(sv => sv.Level > 0))
            .WithMessage("The level must be greater than 0.");
        
        RuleFor(statValues => statValues)
            .Must(statValues => statValues.Select(sv => sv.Level).Distinct().Count() == 1)
            .WithMessage("All stat values in the list must have the same level.");
        
        RuleForEach(statValue => statValue)
            .SetValidator(new StatValueForCreationValidator(statValueService));
    }
}