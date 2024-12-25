using RpgStats.Dto;
using Xunit.Priority;

namespace RpgStats.Services.Tests;

[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public class StatValueServiceTests : IClassFixture<DatabaseFixture>
{
    private readonly StatValueService _service;

    public StatValueServiceTests(DatabaseFixture fixture)
    {
        _service = new StatValueService(fixture.Context);
    }

    [Fact]
    [Priority(1)]
    public async Task GetAllStatValuesAsync_ReturnsAllStatValues()
    {
        var result = await _service.GetAllStatValuesAsync();

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(12, result.Data?.Count);
    }

    [Fact]
    [Priority(1)]
    public async Task GetAllStatValuesByCharacterIdAsync_ReturnsStatValuesByCharacterId()
    {
        var result = await _service.GetAllStatValuesByCharacterIdAsync(1);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(4, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllStatValuesByCharacterIdAsync_Error_WhenCharacterIdNotFound()
    {
        var result = await _service.GetAllStatValuesByCharacterIdAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("No StatValues found", result.ErrorMessage);
    }

    [Fact]
    public async Task GetAllStatValuesByStatIdAsync_ReturnsStatValuesByStatId()
    {
        var result = await _service.GetAllStatValuesByStatIdAsync(1);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(3, result.Data?.Count);
    }

    [Fact]
    public async Task GetAllStatValuesByStatIdAsync_Error_WhenStatIdNotFound()
    {
        var result = await _service.GetAllStatValuesByStatIdAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("No StatValues found", result.ErrorMessage);
    }

    [Fact]
    public async Task GetStatValueByIdAsync_ReturnsStatValueById()
    {
        var result = await _service.GetStatValueByIdAsync(1);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(1, result.Data?.Id);
    }

    [Fact]
    public async Task GetStatValueByIdAsync_Error_WhenIdNotFound()
    {
        var result = await _service.GetStatValueByIdAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("StatValue with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    public async Task CreateStatValueAsync_ReturnsStatValueDto()
    {
        var statValueForCreationDto = new StatValueForCreationDto
        {
            Value = 100,
            ContainedBonusNum = 10,
            ContainedBonusPercent = 5,
            Level = 99
        };

        var result = await _service.CreateStatValueAsync(1, 1, statValueForCreationDto);

        Assert.NotNull(result);
        Assert.Equal(1, result.Data?.CharacterId);
        Assert.Equal(1, result.Data?.StatId);
        Assert.Equal(100, result.Data?.Value);
        Assert.Equal(10, result.Data?.ContainedBonusNum);
        Assert.Equal(5, result.Data?.ContainedBonusPercent);
        Assert.Equal(99, result.Data?.Level);

        if (result.Data != null) await _service.DeleteStatValueAsync(result.Data.Id);
    }

    [Fact]
    public async Task CreateStatValueAsync_Error_WhenCharacterIdNotFound()
    {
        var statValueForCreationDto = new StatValueForCreationDto
        {
            Value = 100,
            ContainedBonusNum = 10,
            ContainedBonusPercent = 5,
            Level = 99
        };

        var result = await _service.CreateStatValueAsync(100, 1, statValueForCreationDto);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Character with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    public async Task CreateStatValueAsync_Error_WhenStatIdNotFound()
    {
        var statValueForCreationDto = new StatValueForCreationDto
        {
            Value = 100,
            ContainedBonusNum = 10,
            ContainedBonusPercent = 5,
            Level = 99
        };

        var result = await _service.CreateStatValueAsync(1, 100, statValueForCreationDto);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Stat with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    public async Task UpdateStatValueAsync_ReturnsStatValueDto()
    {
        var statValueForUpdateDto = new StatValueForUpdateDto
        {
            Value = 200,
            ContainedBonusNum = 20,
            ContainedBonusPercent = 10,
            Level = 199
        };

        var statValue = await _service.UpdateStatValueAsync(1, 1, 1, statValueForUpdateDto);

        Assert.NotNull(statValue);
        Assert.Equal(1, statValue.Data?.CharacterId);
        Assert.Equal(1, statValue.Data?.StatId);
        Assert.Equal(200, statValue.Data?.Value);
        Assert.Equal(20, statValue.Data?.ContainedBonusNum);
        Assert.Equal(10, statValue.Data?.ContainedBonusPercent);
        Assert.Equal(199, statValue.Data?.Level);
    }

    [Fact]
    public async Task UpdateStatValueAsync_Error_WhenIdNotFound()
    {
        var statValueForUpdateDto = new StatValueForUpdateDto
        {
            Value = 200,
            ContainedBonusNum = 20,
            ContainedBonusPercent = 10,
            Level = 199
        };

        var result = await _service.UpdateStatValueAsync(100, 1, 1, statValueForUpdateDto);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("StatValue with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    public async Task UpdateStatValueAsync_Error_WhenCharacterIdNotFound()
    {
        var statValueForUpdateDto = new StatValueForUpdateDto
        {
            Value = 200,
            ContainedBonusNum = 20,
            ContainedBonusPercent = 10,
            Level = 199
        };

        var result = await _service.UpdateStatValueAsync(1, 100, 1, statValueForUpdateDto);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Character with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    public async Task UpdateStatValueAsync_Error_WhenStatIdNotFound()
    {
        var statValueForUpdateDto = new StatValueForUpdateDto
        {
            Value = 200,
            ContainedBonusNum = 20,
            ContainedBonusPercent = 10,
            Level = 199
        };

        var result = await _service.UpdateStatValueAsync(1, 1, 100, statValueForUpdateDto);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("Stat with ID 100 not found", result.ErrorMessage);
    }

    [Fact]
    [Priority(2)]
    public async Task DeleteStatValueAsync_DeletesStatValue()
    {
        var result = await _service.DeleteStatValueAsync(3);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(3, result.Data?.Id);
    }

    [Fact]
    [Priority(2)]
    public async Task DeleteStatValueAsync_Error_WhenIdNotFound()
    {
        var result = await _service.DeleteStatValueAsync(100);

        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal("StatValue with ID 100 not found", result.ErrorMessage);
    }
}