using Microsoft.AspNetCore.Mvc;
using Moq;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;

namespace RpgStats.Controllers.Tests;

public class StatControllerTests
{
    private readonly Mock<IStatService> _mockService;
    private readonly StatController _controller;
    private readonly List<StatDto> _stats;
    private readonly List<StatDetailDto> _statDetailDtos;

    public StatControllerTests()
    {
        _mockService = new Mock<IStatService>();
        _controller = new StatController(_mockService.Object);

        _stats = new List<StatDto>
        {
            new StatDto { Id = 1, Name = "TestStat1", ShortName = "TS1" },
            new StatDto { Id = 2, Name = "TestStat2", ShortName = "TS2" },
            new StatDto { Id = 3, Name = "TestStat3", ShortName = "TS3" }
        };

        _statDetailDtos = new List<StatDetailDto>
        {
            new StatDetailDto
            {
                Id = 1, Name = "TestStat1", ShortName = "TS1",
                GameWithoutFkObjectsDtos = new List<GameWithoutFkObjectsDto>
                {
                    new GameWithoutFkObjectsDto { Id = 1, Name = "TestGame1" },
                    new GameWithoutFkObjectsDto { Id = 2, Name = "TestGame2" }
                },
                StatValueWithCharacterObjectDtos = new List<StatValueWithCharacterObjectDto>
                {
                    new StatValueWithCharacterObjectDto
                    {
                        Id = 1, Value = 1,
                        CharacterWithoutFkObjectsDto = new CharacterWithoutFkObjectsDto
                            { Id = 1, Name = "TestCharacter1" }
                    },
                    new StatValueWithCharacterObjectDto
                    {
                        Id = 2, Value = 2,
                        CharacterWithoutFkObjectsDto = new CharacterWithoutFkObjectsDto
                            { Id = 2, Name = "TestCharacter2" }
                    }
                }
            },
            new StatDetailDto
            {
                Id = 2, Name = "TestStat2", ShortName = "TS2",
                GameWithoutFkObjectsDtos = new List<GameWithoutFkObjectsDto>
                {
                    new GameWithoutFkObjectsDto { Id = 3, Name = "TestGame3" },
                    new GameWithoutFkObjectsDto { Id = 4, Name = "TestGame4" }
                },
                StatValueWithCharacterObjectDtos = new List<StatValueWithCharacterObjectDto>
                {
                    new StatValueWithCharacterObjectDto
                    {
                        Id = 3, Value = 3,
                        CharacterWithoutFkObjectsDto = new CharacterWithoutFkObjectsDto
                            { Id = 3, Name = "TestCharacter3" }
                    },
                    new StatValueWithCharacterObjectDto
                    {
                        Id = 4, Value = 4,
                        CharacterWithoutFkObjectsDto = new CharacterWithoutFkObjectsDto
                            { Id = 4, Name = "TestCharacter4" }
                    }
                }
            },
            new StatDetailDto
            {
                Id = 3, Name = "TestStat3", ShortName = "TS3",
                GameWithoutFkObjectsDtos = new List<GameWithoutFkObjectsDto>
                {
                    new GameWithoutFkObjectsDto { Id = 5, Name = "TestGame5" },
                    new GameWithoutFkObjectsDto { Id = 6, Name = "TestGame6" }
                },
                StatValueWithCharacterObjectDtos = new List<StatValueWithCharacterObjectDto>
                {
                    new StatValueWithCharacterObjectDto
                    {
                        Id = 5, Value = 5,
                        CharacterWithoutFkObjectsDto = new CharacterWithoutFkObjectsDto
                            { Id = 5, Name = "TestCharacter5" }
                    },
                    new StatValueWithCharacterObjectDto
                    {
                        Id = 6, Value = 6,
                        CharacterWithoutFkObjectsDto = new CharacterWithoutFkObjectsDto
                            { Id = 6, Name = "TestCharacter6" }
                    }
                }
            }
        };
    }
    
    [Fact]
    public async Task GetAllStats_ReturnsAllStats()
    {
        _mockService.Setup(x => x.GetAllStatsAsync())
            .ReturnsAsync(_stats);

        var result = await _controller.GetAllStats();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<StatDto>>(okResult.Value);
        Assert.Equal(_stats.Count, returnValue.Count);
    }
    
    [Fact]
    public async Task GetAllStatsByName_ReturnsStatsByName()
    {
        var name = "TestStat1";
        _mockService.Setup(x => x.GetAllStatsByNameAsync(name))
            .ReturnsAsync(_stats.Where(x => x.Name == name).ToList());

        var result = await _controller.GetAllStatsByName(name);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<StatDto>>(okResult.Value);
        Assert.Single(returnValue);
    }
    
    [Fact]
    public async Task GetAllStatsByShortname_ReturnsStatsByShortname()
    {
        var shortName = "TS1";
        _mockService.Setup(x => x.GetAllStatsByShortNameAsync(shortName))
            .ReturnsAsync(_stats.Where(x => x.ShortName == shortName).ToList());

        var result = await _controller.GetAllStatsByShortname(shortName);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<StatDto>>(okResult.Value);
        Assert.Single(returnValue);
    }
    
    [Fact]
    public async Task GetAllStatDetailDtos_ReturnsAllStatDetailDtos()
    {
        _mockService.Setup(x => x.GetAllStatDetailDtosAsync())
            .ReturnsAsync(_statDetailDtos);

        var result = await _controller.GetAllStatDetailDtos();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<StatDetailDto>>(okResult.Value);
        Assert.Equal(_statDetailDtos.Count, returnValue.Count);
    }
    
    [Fact]
    public async Task GetAllStatDetailDtosByName_ReturnsStatDetailDtosByName()
    {
        var name = "TestStat1";
        _mockService.Setup(x => x.GetAllStatDetailDtosByNameAsync(name))
            .ReturnsAsync(_statDetailDtos.Where(x => x.Name == name).ToList());

        var result = await _controller.GetAllStatDetailDtosByName(name);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<StatDetailDto>>(okResult.Value);
        Assert.Single(returnValue);
    }
    
    [Fact]
    public async Task GetAllStatDetailDtosByShortName_ReturnsStatDetailDtosByShortName()
    {
        var shortName = "TS1";
        _mockService.Setup(x => x.GetAllStatDetailDtosByShortNameAsync(shortName))
            .ReturnsAsync(_statDetailDtos.Where(x => x.ShortName == shortName).ToList());

        var result = await _controller.GetAllStatDetailDtosByShortName(shortName);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<StatDetailDto>>(okResult.Value);
        Assert.Single(returnValue);
    }
    
    [Fact]
    public async Task GetStatById_ReturnsStatById()
    {
        var statId = 1;
        _mockService.Setup(x => x.GetStatByIdAsync(statId))
            .ReturnsAsync(_stats.FirstOrDefault(x => x.Id == statId));

        var result = await _controller.GetStatById(statId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<StatDto>(okResult.Value);
        Assert.Equal(statId, returnValue.Id);
    }
    
    [Fact]
    public async Task GetStatDetailDtoById_ReturnsStatDetailDtoById()
    {
        var statId = 1;
        _mockService.Setup(x => x.GetStatDetailDtoByIdAsync(statId))
            .ReturnsAsync(_statDetailDtos.FirstOrDefault(x => x.Id == statId));

        var result = await _controller.GetStatDetailDtoById(statId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<StatDetailDto>(okResult.Value);
        Assert.Equal(statId, returnValue.Id);
    }
    
    [Fact]
    public async Task CreateStat_ReturnsCreatedStat()
    {
        var statForCreationDto = new StatForCreationDto { Name = "TestStat4", ShortName = "TS4" };
        _mockService.Setup(x => x.CreateStatAsync(statForCreationDto))
            .ReturnsAsync(new StatDto { Id = 4, Name = statForCreationDto.Name, ShortName = statForCreationDto.ShortName });

        var result = await _controller.CreateStat(statForCreationDto);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<StatDto>(createdAtActionResult.Value);
        Assert.Equal(statForCreationDto.Name, returnValue.Name);
        Assert.Equal(statForCreationDto.ShortName, returnValue.ShortName);
    }

    [Fact]
    public async Task CreateStat_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.CreateStatAsync(It.IsAny<StatForCreationDto>()))
            .ReturnsAsync((StatDto?)null);

        var result = await _controller.CreateStat(new StatForCreationDto());
        
        Assert.IsType<BadRequestResult>(result);
    }
    
    [Fact]
    public async Task UpdateStat_ReturnsUpdatedStat()
    {
        var statId = 1;
        var statForUpdateDto = new StatForUpdateDto { Name = "TestStat1Updated", ShortName = "TS1U" };
        _mockService.Setup(x => x.UpdateStatAsync(statId, statForUpdateDto))
            .ReturnsAsync(new StatDto { Id = statId, Name = statForUpdateDto.Name, ShortName = statForUpdateDto.ShortName });

        var result = await _controller.UpdateStat(statId, statForUpdateDto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<StatDto>(okResult.Value);
        Assert.Equal(statId, returnValue.Id);
        Assert.Equal(statForUpdateDto.Name, returnValue.Name);
        Assert.Equal(statForUpdateDto.ShortName, returnValue.ShortName);
    }
    
    [Fact]
    public async Task UpdateStat_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.UpdateStatAsync(It.IsAny<long>(), It.IsAny<StatForUpdateDto>()))
            .ReturnsAsync((StatDto?)null);

        var result = await _controller.UpdateStat(1, new StatForUpdateDto());
        
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteStat_ReturnsOk()
    {
        _mockService.Setup(x => x.DeleteStatAsync(It.IsAny<long>()))
            .ReturnsAsync(Task.CompletedTask);
        
        var result = await _controller.DeleteStat(1);
        
        Assert.IsType<OkResult>(result);
    }
    
    [Fact]
    public async Task DeleteStat_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.DeleteStatAsync(It.IsAny<long>()))
            .ReturnsAsync(Task.FromException(new Exception()));
        
        var result = await _controller.DeleteStat(1);
        
        Assert.IsType<BadRequestResult>(result);
    }
}