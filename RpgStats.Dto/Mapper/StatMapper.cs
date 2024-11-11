using RpgStats.Domain.Entities;

namespace RpgStats.Dto.Mapper;

public static class StatMapper
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
        var statValueWithCharacterObjectDto = statValues
            .Select(StatValueMapper.MapToStatValueWithCharacterObject).ToList();

        statDetailDto.StatValueWithCharacterObjectDtos = statValueWithCharacterObjectDto;

        // Map Game-Property
        if (stat.GameStats == null) return statDetailDto;
        var gameWithoutFkObjectsDtos = stat.GameStats
            .Select(gameStat =>
            {
                if (gameStat.Game != null) return GameMapper.MapToGameWithoutFkObjectsDto(gameStat.Game);
                return GameMapper.MapToGameWithoutFkObjectsDto(new Game());
            }).ToList();

        statDetailDto.GameWithoutFkObjectsDtos = gameWithoutFkObjectsDtos;

        return statDetailDto;
    }
}