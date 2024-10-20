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
    
    public CharacterControllerTests()
    {
        _mockService = new Mock<ICharacterService>();
        _controller = new CharacterController(_mockService.Object);
    }
    
    [Fact]
    public async Task GetCharacter_ReturnsCharacter()
    {
        var characterId = 1;
        var character = new CharacterDto { Id = characterId, Name = "TestCharacter" };
        _mockService.Setup(x => x.GetCharacterByIdAsync(characterId))
            .ReturnsAsync(character);

        var result = await _controller.GetCharacterById(characterId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<CharacterDto>(okResult.Value);
        Assert.Equal(characterId, returnValue.Id);
    }
}