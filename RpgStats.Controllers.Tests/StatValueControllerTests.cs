using Microsoft.AspNetCore.Mvc;
using Moq;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;

namespace RpgStats.Controllers.Tests;

public class StatValueControllerTests
{
    private readonly Mock<IStatValueService> _mockService;
    private readonly StatValueController _controller;
    private readonly List<StatValueDto> _statValues;

    public StatValueControllerTests()
    {
        _mockService = new Mock<IStatValueService>();
        _controller = new StatValueController(_mockService.Object);
        
        _statValues = new List<StatValueDto>
        {
            new StatValueDto { Id = 1, CharacterId = 1, StatId = 1, Value = 1},
            new StatValueDto { Id = 2, CharacterId = 2, StatId = 2, Value = 2},
            new StatValueDto { Id = 3, CharacterId = 1, StatId = 3, Value = 3},
            new StatValueDto { Id = 4, CharacterId = 3, StatId = 1, Value = 4}
        };
    }
    
    [Fact]
    public async Task GetAllStatValues_ReturnsAllStatValues()
    {
        _mockService.Setup(x => x.GetAllStatValuesAsync())
            .ReturnsAsync(_statValues);

        var result = await _controller.GetAllStatValues();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<StatValueDto>>(okResult.Value);
        Assert.Equal(_statValues.Count, returnValue.Count);
    }
    
    [Fact]
    public async Task GetAllStatValuesByCharacter_ReturnsStatValuesByCharacter()
    {
        const int characterId = 1;
        _mockService.Setup(x => x.GetAllStatValuesByCharacterIdAsync(characterId))
            .ReturnsAsync(_statValues.Where(x => x.CharacterId == characterId).ToList());

        var result = await _controller.GetAllStatValuesByCharacter(characterId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<StatValueDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }
    
    [Fact]
    public async Task GetAllStatValuesByStat_ReturnsStatValuesByStat()
    {
        const int statId = 1;
        _mockService.Setup(x => x.GetAllStatValuesByStatIdAsync(statId))
            .ReturnsAsync(_statValues.Where(x => x.StatId == statId).ToList());

        var result = await _controller.GetAllStatValuesByStat(statId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<StatValueDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }
    
    [Fact]
    public async Task GetStatValueById_ReturnsStatValueById()
    {
        const int statValueId = 1;
        _mockService.Setup(x => x.GetStatValueByIdAsync(statValueId))
            .ReturnsAsync(_statValues.FirstOrDefault(x => x.Id == statValueId));

        var result = await _controller.GetStatValueById(statValueId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<StatValueDto>(okResult.Value);
        Assert.Equal(statValueId, returnValue.Id);
    }
    
    [Fact]
    public async Task CreateStatValue_ReturnsCreatedStatValue()
    {
        var statValueForCreationDto = new StatValueForCreationDto { Value = 1 };
        const int characterId = 1;
        const int statId = 1;
        var response = new StatValueDto { Id = 5, CharacterId = characterId, StatId = statId, Value = (int)statValueForCreationDto.Value };
        _mockService.Setup(x => x.CreateStatValueAsync(characterId, statId, statValueForCreationDto))
            .ReturnsAsync(response);

        var result = await _controller.CreateStatValue(statValueForCreationDto, characterId, statId);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(StatValueController.GetStatValueById), createdAtActionResult.ActionName);
        Assert.Equal(response.Id, createdAtActionResult.RouteValues?["statValueId"]);
        Assert.Equal(response.StatId, createdAtActionResult.RouteValues?["statId"]);
        Assert.Equal(response.CharacterId, createdAtActionResult.RouteValues?["characterId"]);
        Assert.Equal(response, createdAtActionResult.Value);
    }

    [Fact]
    public async Task CreateStatValue_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.CreateStatValueAsync(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<StatValueForCreationDto>()))
            .ReturnsAsync((StatValueDto?)null);
        
        var result = await _controller.CreateStatValue(new StatValueForCreationDto(), 1, 1);
        
        Assert.IsType<BadRequestResult>(result);
    }
    
    [Fact]
    public async Task UpdateStatValue_ReturnsUpdatedStatValue()
    {
        var statValueForUpdateDto = new StatValueForUpdateDto { Value = 1 };
        const int statValueId = 1;
        const int characterId = 1;
        const int statId = 1;
        var response = new StatValueDto { Id = statValueId, CharacterId = characterId, StatId = statId, Value = (int)statValueForUpdateDto.Value };
        _mockService.Setup(x => x.UpdateStatValueAsync(statValueId, characterId, statId, statValueForUpdateDto))
            .ReturnsAsync(response);

        var result = await _controller.UpdateStatValue(statValueForUpdateDto, statValueId, characterId, statId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<StatValueDto>(okResult.Value);
        Assert.Equal(statValueId, returnValue.Id);
    }
    
    [Fact]
    public async Task UpdateStatValue_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.UpdateStatValueAsync(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<long>(), It.IsAny<StatValueForUpdateDto>()))
            .ReturnsAsync((StatValueDto?)null);
        
        var result = await _controller.UpdateStatValue(new StatValueForUpdateDto(), 1, 1, 1);
        
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteStatValue_ReturnsOk()
    {
        _mockService.Setup(x => x.DeleteStatValueAsync(It.IsAny<long>()))
            .ReturnsAsync(Task.CompletedTask);
        
        var result = await _controller.DeleteStatValue(1);
        
        Assert.IsType<OkResult>(result);
    }
    
    [Fact]
    public async Task DeleteStatValue_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.DeleteStatValueAsync(It.IsAny<long>()))
            .ReturnsAsync(Task.FromException(new Exception()));
        
        var result = await _controller.DeleteStatValue(1);
        
        Assert.IsType<BadRequestResult>(result);
    }
}