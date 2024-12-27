using RpgStats.Dto;

namespace RpgStats.Services.Tests;

public class PlatformServiceTests : IClassFixture<DatabaseFixture>
{
    private readonly PlatformService _service;

    public PlatformServiceTests(DatabaseFixture fixture)
    {
        _service = new PlatformService(fixture.Context);
    }

    [Fact]
    public async Task GetAllPlatformsAsync_ReturnsAllPlatforms()
    {
        var result = await _service.GetAllPlatformsAsync();

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(6, result.Data?.Count);
    }

    [Fact]
    public async Task GetPlatformByIdAsync_ReturnsPlatformById()
    {
        var result = await _service.GetPlatformByIdAsync(1);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(1, result.Data?.Id);
    }

    [Fact]
    public async Task GetPlatformByIdAsync_Error_WhenIdNotFound()
    {
        var result = await _service.GetPlatformByIdAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Equal("Platform with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    public async Task CreatePlatformAsync_ReturnsCreatedPlatform()
    {
        var result = await _service.CreatePlatformAsync(new PlatformForCreationDto { Name = "NewPlatform" });

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal("NewPlatform", result.Data?.Name);

        if (result.Data != null) await _service.DeletePlatformAsync(result.Data.Id);
    }

    [Fact]
    public async Task UpdatePlatformAsync_ReturnsUpdatedPlatform()
    {
        var result = await _service.UpdatePlatformAsync(1, new PlatformForUpdateDto { Name = "UpdatedPlatform" });

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal("UpdatedPlatform", result.Data?.Name);
    }

    [Fact]
    public async Task UpdatePlatformAsync_Error_WhenPlatformNotFound()
    {
        var result = await _service.UpdatePlatformAsync(100, new PlatformForUpdateDto { Name = "UpdatedPlatform" });

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Platform with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    public async Task DeletePlatformAsync_DeletesPlatform()
    {
        var result = await _service.DeletePlatformAsync(3);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(3, result.Data?.Id);
    }

    [Fact]
    public async Task DeletePlatformAsync_Error_WhenIdNotFound()
    {
        var result = await _service.DeletePlatformAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Platform with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    public async Task GetAllPlatformDetailDtosAsync_ReturnsAllPlatformDetailDtos()
    {
        var result = await _service.GetAllPlatformDetailDtosAsync();

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(6, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllPlatformDetailDtosByNameAsync_ReturnsPlatformDetailDtosByName()
    {
        var result = await _service.GetAllPlatformDetailDtosByNameAsync("Platform");

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(6, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllPlatformDetailDtosByNameAsync_Error_WhenNameNotFound()
    {
        var result = await _service.GetAllPlatformDetailDtosByNameAsync("NonExistingPlatform");

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("No platforms found", result.ErrorMessage);
    }

    [Fact]
    public async Task GetPlatformDetailDtoByIdAsync_ReturnsPlatformDetailDtoById()
    {
        var result = await _service.GetPlatformDetailDtoByIdAsync(1);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(1, result.Data?.Id);
    }

    [Fact]
    public async Task GetPlatformDetailDtoByIdAsync_Error_WhenIdNotFound()
    {
        var result = await _service.GetPlatformDetailDtoByIdAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Platform with ID 100 not found", result.ErrorMessage);
    }
}