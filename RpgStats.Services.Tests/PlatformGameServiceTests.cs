using RpgStats.Domain.Exceptions;
using Xunit.Priority;

namespace RpgStats.Services.Tests;

[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public class PlatformGameServiceTests : IClassFixture<DatabaseFixture>
{
    private readonly PlatformGameService _service;

    public PlatformGameServiceTests(DatabaseFixture fixture)
    {
        _service = new PlatformGameService(fixture.Context);
    }

    [Fact]
    [Priority(1)]
    public async Task GetAllPlatformGamesAsync_ReturnsAllPlatformGames()
    {
        var platformGames = await _service.GetAllPlatformGamesAsync();

        Assert.NotNull(platformGames);
        Assert.Equal(12, platformGames.Data?.Count);
    }

    [Fact]
    [Priority(2)]
    public async Task GetAllPlatformGamesByPlatformIdAsync_ReturnsPlatformGamesByPlatformId()
    {
        var platformGames = await _service.GetAllPlatformGamesByPlatformIdAsync(1);

        Assert.NotNull(platformGames);
        Assert.Equal(4, platformGames.Data?.Count);
    }

    [Fact]
    [Priority(3)]
    public async Task GetAllPlatformGamesByPlatformIdAsync_ReturnsEmptyList()
    {
        var platformGames = await _service.GetAllPlatformGamesByPlatformIdAsync(100);

        Assert.NotNull(platformGames);
        Assert.Equal(0, platformGames.Data?.Count);
    }

    [Fact]
    [Priority(4)]
    public async Task GetAllPlatformGamesByGameIdAsync_ReturnsPlatformGamesByGameId()
    {
        var platformGames = await _service.GetAllPlatformGamesByGameIdAsync(1);

        Assert.NotNull(platformGames);
        Assert.Equal(3, platformGames.Data?.Count);
    }

    [Fact]
    [Priority(5)]
    public async Task GetAllPlatformGamesByGameIdAsync_ReturnsEmptyList()
    {
        var platformGames = await _service.GetAllPlatformGamesByGameIdAsync(100);

        Assert.NotNull(platformGames);
        Assert.Equal(0, platformGames.Data?.Count);
    }

    [Fact]
    [Priority(6)]
    public async Task GetPlatformGameByIdAsync_ReturnsPlatformGameById()
    {
        var platformGame = await _service.GetPlatformGameByIdAsync(1);

        Assert.NotNull(platformGame);
        Assert.Equal(1, platformGame.Data?.Id);
    }

    [Fact]
    [Priority(7)]
    public async Task GetPlatformGameByIdAsync_ThrowsPlatformGameNotFoundException_WhenPlatformGameIdNotFound()
    {
        await Assert.ThrowsAsync<PlatformGameNotFoundException>(() => _service.GetPlatformGameByIdAsync(100));
    }

    [Fact]
    [Priority(8)]
    public async Task CreatePlatformGameAsync_ReturnsPlatformGameDto()
    {
        var platformGame = await _service.CreatePlatformGameAsync(1, 1);

        Assert.NotNull(platformGame);
        Assert.Equal(1, platformGame.Data?.PlatformId);
        Assert.Equal(1, platformGame.Data?.GameId);

        if (platformGame.Data != null) await _service.DeletePlatformGameAsync(platformGame.Data.Id);
    }

    [Fact]
    [Priority(9)]
    public async Task CreatePlatformGameAsync_ThrowsPlatformNotFoundException_WhenPlatformIdNotFound()
    {
        await Assert.ThrowsAsync<PlatformNotFoundException>(() => _service.CreatePlatformGameAsync(100, 1));
    }

    [Fact]
    [Priority(10)]
    public async Task CreatePlatformGameAsync_ThrowsGameNotFoundException_WhenGameIdNotFound()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(() => _service.CreatePlatformGameAsync(1, 100));
    }

    [Fact]
    [Priority(11)]
    public async Task UpdatePlatformGameAsync_UpdatesPlatformGame()
    {
        var platformGame = await _service.CreatePlatformGameAsync(1, 1);

        if (platformGame.Data != null) platformGame = await _service.UpdatePlatformGameAsync(platformGame.Data.Id, 2, 2);

        Assert.NotNull(platformGame);
        Assert.Equal(2, platformGame.Data?.PlatformId);
        Assert.Equal(2, platformGame.Data?.GameId);

        if (platformGame.Data != null) await _service.DeletePlatformGameAsync(platformGame.Data.Id);
    }

    [Fact]
    [Priority(12)]
    public async Task UpdatePlatformGameAsync_ThrowsPlatformGameNotFoundException_WhenPlatformGameIdNotFound()
    {
        await Assert.ThrowsAsync<PlatformGameNotFoundException>(() => _service.UpdatePlatformGameAsync(100, 1, 1));
    }

    [Fact]
    [Priority(13)]
    public async Task UpdatePlatformGameAsync_ThrowsPlatformNotFoundException_WhenPlatformIdNotFound()
    {
        await Assert.ThrowsAsync<PlatformNotFoundException>(() => _service.UpdatePlatformGameAsync(1, 100, 1));
    }

    [Fact]
    [Priority(14)]
    public async Task UpdatePlatformGameAsync_ThrowsGameNotFoundException_WhenGameIdNotFound()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(() => _service.UpdatePlatformGameAsync(1, 1, 100));
    }

    [Fact]
    [Priority(15)]
    public async Task DeletePlatformGameAsync_DeletesPlatformGame()
    {
        var platformGame = await _service.CreatePlatformGameAsync(1, 1);

        if (platformGame.Data != null) await _service.DeletePlatformGameAsync(platformGame.Data.Id);

        var platformGames = await _service.GetAllPlatformGamesAsync();

        Assert.NotNull(platformGames);
        Assert.Equal(12, platformGames.Data?.Count);
    }

    [Fact]
    [Priority(16)]
    public async Task DeletePlatformGameAsync_DoesNothing_WhenPlatformGameIdIsNotFound()
    {
        await _service.DeletePlatformGameAsync(100);

        var platformGames = await _service.GetAllPlatformGamesAsync();

        Assert.NotNull(platformGames);
        Assert.Equal(12, platformGames.Data?.Count);
    }

    [Fact]
    [Priority(17)]
    public async Task DeletePlatformGameAsync_DoesNothing_WhenPlatformGameIdIsZero()
    {
        await _service.DeletePlatformGameAsync(0);

        var platformGames = await _service.GetAllPlatformGamesAsync();

        Assert.NotNull(platformGames);
        Assert.Equal(12, platformGames.Data?.Count);
    }

    [Fact]
    [Priority(18)]
    public async Task DeletePlatformGameAsync_DoesNothing_WhenPlatformGameIdIsNegative()
    {
        await _service.DeletePlatformGameAsync(-1);

        var platformGames = await _service.GetAllPlatformGamesAsync();

        Assert.NotNull(platformGames);
        Assert.Equal(12, platformGames.Data?.Count);
    }

    [Fact]
    [Priority(19)]
    public async Task DeletePlatformGameByGameIdAsync_DoesNothing_WhenGameIdIsNotFound()
    {
        await _service.DeletePlatformGameByGameIdAsync(100);

        var platformGames = await _service.GetAllPlatformGamesAsync();

        Assert.NotNull(platformGames);
        Assert.Equal(12, platformGames.Data?.Count);
    }

    [Fact]
    [Priority(20)]
    public async Task DeletePlatformGameByGameIdAsync_DeletesPlatformGamesByGameId()
    {
        await _service.DeletePlatformGameByGameIdAsync(1);

        var platformGames = await _service.GetAllPlatformGamesAsync();

        Assert.NotNull(platformGames);
        Assert.Equal(9, platformGames.Data?.Count);
    }

    [Fact]
    [Priority(21)]
    public async Task DeletePlatformGameByPlatformIdAsync_DoesNothing_WhenPlatformIdIsNotFound()
    {
        await _service.DeletePlatformGameByPlatformIdAsync(100);

        var platformGames = await _service.GetAllPlatformGamesAsync();

        Assert.NotNull(platformGames);
        Assert.Equal(9, platformGames.Data?.Count);
    }

    [Fact]
    [Priority(22)]
    public async Task DeletePlatformGameByPlatformIdAsync_DeletesPlatformGamesByPlatformId()
    {
        await _service.DeletePlatformGameByPlatformIdAsync(1);

        var platformGames = await _service.GetAllPlatformGamesAsync();

        Assert.NotNull(platformGames);
        Assert.Equal(6, platformGames.Data?.Count);
    }
}