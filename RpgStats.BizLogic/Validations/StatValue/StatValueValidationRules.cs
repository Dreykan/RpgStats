using RpgStats.Dto;
using RpgStats.Services.Abstractions;

namespace RpgStats.BizLogic.Validations.StatValue;

public static class StatValueValidationRules
{
    public static async Task<bool> BeMoreOrEqualThanPreviousLevel(IStatValueService statValueService, StatValueForCreationDto statValueForCreationDto)
    {
        var previousLevel = statValueForCreationDto.Level - 1;
        if (previousLevel < 1)
            return true;
        
        var previousStatValue = await statValueService.GetStatValueByCharacterLevelAndStatIdAsync(statValueForCreationDto.CharacterId, previousLevel, statValueForCreationDto.StatId);
        
        if (previousStatValue == null)
            return true; // No previous stat value exists, so we consider the rule satisfied.
        
        return statValueForCreationDto.Value >= previousStatValue.Value;
    }
}