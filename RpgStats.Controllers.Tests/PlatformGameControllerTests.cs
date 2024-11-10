using Microsoft.AspNetCore.Mvc;
using Moq;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;

namespace RpgStats.Controllers.Tests;

public class PlatformGameControllerTests
{
    private readonly Mock<IPlatformGameService> _mockService;
    private readonly PlatformGameController _controller;
    private readonly List<PlatformGameDto> _platformGames;

    public PlatformGameControllerTests()
    {
        _mockService = new Mock<IPlatformGameService>();
        _controller = new PlatformGameController(_mockService.Object);
        
        _platformGames = new List<PlatformGameDto>
        {
            new() { Id = 1, PlatformId = 1, GameId = 1},
            new() { Id = 2, PlatformId = 2, GameId = 2},
            new() { Id = 3, PlatformId = 1, GameId = 3},
            new() { Id = 4, PlatformId = 3, GameId = 1}
        };
    }
    
    [Fact]
    public async Task GetAllPlatformGames_ReturnsAllPlatformGames()
    {
        _mockService.Setup(x => x.GetAllPlatformGamesAsync())
            .ReturnsAsync(_platformGames);

        var result = await _controller.GetAllPlatformGames();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<PlatformGameDto>>(okResult.Value);
        Assert.Equal(_platformGames.Count, returnValue.Count);
    }
    
    [Fact]
    public async Task GetAllPlatformGamesByPlatform_ReturnsPlatformGamesByPlatform()
    {
        const int platformId = 1;
        _mockService.Setup(x => x.GetAllPlatformGamesByPlatformIdAsync(platformId))
            .ReturnsAsync(_platformGames.Where(x => x.PlatformId == platformId).ToList());

        var result = await _controller.GetAllPlatformGamesByPlatform(platformId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<PlatformGameDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }
    
    [Fact]
    public async Task GetAllPlatformGamesByGame_ReturnsPlatformGamesByGame()
    {
        const int gameId = 1;
        _mockService.Setup(x => x.GetAllPlatformGamesByGameIdAsync(gameId))
            .ReturnsAsync(_platformGames.Where(x => x.GameId == gameId).ToList());

        var result = await _controller.GetAllPlatformGamesByGame(gameId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<PlatformGameDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }
    
    [Fact]
    public async Task GetPlatformGameById_ReturnsPlatformGameById()
    {
        const int platformGameId = 1;
        _mockService.Setup(x => x.GetPlatformGameByIdAsync(platformGameId))
            .ReturnsAsync(_platformGames.FirstOrDefault(x => x.Id == platformGameId));

        var result = await _controller.GetPlatformGameById(platformGameId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<PlatformGameDto>(okResult.Value);
        Assert.Equal(platformGameId, returnValue.Id);
    }
    
    [Fact]
    public async Task CreatePlatformGame_ReturnsCreatedPlatformGame()
    {
        const int platformId = 1;
        const int gameId = 1;
        var response = new PlatformGameDto { Id = 5, PlatformId = platformId, GameId = gameId };
        _mockService.Setup(x => x.CreatePlatformGameAsync(platformId, gameId))
            .ReturnsAsync(response);

        var result = await _controller.CreatePlatformGame(platformId, gameId);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(PlatformGameController.GetPlatformGameById), createdResult.ActionName);
        Assert.Equal(response.Id, createdResult.RouteValues?["platformGameId"]);
        Assert.Equal(response.GameId, createdResult.RouteValues?["gameId"]);
        Assert.Equal(response.PlatformId, createdResult.RouteValues?["platformId"]);
        Assert.Equal(response, createdResult.Value);
    }
    
    [Fact]
    public async Task CreatePlatformGame_ReturnsBadRequest()
    {
        const int platformId = 1;
        const int gameId = 1;
        _mockService.Setup(x => x.CreatePlatformGameAsync(platformId, gameId))
            .ReturnsAsync((PlatformGameDto?)null);

        var result = await _controller.CreatePlatformGame(platformId, gameId);

        Assert.IsType<BadRequestResult>(result);
    }
    
    [Fact]
    public async Task UpdatePlatformGame_ReturnsUpdatedPlatformGame()
    {
        const int platformGameId = 1;
        const int platformId = 1;
        const int gameId = 1;
        var response = new PlatformGameDto { Id = platformGameId, PlatformId = platformId, GameId = gameId };
        _mockService.Setup(x => x.UpdatePlatformGameAsync(platformGameId, platformId, gameId))
            .ReturnsAsync(response);

        var result = await _controller.UpdatePlatformGame(platformGameId, platformId, gameId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<PlatformGameDto>(okResult.Value);
        Assert.Equal(platformGameId, returnValue.Id);
    }
    
    [Fact]
    public async Task UpdatePlatformGame_ReturnsBadRequest()
    {
        const int platformGameId = 1;
        const int platformId = 1;
        const int gameId = 1;
        _mockService.Setup(x => x.UpdatePlatformGameAsync(platformGameId, platformId, gameId))
            .ReturnsAsync((PlatformGameDto?)null);

        var result = await _controller.UpdatePlatformGame(platformGameId, platformId, gameId);

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeletePlatformGame_ReturnOk()
    {
        _mockService.Setup(x => x.DeletePlatformGameAsync(It.IsAny<long>()))
            .ReturnsAsync(Task.CompletedTask);
        
        var result = await _controller.DeletePlatformGame(1);
        
        Assert.IsType<OkResult>(result);
    }
    
    [Fact]
    public async Task DeletePlatformGame_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.DeletePlatformGameAsync(It.IsAny<long>()))
            .ReturnsAsync(Task.FromException(new Exception()));
        
        var result = await _controller.DeletePlatformGame(1);
        
        Assert.IsType<BadRequestResult>(result);
    }
    
    [Fact]
    public async Task DeletePlatformGameByGame_ReturnsOk()
    {
        _mockService.Setup(x => x.DeletePlatformGameByGameIdAsync(It.IsAny<long>()))
            .ReturnsAsync(Task.CompletedTask);
        
        var result = await _controller.DeletePlatformGameByGame(1);
        
        Assert.IsType<OkResult>(result);
    }
    
    [Fact]
    public async Task DeletePlatformGameByGame_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.DeletePlatformGameByGameIdAsync(It.IsAny<long>()))
            .ReturnsAsync(Task.FromException(new Exception()));
        
        var result = await _controller.DeletePlatformGameByGame(1);
        
        Assert.IsType<BadRequestResult>(result);
    }
    
    [Fact]
    public async Task DeletePlatformGameByPlatform_ReturnsOk()
    {
        _mockService.Setup(x => x.DeletePlatformGameByPlatformIdAsync(It.IsAny<long>()))
            .ReturnsAsync(Task.CompletedTask);
        
        var result = await _controller.DeletePlatformGameByPlatform(1);
        
        Assert.IsType<OkResult>(result);
    }
    
    [Fact]
    public async Task DeletePlatformGameByPlatform_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.DeletePlatformGameByPlatformIdAsync(It.IsAny<long>()))
            .ReturnsAsync(Task.FromException(new Exception()));
        
        var result = await _controller.DeletePlatformGameByPlatform(1);
        
        Assert.IsType<BadRequestResult>(result);
    }
}