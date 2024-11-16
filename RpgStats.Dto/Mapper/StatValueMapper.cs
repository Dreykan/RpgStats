using RpgStats.Domain.Entities;

namespace RpgStats.Dto.Mapper;

public static class StatValueMapper
{
    public static StatValueWithStatObjectDto MapToStatValueWithStatObjectDto(StatValue statValue)
    {
        // New Object and map simple properties
        var statValueWithStatObjectDto = new StatValueWithStatObjectDto
        {
            Id = statValue.Id,
            Level = statValue.Level,
            Value = statValue.Value,
            ContainedBonusNum = statValue.ContainedBonusNum,
            ContainedBonusPercent = statValue.ContainedBonusPercent
        };

        if (statValue.Stat != null)
            statValueWithStatObjectDto.StatWithoutFkObjectsDto =
                StatMapper.MapToStatWithoutFkObjectsDto(statValue.Stat);

        return statValueWithStatObjectDto;
    }

    public static StatValueWithCharacterObjectDto MapToStatValueWithCharacterObject(StatValue statValue)
    {
        // New Object and map simple properties
        var statValueWithCharacterObjectDto = new StatValueWithCharacterObjectDto
        {
            Id = statValue.Id,
            Level = statValue.Level,
            Value = statValue.Value,
            ContainedBonusNum = statValue.ContainedBonusNum,
            ContainedBonusPercent = statValue.ContainedBonusPercent
        };

        if (statValue.Character != null)
            statValueWithCharacterObjectDto.CharacterWithoutFkObjectsDto =
                CharacterMapper.MapToCharacterWithoutFkObjectsDto(statValue.Character);

        return statValueWithCharacterObjectDto;
    }
}