using RpgStats.Dto;
using Xunit.Priority;

namespace RpgStats.Services.Tests;

[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public class StatServiceTests : IClassFixture<DatabaseFixture>
{
    private readonly StatService _service;

    public StatServiceTests(DatabaseFixture fixture)
    {
        _service = new StatService(fixture.Context);
    }

    [Fact]
    [Priority(1)]
    public async Task GetAllStatsAsync_ReturnsAllStats()
    {
        var result = await _service.GetAllStatsAsync();

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(6, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllStatsByNameAsync_ReturnsStatsByName()
    {
        var result = await _service.GetAllStatsByNameAsync("good");

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(2, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllStatsByNameAsync_Error_WhenNameNotFound()
    {
        var result = await _service.GetAllStatsByNameAsync("NonExistentStat");

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("No stats found", result.ErrorMessage);
    }

    [Fact]
    public async Task GetAllStatsByShortNameAsync_ReturnsStatsByShortName()
    {
        var result = await _service.GetAllStatsByShortNameAsync("gsv");

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(2, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllStatsByShortNameAsync_Error_WhenShortNameNotFound()
    {
        var result = await _service.GetAllStatsByShortNameAsync("NonExistentStat");

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("No stats found", result.ErrorMessage);
    }

    [Fact]
    [Priority(1)]
    public async Task GetAllStatDetailDtosAsync_ReturnsAllStatDetailDtos()
    {
        var result = await _service.GetAllStatDetailDtosAsync();

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(6, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllStatDetailDtosByNameAsync_ReturnsStatDetailDtosByName()
    {
        var result = await _service.GetAllStatDetailDtosByNameAsync("good");

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(2, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllStatDetailDtosByNameAsync_Error_WhenNameNotFound()
    {
        var result = await _service.GetAllStatDetailDtosByNameAsync("NonExistentStat");

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("No stats found", result.ErrorMessage);
    }

    [Fact]
    public async Task GetAllStatDetailDtosByShortNameAsync_ReturnsStatDetailDtosByShortName()
    {
        var result = await _service.GetAllStatDetailDtosByShortNameAsync("gsv");

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(2, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllStatDetailDtosByShortNameAsync_Error_WhenShortNameNotFound()
    {
        var result = await _service.GetAllStatDetailDtosByShortNameAsync("NonExistentStat");

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("No stats found", result.ErrorMessage);

    }

    [Fact]
    public async Task GetStatDetailDtoByIdAsync_ReturnsStatDetailDtoById()
    {
        var result = await _service.GetStatDetailDtoByIdAsync(1);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(1, result.Data?.Id);
    }

    [Fact]
    public async Task GetStatDetailDtoByIdAsync_Error_WhenIdNotFound()
    {
        var result = await _service.GetStatDetailDtoByIdAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Stat with Id 100 not found", result.ErrorMessage);
    }

    [Fact]
    public async Task GetStatByIdAsync_ReturnsStatById()
    {
        var result = await _service.GetStatByIdAsync(1);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(1, result.Data?.Id);
    }

    [Fact]
    public async Task GetStatByIdAsync_Error_WhenIdNotFound()
    {
        var result = await _service.GetStatByIdAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Stat with Id 100 not found", result.ErrorMessage);
    }

    [Fact]
    public async Task CreateStatAsync_ReturnsCreatedStat()
    {
        var result = await _service.CreateStatAsync(new StatForCreationDto
        {
            Name = "NewStat",
            ShortName = "NS"
        });

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal("NewStat", result.Data?.Name);
        Assert.Equal("NS", result.Data?.ShortName);

        if (result.Data != null) await _service.DeleteStatAsync(result.Data.Id);
    }

    [Fact]
    public async Task UpdateStatAsync_ReturnsUpdatedStat()
    {
        var result = await _service.UpdateStatAsync(1, new StatForUpdateDto
        {
            Name = "UpdatedStat",
            ShortName = "US"
        });

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal("UpdatedStat", result.Data?.Name);
        Assert.Equal("US", result.Data?.ShortName);
    }

    [Fact]
    public async Task UpdateStatAsync_Error_WhenIdNotFound()
    {
        var result = await _service.UpdateStatAsync(100, new StatForUpdateDto
        {
            Name = "UpdatedStat",
            ShortName = "US"
        });

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Stat with Id 100 not found", result.ErrorMessage);
    }

    [Fact]
    [Priority(2)]
    public async Task DeleteStatAsync_DeletesStat()
    {
        var result = await _service.DeleteStatAsync(3);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(3, result.Data?.Id);
    }

    [Fact]
    [Priority(2)]
    public async Task DeleteStatAsync_Error_WhenIdNotFound()
    {
        var result = await _service.DeleteStatAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Stat with Id 100 not found", result.ErrorMessage);
    }
}