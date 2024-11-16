namespace RpgStats.Dto.Tests;

public class PlatformDtoTests
{
    [Fact]
    public void PlatformDto_Should_Have_Default_Values()
    {
        var platformDto = new PlatformDto();
        Assert.Equal(0, platformDto.Id);
        Assert.Equal(string.Empty, platformDto.Name);
        Assert.Null(platformDto.PlatformGames);
    }

    [Fact]
    public void PlatformDto_Should_Set_And_Get_Properties()
    {
        var platformGames = new List<PlatformGameDto>();
        var platformDto = new PlatformDto
        {
            Id = 1,
            Name = "Test Platform",
            PlatformGames = platformGames
        };

        Assert.Equal(1, platformDto.Id);
        Assert.Equal("Test Platform", platformDto.Name);
        Assert.Equal(platformGames, platformDto.PlatformGames);
    }

    [Fact]
    public void PlatformDto_Should_Handle_Empty_Name()
    {
        var platformDto = new PlatformDto
        {
            Id = 1,
            Name = string.Empty,
            PlatformGames = new List<PlatformGameDto>()
        };

        Assert.Equal(string.Empty, platformDto.Name);
    }

    [Fact]
    public void PlatformDto_Should_Handle_Null_PlatformGames()
    {
        var platformDto = new PlatformDto
        {
            Id = 1,
            Name = "Test Platform",
            PlatformGames = null
        };

        Assert.Null(platformDto.PlatformGames);
    }
}