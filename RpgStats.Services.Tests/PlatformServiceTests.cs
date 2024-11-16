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
        var platforms = await _service.GetAllPlatformsAsync();
        
        Assert.NotNull(platforms);
        Assert.Equal(6, platforms.Count);
    }
    
    [Fact]
    public async Task GetPlatformByIdAsync_ReturnsPlatformById()
    {
        var platform = await _service.GetPlatformByIdAsync(1);
        
        Assert.NotNull(platform);
        Assert.Equal(1, platform.Id);
    }
    
    [Fact]
    public async Task GetPlatformByIdAsync_ThrowsPlatformNotFoundException()
    {
        await Assert.ThrowsAsync<PlatformNotFoundException>(() => _service.GetPlatformByIdAsync(100));
    }
    
    [Fact]
    public async Task CreatePlatformAsync_ReturnsCreatedPlatform()
    {
        var platformForCreationDto = new PlatformForCreationDto { Name = "NewPlatform" };
        
        var platform = await _service.CreatePlatformAsync(platformForCreationDto);
        
        Assert.NotNull(platform);
        Assert.Equal("NewPlatform", platform.Name);
        
        await _service.DeletePlatformAsync(platform.Id);
    }
    
    [Fact]
    public async Task UpdatePlatformAsync_ReturnsUpdatedPlatform()
    {
        var platformForUpdateDto = new PlatformForUpdateDto { Name = "UpdatedPlatform" };
        
        var platform = await _service.UpdatePlatformAsync(1, platformForUpdateDto);
        
        Assert.NotNull(platform);
        Assert.Equal("UpdatedPlatform", platform.Name);
    }
    
    [Fact]
    public async Task UpdatePlatformAsync_ThrowsPlatformNotFoundException()
    {
        var platformForUpdateDto = new PlatformForUpdateDto { Name = "UpdatedPlatform" };
        
        await Assert.ThrowsAsync<PlatformNotFoundException>(() => _service.UpdatePlatformAsync(100, platformForUpdateDto));
    }
    
    [Fact]
    public async Task UpdatePlatformAsync_ThrowsPlatformNotFoundException_WhenPlatformIdIsZero()
    {
        var platformForUpdateDto = new PlatformForUpdateDto { Name = "UpdatedPlatform" };
        
        await Assert.ThrowsAsync<PlatformNotFoundException>(() => _service.UpdatePlatformAsync(0, platformForUpdateDto));
    }
    
    [Fact]
    public async Task UpdatePlatformAsync_ThrowsPlatformNotFoundException_WhenPlatformIdIsNegative()
    {
        var platformForUpdateDto = new PlatformForUpdateDto { Name = "UpdatedPlatform" };
        
        await Assert.ThrowsAsync<PlatformNotFoundException>(() => _service.UpdatePlatformAsync(-1, platformForUpdateDto));
    }
    
    [Fact]
    public async Task DeletePlatformAsync_DeletesPlatform()
    {
        var platform = await _service.CreatePlatformAsync(new PlatformForCreationDto { Name = "NewPlatform" });
        
        if (platform != null)
        {
            await _service.DeletePlatformAsync(platform.Id);
        }
        
        var platforms = await _service.GetAllPlatformsAsync();
        
        Assert.NotNull(platform);
        Assert.Equal(6, platforms.Count);
    }
    
    [Fact]
    public async Task DeletePlatformAsync_DoesNothing_WhenPlatformIdIsNotFound()
    {
        await _service.DeletePlatformAsync(100);
        
        var platforms = await _service.GetAllPlatformsAsync();
        
        Assert.NotNull(platforms);
        Assert.Equal(6, platforms.Count);
    }
    
    [Fact]
    public async Task DeletePlatformAsync_DoesNothing_WhenPlatformIdIsZero()
    {
        await _service.DeletePlatformAsync(0);
        
        var platforms = await _service.GetAllPlatformsAsync();
        
        Assert.NotNull(platforms);
        Assert.Equal(6, platforms.Count);
    }
    
    [Fact]
    public async Task DeletePlatformAsync_DoesNothing_WhenPlatformIdIsNegative()
    {
        await _service.DeletePlatformAsync(-1);
        
        var platforms = await _service.GetAllPlatformsAsync();
        
        Assert.NotNull(platforms);
        Assert.Equal(6, platforms.Count);
    }
    
    [Fact]
    public async Task GetAllPlatformDetailDtosAsync_ReturnsAllPlatformDetailDtos()
    {
        var platformDetailDtos = await _service.GetAllPlatformDetailDtosAsync();
        
        Assert.NotNull(platformDetailDtos);
        Assert.Equal(6, platformDetailDtos.Count);
    }
    
    [Fact]
    public async Task GetAllPlatformDetailDtosAsync_ReturnsAllPlatformDetailDtosWithGames()
    {
        var platformDetailDtos = await _service.GetAllPlatformDetailDtosAsync();
        
        Assert.NotNull(platformDetailDtos);
        Assert.Equal(6, platformDetailDtos.Count);
        
        var platformDetailDto = platformDetailDtos.First();
        
        Assert.NotNull(platformDetailDto.GameWithoutFkObjectsDtos);
    }
    
    [Fact]
    public async Task GetAllPlatformDetailDtosByNameAsync_ReturnsPlatformDetailDtosByName()
    {
        var platformDetailDtos = await _service.GetAllPlatformDetailDtosByNameAsync("Platform");
        
        Assert.NotNull(platformDetailDtos);
        Assert.Equal(6, platformDetailDtos.Count);
    }
    
    [Fact]
    public async Task GetAllPlatformDetailDtosByNameAsync_ReturnsEmptyList()
    {
        var platformDetailDtos = await _service.GetAllPlatformDetailDtosByNameAsync("NonExistingPlatform");
        
        Assert.NotNull(platformDetailDtos);
        Assert.Empty(platformDetailDtos);
    }
    
    [Fact]
    public async Task GetAllPlatformDetailDtosByNameAsync_ReturnsPlatformDetailDtosByNameCaseInsensitive()
    {
        var platformDetailDtos = await _service.GetAllPlatformDetailDtosByNameAsync("platform");
        
        Assert.NotNull(platformDetailDtos);
        Assert.Equal(6, platformDetailDtos.Count);
    }
    
    [Fact]
    public async Task GetAllPlatformDetailDtosByNameAsync_ReturnsPlatformDetailDtosByNameWithGames()
    {
        var platformDetailDtos = await _service.GetAllPlatformDetailDtosByNameAsync("Platform");
        
        Assert.NotNull(platformDetailDtos);
        Assert.Equal(6, platformDetailDtos.Count);
        
        var platformDetailDto = platformDetailDtos.First();
        
        Assert.NotNull(platformDetailDto.GameWithoutFkObjectsDtos);
    }
    
    [Fact]
    public async Task GetPlatformDetailDtoByIdAsync_ReturnsPlatformDetailDtoById()
    {
        var platformDetailDto = await _service.GetPlatformDetailDtoByIdAsync(1);
        
        Assert.NotNull(platformDetailDto);
        Assert.Equal(1, platformDetailDto.Id);
    }
    
    [Fact]
    public async Task GetPlatformDetailDtoByIdAsync_ReturnsEmptyEntity_WhenPlatformIdNotFound()
    {
        var platformDetailDto = await _service.GetPlatformDetailDtoByIdAsync(100);
        
        Assert.NotNull(platformDetailDto);
        Assert.Equal(0, platformDetailDto.Id);
        Assert.Equal(string.Empty, platformDetailDto.Name);
    }
    
    [Fact]
    public async Task GetPlatformDetailDtoByIdAsync_ReturnsEmptyEntity_WhenPlatformIdIsZero()
    {
        var platformDetailDto = await _service.GetPlatformDetailDtoByIdAsync(0);
        
        Assert.NotNull(platformDetailDto);
        Assert.Equal(0, platformDetailDto.Id);
        Assert.Equal(string.Empty, platformDetailDto.Name);
    }
    
    [Fact]
    public async Task GetPlatformDetailDtoByIdAsync_ReturnsEmptyEntity_WhenPlatformIdIsNegative()
    {
        var platformDetailDto = await _service.GetPlatformDetailDtoByIdAsync(-1);
        
        Assert.NotNull(platformDetailDto);
        Assert.Equal(0, platformDetailDto.Id);
        Assert.Equal(string.Empty, platformDetailDto.Name);
    }
    
    [Fact]
    public async Task GetPlatformDetailDtoByIdAsync_ReturnsPlatformDetailDtoByIdWithGames()
    {
        var platformDetailDto = await _service.GetPlatformDetailDtoByIdAsync(1);
        
        Assert.NotNull(platformDetailDto);
        Assert.Equal(1, platformDetailDto.Id);
        Assert.NotNull(platformDetailDto.GameWithoutFkObjectsDtos);
    }
}