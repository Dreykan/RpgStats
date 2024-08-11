namespace RpgStats.Dto.Tests;

public class StatDtoTests
{
    [Fact]
    public void StatDto_CanBeInitialized()
    {
        var statDto = new StatDto();
        Assert.NotNull(statDto);
    }

    [Fact]
    public void StatDto_PropertiesCanBeSetAndGet()
    {
        var statDto = new StatDto
        {
            Id = 1,
            Name = "Strength",
            ShortName = "STR",
            StatValues = new List<StatValueDto>(),
            GameStats = new List<GameStatDto>()
        };

        Assert.Equal(1, statDto.Id);
        Assert.Equal("Strength", statDto.Name);
        Assert.Equal("STR", statDto.ShortName);
        Assert.NotNull(statDto.StatValues);
        Assert.NotNull(statDto.GameStats);
    }

    [Fact]
    public void StatDto_StatValuesCanBeNull()
    {
        var statDto = new StatDto
        {
            Id = 1,
            Name = "Strength",
            ShortName = "STR",
            StatValues = null,
            GameStats = new List<GameStatDto>()
        };

        Assert.Null(statDto.StatValues);
    }

    [Fact]
    public void StatDto_GameStatsCanBeNull()
    {
        var statDto = new StatDto
        {
            Id = 1,
            Name = "Strength",
            ShortName = "STR",
            StatValues = new List<StatValueDto>(),
            GameStats = null
        };

        Assert.Null(statDto.GameStats);
    }
}