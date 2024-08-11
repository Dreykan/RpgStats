namespace RpgStats.Dto.Tests;

public class StatValueWithCharacterObjectDtoTests
{
    [Fact]
    public void StatValueWithCharacterObjectDto_ShouldInitializeWithDefaultValues()
    {
        var dto = new StatValueWithCharacterObjectDto();

        Assert.Equal(0, dto.Id);
        Assert.Equal(0, dto.Level);
        Assert.Equal(0, dto.Value);
        Assert.Equal(0, dto.ContainedBonusNum);
        Assert.Equal(0, dto.ContainedBonusPercent);
        Assert.Null(dto.CharacterWithoutFkObjectsDto);
    }

    [Fact]
    public void StatValueWithCharacterObjectDto_ShouldSetAndGetProperties()
    {
        var characterDto = new CharacterWithoutFkObjectsDto();
        var dto = new StatValueWithCharacterObjectDto
        {
            Id = 1,
            Level = 10,
            Value = 100,
            ContainedBonusNum = 5,
            ContainedBonusPercent = 20,
            CharacterWithoutFkObjectsDto = characterDto
        };

        Assert.Equal(1, dto.Id);
        Assert.Equal(10, dto.Level);
        Assert.Equal(100, dto.Value);
        Assert.Equal(5, dto.ContainedBonusNum);
        Assert.Equal(20, dto.ContainedBonusPercent);
        Assert.Equal(characterDto, dto.CharacterWithoutFkObjectsDto);
    }

    [Fact]
    public void StatValueWithCharacterObjectDto_ShouldHandleNullCharacterWithoutFkObjectsDto()
    {
        var dto = new StatValueWithCharacterObjectDto
        {
            CharacterWithoutFkObjectsDto = null
        };

        Assert.Null(dto.CharacterWithoutFkObjectsDto);
    }
}