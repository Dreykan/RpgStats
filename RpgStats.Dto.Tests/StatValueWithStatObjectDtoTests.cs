namespace RpgStats.Dto.Tests;

public class StatValueWithStatObjectDtoTests
{
    // TODO: Create all tests like this one.
    [Fact]
    public void Id_ShouldBeSetAndRetrievedCorrectly()
    {
        var dto = new StatValueWithStatObjectDto();
        dto.Id = 12345;
        Assert.Equal(12345, dto.Id);
    }

    [Fact]
    public void Level_ShouldBeSetAndRetrievedCorrectly()
    {
        var dto = new StatValueWithStatObjectDto { Level = 10 };
        Assert.Equal(10, dto.Level);
    }

    [Fact]
    public void StatWithoutFkObjectsDto_ShouldBeSetAndRetrievedCorrectly()
    {
        var statDto = new StatWithoutFkObjectsDto();
        var dto = new StatValueWithStatObjectDto { StatWithoutFkObjectsDto = statDto };
        Assert.Equal(statDto, dto.StatWithoutFkObjectsDto);
    }

    [Fact]
    public void Value_ShouldBeSetAndRetrievedCorrectly()
    {
        var dto = new StatValueWithStatObjectDto { Value = 100 };
        Assert.Equal(100, dto.Value);
    }

    [Fact]
    public void ContainedBonusNum_ShouldBeSetAndRetrievedCorrectly()
    {
        var dto = new StatValueWithStatObjectDto { ContainedBonusNum = 5 };
        Assert.Equal(5, dto.ContainedBonusNum);
    }

    [Fact]
    public void ContainedBonusPercent_ShouldBeSetAndRetrievedCorrectly()
    {
        var dto = new StatValueWithStatObjectDto { ContainedBonusPercent = 20 };
        Assert.Equal(20, dto.ContainedBonusPercent);
    }

    [Fact]
    public void StatWithoutFkObjectsDto_ShouldBeNullByDefault()
    {
        var dto = new StatValueWithStatObjectDto();
        Assert.Null(dto.StatWithoutFkObjectsDto);
    }
}