namespace RpgStats.Dto.Tests;

public class PlatformGameDtoTests
{
    [Fact]
    public void PlatformGameDto_Should_SetAndGet_Id()
    {
        var dto = new PlatformGameDto { Id = 1 };
        Assert.Equal(1, dto.Id);
    }

    [Fact]
    public void PlatformGameDto_Should_SetAndGet_PlatformId()
    {
        var dto = new PlatformGameDto { PlatformId = 2 };
        Assert.Equal(2, dto.PlatformId);
    }

    [Fact]
    public void PlatformGameDto_Should_SetAndGet_GameId()
    {
        var dto = new PlatformGameDto { GameId = 3 };
        Assert.Equal(3, dto.GameId);
    }

    [Fact]
    public void PlatformGameDto_Should_Handle_DefaultValues()
    {
        var dto = new PlatformGameDto();
        Assert.Equal(0, dto.Id);
        Assert.Equal(0, dto.PlatformId);
        Assert.Equal(0, dto.GameId);
    }
}