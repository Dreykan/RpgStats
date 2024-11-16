using Microsoft.AspNetCore.Mvc;
using Moq;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;

namespace RpgStats.ControllersLegacy.Tests;

public class PlatformControllerTests
{
    private readonly PlatformController _controller;
    private readonly Mock<IPlatformService> _mockService;
    private readonly List<PlatformDetailDto> _platformDetailDtos;
    private readonly List<PlatformDto> _platforms;

    public PlatformControllerTests()
    {
        _mockService = new Mock<IPlatformService>();
        _controller = new PlatformController(_mockService.Object);

        _platforms = new List<PlatformDto>
        {
            new() { Id = 1, Name = "TestPlatform1" },
            new() { Id = 2, Name = "TestPlatform2" },
            new() { Id = 3, Name = "TestPlatform3" }
        };

        _platformDetailDtos = new List<PlatformDetailDto>
        {
            new()
            {
                Id = 1, Name = "TestPlatform1",
                GameWithoutFkObjectsDtos = new List<GameWithoutFkObjectsDto>
                {
                    new() { Id = 1, Name = "TestGame1" },
                    new() { Id = 2, Name = "TestGame2" }
                }
            },
            new()
            {
                Id = 2, Name = "TestPlatform2",
                GameWithoutFkObjectsDtos = new List<GameWithoutFkObjectsDto>
                {
                    new() { Id = 3, Name = "TestGame3" },
                    new() { Id = 4, Name = "TestGame4" }
                }
            },
            new()
            {
                Id = 3, Name = "TestPlatform3",
                GameWithoutFkObjectsDtos = new List<GameWithoutFkObjectsDto>
                {
                    new() { Id = 5, Name = "TestGame5" },
                    new() { Id = 6, Name = "TestGame6" }
                }
            }
        };
    }

    [Fact]
    public async Task GetAllPlatforms_ReturnsAllPlatforms()
    {
        _mockService.Setup(x => x.GetAllPlatformsAsync())
            .ReturnsAsync(_platforms);

        var result = await _controller.GetAllPlatforms();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<PlatformDto>>(okResult.Value);
        Assert.Equal(_platforms.Count, returnValue.Count);
    }

    [Fact]
    public async Task GetAllPlatformDetailDtos_ReturnsAllPlatformDetailDtos()
    {
        _mockService.Setup(x => x.GetAllPlatformDetailDtosAsync())
            .ReturnsAsync(_platformDetailDtos);

        var result = await _controller.GetAllPlatformDetailDtos();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<PlatformDetailDto>>(okResult.Value);
        Assert.Equal(_platformDetailDtos.Count, returnValue.Count);
    }

    [Fact]
    public async Task GetAllPlatformDetailDtosByName_ReturnsPlatformDetailDtosByName()
    {
        const string name = "TestPlatform1";
        _mockService.Setup(x => x.GetAllPlatformDetailDtosByNameAsync(name))
            .ReturnsAsync(_platformDetailDtos.Where(x => x.Name == name).ToList());

        var result = await _controller.GetAllPlatformDetailDtosByName(name);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<PlatformDetailDto>>(okResult.Value);
        Assert.Single(returnValue);
    }

    [Fact]
    public async Task GetPlatformById_ReturnsPlatformById()
    {
        const int platformId = 1;
        _mockService.Setup(x => x.GetPlatformByIdAsync(platformId))
            .ReturnsAsync(_platforms.FirstOrDefault(x => x.Id == platformId));

        var result = await _controller.GetPlatformById(platformId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<PlatformDto>(okResult.Value);
        Assert.Equal(platformId, returnValue.Id);
    }

    [Fact]
    public async Task GetPlatformDetailDtoById_ReturnsPlatformDetailDtoById()
    {
        const int platformId = 1;
        _mockService.Setup(x => x.GetPlatformDetailDtoByIdAsync(platformId))!
            .ReturnsAsync(_platformDetailDtos.FirstOrDefault(x => x.Id == platformId));

        var result = await _controller.GetPlatformDetailDtoById(platformId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<PlatformDetailDto>(okResult.Value);
        Assert.Equal(platformId, returnValue.Id);
    }

    [Fact]
    public async Task CreatePlatform_ReturnsCreatedPlatform()
    {
        var platformForCreationDto = new PlatformForCreationDto { Name = "TestPlatform4" };
        var platformDto = new PlatformDto { Id = 4, Name = platformForCreationDto.Name };
        _mockService.Setup(x => x.CreatePlatformAsync(platformForCreationDto))
            .ReturnsAsync(platformDto);

        var result = await _controller.CreatePlatform(platformForCreationDto);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<PlatformDto>(createdAtActionResult.Value);
        Assert.Equal(platformDto.Id, returnValue.Id);
    }

    [Fact]
    public async Task CreatePlatform_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.CreatePlatformAsync(It.IsAny<PlatformForCreationDto>()))
            .ReturnsAsync((PlatformDto?)null);

        var result = await _controller.CreatePlatform(new PlatformForCreationDto());

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task UpdatePlatform_ReturnsUpdatedPlatform()
    {
        const int platformId = 1;
        var platformForUpdateDto = new PlatformForUpdateDto { Name = "TestPlatform1Updated" };
        var platformDto = new PlatformDto { Id = platformId, Name = platformForUpdateDto.Name };
        _mockService.Setup(x => x.UpdatePlatformAsync(platformId, platformForUpdateDto))
            .ReturnsAsync(platformDto);

        var result = await _controller.UpdatePlatform(platformId, platformForUpdateDto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<PlatformDto>(okResult.Value);
        Assert.Equal(platformId, returnValue.Id);
    }

    [Fact]
    public async Task UpdatePlatform_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.UpdatePlatformAsync(It.IsAny<long>(), It.IsAny<PlatformForUpdateDto>()))
            .ReturnsAsync((PlatformDto?)null);

        var result = await _controller.UpdatePlatform(1, new PlatformForUpdateDto());

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeletePlatform_ReturnsOk()
    {
        _mockService.Setup(x => x.DeletePlatformAsync(It.IsAny<long>()))
            .ReturnsAsync(Task.CompletedTask);

        var result = await _controller.DeletePlatform(1);

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeletePlatform_ReturnsBadRequest()
    {
        _mockService.Setup(x => x.DeletePlatformAsync(It.IsAny<long>()))
            .ReturnsAsync(Task.FromException(new Exception()));

        var result = await _controller.DeletePlatform(1);

        Assert.IsType<BadRequestResult>(result);
    }
}