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
    public void GameStat_GameId_RequiredValidation()
    {
        var gameStat = new GameStat
        {
            StatId = 1
        };

        var validationResults = TestHelper.ValidateModel(gameStat);
            
        Assert.NotEmpty(validationResults);
        Assert.Contains(validationResults,
            v => v.ErrorMessage != null && v.ErrorMessage.Contains("An entry for the column GameId is required."));
    }

    [Fact]
    public void GameStat_StatId_RequiredValidation()
    {
        var gameStat = new GameStat
        {
            GameId = 1
        };
            
        var validationResults = TestHelper.ValidateModel(gameStat);

        Assert.NotEmpty(validationResults);
        Assert.Contains(validationResults,
            v => v.ErrorMessage != null && v.ErrorMessage.Contains("An entry for the column StatId is required."));
    }
}