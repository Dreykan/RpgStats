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
        Assert.Equal(6, result.Count);
    }

    [Fact]
    public async Task GetAllStatsByNameAsync_ReturnsStatsByName()
    {
        var result = await _service.GetAllStatsByNameAsync("good");

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetAllStatsByNameAsync_ReturnsEmptyList_WhenNameNotFound()
    {
        var result = await _service.GetAllStatsByNameAsync("NonExistentStat");

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllStatsByShortNameAsync_ReturnsStatsByShortName()
    {
        var result = await _service.GetAllStatsByShortNameAsync("gsv");

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetAllStatsByShortNameAsync_ReturnEmptyList_WhenShortNameNotFound()
    {
        var result = await _service.GetAllStatsByShortNameAsync("NonExistentStat");

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    [Priority(1)]
    public async Task GetAllStatsByGameIdAsync_ReturnsStatsByGameId()
    {
        var result = await _service.GetAllStatsByGameIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(4, result.Count);
    }

    [Fact]
    public async Task GetAllStatsByGameIdAsync_Error_WhenGameIdNotFound()
    {
        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _service.GetAllStatsByGameIdAsync(100));
    }

    [Fact]
    public async Task GetStatByIdAsync_ReturnsStatById()
    {
        var result = await _service.GetStatByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetStatByIdAsync_ReturnsEmptyDto_WhenIdNotFound()
    {
        var result = await _service.GetStatByIdAsync(100);

        Assert.Null(result);
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
        Assert.Equal("NewStat", result.Name);
        Assert.Equal("NS", result.ShortName);

        await _service.DeleteStatAsync(result.Id);
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
        Assert.Equal("UpdatedStat", result.Name);
        Assert.Equal("US", result.ShortName);
    }

    [Fact]
    public async Task UpdateStatAsync_Error_WhenIdNotFound()
    {
        var statForUpdate = new StatForUpdateDto
        {
            Name = "UpdatedStat",
            ShortName = "US"
        };

        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _service.UpdateStatAsync(100, statForUpdate));
    }

    [Fact]
    [Priority(2)]
    public async Task DeleteStatAsync_DeletesStat()
    {
        var result = await _service.DeleteStatAsync(3);

        Assert.NotNull(result);
        Assert.Equal(3, result.Id);
    }

    [Fact]
    [Priority(2)]
    public async Task DeleteStatAsync_Error_WhenIdNotFound()
    {
        await Assert.ThrowsAsync<ArgumentException>(async () =>
            await _service.DeleteStatAsync(100));
    }
}