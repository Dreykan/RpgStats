using RpgStats.Domain.Exceptions;
using RpgStats.Dto;
using Xunit.Priority;

namespace RpgStats.Services.Tests;

[Collection("Database collection")]
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
        Assert.Equal(16, result.Count);
    }

    [Fact]
    [Priority(1)]
    public async Task GetAllStatValuesByCharacterIdAsync_ReturnsStatValuesByCharacterId()
    {
        var result = await _service.GetAllStatValuesByCharacterIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(4, result.Count);
    }

    [Fact]
    public async Task GetAllStatValuesByCharacterIdAsync_Error_WhenCharacterIdNotFound()
    {
        await Assert.ThrowsAsync<CharacterNotFoundException>(async () =>
        {
            await _service.GetAllStatValuesByCharacterIdAsync(100);
        });
    }

    [Fact]
    [Priority(1)]
    public async Task GetAllStatValuesByStatIdAsync_ReturnsStatValuesByStatId()
    {
        var result = await _service.GetAllStatValuesByStatIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(4, result.Count);
    }

    [Fact]
    public async Task GetAllStatValuesByStatIdAsync_Error_WhenStatIdNotFound()
    {
        await Assert.ThrowsAsync<StatNotFoundException>(async () =>
        {
            await _service.GetAllStatValuesByStatIdAsync(100);
        });
    }
    
    [Fact]
    public async Task GetHighestLevelByCharactersAsync_ReturnsHighestLevelByCharacterId()
    {
        var result = await _service.GetHighestLevelByCharactersAsync(new List<long>{1, 2, 3});

        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
        Assert.Equal(199, result[1]);
        Assert.Equal(6, result[2]);
        Assert.Equal(9, result[3]);
    }
    
    [Fact]
    public async Task GetHighestLevelByCharactersAsync_Error_WhenListIsEmpty()
    {
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await _service.GetHighestLevelByCharactersAsync(new List<long>());
        });
    }

    [Fact]
    public async Task GetHighestLevelByCharactersAsync_Error_WhenCharacterNotFound()
    {
        await Assert.ThrowsAsync<CharacterNotFoundException>(async () =>
        {
            await _service.GetHighestLevelByCharactersAsync(new List<long>{1, 100});
        });
    }

    [Fact]
    public async Task GetHighestLevelByCharactersAsync_ReturnsZero_WhenCharacterHasNoStatValues()
    {
        var result = await _service.GetHighestLevelByCharactersAsync(new List<long>{4});

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(0, result[4]);
    }

    [Fact]
    [Priority(1)]
    public async Task GetStatValueByIdAsync_ReturnsStatValueById()
    {
        var result = await _service.GetStatValueByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetStatValueByIdAsync_ReturnsEmptyDto_WhenIdNotFound()
    {
        var result = await _service.GetStatValueByIdAsync(100);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateStatValueAsync_ReturnsStatValueDto()
    {
        var statValueForCreationDto = new StatValueForCreationDto
        {
            Value = 100,
            ContainedBonusNum = 10,
            ContainedBonusPercent = 5,
            Level = 99,
            CharacterId = 1,
            StatId = 1
        };

        var result = await _service.CreateStatValueAsync(statValueForCreationDto);

        Assert.NotNull(result);
        Assert.Equal(1, result.CharacterId);
        Assert.Equal(1, result.StatId);
        Assert.Equal(100, result.Value);
        Assert.Equal(10, result.ContainedBonusNum);
        Assert.Equal(5, result.ContainedBonusPercent);
        Assert.Equal(99, result.Level);

        await _service.DeleteStatValueAsync(result.Id);
    }

    [Fact]
    public async Task CreateStatValueAsync_Error_WhenCharacterIdNotFound()
    {
        var statValueForCreationDto = new StatValueForCreationDto
        {
            Value = 100,
            ContainedBonusNum = 10,
            ContainedBonusPercent = 5,
            Level = 99,
            CharacterId = 100,
            StatId = 1
        };

        await Assert.ThrowsAsync<CharacterNotFoundException>(async () =>
        {
            await _service.CreateStatValueAsync(statValueForCreationDto);
        });
    }

    [Fact]
    public async Task CreateStatValueAsync_Error_WhenStatIdNotFound()
    {
        var statValueForCreationDto = new StatValueForCreationDto
        {
            Value = 100,
            ContainedBonusNum = 10,
            ContainedBonusPercent = 5,
            Level = 99,
            CharacterId = 1,
            StatId = 100
        };

        await Assert.ThrowsAsync<StatNotFoundException>(async () =>
        {
            await _service.CreateStatValueAsync(statValueForCreationDto);
        });
    }

    [Fact]
    public async Task CreateStatValueAsync_Error_WhenDuplicateExists()
    {
        var statValueForCreationDto = new StatValueForCreationDto
        {
            Value = 100,
            ContainedBonusNum = 10,
            ContainedBonusPercent = 5,
            Level = 5,
            CharacterId = 2,
            StatId = 1
        };

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await _service.CreateStatValueAsync(statValueForCreationDto);
        });
    }

    [Fact]
    public async Task CreateStatValuesAsync_ReturnsStatValuesDto()
    {
        var statValuesForCreationDto = new List<StatValueForCreationDto>
        {
            new()
            {
                Value = 100,
                ContainedBonusNum = 10,
                ContainedBonusPercent = 5,
                Level = 98,
                CharacterId = 1,
                StatId = 1
            },
            new()
            {
                Value = 200,
                ContainedBonusNum = 20,
                ContainedBonusPercent = 10,
                Level = 198,
                CharacterId = 1,
                StatId = 1
            }
        };

        var result = await _service.CreateMultipleStatValuesAsync(statValuesForCreationDto);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

        foreach (var statValue in result)
        {
            await _service.DeleteStatValueAsync(statValue.Id);
        }
    }

    [Fact]
    public async Task CreateStatValuesAsync_Error_WhenDuplicateExists()
    {
        var statValuesForCreationDto = new List<StatValueForCreationDto>
        {
            new()
            {
                Value = 100,
                ContainedBonusNum = 10,
                ContainedBonusPercent = 5,
                Level = 1,
                CharacterId = 1,
                StatId = 1
            },
            new()
            {
                Value = 200,
                ContainedBonusNum = 20,
                ContainedBonusPercent = 10,
                Level = 5,
                CharacterId = 2,
                StatId = 1
            }
        };
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await _service.CreateMultipleStatValuesAsync(statValuesForCreationDto);
        });
    }

    [Fact]
    [Priority(1)]
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
    public async Task UpdateStatValueAsync_Error_WhenIdNotFound()
    {
        var statValueForUpdateDto = new StatValueForUpdateDto
        {
            Value = 200,
            ContainedBonusNum = 20,
            ContainedBonusPercent = 10,
            Level = 199
        };

        await Assert.ThrowsAsync<StatValueNotFoundException>(async () =>
        {
            await _service.UpdateStatValueAsync(100, 1, 1, statValueForUpdateDto);
        });
    }

    [Fact]
    [Priority(1)]
    public async Task UpdateStatValueAsync_Error_WhenCharacterIdNotFound()
    {
        var statValueForUpdateDto = new StatValueForUpdateDto
        {
            Value = 200,
            ContainedBonusNum = 20,
            ContainedBonusPercent = 10,
            Level = 199
        };

        await Assert.ThrowsAsync<CharacterNotFoundException>(async () =>
        {
            await _service.UpdateStatValueAsync(1, 100, 1, statValueForUpdateDto);
        });
    }

    [Fact]
    [Priority(1)]
    public async Task UpdateStatValueAsync_Error_WhenStatIdNotFound()
    {
        var statValueForUpdateDto = new StatValueForUpdateDto
        {
            Value = 200,
            ContainedBonusNum = 20,
            ContainedBonusPercent = 10,
            Level = 199
        };

        await Assert.ThrowsAsync<StatNotFoundException>(async () =>
        {
            await _service.UpdateStatValueAsync(1, 1, 100, statValueForUpdateDto);
        });
    }

    [Fact]
    [Priority(2)]
    public async Task DeleteStatValueAsync_DeletesStatValue()
    {
        var result = await _service.DeleteStatValueAsync(3);

        Assert.NotNull(result);
        Assert.Equal(3, result.Id);
    }

    [Fact]
    [Priority(2)]
    public async Task DeleteStatValueAsync_Error_WhenIdNotFound()
    {
        await Assert.ThrowsAsync<StatValueNotFoundException>(async () =>
        {
            await _service.DeleteStatValueAsync(100);
        });
    }

    [Fact]
    [Priority(3)]
    public async Task DeleteStatValuesByCharacterIdAndLevelAsync_DeletesStatValuesByCharacterIdAndLevel()
    {
        var result = await _service.DeleteStatValuesByCharacterIdAndLevelAsync(1, 1);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    [Priority(3)]
    public async Task DeleteStatValuesByCharacterIdAndLevelAsync_Error_WhenCharacterIdNotFound()
    {
        await Assert.ThrowsAsync<CharacterNotFoundException>(async () =>
        {
            await _service.DeleteStatValuesByCharacterIdAndLevelAsync(100, 1);
        });
    }

    [Fact]
    [Priority(3)]
    public async Task DeleteStatValuesByCharacterIdAndLevelAsync_Error_WhenNoStatValuesFound()
    {
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await _service.DeleteStatValuesByCharacterIdAndLevelAsync(1, 1000);
        });
    }
}