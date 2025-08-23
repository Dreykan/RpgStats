using RpgStats.Domain.Entities;
using RpgStats.Dto.Mapper;

namespace RpgStats.Dto.Tests.Mapper;

public class PlatformMapperTests
{
    [Fact]
    public void MapToPlatformWithoutFkObjectsDto_ValidPlatform_ReturnsDto()
    {
        var platform = new Platform { Id = 1, Name = "Test Platform" };

        var result = PlatformMapper.MapToPlatformWithoutFkObjectsDto(platform);

        Assert.Equal(platform.Id, result.Id);
        Assert.Equal(platform.Name, result.Name);
    }

    [Fact]
    public void MapToPlatformDetailDto_ValidPlatformAndGames_ReturnsDto()
    {
        var platform = new Platform { Id = 1, Name = "Test Platform" };
        var games = new List<Game?> { new() { Id = 1, Name = "Test Game" }, null };

        var result = PlatformMapper.MapToPlatformWithGamesDto(platform, games);

        Assert.Equal(platform.Id, result.Id);
        Assert.Equal(platform.Name, result.Name);
        if (result.GameDtos == null) return;
        Assert.Single(result.GameDtos);
        Assert.Equal(games[0]?.Id, result.GameDtos.FirstOrDefault()?.Id);
        Assert.Equal(games[0]?.Name, result.GameDtos.FirstOrDefault()?.Name);
    }

    [Fact]
    public void MapToPlatformDetailDto_NullGames_ReturnsDtoWithEmptyGames()
    {
        var platform = new Platform { Id = 1, Name = "Test Platform" };
        var games = new List<Game?>();

        var result = PlatformMapper.MapToPlatformWithGamesDto(platform, games);

        Assert.Equal(platform.Id, result.Id);
        Assert.Equal(platform.Name, result.Name);
        if (result.GameDtos != null) Assert.Empty(result.GameDtos);
    }

    [Fact]
    public void MapToPlatformDetailDto_EmptyGamesList_ReturnsDtoWithEmptyGames()
    {
        var platform = new Platform { Id = 1, Name = "Test Platform" };
        var games = new List<Game?>();

        var result = PlatformMapper.MapToPlatformWithGamesDto(platform, games);

        Assert.Equal(platform.Id, result.Id);
        Assert.Equal(platform.Name, result.Name);
        if (result.GameDtos != null) Assert.Empty(result.GameDtos);
    }
}