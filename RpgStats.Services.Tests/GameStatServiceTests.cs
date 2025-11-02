using RpgStats.Domain.Exceptions;
using RpgStats.Dto;
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
        var result = await _service.GetAllGameStatsAsync();

        Assert.NotNull(result);
        Assert.Equal(12, result.Count);
    }

    [Fact]
    [Priority(2)]
    public async Task GetAllGameStatsByGameIdAsync_ReturnsGameStatsByGameId()
    {
        var result = await _service.GetAllGameStatsByGameIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(4, result.Count);
    }

    [Fact]
    [Priority(3)]
    public async Task GetAllGameStatsByGameIdAsync_ReturnsEmptyList_WhenGameIdNotFound()
    {
        var result = await _service.GetAllGameStatsByGameIdAsync(100);

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    [Priority(4)]
    public async Task GetAllGameStatsByStatIdAsync_ReturnsGameStatsByStatId()
    {
        var result = await _service.GetAllGameStatsByStatIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    [Priority(5)]
    public async Task GetAllGameStatsByStatIdAsync_ReturnsEmptyList_WhenStatIdNotFound()
    {
        var result = await _service.GetAllGameStatsByStatIdAsync(100);

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    [Priority(6)]
    public async Task GetGameStatByIdAsync_ReturnsGameStatById()
    {
        var result = await _service.GetGameStatByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    [Priority(7)]
    public async Task GetGameStatByIdAsync_ReturnsEmptyDto_WhenIdNotFound()
    {
        var result = await _service.GetGameStatByIdAsync(100);

        Assert.Null(result);
    }

    [Fact]
    [Priority(8)]
    public async Task CreateGameStatAsync_ReturnsGameStat()
    {
        var gameStatForCreationDto = new GameStatForCreationDto
        {
            GameId = 1,
            StatId = 1,
            SortIndex = 2
        };

        var result = await _service.CreateGameStatAsync(gameStatForCreationDto);

        Assert.NotNull(result);
        Assert.Equal(1, result.GameId);
        Assert.Equal(1, result.StatId);
        Assert.Equal(2, result.SortIndex);

        await _service.DeleteGameStatAsync(result.Id);
    }

    [Fact]
    [Priority(9)]
    public async Task CreateGameStatAsync_Error_WhenGameIdNotFound()
    {
        var gameStatForCreationDto = new GameStatForCreationDto
        {
            GameId = 100,
            StatId = 1,
            SortIndex = 2
        };

        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _service.CreateGameStatAsync(gameStatForCreationDto));
    }

    [Fact]
    [Priority(10)]
    public async Task CreateGameStatAsync_Error_WhenStatIdNotFound()
    {
        var gameStatForCreationDto = new GameStatForCreationDto
        {
            GameId = 1,
            StatId = 100,
            SortIndex = 2
        };

        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _service.CreateGameStatAsync(gameStatForCreationDto));
    }

    [Fact]
    [Priority(11)]
    public async Task UpdateGameStatAsync_ReturnsUpdatedGameStat()
    {
        var gameStatForUpdateDto = new GameStatForUpdateDto
        {
            GameId = 1,
            StatId = 1,
            SortIndex = 2
        };

        var result = await _service.UpdateGameStatAsync(1, gameStatForUpdateDto);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(1, result.GameId);
        Assert.Equal(1, result.StatId);
        Assert.Equal(2, result.SortIndex);
    }

    [Fact]
    [Priority(12)]
    public async Task UpdateGameStatAsync_Error_WhenIdNotFound()
    {
        var gameStatForUpdateDto = new GameStatForUpdateDto
        {
            GameId = 1,
            StatId = 1,
            SortIndex = 2
        };

        await Assert.ThrowsAsync<GameStatNotFoundException>(async () =>
            await _service.UpdateGameStatAsync(100, gameStatForUpdateDto));
    }

    [Fact]
    [Priority(13)]
    public async Task UpdateGameStatAsync_Error_WhenGameIdNotFound()
    {
        var gameStatForUpdateDto = new GameStatForUpdateDto
        {
            GameId = 100,
            StatId = 1,
            SortIndex = 2
        };

        await Assert.ThrowsAsync<GameNotFoundException>(async () =>
            await _service.UpdateGameStatAsync(1, gameStatForUpdateDto));
    }

    [Fact]
    [Priority(14)]
    public async Task UpdateGameStatAsync_Error_WhenStatIdNotFound()
    {
        var gameStatForUpdateDto = new GameStatForUpdateDto
        {
            GameId = 1,
            StatId = 100,
            SortIndex = 2
        };

        await Assert.ThrowsAsync<StatNotFoundException>(async () =>
            await _service.UpdateGameStatAsync(1, gameStatForUpdateDto));
    }

    [Fact]
    [Priority(15)]
    public async Task DeleteGameStatAsync_DeletesGameStat()
    {
        var result = await _service.DeleteGameStatAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    [Priority(16)]
    public async Task DeleteGameStatAsync_Error_WhenIdNotFound()
    {
        await Assert.ThrowsAsync<GameStatNotFoundException>(async () =>
            await _service.DeleteGameStatAsync(100));
    }

    [Fact]
    [Priority(17)]
    public async Task DeleteGameStatByGameIdAsync_DeletesGameStats()
    {
        var result = await _service.DeleteGameStatsByGameIdAsync(2);

        Assert.NotNull(result);
        Assert.Equal(4, result.Count);
    }

    [Fact]
    [Priority(18)]
    public async Task DeleteGameStatByGameIdAsync_Error_WhenGameIdNotFound()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(async () =>
            await _service.DeleteGameStatsByGameIdAsync(100));
    }

    [Fact]
    [Priority(19)]
    public async Task DeleteGameStatByStatIdAsync_DeletesGameStats()
    {
        var result = await _service.DeleteGameStatsByStatIdAsync(4);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    [Priority(20)]
    public async Task DeleteGameStatByStatIdAsync_Error_WhenStatIdNotFound()
    {
        await Assert.ThrowsAsync<StatNotFoundException>(async () =>
            await _service.DeleteGameStatsByStatIdAsync(100));
    }
}