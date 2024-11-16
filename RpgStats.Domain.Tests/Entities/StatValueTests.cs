using RpgStats.Domain.Entities;

// ReSharper disable UseObjectOrCollectionInitializer

namespace RpgStats.Domain.Tests.Entities;

public class StatValueTests
{
    [Fact]
    public void StatValue_Id_ShouldBeSetAndRetrievedCorrectly()
    {
        var statValue = new StatValue();

        statValue.Id = 12345;

        Assert.Equal(12345, statValue.Id);
    }

    [Fact]
    public void StatValue_Level_ShouldBeSetAndRetrievedCorrectly()
    {
        var statValue = new StatValue();

        statValue.Level = 10;

        Assert.Equal(10, statValue.Level);
    }

    [Fact]
    public void StatValue_CharacterId_ShouldBeSetAndRetrievedCorrectly()
    {
        var statValue = new StatValue();

        statValue.CharacterId = 12345;

        Assert.Equal(12345, statValue.CharacterId);
    }

    [Fact]
    public void StatValue_Character_ShouldBeSetAndRetrievedCorrectly()
    {
        var statValue = new StatValue();

        statValue.Character = new Character { Id = 12345, Name = "TestCharacter" };

        Assert.Equal(12345, statValue.Character.Id);
        Assert.Equal("TestCharacter", statValue.Character.Name);
    }

    [Fact]
    public void StatValue_StatId_ShouldBeSetAndRetrievedCorrectly()
    {
        var statValue = new StatValue();

        statValue.StatId = 12345;

        Assert.Equal(12345, statValue.StatId);
    }

    [Fact]
    public void StatValue_Stat_ShouldBeSetAndRetrievedCorrectly()
    {
        var statValue = new StatValue();

        statValue.Stat = new Stat { Id = 12345, Name = "TestStat" };

        Assert.Equal(12345, statValue.Stat.Id);
        Assert.Equal("TestStat", statValue.Stat.Name);
    }

    [Fact]
    public void StatValue_Value_ShouldBeSetAndRetrievedCorrectly()
    {
        var statValue = new StatValue();

        statValue.Value = 100;

        Assert.Equal(100, statValue.Value);
    }

    [Fact]
    public void StatValue_ContainedBonusNum_ShouldBeSetAndRetrievedCorrectly()
    {
        var statValue = new StatValue();

        statValue.ContainedBonusNum = 5;

        Assert.Equal(5, statValue.ContainedBonusNum);
    }

    [Fact]
    public void StatValue_ContainedBonusPercent_ShouldBeSetAndRetrievedCorrectly()
    {
        var statValue = new StatValue();

        statValue.ContainedBonusPercent = 20;

        Assert.Equal(20, statValue.ContainedBonusPercent);
    }
}