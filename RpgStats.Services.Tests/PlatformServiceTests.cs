using RpgStats.Domain.Exceptions;
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
        Assert.Equal(6, result.Count);
    }

    [Fact]
    public async Task GetPlatformByIdAsync_ReturnsPlatformById()
    {
        var result = await _service.GetPlatformByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetPlatformByIdAsync_ReturnsEmptyDto_WhenIdNotFound()
    {
        var result = await _service.GetPlatformByIdAsync(100);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreatePlatformAsync_ReturnsCreatedPlatform()
    {
        var platformForCreation = new PlatformForCreationDto
        {
            Name = "NewPlatform"
        };

        var result = await _service.CreatePlatformAsync(platformForCreation);

        Assert.NotNull(result);
        Assert.Equal("NewPlatform", result.Name);

        await _service.DeletePlatformAsync(result.Id);
    }

    [Fact]
    public async Task UpdatePlatformAsync_ReturnsUpdatedPlatform()
    {
        var platformForUpdate = new PlatformForUpdateDto()
        {
            Name = "PlatformToUpdate"
        };

        var result = await _service.UpdatePlatformAsync(1, platformForUpdate);

        Assert.NotNull(result);
        Assert.Equal("PlatformToUpdate", result.Name);
    }

    [Fact]
    public async Task UpdatePlatformAsync_Error_WhenPlatformNotFound()
    {
        var platformForUpdate = new PlatformForUpdateDto()
        {
            Name = "PlatformToUpdate"
        };

        await Assert.ThrowsAsync<PlatformNotFoundException>(async () =>
            await _service.UpdatePlatformAsync(100, platformForUpdate));
    }

    [Fact]
    public async Task DeletePlatformAsync_DeletesPlatform()
    {
        var result = await _service.DeletePlatformAsync(3);

        Assert.NotNull(result);
        Assert.Equal(3, result.Id);
    }

    [Fact]
    public async Task DeletePlatformAsync_Error_WhenIdNotFound()
    {
        await Assert.ThrowsAsync<PlatformNotFoundException>(async () =>
            await _service.DeletePlatformAsync(100));
    }
}