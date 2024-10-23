using RpgStats.Domain.Entities;
using RpgStats.Dto.Mapper;

namespace RpgStats.Dto.Tests.Mapper;

public class StatValueMapperTests
{
    [Fact]
    public void MapToStatValueWithStatObjectDto_MapsAllPropertiesCorrectly()
    {
        var statValue = new StatValue
        {
            Id = 1,
            Level = 2,
            Value = 3,
            ContainedBonusNum = 4,
            ContainedBonusPercent = 5,
            Stat = new Stat
            {
                /* initialize properties */
            }
        };

        var mapper = new StatValueMapper();
        var result = mapper.MapToStatValueWithStatObjectDto(statValue);

        Assert.Equal(statValue.Id, result.Id);
        Assert.Equal(statValue.Level, result.Level);
        Assert.Equal(statValue.Value, result.Value);
        Assert.Equal(statValue.ContainedBonusNum, result.ContainedBonusNum);
        Assert.Equal(statValue.ContainedBonusPercent, result.ContainedBonusPercent);
        Assert.NotNull(result.StatWithoutFkObjectsDto);
    }

    [Fact]
    public void MapToStatValueWithStatObjectDto_HandlesNullStat()
    {
        var statValue = new StatValue
        {
            Id = 1,
            Level = 2,
            Value = 3,
            ContainedBonusNum = 4,
            ContainedBonusPercent = 5,
            Stat = null
        };

        var mapper = new StatValueMapper();
        var result = mapper.MapToStatValueWithStatObjectDto(statValue);

        Assert.Equal(statValue.Id, result.Id);
        Assert.Equal(statValue.Level, result.Level);
        Assert.Equal(statValue.Value, result.Value);
        Assert.Equal(statValue.ContainedBonusNum, result.ContainedBonusNum);
        Assert.Equal(statValue.ContainedBonusPercent, result.ContainedBonusPercent);
        Assert.Null(result.StatWithoutFkObjectsDto);
    }

    [Fact]
    public void MapToStatValueWithCharacterObject_MapsAllPropertiesCorrectly()
    {
        var statValue = new StatValue
        {
            Id = 1,
            Level = 2,
            Value = 3,
            ContainedBonusNum = 4,
            ContainedBonusPercent = 5,
            Character = new Character
            {
                /* initialize properties */
            }
        };

        var mapper = new StatValueMapper();
        var result = mapper.MapToStatValueWithCharacterObject(statValue);

        Assert.Equal(statValue.Id, result.Id);
        Assert.Equal(statValue.Level, result.Level);
        Assert.Equal(statValue.Value, result.Value);
        Assert.Equal(statValue.ContainedBonusNum, result.ContainedBonusNum);
        Assert.Equal(statValue.ContainedBonusPercent, result.ContainedBonusPercent);
        Assert.NotNull(result.CharacterWithoutFkObjectsDto);
    }

    [Fact]
    public void MapToStatValueWithCharacterObject_HandlesNullCharacter()
    {
        var statValue = new StatValue
        {
            Id = 1,
            Level = 2,
            Value = 3,
            ContainedBonusNum = 4,
            ContainedBonusPercent = 5,
            Character = null
        };

        var mapper = new StatValueMapper();
        var result = mapper.MapToStatValueWithCharacterObject(statValue);

        Assert.Equal(statValue.Id, result.Id);
        Assert.Equal(statValue.Level, result.Level);
        Assert.Equal(statValue.Value, result.Value);
        Assert.Equal(statValue.ContainedBonusNum, result.ContainedBonusNum);
        Assert.Equal(statValue.ContainedBonusPercent, result.ContainedBonusPercent);
        Assert.Null(result.CharacterWithoutFkObjectsDto);
    }
}