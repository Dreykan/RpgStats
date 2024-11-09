using Microsoft.AspNetCore.Mvc;
using Moq;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;

namespace RpgStats.Controllers.Tests;

public class GameControllerTests
{
    private readonly Mock<IGameService> _mockService;
    private readonly GameController _controller;
    private readonly List<GameDto> _games;
    private readonly List<GameDetailDto> _gameDetailDtos;

    public GameControllerTests()
    {
        _mockService = new Mock<IGameService>();
        _controller = new GameController(_mockService.Object);

        _games = new List<GameDto>
        {
            new() { Id = 1, Name = "TestGame1" },
            new() { Id = 2, Name = "TestGame2" },
            new() { Id = 3, Name = "TestGame3" }
        };

        _gameDetailDtos = new List<GameDetailDto>
        {
            new()
            {
                Id = 1, Name = "TestGame1",
                CharacterWithoutFkObjectsDtos = new List<CharacterWithoutFkObjectsDto>
                {
                    new() { Id = 1, Name = "TestCharacter1" },
                    new() { Id = 2, Name = "TestCharacter2" },
                    new() { Id = 3, Name = "TestCharacter3" }
                },
                PlatformWithoutFkObjectsDtos = new List<PlatformWithoutFkObjectsDto>
                {
                    new() { Id = 1, Name = "TestPlatform1" },
                    new() { Id = 2, Name = "TestPlatform2" },
                    new() { Id = 3, Name = "TestPlatform3" }
                },
                StatWithoutFkObjectsDtos = new List<StatWithoutFkObjectsDto>
                {
                    new() { Id = 1, Name = "TestStat1" },
                    new() { Id = 2, Name = "TestStat2" },
                    new() { Id = 3, Name = "TestStat3" }
                }
            },
            new()
            {
                Id = 2, Name = "TestGame2",
                CharacterWithoutFkObjectsDtos = new List<CharacterWithoutFkObjectsDto>
                {
                    new() { Id = 4, Name = "TestCharacter4" },
                    new() { Id = 5, Name = "TestCharacter5" },
                    new() { Id = 6, Name = "TestCharacter6" }
                },
                PlatformWithoutFkObjectsDtos = new List<PlatformWithoutFkObjectsDto>
                {
                    new() { Id = 4, Name = "TestPlatform4" },
                    new() { Id = 5, Name = "TestPlatform5" },
                    new() { Id = 6, Name = "TestPlatform6" }
                },
                StatWithoutFkObjectsDtos = new List<StatWithoutFkObjectsDto>
                {
                    new() { Id = 4, Name = "TestStat4" },
                    new() { Id = 5, Name = "TestStat5" },
                    new() { Id = 6, Name = "TestStat6" }
                }
            },
            new()
            {
                Id = 3, Name = "TestGame3",
                CharacterWithoutFkObjectsDtos = new List<CharacterWithoutFkObjectsDto>
                {
                    new() { Id = 7, Name = "TestCharacter7" },
                    new() { Id = 8, Name = "TestCharacter8" },
                    new() { Id = 9, Name = "TestCharacter9" }
                },
                PlatformWithoutFkObjectsDtos = new List<PlatformWithoutFkObjectsDto>
                {
                    new() { Id = 7, Name = "TestPlatform7" },
                    new() { Id = 8, Name = "TestPlatform8" },
                    new() { Id = 9, Name = "TestPlatform9" }
                },
                StatWithoutFkObjectsDtos = new List<StatWithoutFkObjectsDto>
                {
                    new() { Id = 7, Name = "TestStat7" },
                    new() { Id = 8, Name = "TestStat8" },
                    new() { Id = 9, Name = "TestStat9" }
                }
            }
        }; 
    }
    
    [Fact]
    public async Task GetAllGames_ReturnsAllGames()
    {
        _mockService.Setup(x => x.GetAllGamesAsync())
            .ReturnsAsync(_games);

        var result = await _controller.GetAllGames();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<GameDto>>(okResult.Value);
        Assert.Equal(_games.Count, returnValue.Count);
    }
    
    [Fact]
    public async Task GetAllGamesByName_ReturnsGamesByName()
    {
        const string name = "TestGame1";
        _mockService.Setup(x => x.GetAllGamesByNameAsync(name))
            .ReturnsAsync(_games.Where(x => x.Name == name).ToList());

        var result = await _controller.GetAllGamesByName(name);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<GameDto>>(okResult.Value);
        Assert.Single(returnValue);
    }
    
    [Fact]
    public async Task GetAllGameDetailDtos_ReturnsAllGameDetailDtos()
    {
        _mockService.Setup(x => x.GetAllGameDetailDtosAsync())
            .ReturnsAsync(_gameDetailDtos);

        var result = await _controller.GetAllGameDetailDtos();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<GameDetailDto>>(okResult.Value);
        Assert.Equal(_gameDetailDtos.Count, returnValue.Count);
    }
    
    [Fact]
    public async Task GetAllGameDetailDtosByName_ReturnsGameDetailDtosByName()
    {
        const string name = "TestGame1";
        _mockService.Setup(x => x.GetAllGameDetailDtosByNameAsync(name))
            .ReturnsAsync(_gameDetailDtos.Where(x => x.Name == name).ToList());

        var result = await _controller.GetAllGameDetailDtosByName(name);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<GameDetailDto>>(okResult.Value);
        Assert.Single(returnValue);
    }
    
    [Fact]
    public async Task GetGameById_ReturnsGameById()
    {
        const int gameId = 1;
        _mockService.Setup(x => x.GetGameByIdAsync(gameId))
            .ReturnsAsync(_games.FirstOrDefault(x => x.Id == gameId));

        var result = await _controller.GetGameById(gameId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<GameDto>(okResult.Value);
        Assert.Equal(gameId, returnValue.Id);
    }
    
    [Fact]
    public async Task GetGameDetailDtoById_ReturnsGameDetailDtoById()
    {
        const int gameId = 1;
        _mockService.Setup(x => x.GetGameDetailDtoByIdAsync(gameId))
            .ReturnsAsync(_gameDetailDtos.FirstOrDefault(x => x.Id == gameId));

        var result = await _controller.GetGameDetailDtoById(gameId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<GameDetailDto>(okResult.Value);
        Assert.Equal(gameId, returnValue.Id);
    }
    
    [Fact]
    public async Task CreateGame_ReturnsCreatedGame()
    {
        var gameForCreationDto = new GameForCreationDto { Name = "TestGame4" };
        var gameDto = new GameDto { Id = 4, Name = gameForCreationDto.Name };
        _mockService.Setup(x => x.CreateGameAsync(gameForCreationDto))
            .ReturnsAsync(gameDto);

        var result = await _controller.CreateGame(gameForCreationDto);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<GameDto>(createdAtActionResult.Value);
        Assert.Equal(gameDto.Id, returnValue.Id);
    }

    [Fact]
    public async Task CreateGame_ReturnsBadRequest()
    {
        var gameForCreationDto = new GameForCreationDto { Name = "TestGame4" };
        _mockService.Setup(x => x.CreateGameAsync(gameForCreationDto))
            .ReturnsAsync((GameDto?)null);

        var result = await _controller.CreateGame(gameForCreationDto);

        Assert.IsType<BadRequestResult>(result);
    }
    
    [Fact]
    public async Task UpdateGame_ReturnsGame()
    {
        var gameForUpdateDto = new GameForUpdateDto { Name = "TestGame4" };
        const int gameId = 1;
        var gameDto = new GameDto { Id = gameId, Name = gameForUpdateDto.Name };
        _mockService.Setup(x => x.UpdateGameAsync(gameId, gameForUpdateDto))
            .ReturnsAsync(gameDto);

        var result = await _controller.UpdateGame(gameForUpdateDto, gameId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<GameDto>(okResult.Value);
        Assert.Equal(gameId, returnValue.Id);
    }
    
    [Fact]
    public async Task UpdateGame_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.UpdateGameAsync(It.IsAny<long>(), It.IsAny<GameForUpdateDto>()))
            .ReturnsAsync((GameDto?)null);

        var result = await _controller.UpdateGame(new GameForUpdateDto(), 1);

        Assert.IsType<BadRequestResult>(result);
    }
    
    [Fact]
    public async Task DeleteGame_ReturnsOk()
    {
        const int gameId = 1;
        _mockService.Setup(x => x.DeleteGameAsync(gameId))
            .ReturnsAsync(Task.CompletedTask);

        var result = await _controller.DeleteGame(gameId);

        Assert.IsType<OkResult>(result);
    }
    
    [Fact]
    public async Task DeleteGame_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.DeleteGameAsync(It.IsAny<long>()))
            .ReturnsAsync(Task.FromException(new Exception()));

        var result = await _controller.DeleteGame(1);

        Assert.IsType<BadRequestResult>(result);
    }
}