using RpgStats.Domain.Exceptions;
using RpgStats.Dto;

namespace RpgStats.Services.Tests;

public class StatValueServiceTests : IClassFixture<DatabaseFixture>
{
    private readonly StatValueService _service;

    public StatValueServiceTests(DatabaseFixture fixture)
    {
        _service = new StatValueService(fixture.Context);
    }

    [Fact]
    public async Task GetAllStatValuesAsync_ReturnsAllStatValues()
    {
        var statValues = await _service.GetAllStatValuesAsync();

        Assert.NotNull(statValues);
        Assert.Equal(12, statValues.Count);
    }

    [Fact]
    public async Task GetAllStatValuesByCharacterIdAsync_ReturnsStatValuesByCharacterId()
    {
        var statValues = await _service.GetAllStatValuesByCharacterIdAsync(1);

        Assert.NotNull(statValues);
        Assert.Equal(4, statValues.Count);
    }

    [Fact]
    public async Task GetAllStatValuesByCharacterIdAsync_ReturnsEmptyList()
    {
        var statValues = await _service.GetAllStatValuesByCharacterIdAsync(100);

        Assert.NotNull(statValues);
        Assert.Empty(statValues);
    }

    [Fact]
    public async Task GetAllStatValuesByStatIdAsync_ReturnsStatValuesByStatId()
    {
        var statValues = await _service.GetAllStatValuesByStatIdAsync(1);

        Assert.NotNull(statValues);
        Assert.Equal(3, statValues.Count);
    }

    [Fact]
    public async Task GetAllStatValuesByStatIdAsync_ReturnsEmptyList()
    {
        var statValues = await _service.GetAllStatValuesByStatIdAsync(100);

        Assert.NotNull(statValues);
        Assert.Empty(statValues);
    }

    [Fact]
    public async Task GetStatValueByIdAsync_ReturnsStatValueById()
    {
        var statValue = await _service.GetStatValueByIdAsync(1);

        Assert.NotNull(statValue);
        Assert.Equal(1, statValue.Id);
    }

    [Fact]
    public async Task GetStatValueByIdAsync_ThrowsStatValueNotFoundExemption()
    {
        await Assert.ThrowsAsync<StatValueNotFoundException>(() => _service.GetStatValueByIdAsync(100));
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

        var statValue = await _service.CreateStatValueAsync(1, 1, statValueForCreationDto);

        Assert.NotNull(statValue);
        Assert.Equal(1, statValue.CharacterId);
        Assert.Equal(1, statValue.StatId);
        Assert.Equal(100, statValue.Value);
        Assert.Equal(10, statValue.ContainedBonusNum);
        Assert.Equal(5, statValue.ContainedBonusPercent);
        Assert.Equal(99, statValue.Level);

        await _service.DeleteStatValueAsync(statValue.Id);
    }

    [Fact]
    public async Task CreateStatValueAsync_ThrowsCharacterNotFoundExemption()
    {
        var statValueForCreationDto = new StatValueForCreationDto
        {
            Value = 100,
            ContainedBonusNum = 10,
            ContainedBonusPercent = 5,
            Level = 99
        };

        await Assert.ThrowsAsync<CharacterNotFoundException>(() =>
            _service.CreateStatValueAsync(100, 1, statValueForCreationDto));
    }

    [Fact]
    public async Task CreateStatValueAsync_ThrowsStatNotFoundExemption()
    {
        var statValueForCreationDto = new StatValueForCreationDto
        {
            Value = 100,
            ContainedBonusNum = 10,
            ContainedBonusPercent = 5,
            Level = 99
        };

        await Assert.ThrowsAsync<StatNotFoundException>(() =>
            _service.CreateStatValueAsync(1, 100, statValueForCreationDto));
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
        Assert.Equal(1, statValue.CharacterId);
        Assert.Equal(1, statValue.StatId);
        Assert.Equal(200, statValue.Value);
        Assert.Equal(20, statValue.ContainedBonusNum);
        Assert.Equal(10, statValue.ContainedBonusPercent);
        Assert.Equal(199, statValue.Level);
    }

    [Fact]
    public async Task UpdateStatValueAsync_ThrowsStatValueNotFoundExemption()
    {
        var statValueForUpdateDto = new StatValueForUpdateDto
        {
            Value = 200,
            ContainedBonusNum = 20,
            ContainedBonusPercent = 10,
            Level = 199
        };

        await Assert.ThrowsAsync<StatValueNotFoundException>(() =>
            _service.UpdateStatValueAsync(100, 1, 1, statValueForUpdateDto));
    }

    [Fact]
    public async Task UpdateStatValueAsync_ThrowsCharacterNotFoundExemption()
    {
        var statValueForUpdateDto = new StatValueForUpdateDto
        {
            Value = 200,
            ContainedBonusNum = 20,
            ContainedBonusPercent = 10,
            Level = 199
        };

        await Assert.ThrowsAsync<CharacterNotFoundException>(() =>
            _service.UpdateStatValueAsync(1, 100, 1, statValueForUpdateDto));
    }

    [Fact]
    public async Task UpdateStatValueAsync_ThrowsStatNotFoundExemption()
    {
        var statValueForUpdateDto = new StatValueForUpdateDto
        {
            Value = 200,
            ContainedBonusNum = 20,
            ContainedBonusPercent = 10,
            Level = 199
        };

        await Assert.ThrowsAsync<StatNotFoundException>(() =>
            _service.UpdateStatValueAsync(1, 1, 100, statValueForUpdateDto));
    }

    [Fact]
    public async Task DeleteStatValueAsync_DoesNothing_WhenStatValueIdIsNotFound()
    {
        await _service.DeleteStatValueAsync(100);

        var statValues = await _service.GetAllStatValuesAsync();

        Assert.NotNull(statValues);
        Assert.Equal(12, statValues.Count);
    }

    [Fact]
    public async Task DeleteStatValueAsync_DoesNothing_WhenStatValueIdIsZero()
    {
        await _service.DeleteStatValueAsync(0);

        var statValues = await _service.GetAllStatValuesAsync();

        Assert.NotNull(statValues);
        Assert.Equal(12, statValues.Count);
    }

    [Fact]
    public async Task DeleteStatValueAsync_DoesNothing_WhenStatValueIdIsNegative()
    {
        await _service.DeleteStatValueAsync(-1);

        var statValues = await _service.GetAllStatValuesAsync();

        Assert.NotNull(statValues);
        Assert.Equal(12, statValues.Count);
    }

    [Fact]
    public async Task DeleteStatValueAsync_DeletesStatValue()
    {
        var statValueForCreationDto = new StatValueForCreationDto
        {
            Value = 100,
            ContainedBonusNum = 10,
            ContainedBonusPercent = 5,
            Level = 99
        };

        var statValue = await _service.CreateStatValueAsync(1, 1, statValueForCreationDto);

        if (statValue != null) await _service.DeleteStatValueAsync(statValue.Id);

        var statValues = await _service.GetAllStatValuesAsync();

        Assert.NotNull(statValues);
        Assert.Equal(12, statValues.Count);
    }
}