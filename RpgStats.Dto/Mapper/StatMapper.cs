using RpgStats.Domain.Entities;

namespace RpgStats.Dto.Mapper;

public class StatMapper
{
    public StatWithoutFkObjectsDto MapToStatWithoutFkObjectsDto(Stat stat)
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

    public StatDetailDto MapToStatDetailDto(Stat stat, List<StatValue> statValues)
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
            statValueWithCharacterObjectDto.Add(statValueMapper.MapToStatValueWithCharacterObject(statValue));
        }

        statDetailDto.StatValueWithCharacterObjectDtos = statValueWithCharacterObjectDto;

        return statDetailDto;
    }
}