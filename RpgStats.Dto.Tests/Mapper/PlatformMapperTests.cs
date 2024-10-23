using RpgStats.Domain.Entities;
using RpgStats.Dto.Mapper;

namespace RpgStats.Dto.Tests.Mapper;

public class PlatformMapperTests
{
    [Fact]
    public void MapToPlatformWithoutFkObjectsDto_ValidPlatform_ReturnsDto()
    {
        var platform = new Platform { Id = 1, Name = "Test Platform" };
        var mapper = new PlatformMapper();

        var result = mapper.MapToPlatformWithoutFkObjectsDto(platform);

        Assert.Equal(platform.Id, result.Id);
        Assert.Equal(platform.Name, result.Name);
    }

    [Fact]
    public void MapToPlatformDetailDto_ValidPlatformAndGames_ReturnsDto()
    {
        var platform = new Platform { Id = 1, Name = "Test Platform" };
        var games = new List<Game?> { new Game { Id = 1, Name = "Test Game" }, null };
        var mapper = new PlatformMapper();

        var result = mapper.MapToPlatformDetailDto(platform, games);

        Assert.Equal(platform.Id, result.Id);
        Assert.Equal(platform.Name, result.Name);
        Assert.Single(result.GameWithoutFkObjectsDtos);
        Assert.Equal(games[0].Id, result.GameWithoutFkObjectsDtos.FirstOrDefault().Id);
        Assert.Equal(games[0].Name, result.GameWithoutFkObjectsDtos.FirstOrDefault().Name);
    }

    [Fact]
    public void MapToPlatformDetailDto_NullGames_ReturnsDtoWithEmptyGames()
    {
        var platform = new Platform { Id = 1, Name = "Test Platform" };
        var games = new List<Game>();
        var mapper = new PlatformMapper();

        var result = mapper.MapToPlatformDetailDto(platform, games);

        Assert.Equal(platform.Id, result.Id);
        Assert.Equal(platform.Name, result.Name);
        Assert.Empty(result.GameWithoutFkObjectsDtos);
    }

    [Fact]
    public void MapToPlatformDetailDto_EmptyGamesList_ReturnsDtoWithEmptyGames()
    {
        var platform = new Platform { Id = 1, Name = "Test Platform" };
        var games = new List<Game?>();
        var mapper = new PlatformMapper();

        var result = mapper.MapToPlatformDetailDto(platform, games);

        Assert.Equal(platform.Id, result.Id);
        Assert.Equal(platform.Name, result.Name);
        Assert.Empty(result.GameWithoutFkObjectsDtos);
    }
}