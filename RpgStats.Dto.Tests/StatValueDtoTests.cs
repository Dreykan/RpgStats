namespace RpgStats.Dto.Tests;

public class StatValueDtoTests
{
    [Fact]
    public void StatValueDto_Should_Have_Default_Values()
    {
        var dto = new StatValueDto();
        Assert.Equal(0, dto.Id);
        Assert.Equal(0, dto.Level);
        Assert.Equal(0, dto.CharacterId);
        Assert.Equal(0, dto.StatId);
        Assert.Equal(0, dto.Value);
        Assert.Equal(0, dto.ContainedBonusNum);
        Assert.Equal(0, dto.ContainedBonusPercent);
    }

    [Fact]
    public void StatValueDto_Should_Set_And_Get_Properties()
    {
        var dto = new StatValueDto
        {
            Id = 1,
            Level = 10,
            CharacterId = 100,
            StatId = 200,
            Value = 50,
            ContainedBonusNum = 5,
            ContainedBonusPercent = 20
        };

        Assert.Equal(1, dto.Id);
        Assert.Equal(10, dto.Level);
        Assert.Equal(100, dto.CharacterId);
        Assert.Equal(200, dto.StatId);
        Assert.Equal(50, dto.Value);
        Assert.Equal(5, dto.ContainedBonusNum);
        Assert.Equal(20, dto.ContainedBonusPercent);
    }

    [Fact]
    public void StatValueDto_Should_Handle_Max_Values()
    {
        var dto = new StatValueDto
        {
            Id = long.MaxValue,
            Level = int.MaxValue,
            CharacterId = long.MaxValue,
            StatId = long.MaxValue,
            Value = int.MaxValue,
            ContainedBonusNum = int.MaxValue,
            ContainedBonusPercent = int.MaxValue
        };

        Assert.Equal(long.MaxValue, dto.Id);
        Assert.Equal(int.MaxValue, dto.Level);
        Assert.Equal(long.MaxValue, dto.CharacterId);
        Assert.Equal(long.MaxValue, dto.StatId);
        Assert.Equal(int.MaxValue, dto.Value);
        Assert.Equal(int.MaxValue, dto.ContainedBonusNum);
        Assert.Equal(int.MaxValue, dto.ContainedBonusPercent);
    }

    [Fact]
    public void StatValueDto_Should_Handle_Min_Values()
    {
        var dto = new StatValueDto
        {
            Id = long.MinValue,
            Level = int.MinValue,
            CharacterId = long.MinValue,
            StatId = long.MinValue,
            Value = int.MinValue,
            ContainedBonusNum = int.MinValue,
            ContainedBonusPercent = int.MinValue
        };

        Assert.Equal(long.MinValue, dto.Id);
        Assert.Equal(int.MinValue, dto.Level);
        Assert.Equal(long.MinValue, dto.CharacterId);
        Assert.Equal(long.MinValue, dto.StatId);
        Assert.Equal(int.MinValue, dto.Value);
        Assert.Equal(int.MinValue, dto.ContainedBonusNum);
        Assert.Equal(int.MinValue, dto.ContainedBonusPercent);
    }
}