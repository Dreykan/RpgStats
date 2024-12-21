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
    public async Task DeleteGameAsync_DoesNothing_WhenGameIdIsZero()
    {
        await _service.DeleteGameAsync(0);

        var result = await _service.GetAllGamesAsync();

        Assert.NotNull(result);
        Assert.Equal(6, result.Data?.Count);
    }

    [Fact]
    public async Task DeleteGameAsync_DoesNothing_WhenGameIdIsNegative()
    {
        await _service.DeleteGameAsync(-1);

        var result = await _service.GetAllGamesAsync();

        Assert.NotNull(result);
        Assert.Equal(6, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllGameDetailDtosAsync_ReturnsAllGameDetailDtos()
    {
        var result = await _service.GetAllGameDetailDtosAsync();

        Assert.NotNull(result);
        Assert.Equal(6, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllGameDetailDtosAsync_ReturnsAllGameDetailDtosWithCharacters()
    {
        var result = await _service.GetAllGameDetailDtosAsync();

        Assert.NotNull(result);
        Assert.Equal(6, result.Data?.Count);

        if (result.Data != null)
        {
            var gameDetailDto = result.Data.First();

            Assert.NotNull(gameDetailDto.CharacterWithoutFkObjectsDtos);
            Assert.Single(gameDetailDto.CharacterWithoutFkObjectsDtos);
        }
    }

    [Fact]
    public async Task GetAllGameDetailDtosAsync_ReturnsAllGameDetailDtosWithStats()
    {
        var result = await _service.GetAllGameDetailDtosAsync();

        Assert.NotNull(result);
        Assert.Equal(6, result.Data?.Count);

        if (result.Data != null)
        {
            var gameDetailDto = result.Data.First();

            Assert.NotNull(gameDetailDto.StatWithoutFkObjectsDtos);
            Assert.Equal(4, gameDetailDto.StatWithoutFkObjectsDtos.Count());
        }
    }

    [Fact]
    public async Task GetAllGameDetailDtosAsync_ReturnsAllGameDetailDtosWithPlatforms()
    {
        var result = await _service.GetAllGameDetailDtosAsync();

        Assert.NotNull(result);
        Assert.Equal(6, result.Data?.Count);

        if (result.Data != null)
        {
            var gameDetailDto = result.Data.First();

            Assert.NotNull(gameDetailDto.PlatformWithoutFkObjectsDtos);
            Assert.Equal(3, gameDetailDto.PlatformWithoutFkObjectsDtos.Count());
        }
    }

    [Fact]
    public async Task GetAllGameDetailDtosByNameAsync_ReturnsGameDetailDtosByName()
    {
        var result = await _service.GetAllGameDetailDtosByNameAsync("GoodGame");

        Assert.NotNull(result);
        Assert.Equal(2, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllGameDetailDtosByNameAsync_ReturnsGameDetailDtosByNameCaseInsensitive()
    {
        var result = await _service.GetAllGameDetailDtosByNameAsync("goodgame");

        Assert.NotNull(result);
        Assert.Equal(2, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllGameDetailDtosByNameAsync_ReturnsEmptyList()
    {
        var result = await _service.GetAllGameDetailDtosByNameAsync("NonExistentGame");

        Assert.NotNull(result);
        Assert.Equal(0, result.Data?.Count);
    }

    [Fact]
    public async Task GetGameDetailDtoByIdAsync_ReturnsGameDetailDtoById()
    {
        var gameDetailDto = await _service.GetGameDetailDtoByIdAsync(1);

        Assert.NotNull(gameDetailDto);
        Assert.Equal(1, gameDetailDto.Data?.Id);
    }

    [Fact]
    public async Task GetGameDetailDtoByIdAsync_ReturnsEmptyEntity_WhenGameIdNotFound()
    {
        var gameDetailDto = await _service.GetGameDetailDtoByIdAsync(100);

        Assert.NotNull(gameDetailDto);
        Assert.Equal(0, gameDetailDto.Data?.Id);
        Assert.Equal(string.Empty, gameDetailDto.Data?.Name);
    }

    [Fact]
    public async Task GetGameDetailDtoByIdAsync_ReturnsEmptyEntity_WhenGameIdIsZero()
    {
        var gameDetailDto = await _service.GetGameDetailDtoByIdAsync(0);

        Assert.NotNull(gameDetailDto);
        Assert.Equal(0, gameDetailDto.Data?.Id);
        Assert.Equal(string.Empty, gameDetailDto.Data?.Name);
    }

    [Fact]
    public async Task GetGameDetailDtoByIdAsync_ReturnsEmptyEntity_WhenGameIdIsNegative()
    {
        var gameDetailDto = await _service.GetGameDetailDtoByIdAsync(-1);

        Assert.NotNull(gameDetailDto);
        Assert.Equal(0, gameDetailDto.Data?.Id);
        Assert.Equal(string.Empty, gameDetailDto.Data?.Name);
    }

    [Fact]
    public async Task GetGameDetailDtoByIdAsync_ReturnsGameDetailDtoWithCharacters()
    {
        var gameDetailDto = await _service.GetGameDetailDtoByIdAsync(1);

        Assert.NotNull(gameDetailDto);
        Assert.NotNull(gameDetailDto.Data?.CharacterWithoutFkObjectsDtos);
        Assert.Equal(1, gameDetailDto.Data?.CharacterWithoutFkObjectsDtos.Count());
    }

    [Fact]
    public async Task GetGameDetailDtoByIdAsync_ReturnsGameDetailDtoWithStats()
    {
        var gameDetailDto = await _service.GetGameDetailDtoByIdAsync(1);

        Assert.NotNull(gameDetailDto);
        Assert.NotNull(gameDetailDto.Data?.StatWithoutFkObjectsDtos);
        Assert.Equal(4, gameDetailDto.Data?.StatWithoutFkObjectsDtos.Count());
    }

    [Fact]
    public async Task GetGameDetailDtoByIdAsync_ReturnsGameDetailDtoWithPlatforms()
    {
        var gameDetailDto = await _service.GetGameDetailDtoByIdAsync(1);

        Assert.NotNull(gameDetailDto);
        Assert.NotNull(gameDetailDto.Data?.PlatformWithoutFkObjectsDtos);
        Assert.Equal(3, gameDetailDto.Data?.PlatformWithoutFkObjectsDtos.Count());
    }
}