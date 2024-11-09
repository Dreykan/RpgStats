using Microsoft.AspNetCore.Mvc;
using Moq;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;

namespace RpgStats.Controllers.Tests;

public class GameStatControllerTests
{
    private readonly Mock<IGameStatService> _mockService;
    private readonly GameStatController _controller;
    private readonly List<GameStatDto> _gameStats;

    public GameStatControllerTests()
    {
        _mockService = new Mock<IGameStatService>();
        _controller = new GameStatController(_mockService.Object);
        
        _gameStats = new List<GameStatDto>
        {
            new GameStatDto { Id = 1, GameId = 1, StatId = 1},
            new GameStatDto { Id = 2, GameId = 2, StatId = 2},
            new GameStatDto { Id = 3, GameId = 1, StatId = 3},
            new GameStatDto { Id = 4, GameId = 3, StatId = 1}
        };
    }
    
    [Fact]
    public async Task GetAllGameStats_ReturnsAllGameStats()
    {
        _mockService.Setup(x => x.GetAllGameStatsAsync())
            .ReturnsAsync(_gameStats);

        var result = await _controller.GetAllGameStats();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<GameStatDto>>(okResult.Value);
        Assert.Equal(_gameStats.Count, returnValue.Count);
    }
    
    [Fact]
    public async Task GetAllGameStatsByGame_ReturnsGameStatsByGame()
    {
        const int gameId = 1;
        _mockService.Setup(x => x.GetAllGameStatsByGameIdAsync(gameId))
            .ReturnsAsync(_gameStats.Where(x => x.GameId == gameId).ToList());

        var result = await _controller.GetAllGameStatsByGame(gameId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<GameStatDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }
    
    [Fact]
    public async Task GetAllGameStatsByStat_ReturnsGameStatsByStat()
    {
        const int statId = 1;
        _mockService.Setup(x => x.GetAllGameStatsByStatIdAsync(statId))
            .ReturnsAsync(_gameStats.Where(x => x.StatId == statId).ToList());

        var result = await _controller.GetAllGameStatsByStat(statId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<GameStatDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }
    
    [Fact]
    public async Task GetGameStatById_ReturnsGameStatById()
    {
        const int gameStatId = 1;
        _mockService.Setup(x => x.GetGameStatByIdAsync(gameStatId))
            .ReturnsAsync(_gameStats.FirstOrDefault(x => x.Id == gameStatId));

        var result = await _controller.GetGameStatById(gameStatId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<GameStatDto>(okResult.Value);
        Assert.Equal(gameStatId, returnValue.Id);
    }
    
    [Fact]
    public async Task CreateGameStat_ReturnsCreatedGameStat()
    {
        const int gameId = 1;
        const int statId = 1;
        var gameStat = new GameStatDto { Id = 5, GameId = gameId, StatId = statId };
        _mockService.Setup(x => x.CreateGameStatAsync(gameId, statId))
            .ReturnsAsync(gameStat);

        var result = await _controller.CreateGameStat(gameId, statId);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<GameStatDto>(createdAtActionResult.Value);
        Assert.Equal(gameStat.Id, returnValue.Id);
        Assert.Equal(gameStat.GameId, returnValue.GameId);
        Assert.Equal(gameStat.StatId, returnValue.StatId);
    }

    [Fact]
    public async Task CreateGameStat_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.CreateGameStatAsync(It.IsAny<long>(), It.IsAny<long>()))
            .ReturnsAsync((GameStatDto?)null);
        
        var result = await _controller.CreateGameStat(1, 1);
        
        Assert.IsType<BadRequestResult>(result);
    }
    
    [Fact]
    public async Task UpdateGameStat_ReturnsUpdatedGameStat()
    {
        const int gameStatId = 1;
        const int gameId = 1;
        const int statId = 1;
        var gameStat = new GameStatDto { Id = gameStatId, GameId = gameId, StatId = statId };
        _mockService.Setup(x => x.UpdateGameStatAsync(gameStatId, gameId, statId))
            .ReturnsAsync(gameStat);

        var result = await _controller.UpdateGameStat(gameStatId, gameId, statId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<GameStatDto>(okResult.Value);
        Assert.Equal(gameStatId, returnValue.Id);
        Assert.Equal(gameId, returnValue.GameId);
        Assert.Equal(statId, returnValue.StatId);
    }
    
    [Fact]
    public async Task UpdateGameStat_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.UpdateGameStatAsync(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<long>()))
            .ReturnsAsync((GameStatDto?)null);
        
        var result = await _controller.UpdateGameStat(1, 1, 1);
        
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteGameStat_ReturnsOk()
    {
        _mockService.Setup(x => x.DeleteGameStatAsync(It.IsAny<long>()))
            .ReturnsAsync(Task.CompletedTask);
        
        var result = await _controller.DeleteGameStat(1);
        
        Assert.IsType<OkResult>(result);
    }
    
    [Fact]
    public async Task DeleteGameStat_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.DeleteGameStatAsync(It.IsAny<long>()))
            .ReturnsAsync(Task.FromException(new Exception()));
        
        var result = await _controller.DeleteGameStat(1);
        
        Assert.IsType<BadRequestResult>(result);
    }
    
    [Fact]
    public async Task DeleteGameStatByGame_ReturnsOk()
    {
        _mockService.Setup(x => x.DeleteGameStatByGameIdAsync(It.IsAny<long>()))
            .ReturnsAsync(Task.CompletedTask);
        
        var result = await _controller.DeleteGameStatByGame(1);
        
        Assert.IsType<OkResult>(result);
    }
    
    [Fact]
    public async Task DeleteGameStatByStat_ReturnsOk()
    {
        _mockService.Setup(x => x.DeleteGameStatByStatIdAsync(It.IsAny<long>()))
            .ReturnsAsync(Task.CompletedTask);
        
        var result = await _controller.DeleteGameStatByStat(1);
        
        Assert.IsType<OkResult>(result);
    }
    
    [Fact]
    public async Task DeleteGameStatByGame_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.DeleteGameStatByGameIdAsync(It.IsAny<long>()))
            .ReturnsAsync(Task.FromException(new Exception()));
        
        var result = await _controller.DeleteGameStatByGame(1);
        
        Assert.IsType<BadRequestResult>(result);
    }
    
    [Fact]
    public async Task DeleteGameStatByStat_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.DeleteGameStatByStatIdAsync(It.IsAny<long>()))
            .ReturnsAsync(Task.FromException(new Exception()));
        
        var result = await _controller.DeleteGameStatByStat(1);
        
        Assert.IsType<BadRequestResult>(result);
    }
}