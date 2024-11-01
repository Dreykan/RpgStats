using RpgStats.Domain.Entities;

namespace RpgStats.Dto.Mapper;

public class StatValueMapper
{
    public static StatValueWithStatObjectDto MapToStatValueWithStatObjectDto(StatValue statValue)
    {
        // New Object and map simple properties
        var statValueWithStatObjectDto = new StatValueWithStatObjectDto
        {
            Id = statValue.Id,
            Level = (int)statValue.Level,
            Value = (int)statValue.Value,
            ContainedBonusNum = (int)statValue.ContainedBonusNum,
            ContainedBonusPercent = (int)statValue.ContainedBonusPercent
        };

        // Map Stat-Property
        var statMapper = new StatMapper();

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
            Level = (int)statValue.Level,
            Value = (int)statValue.Value,
            ContainedBonusNum = (int)statValue.ContainedBonusNum,
            ContainedBonusPercent = (int)statValue.ContainedBonusPercent
        };

        // Map Character-Property
        var characterMapper = new CharacterMapper();

        if (statValue.Character != null)
        {
            statValueWithCharacterObjectDto.CharacterWithoutFkObjectsDto =
                CharacterMapper.MapToCharacterWithoutFkObjectsDto(statValue.Character);
        }

        return statValueWithCharacterObjectDto;
    }
}