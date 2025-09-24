using RpgStats.Dto;
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
        Assert.Equal(12, result.Count);
    }

    [Fact]
    [Priority(2)]
    public async Task GetAllPlatformGamesByPlatformIdAsync_ReturnsPlatformGamesByPlatformId()
    {
        var result = await _service.GetAllPlatformGamesByPlatformIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(4, result.Count);
    }

    [Fact]
    [Priority(3)]
    public async Task GetAllPlatformGamesByPlatformIdAsync_ReturnsEmptyList_WhenPlatformIdNotFound()
    {
        var result = await _service.GetAllPlatformGamesByPlatformIdAsync(100);

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    [Priority(4)]
    public async Task GetAllPlatformGamesByGameIdAsync_ReturnsPlatformGamesByGameId()
    {
        var result = await _service.GetAllPlatformGamesByGameIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    [Priority(5)]
    public async Task GetAllPlatformGamesByGameIdAsync_ReturnsEmptyList_WhenGameIdNotFound()
    {
        var result = await _service.GetAllPlatformGamesByGameIdAsync(100);

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    [Priority(6)]
    public async Task GetPlatformGameByIdAsync_ReturnsPlatformGameById()
    {
        var result = await _service.GetPlatformGameByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    [Priority(7)]
    public async Task GetPlatformGameByIdAsync_ReturnsEmptyDto_WhenPlatformGameIdNotFound()
    {
        var result = await _service.GetPlatformGameByIdAsync(100);

        Assert.Null(result);
    }

    [Fact]
    [Priority(8)]
    public async Task CreatePlatformGameAsync_ReturnsPlatformGameDto()
    {
        var platformGameForCreation = new PlatformGameForCreationDto
        {
            PlatformId = 6,
            GameId = 6
        };

        var result = await _service.CreatePlatformGameAsync(platformGameForCreation);

        Assert.NotNull(result);
        Assert.Equal(6, result.PlatformId);
        Assert.Equal(6, result.GameId);

        await _service.DeletePlatformGameAsync(result.Id);
    }

    [Fact]
    [Priority(9)]
    public async Task CreatePlatformGameAsync_Error_WhenPlatformIdNotFound()
    {
        var platformGameForCreation = new PlatformGameForCreationDto
        {
            PlatformId = 100,
            GameId = 1
        };

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _service.CreatePlatformGameAsync(platformGameForCreation));
    }

    [Fact]
    [Priority(10)]
    public async Task CreatePlatformGameAsync_Error_WhenGameIdNotFound()
    {
        var platformGameForCreation = new PlatformGameForCreationDto
        {
            PlatformId = 1,
            GameId = 100
        };

        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _service.CreatePlatformGameAsync(platformGameForCreation));
    }

    [Fact]
    [Priority(10)]
    public async Task CreatePlatformGameAsync_Error_WhenPlatformGameExists()
    {
        var platformGameForCreation = new PlatformGameForCreationDto
        {
            PlatformId = 1,
            GameId = 1
        };

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _service.CreatePlatformGameAsync(platformGameForCreation));
    }

    [Fact]
    [Priority(11)]
    public async Task UpdatePlatformGameAsync_UpdatesPlatformGame()
    {
        var platformGameForCreation = new PlatformGameForCreationDto()
        {
            PlatformId = 6,
            GameId = 1
        };

        var result = await _service.CreatePlatformGameAsync(platformGameForCreation);

        var platformGameForUpdate = new PlatformGameForUpdateDto()
        {
            PlatformId = 2,
            GameId = 2
        };

        result = await _service.UpdatePlatformGameAsync(result.Id, platformGameForUpdate);

        Assert.NotNull(result);
        Assert.Equal(2, result.PlatformId);
        Assert.Equal(2, result.GameId);

        await _service.DeletePlatformGameAsync(result.Id);
    }

    [Fact]
    [Priority(12)]
    public async Task UpdatePlatformGameAsync_Error_WhenPlatformGameIdNotFound()
    {
        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _service.UpdatePlatformGameAsync(100, new PlatformGameForUpdateDto { PlatformId = 1, GameId = 1 }));
    }

    [Fact]
    [Priority(13)]
    public async Task UpdatePlatformGameAsync_Error_WhenPlatformIdNotFound()
    {
        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _service.UpdatePlatformGameAsync(1, new PlatformGameForUpdateDto { PlatformId = 100, GameId = 1 }));

    }

    [Fact]
    [Priority(14)]
    public async Task UpdatePlatformGameAsync_Error_WhenGameIdNotFound()
    {
        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _service.UpdatePlatformGameAsync(1, new PlatformGameForUpdateDto { PlatformId = 1, GameId = 100 }));
    }

    [Fact]
    [Priority(15)]
    public async Task DeletePlatformGameAsync_DeletesPlatformGame()
    {
        var platformForCreation = new PlatformGameForCreationDto()
        {
            PlatformId = 5,
            GameId = 6
        };

        var result = await _service.CreatePlatformGameAsync(platformForCreation);

        var id = result.Id;

        result = await _service.DeletePlatformGameAsync(result.Id);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
    }

    [Fact]
    [Priority(16)]
    public async Task DeletePlatformGameAsync_Error_WhenPlatformGameIdNotFound()
    {
        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _service.DeletePlatformGameAsync(100));
    }

    [Fact]
    [Priority(17)]
    public async Task DeletePlatformGameByGameIdAsync_DeletesPlatformGamesByGameId()
    {
        var result = await _service.DeletePlatformGameByGameIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    [Priority(18)]
    public async Task DeletePlatformGameByGameIdAsync_Error_WhenGameIdNotFound()
    {
        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _service.DeletePlatformGameByGameIdAsync(100));
    }

    [Fact]
    [Priority(19)]
    public async Task DeletePlatformGameByPlatformIdAsync_DeletesPlatformGamesByPlatformId()
    {
        var result = await _service.DeletePlatformGameByPlatformIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    [Priority(18)]
    public async Task DeletePlatformGameByPlatformIdAsync_Error_WhenPlatformIdNotFound()
    {
        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _service.DeletePlatformGameByPlatformIdAsync(100));
    }
}