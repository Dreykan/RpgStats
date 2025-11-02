using RpgStats.Domain.Exceptions;
using RpgStats.Dto;

namespace RpgStats.Services.Tests;

public class GameServiceTests : IClassFixture<DatabaseFixture>
{
    private readonly GameService _service;

    public GameServiceTests(DatabaseFixture fixture)
    {
        _service = new GameService(fixture.Context);
    }

    [Fact]
    public async Task GetAllGamesAsync_ReturnsAllGames()
    {
        var result = await _service.GetAllGamesAsync();

        Assert.NotNull(result);
        Assert.Equal(6, result.Count);
    }

    [Fact]
    public async Task GetAllGamesByNameAsync_ReturnsGamesByName()
    {
        var result = await _service.GetAllGamesByNameAsync("GoodGame");

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetAllGamesByNameAsync_ReturnsGamesByNameCaseInsensitive()
    {
        var result = await _service.GetAllGamesByNameAsync("goodgame");

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetAllGamesByNameAsync_ReturnsEmptyList_WhenNameNotFound()
    {
        var result = await _service.GetAllGamesByNameAsync("NonExistentGame");

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetGameByIdAsync_ReturnsGame()
    {
        var result = await _service.GetGameByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetGameByIdAsync_ReturnsEmptyDto_WhenIdNotFound()
    {
        var result = await _service.GetGameByIdAsync(100);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateGameAsync_ReturnsCreatedGame()
    {
        var result = await _service.CreateGameAsync( new GameForCreationDto { Name = "NewGame" });

        Assert.NotNull(result);
        Assert.Equal("NewGame", result.Name);

        await _service.DeleteGameAsync(result.Id);
    }

    [Fact]
    public async Task UpdateGameAsync_ReturnsUpdatedGame()
    {
        var result = await _service.UpdateGameAsync(4, new GameForUpdateDto { Name = "UpdatedGame" });

        Assert.NotNull(result);
        Assert.Equal("UpdatedGame", result.Name);
    }

    [Fact]
    public async Task UpdateGameAsync_Error_WhenGameNotFound()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(async () =>
            await _service.UpdateGameAsync(100, new GameForUpdateDto { Name = "UpdatedGame" }));
    }

    [Fact]
    public async Task DeleteGameAsync_DeletesGame()
    {
        var result = await _service.DeleteGameAsync(3);

        Assert.NotNull(result);
        Assert.Equal(3, result.Id);
    }

    [Fact]
    public async Task DeleteGameAsync_Error_WhenGameIdIsNotFound()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(async () =>
            await _service.DeleteGameAsync(100));
    }
}