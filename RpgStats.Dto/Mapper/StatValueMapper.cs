using RpgStats.Domain.Entities;

namespace RpgStats.Dto.Mapper;

public class StatValueMapper
{
    public StatValueWithStatObjectDto MapToStatValueWithStatObjectDto(StatValue statValue)
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

        // Map Stat-Property
        var statMapper = new StatMapper();

        if (statValue.Stat != null)
            statValueWithStatObjectDto.StatWithoutFkObjectsDto =
                statMapper.MapToStatWithoutFkObjectsDto(statValue.Stat);

        return statValueWithStatObjectDto;
    }

    public StatValueWithCharacterObjectDto MapToStatValueWithCharacterObject(StatValue statValue)
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

        // Map Character-Property
        var characterMapper = new CharacterMapper();

        if (statValue.Character != null)
        {
            statValueWithCharacterObjectDto.CharacterWithoutFkObjectsDto =
                characterMapper.MapToCharacterWithoutFkObjectsDto(statValue.Character);
        }

        return statValueWithCharacterObjectDto;
    }
}