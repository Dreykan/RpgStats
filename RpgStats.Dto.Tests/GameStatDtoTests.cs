namespace RpgStats.Dto.Tests;

public class GameStatDtoTests
{
    [Fact]
    public void GameStatDto_Should_Have_Default_Values()
    {
        var dto = new GameStatDto();
        Assert.Equal(0, dto.Id);
        Assert.Equal(0, dto.GameId);
        Assert.Equal(0, dto.StatId);
    }

    [Fact]
    public void GameStatDto_Should_Allow_Setting_Values()
    {
        var dto = new GameStatDto
        {
            Id = 1,
            GameId = 2,
            StatId = 3
        };
        Assert.Equal(1, dto.Id);
        Assert.Equal(2, dto.GameId);
        Assert.Equal(3, dto.StatId);
    }

    [Fact]
    public void GameStatDto_Should_Handle_Max_Long_Values()
    {
        var dto = new GameStatDto
        {
            Id = long.MaxValue,
            GameId = long.MaxValue,
            StatId = long.MaxValue
        };
        Assert.Equal(long.MaxValue, dto.Id);
        Assert.Equal(long.MaxValue, dto.GameId);
        Assert.Equal(long.MaxValue, dto.StatId);
    }

    [Fact]
    public void GameStatDto_Should_Handle_Min_Long_Values()
    {
        var dto = new GameStatDto
        {
            Id = long.MinValue,
            GameId = long.MinValue,
            StatId = long.MinValue
        };
        Assert.Equal(long.MinValue, dto.Id);
        Assert.Equal(long.MinValue, dto.GameId);
        Assert.Equal(long.MinValue, dto.StatId);
    }
}