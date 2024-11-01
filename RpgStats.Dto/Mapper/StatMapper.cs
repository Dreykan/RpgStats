using RpgStats.Domain.Entities;

namespace RpgStats.Dto.Mapper;

public class StatMapper
{
    public static StatWithoutFkObjectsDto MapToStatWithoutFkObjectsDto(Stat stat)
    {
        // New Object and map simple properties
        var statWithoutFkObjectDto = new StatWithoutFkObjectsDto
        {
            Id = stat.Id,
            Name = stat.Name,
            ShortName = stat.ShortName
        };

        return statWithoutFkObjectDto;
    }

    public static StatDetailDto MapToStatDetailDto(Stat stat, List<StatValue> statValues)
    {
        // New Object and map simple properties
        var statDetailDto = new StatDetailDto
        {
            Id = stat.Id,
            Name = stat.Name,
            ShortName = stat.ShortName
        };

        // Map StatValue-Property
        var statValueMapper = new StatValueMapper();
        var statValueWithCharacterObjectDto = new List<StatValueWithCharacterObjectDto>();
        foreach (var statValue in statValues)
        {
            statValueWithCharacterObjectDto.Add(StatValueMapper.MapToStatValueWithCharacterObject(statValue));
        }

        statDetailDto.StatValueWithCharacterObjectDtos = statValueWithCharacterObjectDto;

        // Map Game-Property
        var gameMapper = new GameMapper();
        var gameWithoutFkObjectsDtos = new List<GameWithoutFkObjectsDto>();
        if (stat.GameStats == null) return statDetailDto;
        foreach (var gameStat in stat.GameStats)
        {
            gameWithoutFkObjectsDtos.Add(GameMapper.MapToGameWithoutFkObjectsDto(gameStat.Game));
        }

        statDetailDto.GameWithoutFkObjectsDtos = gameWithoutFkObjectsDtos;

        return statDetailDto;
    }
}