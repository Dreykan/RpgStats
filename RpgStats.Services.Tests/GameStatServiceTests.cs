using RpgStats.Domain.Exceptions;
using Xunit.Priority;

namespace RpgStats.Services.Tests;

[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public class GameStatServiceTests : IClassFixture<DatabaseFixture>
{
    private readonly GameStatService _service;

    public GameStatServiceTests(DatabaseFixture fixture)
    {
        _service = new GameStatService(fixture.Context);
    }

    [Fact]
    [Priority(1)]
    public async Task GetAllGameStatsAsync_ReturnsAllGameStats()
    {
        var gameStats = await _service.GetAllGameStatsAsync();

        Assert.NotNull(gameStats);
        Assert.Equal(12, gameStats.Count);
    }

    [Fact]
    [Priority(2)]
    public async Task GetAllGameStatsByGameIdAsync_ReturnsGameStatsByGameId()
    {
        var gameStats = await _service.GetAllGameStatsByGameIdAsync(1);

        Assert.NotNull(gameStats);
        Assert.Equal(4, gameStats.Count);
    }

    [Fact]
    [Priority(3)]
    public async Task GetAllGameStatsByStatIdAsync_ReturnsGameStatsByStatId()
    {
        var gameStats = await _service.GetAllGameStatsByStatIdAsync(1);

        Assert.NotNull(gameStats);
        Assert.Equal(3, gameStats.Count);
    }

    [Fact]
    [Priority(4)]
    public async Task GetGameStatByIdAsync_ReturnsGameStatById()
    {
        var gameStat = await _service.GetGameStatByIdAsync(1);

        Assert.NotNull(gameStat);
        Assert.Equal(1, gameStat.Id);
    }

    [Fact]
    [Priority(5)]
    public async Task GetGameStatByIdAsync_ThrowsGameStatNotFoundException()
    {
        await Assert.ThrowsAsync<GameStatNotFoundException>(() => _service.GetGameStatByIdAsync(100));
    }

    [Fact]
    [Priority(6)]
    public async Task CreateGameStatAsync_ReturnsGameStat()
    {
        var gameStat = await _service.CreateGameStatAsync(1, 1);

        Assert.NotNull(gameStat);
        Assert.Equal(13, gameStat.Id);
        Assert.Equal(1, gameStat.GameId);
        Assert.Equal(1, gameStat.StatId);

        await _service.DeleteGameStatAsync(gameStat.Id);
    }

    [Fact]
    [Priority(7)]
    public async Task CreateGameStatAsync_ThrowsGameNotFoundException()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(() => _service.CreateGameStatAsync(100, 1));
    }

    [Fact]
    [Priority(8)]
    public async Task CreateGameStatAsync_ThrowsStatNotFoundException()
    {
        await Assert.ThrowsAsync<StatNotFoundException>(() => _service.CreateGameStatAsync(1, 100));
    }

    [Fact]
    [Priority(9)]
    public async Task CreateGameStatAsync_ThrowsGameNotFoundException_WhenGameIdIsZero()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(() => _service.CreateGameStatAsync(0, 1));
    }

    [Fact]
    [Priority(10)]
    public async Task CreateGameStatAsync_ThrowsGameNotFoundException_WhenGameIdIsNegative()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(() => _service.CreateGameStatAsync(-1, 1));
    }

    [Fact]
    [Priority(11)]
    public async Task CreateGameStatAsync_ThrowsStatNotFoundException_WhenStatIdIsZero()
    {
        await Assert.ThrowsAsync<StatNotFoundException>(() => _service.CreateGameStatAsync(1, 0));
    }

    [Fact]
    [Priority(12)]
    public async Task CreateGameStatAsync_ThrowsStatNotFoundException_WhenStatIdIsNegative()
    {
        await Assert.ThrowsAsync<StatNotFoundException>(() => _service.CreateGameStatAsync(1, -1));
    }

    [Fact]
    [Priority(13)]
    public async Task UpdateGameStatAsync_ReturnsUpdatedGameStat()
    {
        var gameStat = await _service.UpdateGameStatAsync(1, 1, 1);

        Assert.NotNull(gameStat);
        Assert.Equal(1, gameStat.Id);
        Assert.Equal(1, gameStat.GameId);
        Assert.Equal(1, gameStat.StatId);
    }

    [Fact]
    [Priority(14)]
    public async Task UpdateGameStatAsync_ThrowsGameStatNotFoundException()
    {
        await Assert.ThrowsAsync<GameStatNotFoundException>(() => _service.UpdateGameStatAsync(100, 1, 1));
    }

    [Fact]
    [Priority(15)]
    public async Task UpdateGameStatAsync_ThrowsGameNotFoundException()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(() => _service.UpdateGameStatAsync(1, 100, 1));
    }

    [Fact]
    [Priority(16)]
    public async Task UpdateGameStatAsync_ThrowsStatNotFoundException()
    {
        await Assert.ThrowsAsync<StatNotFoundException>(() => _service.UpdateGameStatAsync(1, 1, 100));
    }

    [Fact]
    [Priority(17)]
    public async Task UpdateGameStatAsync_ThrowsGameStatNotFoundException_WhenGameStatIdIsZero()
    {
        await Assert.ThrowsAsync<GameStatNotFoundException>(() => _service.UpdateGameStatAsync(0, 1, 1));
    }

    [Fact]
    [Priority(18)]
    public async Task UpdateGameStatAsync_ThrowsGameStatNotFoundException_WhenGameStatIdIsNegative()
    {
        await Assert.ThrowsAsync<GameStatNotFoundException>(() => _service.UpdateGameStatAsync(-1, 1, 1));
    }

    [Fact]
    [Priority(19)]
    public async Task UpdateGameStatAsync_ThrowsGameNotFoundException_WhenGameIdIsZero()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(() => _service.UpdateGameStatAsync(1, 0, 1));
    }

    [Fact]
    [Priority(20)]
    public async Task UpdateGameStatAsync_ThrowsGameNotFoundException_WhenGameIdIsNegative()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(() => _service.UpdateGameStatAsync(1, -1, 1));
    }

    [Fact]
    [Priority(21)]
    public async Task UpdateGameStatAsync_ThrowsStatNotFoundException_WhenStatIdIsZero()
    {
        await Assert.ThrowsAsync<StatNotFoundException>(() => _service.UpdateGameStatAsync(1, 1, 0));
    }

    [Fact]
    [Priority(22)]
    public async Task UpdateGameStatAsync_ThrowsStatNotFoundException_WhenStatIdIsNegative()
    {
        await Assert.ThrowsAsync<StatNotFoundException>(() => _service.UpdateGameStatAsync(1, 1, -1));
    }

    [Fact]
    [Priority(23)]
    public async Task DeleteGameStatAsync_DeletesGameStat()
    {
        var gameStat = await _service.CreateGameStatAsync(1, 1);

        if (gameStat != null) await _service.DeleteGameStatAsync(gameStat.Id);

        var gameStats = await _service.GetAllGameStatsAsync();

        Assert.NotNull(gameStats);
        Assert.Equal(12, gameStats.Count);
    }

    [Fact]
    [Priority(27)]
    public async Task DeleteGameStatByGameIdAsync_DoesNothing_WhenGameIdIsNotFound()
    {
        await _service.DeleteGameStatsByGameIdAsync(100);

        var gameStats = await _service.GetAllGameStatsAsync();

        Assert.NotNull(gameStats);
        Assert.Equal(12, gameStats.Count);
    }

    [Fact]
    [Priority(28)]
    public async Task DeleteGameStatByGameIdAsync_DoesNothing_WhenGameIdIsZero()
    {
        await _service.DeleteGameStatsByGameIdAsync(0);

        var gameStats = await _service.GetAllGameStatsAsync();

        Assert.NotNull(gameStats);
        Assert.Equal(12, gameStats.Count);
    }

    [Fact]
    [Priority(29)]
    public async Task DeleteGameStatByGameIdAsync_DoesNothing_WhenGameIdIsNegative()
    {
        await _service.DeleteGameStatsByGameIdAsync(-1);

        var gameStats = await _service.GetAllGameStatsAsync();

        Assert.NotNull(gameStats);
        Assert.Equal(12, gameStats.Count);
    }

    [Fact]
    [Priority(30)]
    public async Task DeleteGameStatByGameIdAsync_DeletesGameStatByGameId()
    {
        await _service.DeleteGameStatsByGameIdAsync(1);

        var gameStats = await _service.GetAllGameStatsAsync();

        Assert.NotNull(gameStats);
        Assert.Equal(8, gameStats.Count);
    }

    [Fact]
    [Priority(31)]
    public async Task DeleteGameStatByStatIdAsync_DeletesGameStatByStatId_WhenStatIdIsZero()
    {
        await _service.DeleteGameStatsByStatIdAsync(0);

        var gameStats = await _service.GetAllGameStatsAsync();

        Assert.NotNull(gameStats);
        Assert.Equal(8, gameStats.Count);
    }

    [Fact]
    [Priority(32)]
    public async Task DeleteGameStatByStatIdAsync_DeletesGameStatByStatId_WhenStatIdIsNegative()
    {
        await _service.DeleteGameStatsByStatIdAsync(-1);

        var gameStats = await _service.GetAllGameStatsAsync();

        Assert.NotNull(gameStats);
        Assert.Equal(8, gameStats.Count);
    }

    [Fact]
    [Priority(33)]
    public async Task DeleteGameStatByStatIdAsync_DoesNothing_WhenStatIdIsNotFound()
    {
        await _service.DeleteGameStatsByStatIdAsync(100);

        var gameStats = await _service.GetAllGameStatsAsync();

        Assert.NotNull(gameStats);
        Assert.Equal(8, gameStats.Count);
    }

    [Fact]
    [Priority(34)]
    public async Task DeleteGameStatByStatIdAsync_DeletesGameStatByStatId()
    {
        await _service.DeleteGameStatsByStatIdAsync(1);

        var gameStats = await _service.GetAllGameStatsAsync();

        Assert.NotNull(gameStats);
        Assert.Equal(6, gameStats.Count);
    }
}