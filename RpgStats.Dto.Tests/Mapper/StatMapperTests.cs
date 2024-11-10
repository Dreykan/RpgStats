using System.Collections;
using RpgStats.Domain.Entities;
using RpgStats.Dto.Mapper;

namespace RpgStats.Dto.Tests.Mapper;

public class StatMapperTests
{
    [Fact]
    public void MapToStatWithoutFkObjectsDto_MapsCorrectly()
    {
        var stat = new Stat { Id = 1, Name = "Strength", ShortName = "STR" };

        var result = StatMapper.MapToStatWithoutFkObjectsDto(stat);

        Assert.Equal(1, result.Id);
        Assert.Equal("Strength", result.Name);
        Assert.Equal("STR", result.ShortName);
    }

    [Fact]
    public void MapToStatDetailDto_MapsCorrectly()
    {
        var stat = new Stat { Id = 1, Name = "Strength", ShortName = "STR", GameStats = new List<GameStat> { new() { Game = new Game { Id = 1, Name = "Game1" } } } };
        var statValues = new List<StatValue> { new() { Id = 1, Value = 10, Level = 1, ContainedBonusPercent = 5, ContainedBonusNum = 0 } };

        var result = StatMapper.MapToStatDetailDto(stat, statValues);

        Assert.Equal(1, result.Id);
        Assert.Equal("Strength", result.Name);
        Assert.Equal("STR", result.ShortName);
        if (result.StatValueWithCharacterObjectDtos != null)
            Assert.Single((IEnumerable)result.StatValueWithCharacterObjectDtos);
        if (result.GameWithoutFkObjectsDtos != null) Assert.Single((IEnumerable)result.GameWithoutFkObjectsDtos);
    }

    [Fact]
    public void MapToStatDetailDto_HandlesEmptyGameStats()
    {
        var stat = new Stat { Id = 1, Name = "Strength", ShortName = "STR", GameStats = new List<GameStat>() };
        var statValues = new List<StatValue> { new() { Id = 1, Value = 10, Level = 1, ContainedBonusPercent = 5, ContainedBonusNum = 0 } };

        var result = StatMapper.MapToStatDetailDto(stat, statValues);

        Assert.Equal(1, result.Id);
        Assert.Equal("Strength", result.Name);
        Assert.Equal("STR", result.ShortName);
        if (result.StatValueWithCharacterObjectDtos != null)
            Assert.Single((IEnumerable)result.StatValueWithCharacterObjectDtos);
        if (result.GameWithoutFkObjectsDtos != null) Assert.Empty(result.GameWithoutFkObjectsDtos);
    }

    [Fact]
    public void MapToStatDetailDto_HandlesEmptyStatValues()
    {
        var stat = new Stat { Id = 1, Name = "Strength", ShortName = "STR", GameStats = new List<GameStat> { new() { Game = new Game { Id = 1, Name = "Game1" } } } };
        var statValues = new List<StatValue>();

        var result = StatMapper.MapToStatDetailDto(stat, statValues);

        Assert.Equal(1, result.Id);
        Assert.Equal("Strength", result.Name);
        Assert.Equal("STR", result.ShortName);
        if (result.StatValueWithCharacterObjectDtos != null) Assert.Empty(result.StatValueWithCharacterObjectDtos);
        if (result.GameWithoutFkObjectsDtos != null) Assert.Single((IEnumerable)result.GameWithoutFkObjectsDtos);
    }
}