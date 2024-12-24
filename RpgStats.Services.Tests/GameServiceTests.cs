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
        Assert.True(result.Success);
        Assert.Equal(6, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllGamesByNameAsync_ReturnsGamesByName()
    {
        var result = await _service.GetAllGamesByNameAsync("GoodGame");

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(2, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllGamesByNameAsync_ReturnsGamesByNameCaseInsensitive()
    {
        var result = await _service.GetAllGamesByNameAsync("goodgame");

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(2, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllGamesByNameAsync_Error_WhenNameNotFound()
    {
        var result = await _service.GetAllGamesByNameAsync("NonExistentGame");

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Equal("No games found", result.ErrorMessage);
    }

    [Fact]
    public async Task GetGameByIdAsync_ReturnsGame()
    {
        var result = await _service.GetGameByIdAsync(1);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(1, result.Data.Id);
    }

    [Fact]
    public async Task GetGameByIdAsync_Error_WhenIdNotFound()
    {
        var result = await _service.GetGameByIdAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Equal("Game with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    public async Task CreateGameAsync_ReturnsCreatedGame()
    {
        var result = await _service.CreateGameAsync( new GameForCreationDto { Name = "NewGame" });

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal("NewGame", result.Data?.Name);

        if (result.Data != null) await _service.DeleteGameAsync(result.Data.Id);
    }

    [Fact]
    public async Task UpdateGameAsync_ReturnsUpdatedGame()
    {
        var result = await _service.UpdateGameAsync(4, new GameForUpdateDto { Name = "UpdatedGame" });

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal("UpdatedGame", result.Data?.Name);
    }

    [Fact]
    public async Task UpdateGameAsync_Error_WhenGameNotFound()
    {
        var result = await _service.UpdateGameAsync(100, new GameForUpdateDto { Name = "UpdatedGame" });

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Game with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    public async Task DeleteGameAsync_DeletesGame()
    {
        var result = await _service.DeleteGameAsync(3);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(3, result.Data?.Id);
    }

    [Fact]
    public async Task DeleteGameAsync_Error_WhenGameIdIsNotFound()
    {
        var result = await _service.DeleteGameAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Game with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    public async Task GetAllGameDetailDtosAsync_ReturnsAllGameDetailDtos()
    {
        var result = await _service.GetAllGameDetailDtosAsync();
        var characterWithoutFkObjectsDtos = result.Data?.First().CharacterWithoutFkObjectsDtos;
        var platformWithoutFkObjectsDtos = result.Data?.First().PlatformWithoutFkObjectsDtos;
        var statWithoutFkObjectsDtos = result.Data?.First().StatWithoutFkObjectsDtos;

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(6, result.Data?.Count);
        if (characterWithoutFkObjectsDtos != null) Assert.Single(characterWithoutFkObjectsDtos);
        if (platformWithoutFkObjectsDtos != null) Assert.Equal(3, platformWithoutFkObjectsDtos.Count());
        if (statWithoutFkObjectsDtos != null) Assert.Equal(4, statWithoutFkObjectsDtos.Count());
    }

    [Fact]
    public async Task GetAllGameDetailDtosByNameAsync_ReturnsGameDetailDtosByName()
    {
        var result = await _service.GetAllGameDetailDtosByNameAsync("GoodGame");

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(2, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllGameDetailDtosByNameAsync_ReturnsGameDetailDtosByNameCaseInsensitive()
    {
        var result = await _service.GetAllGameDetailDtosByNameAsync("goodgame");

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(2, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllGameDetailDtosByNameAsync_Error_WhenNameNotFound()
    {
        var result = await _service.GetAllGameDetailDtosByNameAsync("NonExistentGame");

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("No games found", result.ErrorMessage);
    }

    [Fact]
    public async Task GetGameDetailDtoByIdAsync_ReturnsGameDetailDtoById()
    {
        var result = await _service.GetGameDetailDtoByIdAsync(1);
        var characterWithoutFkObjectsDtos = result.Data?.CharacterWithoutFkObjectsDtos;
        var platformWithoutFkObjectsDtos = result.Data?.PlatformWithoutFkObjectsDtos;
        var statWithoutFkObjectsDtos = result.Data?.StatWithoutFkObjectsDtos;

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(1, result.Data?.Id);
        if (characterWithoutFkObjectsDtos != null) Assert.Single(characterWithoutFkObjectsDtos);
        if (platformWithoutFkObjectsDtos != null) Assert.Equal(3, platformWithoutFkObjectsDtos.Count());
        if (statWithoutFkObjectsDtos != null) Assert.Equal(4, statWithoutFkObjectsDtos.Count());
    }

    [Fact]
    public async Task GetGameDetailDtoByIdAsync_Error_WhenGameIdNotFound()
    {
        var result = await _service.GetGameDetailDtoByIdAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Game with ID 100 not found", result.ErrorMessage);
    }
}