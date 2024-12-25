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
        var result = await _service.GetAllPlatformGamesAsync();

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(12, result.Data?.Count);
    }

    [Fact]
    [Priority(2)]
    public async Task GetAllPlatformGamesByPlatformIdAsync_ReturnsPlatformGamesByPlatformId()
    {
        var result = await _service.GetAllPlatformGamesByPlatformIdAsync(1);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(4, result.Data?.Count);
    }

    [Fact]
    [Priority(3)]
    public async Task GetAllPlatformGamesByPlatformIdAsync_Error_WhenPlatformIdNotFound()
    {
        var result = await _service.GetAllPlatformGamesByPlatformIdAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Equal("No PlatformGames found", result.ErrorMessage);
    }

    [Fact]
    [Priority(4)]
    public async Task GetAllPlatformGamesByGameIdAsync_ReturnsPlatformGamesByGameId()
    {
        var result = await _service.GetAllPlatformGamesByGameIdAsync(1);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(3, result.Data?.Count);
    }

    [Fact]
    [Priority(5)]
    public async Task GetAllPlatformGamesByGameIdAsync_Error_WhenGameIdNotFound()
    {
        var result = await _service.GetAllPlatformGamesByGameIdAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Equal("No PlatformGames found", result.ErrorMessage);
    }

    [Fact]
    [Priority(6)]
    public async Task GetPlatformGameByIdAsync_ReturnsPlatformGameById()
    {
        var result = await _service.GetPlatformGameByIdAsync(1);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(1, result.Data?.Id);
    }

    [Fact]
    [Priority(7)]
    public async Task GetPlatformGameByIdAsync_Error_WhenPlatformGameIdNotFound()
    {
        var result = await _service.GetPlatformGameByIdAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("PlatformGame with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    [Priority(8)]
    public async Task CreatePlatformGameAsync_ReturnsPlatformGameDto()
    {
        var result = await _service.CreatePlatformGameAsync(1, 1);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(1, result.Data?.PlatformId);
        Assert.Equal(1, result.Data?.GameId);

        if (result.Data != null) await _service.DeletePlatformGameAsync(result.Data.Id);
    }

    [Fact]
    [Priority(9)]
    public async Task CreatePlatformGameAsync_Error_WhenPlatformIdNotFound()
    {
        var result = await _service.CreatePlatformGameAsync(100, 1);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Platform with ID 100 not found", result.ErrorMessage);

        if (result.Data != null) await _service.DeletePlatformGameAsync(result.Data.Id);
    }

    [Fact]
    [Priority(10)]
    public async Task CreatePlatformGameAsync_Error_WhenGameIdNotFound()
    {
        var result = await _service.CreatePlatformGameAsync(1, 100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Game with ID 100 not found", result.ErrorMessage);

        if (result.Data != null) await _service.DeletePlatformGameAsync(result.Data.Id);
    }

    [Fact]
    [Priority(11)]
    public async Task UpdatePlatformGameAsync_UpdatesPlatformGame()
    {
        var result = await _service.CreatePlatformGameAsync(1, 1);

        if (result.Data != null) result = await _service.UpdatePlatformGameAsync(result.Data.Id, 2, 2);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(2, result.Data?.PlatformId);
        Assert.Equal(2, result.Data?.GameId);

        if (result.Data != null) await _service.DeletePlatformGameAsync(result.Data.Id);
    }

    [Fact]
    [Priority(12)]
    public async Task UpdatePlatformGameAsync_Error_WhenPlatformGameIdNotFound()
    {
        var result = await _service.UpdatePlatformGameAsync(100, 1, 1);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("PlatformGame with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    [Priority(13)]
    public async Task UpdatePlatformGameAsync_Error_WhenPlatformIdNotFound()
    {
        var result = await _service.UpdatePlatformGameAsync(1, 100, 1);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Platform with ID 100 not found", result.ErrorMessage);

    }

    [Fact]
    [Priority(14)]
    public async Task UpdatePlatformGameAsync_Error_WhenGameIdNotFound()
    {
        var result = await _service.UpdatePlatformGameAsync(1, 1, 100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Game with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    [Priority(15)]
    public async Task DeletePlatformGameAsync_DeletesPlatformGame()
    {
        var result = await _service.CreatePlatformGameAsync(1, 1);

        var id = result.Data?.Id;

        if (result.Data != null) result = await _service.DeletePlatformGameAsync(result.Data.Id);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(id, result.Data?.Id);
    }

    [Fact]
    [Priority(16)]
    public async Task DeletePlatformGameAsync_Error_WhenPlatformGameIdNotFound()
    {
        var result = await _service.DeletePlatformGameAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("PlatformGame with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    [Priority(17)]
    public async Task DeletePlatformGameByGameIdAsync_DeletesPlatformGamesByGameId()
    {
        var result = await _service.DeletePlatformGameByGameIdAsync(1);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(3, result.Data?.Count);
    }

    [Fact]
    [Priority(18)]
    public async Task DeletePlatformGameByGameIdAsync_Error_WhenGameIdNotFound()
    {
        var result = await _service.DeletePlatformGameByGameIdAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("No PlatformGames found", result.ErrorMessage);
    }

    [Fact]
    [Priority(19)]
    public async Task DeletePlatformGameByPlatformIdAsync_DeletesPlatformGamesByPlatformId()
    {
        var result = await _service.DeletePlatformGameByPlatformIdAsync(1);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(3, result.Data?.Count);
    }

    [Fact]
    [Priority(18)]
    public async Task DeletePlatformGameByPlatformIdAsync_Error_WhenPlatformIdNotFound()
    {
        var result = await _service.DeletePlatformGameByPlatformIdAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("No PlatformGames found", result.ErrorMessage);
    }
}