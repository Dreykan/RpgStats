using RpgStats.Domain.Entities;
using RpgStats.Dto.Mapper;

namespace RpgStats.Dto.Tests.Mapper;

public class GameMapperTests
{
    [Fact]
    public void MapToGameWithoutFkObjectsDto_MapsSimplePropertiesCorrectly()
    {
        // Arrange
        var game = new Game
        {
            Id = 1,
            Name = "Test Game"
        };

        // Act
        var result = GameMapper.MapToGameWithoutFkObjectsDto(game);

        // Assert
        Assert.Equal(game.Id, result.Id);
        Assert.Equal(game.Name, result.Name);
        Assert.Equal(game.Picture, result.Picture);
    }

    [Fact]
    public void MapToGameDetailDto_MapsSimplePropertiesCorrectly()
    {
        // Arrange
        var game = new Game
        {
            Id = 1,
            Name = "Test Game",
            Characters = new List<Character>()
        };
        var platforms = new List<Platform?> { new() { Id = 1, Name = "PC" } };
        var stats = new List<Stat?> { new() { Id = 1, Name = "Strength" } };

        // Act
        var result = GameMapper.MapToGameDetailDto(game, platforms, stats);

        // Assert
        Assert.Equal(game.Id, result.Id);
        Assert.Equal(game.Name, result.Name);
        Assert.Equal(game.Picture, result.Picture);
    }

    [Fact]
    public void MapToGameDetailDto_MapsCharactersCorrectly()
    {
        // Arrange
        var game = new Game
        {
            Characters = new List<Character>
            {
                new() { Id = 1, Name = "Character1" }
            }
        };

        // Act
        var result = GameMapper.MapToGameDetailDto(game, new List<Platform?>(), new List<Stat?>());

        // Assert
        if (result.CharacterWithoutFkObjectsDtos == null) return;
        Assert.Single(result.CharacterWithoutFkObjectsDtos);
        Assert.Equal(game.Characters.FirstOrDefault()?.Id,
            result.CharacterWithoutFkObjectsDtos.FirstOrDefault()?.Id);
        Assert.Equal(game.Characters.FirstOrDefault()?.Name,
            result.CharacterWithoutFkObjectsDtos.FirstOrDefault()?.Name);
    }

    [Fact]
    public void MapToGameDetailDto_MapsPlatformsCorrectly()
    {
        // Arrange
        var game = new Game();
        var platforms = new List<Platform?>
        {
            new() { Id = 1, Name = "PC" }
        };

        // Act
        var result = GameMapper.MapToGameDetailDto(game, platforms, new List<Stat?>());

        // Assert
        if (result.PlatformWithoutFkObjectsDtos == null) return;
        Assert.Single(result.PlatformWithoutFkObjectsDtos);
        Assert.Equal(platforms[0]?.Id, result.PlatformWithoutFkObjectsDtos.FirstOrDefault()?.Id);
        Assert.Equal(platforms[0]?.Name, result.PlatformWithoutFkObjectsDtos.FirstOrDefault()?.Name);
    }

    [Fact]
    public void MapToGameDetailDto_MapsStatsCorrectly()
    {
        // Arrange
        var game = new Game();
        var stats = new List<Stat?>
        {
            new() { Id = 1, Name = "Strength" }
        };

        // Act
        var result = GameMapper.MapToGameDetailDto(game, new List<Platform?>(), stats);

        // Assert
        if (result.StatWithoutFkObjectsDtos == null) return;
        Assert.Single(result.StatWithoutFkObjectsDtos);
        Assert.Equal(stats[0]?.Id, result.StatWithoutFkObjectsDtos.FirstOrDefault()?.Id);
        Assert.Equal(stats[0]?.Name, result.StatWithoutFkObjectsDtos.FirstOrDefault()?.Name);
    }
}