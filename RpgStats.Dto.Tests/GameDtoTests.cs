namespace RpgStats.Dto.Tests;

public class GameDtoTests
{
    [Fact]
    public void GameDto_Should_Have_Default_Values()
    {
        var gameDto = new GameDto();
        Assert.Equal(0, gameDto.Id);
        Assert.Equal(string.Empty, gameDto.Name);
        Assert.Null(gameDto.Picture);
    }

    [Fact]
    public void GameDto_Should_Set_And_Get_Properties()
    {
        var platformGames = new List<PlatformGameDto> { new() };
        var gameStats = new List<GameStatDto> { new() };
        var characters = new List<CharacterDto> { new() };

        var gameDto = new GameDto
        {
            Id = 1,
            Name = "Test Game",
            Picture = new byte[] { 1, 2, 3 },
        };

        Assert.Equal(1, gameDto.Id);
        Assert.Equal("Test Game", gameDto.Name);
        Assert.Equal(new byte[] { 1, 2, 3 }, gameDto.Picture);
    }

    [Fact]
    public void GameDto_Should_Handle_Null_Values()
    {
        var gameDto = new GameDto
        {
            Id = 1,
            Picture = null,
        };

        Assert.Equal(1, gameDto.Id);
        Assert.Equal(string.Empty, gameDto.Name);
        Assert.Null(gameDto.Picture);
    }
}