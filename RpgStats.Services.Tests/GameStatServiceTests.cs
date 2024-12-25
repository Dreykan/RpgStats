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
        var result = await _service.GetAllGameStatsAsync();

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(12, result.Data?.Count);
    }

    [Fact]
    [Priority(2)]
    public async Task GetAllGameStatsByGameIdAsync_ReturnsGameStatsByGameId()
    {
        var result = await _service.GetAllGameStatsByGameIdAsync(1);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(4, result.Data?.Count);
    }

    [Fact]
    [Priority(3)]
    public async Task GetAllGameStatsByGameIdAsync_Error_WhenGameIdNotFound()
    {
        var result = await _service.GetAllGameStatsByGameIdAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Equal("No GameStats found", result.ErrorMessage);
    }

    [Fact]
    [Priority(4)]
    public async Task GetAllGameStatsByStatIdAsync_ReturnsGameStatsByStatId()
    {
        var result = await _service.GetAllGameStatsByStatIdAsync(1);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(3, result.Data?.Count);
    }

    [Fact]
    [Priority(5)]
    public async Task GetAllGameStatsByStatIdAsync_Error_WhenStatIdNotFound()
    {
        var result = await _service.GetAllGameStatsByStatIdAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Equal("No GameStats found", result.ErrorMessage);
    }

    [Fact]
    [Priority(6)]
    public async Task GetGameStatByIdAsync_ReturnsGameStatById()
    {
        var result = await _service.GetGameStatByIdAsync(1);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(1, result.Data?.Id);
    }

    [Fact]
    [Priority(7)]
    public async Task GetGameStatByIdAsync_Error_WhenIdNotFound()
    {
        var result = await _service.GetGameStatByIdAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("GameStat with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    [Priority(8)]
    public async Task CreateGameStatAsync_ReturnsGameStat()
    {
        var result = await _service.CreateGameStatAsync(1, 1);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(1, result.Data?.GameId);
        Assert.Equal(1, result.Data?.StatId);

        if (result.Data != null) await _service.DeleteGameStatAsync(result.Data.Id);
    }

    [Fact]
    [Priority(9)]
    public async Task CreateGameStatAsync_Error_WhenGameIdNotFound()
    {
        var result = await _service.CreateGameStatAsync(100, 1);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Game with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    [Priority(10)]
    public async Task CreateGameStatAsync_Error_WhenStatIdNotFound()
    {
        var result = await _service.CreateGameStatAsync(1, 100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Stat with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    [Priority(11)]
    public async Task UpdateGameStatAsync_ReturnsUpdatedGameStat()
    {
        var result = await _service.UpdateGameStatAsync(1, 1, 1);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(1, result.Data?.Id);
        Assert.Equal(1, result.Data?.GameId);
        Assert.Equal(1, result.Data?.StatId);
    }

    [Fact]
    [Priority(12)]
    public async Task UpdateGameStatAsync_Error_WhenIdNotFound()
    {
        var result = await _service.UpdateGameStatAsync(100, 1, 1);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("GameStat with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    [Priority(13)]
    public async Task UpdateGameStatAsync_Error_WhenGameIdNotFound()
    {
        var result = await _service.UpdateGameStatAsync(1, 100, 1);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Game with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    [Priority(14)]
    public async Task UpdateGameStatAsync_Error_WhenStatIdNotFound()
    {
        var result = await _service.UpdateGameStatAsync(1, 1, 100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Stat with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    [Priority(15)]
    public async Task DeleteGameStatAsync_DeletesGameStat()
    {
        var result = await _service.DeleteGameStatAsync(1);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(1, result.Data?.Id);
    }

    [Fact]
    [Priority(16)]
    public async Task DeleteGameStatAsync_Error_WhenIdNotFound()
    {
        var result = await _service.DeleteGameStatAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("GameStat with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    [Priority(17)]
    public async Task DeleteGameStatByGameIdAsync_DeletesGameStats()
    {
        var result = await _service.DeleteGameStatsByGameIdAsync(2);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(4, result.Data?.Count);
    }

    [Fact]
    [Priority(18)]
    public async Task DeleteGameStatByGameIdAsync_Error_WhenGameIdNotFound()
    {
        var result = await _service.DeleteGameStatsByGameIdAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("No GameStats found", result.ErrorMessage);
    }

    [Fact]
    [Priority(19)]
    public async Task DeleteGameStatByStatIdAsync_DeletesGameStats()
    {
        var result = await _service.DeleteGameStatsByStatIdAsync(4);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(2, result.Data?.Count);
    }

    [Fact]
    [Priority(20)]
    public async Task DeleteGameStatByStatIdAsync_Error_WhenStatIdNotFound()
    {
        var result = await _service.DeleteGameStatsByStatIdAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("No GameStats found", result.ErrorMessage);
    }
}