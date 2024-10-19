using RpgStats.Domain.Exceptions;
using RpgStats.Dto;

namespace RpgStats.Services.Tests;

public class StatServiceTests : IClassFixture<DatabaseFixture>
{
    private readonly StatService _service;

    public StatServiceTests(DatabaseFixture fixture)
    {
        _service = new StatService(fixture.Context);
    }
    
    [Fact]
    public async Task GetAllStatsAsync_ReturnsAllStats()
    {
        var stats = await _service.GetAllStatsAsync();
        
        Assert.NotNull(stats);
        Assert.Equal(6, stats.Count);
    }
    
    [Fact]
    public async Task GetAllStatsByNameAsync_ReturnsStatsByName()
    {
        var stats = await _service.GetAllStatsByNameAsync("good");
        
        Assert.NotNull(stats);
        Assert.Equal(2, stats.Count);
    }

    [Fact]
    public async Task GetAllStatsByNameAsync_ReturnsStatsByNameCaseInsensitive()
    {
        var stats = await _service.GetAllStatsByNameAsync("GOOD");
        
        Assert.NotNull(stats);
        Assert.Equal(2, stats.Count);
    }
    
    [Fact]
    public async Task GetAllStatsByNameAsync_ReturnsEmptyList()
    {
        var stats = await _service.GetAllStatsByNameAsync("NonExistentStat");
        
        Assert.NotNull(stats);
        Assert.Empty(stats);
    }
    
    [Fact]
    public async Task GetAllStatsByShortNameAsync_ReturnsStatsByShortName()
    {
        var stats = await _service.GetAllStatsByShortNameAsync("gsv");
        
        Assert.NotNull(stats);
        Assert.Equal(2, stats.Count);
    }
    
    [Fact]
    public async Task GetAllStatsByShortNameAsync_ReturnsStatsByShortNameCaseInsensitive()
    {
        var stats = await _service.GetAllStatsByShortNameAsync("GSV");
        
        Assert.NotNull(stats);
        Assert.Equal(2, stats.Count);
    }
    
    [Fact]
    public async Task GetAllStatsByShortNameAsync_ReturnsEmptyList()
    {
        var stats = await _service.GetAllStatsByShortNameAsync("NonExistentStat");
        
        Assert.NotNull(stats);
        Assert.Empty(stats);
    }
    
    [Fact]
    public async Task GetAllStatDetailDtosAsync_ReturnsAllStatDetailDtos()
    {
        var statDetailDtos = await _service.GetAllStatDetailDtosAsync();
        
        Assert.NotNull(statDetailDtos);
        Assert.Equal(6, statDetailDtos.Count);
    }
    
    [Fact]
    public async Task GetAllStatDetailDtosByNameAsync_ReturnsStatDetailDtosByName()
    {
        var statDetailDtos = await _service.GetAllStatDetailDtosByNameAsync("good");
        
        Assert.NotNull(statDetailDtos);
        Assert.Equal(2, statDetailDtos.Count);
    }
    
    [Fact]
    public async Task GetAllStatDetailDtosByNameAsync_ReturnsStatDetailDtosByNameCaseInsensitive()
    {
        var statDetailDtos = await _service.GetAllStatDetailDtosByNameAsync("GOOD");
        
        Assert.NotNull(statDetailDtos);
        Assert.Equal(2, statDetailDtos.Count);
    }
    
    [Fact]
    public async Task GetAllStatDetailDtosByNameAsync_ReturnsEmptyList()
    {
        var statDetailDtos = await _service.GetAllStatDetailDtosByNameAsync("NonExistentStat");
        
        Assert.NotNull(statDetailDtos);
        Assert.Empty(statDetailDtos);
    }
    
    [Fact]
    public async Task GetAllStatDetailDtosByShortNameAsync_ReturnsStatDetailDtosByShortName()
    {
        var statDetailDtos = await _service.GetAllStatDetailDtosByShortNameAsync("gsv");
        
        Assert.NotNull(statDetailDtos);
        Assert.Equal(2, statDetailDtos.Count);
    }
    
    [Fact]
    public async Task GetAllStatDetailDtosByShortNameAsync_ReturnsStatDetailDtosByShortNameCaseInsensitive()
    {
        var statDetailDtos = await _service.GetAllStatDetailDtosByShortNameAsync("GSV");
        
        Assert.NotNull(statDetailDtos);
        Assert.Equal(2, statDetailDtos.Count);
    }
    
    [Fact]
    public async Task GetAllStatDetailDtosByShortNameAsync_ReturnsEmptyList()
    {
        var statDetailDtos = await _service.GetAllStatDetailDtosByShortNameAsync("NonExistentStat");
        
        Assert.NotNull(statDetailDtos);
        Assert.Empty(statDetailDtos);
    }
    
    [Fact]
    public async Task GetStatDetailDtoByIdAsync_ReturnsStatDetailDtoById()
    {
        var statDetailDto = await _service.GetStatDetailDtoByIdAsync(1);
        
        Assert.NotNull(statDetailDto);
        Assert.Equal(1, statDetailDto.Id);
    }
    
    [Fact]
    public async Task GetStatDetailDtoByIdAsync_ReturnsEmptyEntity_WhenStatIdNotFound()
    {
        var statDetailDto = await _service.GetStatDetailDtoByIdAsync(100);
        
        Assert.NotNull(statDetailDto);
        Assert.Equal(0, statDetailDto.Id);
        Assert.Null(statDetailDto.Name);
    }
    
    [Fact]
    public async Task GetStatDetailDtoByIdAsync_ReturnsEmptyEntity_WhenStatIdIsZero()
    {
        var statDetailDto = await _service.GetStatDetailDtoByIdAsync(0);
        
        Assert.NotNull(statDetailDto);
        Assert.Equal(0, statDetailDto.Id);
        Assert.Null(statDetailDto.Name);
    }
    
    [Fact]
    public async Task GetStatDetailDtoByIdAsync_ReturnsEmptyEntity_WhenStatIdIsNegative()
    {
        var statDetailDto = await _service.GetStatDetailDtoByIdAsync(-1);
        
        Assert.NotNull(statDetailDto);
        Assert.Equal(0, statDetailDto.Id);
        Assert.Null(statDetailDto.Name);
    }
    
    [Fact]
    public async Task GetStatByIdAsync_ReturnsStatById()
    {
        var statDto = await _service.GetStatByIdAsync(1);
        
        Assert.NotNull(statDto);
        Assert.Equal(1, statDto.Id);
    }
    
    [Fact]
    public async Task GetStatByIdAsync_ThrowsStatNotFoundException_WhenStatIdNotFound()
    {
        await Assert.ThrowsAsync<StatNotFoundException>(() => _service.GetStatByIdAsync(100));
    }
    
    [Fact]
    public async Task GetStatByIdAsync_ThrowsStatNotFoundException_WhenStatIdIsZero()
    {
        await Assert.ThrowsAsync<StatNotFoundException>(() => _service.GetStatByIdAsync(0));
    }
    
    [Fact]
    public async Task GetStatByIdAsync_ThrowsStatNotFoundException_WhenStatIdIsNegative()
    {
        await Assert.ThrowsAsync<StatNotFoundException>(() => _service.GetStatByIdAsync(-1));
    }
    
    [Fact]
    public async Task CreateStatAsync_ReturnsCreatedStat()
    {
        var statForCreationDto = new StatForCreationDto
        {
            Name = "NewStat",
            ShortName = "NS"
        };
        
        var statDto = await _service.CreateStatAsync(statForCreationDto);
        
        Assert.NotNull(statDto);
        Assert.Equal("NewStat", statDto.Name);
        Assert.Equal("NS", statDto.ShortName);
        
        await _service.DeleteStatAsync(statDto.Id);
    }
    
    [Fact]
    public async Task UpdateStatAsync_ReturnsUpdatedStat()
    {
        var statForUpdateDto = new StatForUpdateDto
        {
            Name = "UpdatedStat",
            ShortName = "US"
        };
        
        var statDto = await _service.UpdateStatAsync(1, statForUpdateDto);
        
        Assert.NotNull(statDto);
        Assert.Equal("UpdatedStat", statDto.Name);
        Assert.Equal("US", statDto.ShortName);
    }
    
    [Fact]
    public async Task UpdateStatAsync_ThrowsStatNotFoundException()
    {
        var statForUpdateDto = new StatForUpdateDto
        {
            Name = "UpdatedStat",
            ShortName = "US"
        };
        
        await Assert.ThrowsAsync<StatNotFoundException>(() => _service.UpdateStatAsync(100, statForUpdateDto));
    }
    
    [Fact]
    public async Task UpdateStatAsync_ThrowsStatNotFoundException_WhenStatIdIsZero()
    {
        var statForUpdateDto = new StatForUpdateDto
        {
            Name = "UpdatedStat",
            ShortName = "US"
        };
        
        await Assert.ThrowsAsync<StatNotFoundException>(() => _service.UpdateStatAsync(0, statForUpdateDto));
    }
    
    [Fact]
    public async Task UpdateStatAsync_ThrowsStatNotFoundException_WhenStatIdIsNegative()
    {
        var statForUpdateDto = new StatForUpdateDto
        {
            Name = "UpdatedStat",
            ShortName = "US"
        };
        
        await Assert.ThrowsAsync<StatNotFoundException>(() => _service.UpdateStatAsync(-1, statForUpdateDto));
    }
    
    [Fact]
    public async Task DeleteStatAsync_DeletesStat()
    {
        var statForCreationDto = new StatForCreationDto
        {
            Name = "NewStat",
            ShortName = "NS"
        };
        
        var statDto = await _service.CreateStatAsync(statForCreationDto);

        if (statDto != null)
        {
            await _service.DeleteStatAsync(statDto.Id);
        }
        
        var stats = await _service.GetAllStatsAsync();
        
        Assert.NotNull(stats);
        Assert.Equal(6, stats.Count);
    }
    
    [Fact]
    public async Task DeleteStatAsync_DoesNothing_WhenStatIdIsNotFound()
    {
        await _service.DeleteStatAsync(100);
        
        var stats = await _service.GetAllStatsAsync();
        
        Assert.NotNull(stats);
        Assert.Equal(6, stats.Count);
    }
    
    [Fact]
    public async Task DeleteStatAsync_DoesNothing_WhenStatIdIsZero()
    {
        await _service.DeleteStatAsync(0);
        
        var stats = await _service.GetAllStatsAsync();
        
        Assert.NotNull(stats);
        Assert.Equal(6, stats.Count);
    }
    
    [Fact]
    public async Task DeleteStatAsync_DoesNothing_WhenStatIdIsNegative()
    {
        await _service.DeleteStatAsync(-1);
        
        var stats = await _service.GetAllStatsAsync();
        
        Assert.NotNull(stats);
        Assert.Equal(6, stats.Count);
    }
}