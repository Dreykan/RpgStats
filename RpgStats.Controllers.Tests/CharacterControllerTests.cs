using Microsoft.AspNetCore.Mvc;
using Moq;
using RpgStats.Domain.Entities;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;

namespace RpgStats.Controllers.Tests;

public class CharacterControllerTests
{
    private readonly Mock<ICharacterService> _mockService;
    private readonly CharacterController _controller;
    private readonly List<CharacterDto> _characters;
    private readonly List<CharacterDetailDto> _characterDetailDtos;
    
    public CharacterControllerTests()
    {
        _mockService = new Mock<ICharacterService>();
        _controller = new CharacterController(_mockService.Object);
        
        _characters = new List<CharacterDto>
        {
            new CharacterDto { Id = 1, Name = "TestCharacter1", GameId = 1},
            new CharacterDto { Id = 2, Name = "TestCharacter2", GameId = 2},
            new CharacterDto { Id = 3, Name = "TestCharacter3", GameId = 1}
        };

        _characterDetailDtos = new List<CharacterDetailDto>
        {
            new CharacterDetailDto { Id = 1, Name = "TestCharacter1", GameWithoutFkObjectsDto = new GameWithoutFkObjectsDto { Id = 1, Name = "TestGame1" } },
            new CharacterDetailDto { Id = 2, Name = "TestCharacter2", GameWithoutFkObjectsDto = new GameWithoutFkObjectsDto { Id = 2, Name = "TestGame2" } },
            new CharacterDetailDto { Id = 3, Name = "TestCharacter3", GameWithoutFkObjectsDto = new GameWithoutFkObjectsDto { Id = 1, Name = "TestGame1" } }
        };
    }

    [Fact]
    public async Task GetAllCharacters_ReturnsAllCharacters()
    {
        _mockService.Setup(x => x.GetAllCharactersAsync())
            .ReturnsAsync(_characters);

        var result = await _controller.GetAllCharacters();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<CharacterDto>>(okResult.Value);
        Assert.Equal(_characters.Count, returnValue.Count);
    }
    
    [Fact]
    public async Task GetAllCharactersByGame_ReturnsCharactersByGame()
    {
        var gameId = 1;
        _mockService.Setup(x => x.GetAllCharactersByGameIdAsync(gameId))
            .ReturnsAsync(_characters.Where(x => x.GameId == gameId).ToList());

        var result = await _controller.GetAllCharactersByGame(gameId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<CharacterDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }
    
    [Fact]
    public async Task GetAllCharactersByName_ReturnsCharactersByName()
    {
        var name = "TestCharacter1";
        _mockService.Setup(x => x.GetAllCharactersByNameAsync(name))
            .ReturnsAsync(_characters.Where(x => x.Name == name).ToList());

        var result = await _controller.GetAllCharactersByName(name);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<CharacterDto>>(okResult.Value);
        Assert.Single(returnValue);
    }
    
    [Fact]
    public async Task GetAllCharacterDetailDtos_ReturnsAllCharacterDetailDtos()
    {
        _mockService.Setup(x => x.GetAllCharacterDetailDtosAsync())
            .ReturnsAsync(_characterDetailDtos);

        var result = await _controller.GetAllCharacterDetailDtos();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<CharacterDetailDto>>(okResult.Value);
        Assert.Equal(_characterDetailDtos.Count, returnValue.Count);
    }
    
    [Fact]
    public async Task GetAllCharacterDetailDtosByGame_ReturnsCharacterDetailDtosByGame()
    {
        var gameId = 1;
        _mockService.Setup(x => x.GetAllCharacterDetailDtosByGameIdAsync(gameId))
            .ReturnsAsync(_characterDetailDtos.Where(x => x.GameWithoutFkObjectsDto != null && x.GameWithoutFkObjectsDto.Id == gameId).ToList());

        var result = await _controller.GetAllCharacterDetailDtosByGame(gameId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<CharacterDetailDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }
    
    [Fact]
    public async Task GetAllCharacterDetailDtosByName_ReturnsCharacterDetailDtosByName()
    {
        var name = "TestCharacter1";
        _mockService.Setup(x => x.GetAllCharacterDetailDtosByNameAsync(name))
            .ReturnsAsync(_characterDetailDtos.Where(x => x.Name == name).ToList());

        var result = await _controller.GetAllCharacterDetailDtosByName(name);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<CharacterDetailDto>>(okResult.Value);
        Assert.Single(returnValue);
    }
    
    [Fact]
    public async Task GetCharacterById_ReturnsCharacter()
    {
        var characterId = 1;
        _mockService.Setup(x => x.GetCharacterByIdAsync(characterId))
            .ReturnsAsync(_characters[0]);

        var result = await _controller.GetCharacterById(characterId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<CharacterDto>(okResult.Value);
        Assert.Equal(characterId, returnValue.Id);
    }
    
    [Fact]
    public async Task GetCharacterDetailDtoById_ReturnsCharacterDetailDto()
    {
        var characterId = 1;
        _mockService.Setup(x => x.GetCharacterDetailDtoByIdAsync(characterId))
            .ReturnsAsync(_characterDetailDtos[0]);

        var result = await _controller.GetCharacterDetailDtoById(characterId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<CharacterDetailDto>(okResult.Value);
        Assert.Equal(characterId, returnValue.Id);
    }
    
    [Fact]
    public async Task CreateCharacter_ReturnsCharacter()
    {
        var characterForCreationDto = new CharacterForCreationDto { Name = "TestCharacter4" };
        var gameId = 1;
        var characterDto = new CharacterDto { Id = 4, Name = characterForCreationDto.Name, GameId = gameId };
        _mockService.Setup(x => x.CreateCharacterAsync(gameId, characterForCreationDto))
            .ReturnsAsync(characterDto);

        var result = await _controller.CreateCharacter(characterForCreationDto, gameId);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<CharacterDto>(createdAtActionResult.Value);
        Assert.Equal(characterDto.Id, returnValue.Id);
    }
    
    [Fact]
    public async Task CreateCharacter_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.CreateCharacterAsync(It.IsAny<long>(), It.IsAny<CharacterForCreationDto>()))
            .ReturnsAsync((CharacterDto?)null);

        var result = await _controller.CreateCharacter(new CharacterForCreationDto(), 1);

        Assert.IsType<BadRequestResult>(result);
    }
    
    [Fact]
    public async Task UpdateCharacter_ReturnsCharacter()
    {
        var characterForUpdateDto = new CharacterForUpdateDto { Name = "TestCharacter4" };
        var characterId = 1;
        var gameId = 1;
        var characterDto = new CharacterDto { Id = characterId, Name = characterForUpdateDto.Name, GameId = gameId };
        _mockService.Setup(x => x.UpdateCharacterAsync(characterId, gameId, characterForUpdateDto))
            .ReturnsAsync(characterDto);

        var result = await _controller.UpdateCharacter(characterForUpdateDto, characterId, gameId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<CharacterDto>(okResult.Value);
        Assert.Equal(characterId, returnValue.Id);
    }
    
    [Fact]
    public async Task UpdateCharacter_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.UpdateCharacterAsync(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<CharacterForUpdateDto>()))
            .ReturnsAsync((CharacterDto?)null);

        var result = await _controller.UpdateCharacter(new CharacterForUpdateDto(), 1, 1);

        Assert.IsType<BadRequestResult>(result);
    }
    
    [Fact]
    public async Task DeleteCharacter_ReturnsOk()
    {
        var characterId = 1;
        _mockService.Setup(x => x.DeleteCharacterAsync(characterId))
            .ReturnsAsync(Task.CompletedTask);

        var result = await _controller.DeleteCharacter(characterId);

        Assert.IsType<OkResult>(result);
    }
    
    [Fact]
    public async Task DeleteCharacter_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.DeleteCharacterAsync(It.IsAny<long>()))
            .ReturnsAsync(Task.FromException(new Exception()));

        var result = await _controller.DeleteCharacter(1);

        Assert.IsType<BadRequestResult>(result);
    }
}