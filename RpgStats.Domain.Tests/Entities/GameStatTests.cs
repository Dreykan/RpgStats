using RpgStats.Domain.Entities;

// ReSharper disable UseObjectOrCollectionInitializer

namespace RpgStats.Domain.Tests.Entities;

public class GameStatTests
{
    [Fact]
    public void GameStat_Id_ShouldBeSetAndRetrievedCorrectly()
    {
        var gameStat = new GameStat();

        gameStat.Id = 12345;

        Assert.Equal(12345, gameStat.Id);
    }

    [Fact]
    public void GameStat_SortIndex_ShouldBeSetAndRetrievedCorrectly()
    {
        var gameStat = new GameStat();

        gameStat.SortIndex = 12345;

        Assert.Equal(12345, gameStat.SortIndex);
    }

    [Fact]
    public void GameStat_CustomStatName_ShouldBeSetAndRetrievedCorrectly()
    {
        var gameStat = new GameStat();

        gameStat.CustomStatName = "TestCustomStatName";

        Assert.Equal("TestCustomStatName", gameStat.CustomStatName);
    }

    [Fact]
    public void GameStat_CustomStatShortName_ShouldBeSetAndRetrievedCorrectly()
    {
        var gameStat = new GameStat();

        gameStat.CustomStatShortName = "TestShortName";

        Assert.Equal("TestShortName", gameStat.CustomStatShortName);
    }

    [Fact]
    public void GameStat_GameId_ShouldBeSetAndRetrievedCorrectly()
    {
        var gameStat = new GameStat();

        gameStat.GameId = 12345;

        Assert.Equal(12345, gameStat.GameId);
    }

    [Fact]
    public void GameStat_Game_ShouldBeSetAndRetrievedCorrectly()
    {
        var gameStat = new GameStat();

        gameStat.Game = new Game { Id = 12345, Name = "TestGame" };

        Assert.Equal(12345, gameStat.Game.Id);
        Assert.Equal("TestGame", gameStat.Game.Name);
    }

    [Fact]
    public void GameStat_StatId_ShouldBeSetAndRetrievedCorrectly()
    {
        var gameStat = new GameStat();

        gameStat.StatId = 12345;

        Assert.Equal(12345, gameStat.StatId);
    }

    [Fact]
    public void GameStat_Stat_ShouldBeSetAndRetrievedCorrectly()
    {
        var gameStat = new GameStat();

        gameStat.Stat = new Stat { Id = 12345, Name = "TestStat" };

        Assert.Equal(12345, gameStat.Stat.Id);
        Assert.Equal("TestStat", gameStat.Stat.Name);
    }

    [Fact]
    public void GameStat_CustomStatName_RequiredValidation()
    {
        var gameStat = new GameStat();

        var validationResults = TestHelper.ValidateModel(gameStat);

        Assert.Contains(validationResults, x => x.MemberNames.Contains("CustomStatName"));
    }

    [Fact]
    public void GameStat_CustomStatShortName_RequiredValidation()
    {
        var gameStat = new GameStat();

        var validationResults = TestHelper.ValidateModel(gameStat);

        Assert.Contains(validationResults, x => x.MemberNames.Contains("CustomStatShortName"));
    }

    [Fact]
    public void GameStat_CustomStatName_MaxLengthValidation()
    {
        var gameStat = new GameStat { CustomStatName = new string('A', 51) };

        var validationResults = TestHelper.ValidateModel(gameStat);

        Assert.Contains(validationResults, x => x.MemberNames.Contains("CustomStatName"));
    }

    [Fact]
    public void GameStat_CustomStatShortName_MaxLengthValidation()
    {
        var gameStat = new GameStat { CustomStatShortName = new string('A', 9) };

        var validationResults = TestHelper.ValidateModel(gameStat);

        Assert.Contains(validationResults, x => x.MemberNames.Contains("CustomStatShortName"));
    }
}