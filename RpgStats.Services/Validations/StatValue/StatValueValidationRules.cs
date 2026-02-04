using Microsoft.EntityFrameworkCore;
using RpgStats.Dto;
using RpgStats.Repo;

namespace RpgStats.Services.Validations.StatValue;

public static class StatValueValidationRules
{
    public static async Task<bool> BeMoreOrEqualThanPreviousLevel(RpgStatsContext rpgStatsContext, StatValueForCreationDto statValueForCreationDto,
        int newValue, CancellationToken cancellationToken)
    {
        var previousLevel = statValueForCreationDto.Level - 1;
        if (previousLevel < 1)
            return true;

        var previousStatValue = await rpgStatsContext.StatValues.FirstOrDefaultAsync(
            statValue => statValue.CharacterId == statValueForCreationDto.CharacterId &&
                         statValue.Level == previousLevel &&
                         statValue.StatId == statValueForCreationDto.StatId, cancellationToken);

        if (previousStatValue == null)
            return true; // No previous stat value exists, so we consider the rule satisfied.

        return newValue >= previousStatValue.Value;
    }
}