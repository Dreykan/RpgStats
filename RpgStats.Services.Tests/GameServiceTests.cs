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
        var games = await _service.GetAllGamesAsync();

        Assert.NotNull(games);
        Assert.Equal(6, games.Data?.Count);
    }

    [Fact]
    public async Task GetAllGamesByNameAsync_ReturnsGamesByName()
    {
        var games = await _service.GetAllGamesByNameAsync("GoodGame");

        Assert.NotNull(games);
        Assert.Equal(2, games.Data?.Count);
    }

    [Fact]
    public async Task GetAllGamesByNameAsync_ReturnsGamesByNameCaseInsensitive()
    {
        var games = await _service.GetAllGamesByNameAsync("goodgame");

        Assert.NotNull(games);
        Assert.Equal(2, games.Data?.Count);
    }

    [Fact]
    public async Task GetAllGamesByNameAsync_ReturnsEmptyList()
    {
        var games = await _service.GetAllGamesByNameAsync("NonExistentGame");

        Assert.NotNull(games);
        Assert.Equal(0, games.Data?.Count);
    }

    [Fact]
    public async Task GetGameByIdAsync_ReturnsGameById()
    {
        var game = await _service.GetGameByIdAsync(1);

        Assert.NotNull(game);
        Assert.Equal(1, game.Data?.Id);
    }

    [Fact]
    public async Task GetGameByIdAsync_ThrowsGameNotFoundException()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(() => _service.GetGameByIdAsync(100));
    }

    [Fact]
    public async Task CreateGameAsync_ReturnsCreatedGame()
    {
        var gameForCreationDto = new GameForCreationDto { Name = "NewGame" };

        var game = await _service.CreateGameAsync(gameForCreationDto);

        Assert.NotNull(game);
        Assert.Equal("NewGame", game.Data?.Name);

        if (game.Data != null) await _service.DeleteGameAsync(game.Data.Id);
    }

    [Fact]
    public async Task UpdateGameAsync_ReturnsUpdatedGame()
    {
        var gameForUpdateDto = new GameForUpdateDto { Name = "UpdatedGame" };

        var updatedGame = await _service.UpdateGameAsync(4, gameForUpdateDto);

        Assert.NotNull(updatedGame);
        Assert.Equal("UpdatedGame", updatedGame.Data?.Name);
    }

    [Fact]
    public async Task UpdateGameAsync_ThrowsGameNotFoundException()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(() =>
            _service.UpdateGameAsync(100, new GameForUpdateDto { Name = "UpdatedGame" }));
    }

    [Fact]
    public async Task UpdateGameAsync_ThrowsGameNotFoundException_WhenGameIdIsZero()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(() =>
            _service.UpdateGameAsync(0, new GameForUpdateDto { Name = "UpdatedGame" }));
    }

    [Fact]
    public async Task UpdateGameAsync_ThrowsGameNotFoundException_WhenGameIdIsNegative()
    {
        await Assert.ThrowsAsync<GameNotFoundException>(() =>
            _service.UpdateGameAsync(-1, new GameForUpdateDto { Name = "UpdatedGame" }));
    }

    [Fact]
    public async Task DeleteGameAsync_DeletesGame()
    {
        var game = await _service.CreateGameAsync(new GameForCreationDto { Name = "NewGame" });

        if (game.Data != null) await _service.DeleteGameAsync(game.Data.Id);

        var games = await _service.GetAllGamesAsync();

        Assert.NotNull(games);
        Assert.Equal(6, games.Data?.Count);
    }

    [Fact]
    public async Task DeleteGameAsync_DoesNothing_WhenGameIdIsNotFound()
    {
        await _service.DeleteGameAsync(100);

        var games = await _service.GetAllGamesAsync();

        Assert.NotNull(games);
        Assert.Equal(6, games.Data?.Count);
    }

    [Fact]
    public async Task DeleteGameAsync_DoesNothing_WhenGameIdIsZero()
    {
        await _service.DeleteGameAsync(0);

        var games = await _service.GetAllGamesAsync();

        Assert.NotNull(games);
        Assert.Equal(6, games.Data?.Count);
    }

    [Fact]
    public async Task DeleteGameAsync_DoesNothing_WhenGameIdIsNegative()
    {
        await _service.DeleteGameAsync(-1);

        var games = await _service.GetAllGamesAsync();

        Assert.NotNull(games);
        Assert.Equal(6, games.Data?.Count);
    }

    [Fact]
    public async Task GetAllGameDetailDtosAsync_ReturnsAllGameDetailDtos()
    {
        var gameDetailDtos = await _service.GetAllGameDetailDtosAsync();

        Assert.NotNull(gameDetailDtos);
        Assert.Equal(6, gameDetailDtos.Data?.Count);
    }

    [Fact]
    public async Task GetAllGameDetailDtosAsync_ReturnsAllGameDetailDtosWithCharacters()
    {
        var gameDetailDtos = await _service.GetAllGameDetailDtosAsync();

        Assert.NotNull(gameDetailDtos);
        Assert.Equal(6, gameDetailDtos.Data?.Count);

        if (gameDetailDtos.Data != null)
        {
            var gameDetailDto = gameDetailDtos.Data.First();

            Assert.NotNull(gameDetailDto.CharacterWithoutFkObjectsDtos);
            Assert.Single(gameDetailDto.CharacterWithoutFkObjectsDtos);
        }
    }

    [Fact]
    public async Task GetAllGameDetailDtosAsync_ReturnsAllGameDetailDtosWithStats()
    {
        var gameDetailDtos = await _service.GetAllGameDetailDtosAsync();

        Assert.NotNull(gameDetailDtos);
        Assert.Equal(6, gameDetailDtos.Data?.Count);

        if (gameDetailDtos.Data != null)
        {
            var gameDetailDto = gameDetailDtos.Data.First();

            Assert.NotNull(gameDetailDto.StatWithoutFkObjectsDtos);
            Assert.Equal(4, gameDetailDto.StatWithoutFkObjectsDtos.Count());
        }
    }

    [Fact]
    public async Task GetAllGameDetailDtosAsync_ReturnsAllGameDetailDtosWithPlatforms()
    {
        var gameDetailDtos = await _service.GetAllGameDetailDtosAsync();

        Assert.NotNull(gameDetailDtos);
        Assert.Equal(6, gameDetailDtos.Data?.Count);

        if (gameDetailDtos.Data != null)
        {
            var gameDetailDto = gameDetailDtos.Data.First();

            Assert.NotNull(gameDetailDto.PlatformWithoutFkObjectsDtos);
            Assert.Equal(3, gameDetailDto.PlatformWithoutFkObjectsDtos.Count());
        }
    }

    [Fact]
    public async Task GetAllGameDetailDtosByNameAsync_ReturnsGameDetailDtosByName()
    {
        var gameDetailDtos = await _service.GetAllGameDetailDtosByNameAsync("GoodGame");

        Assert.NotNull(gameDetailDtos);
        Assert.Equal(2, gameDetailDtos.Data?.Count);
    }

    [Fact]
    public async Task GetAllGameDetailDtosByNameAsync_ReturnsGameDetailDtosByNameCaseInsensitive()
    {
        var gameDetailDtos = await _service.GetAllGameDetailDtosByNameAsync("goodgame");

        Assert.NotNull(gameDetailDtos);
        Assert.Equal(2, gameDetailDtos.Data?.Count);
    }

    [Fact]
    public async Task GetAllGameDetailDtosByNameAsync_ReturnsEmptyList()
    {
        var gameDetailDtos = await _service.GetAllGameDetailDtosByNameAsync("NonExistentGame");

        Assert.NotNull(gameDetailDtos);
        Assert.Equal(0, gameDetailDtos.Data?.Count);
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