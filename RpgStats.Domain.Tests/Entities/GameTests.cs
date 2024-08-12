using RpgStats.Domain.Entities;
// ReSharper disable UseObjectOrCollectionInitializer

namespace RpgStats.Domain.Tests.Entities;

public class GameTests
{
    [Fact]
    public void Game_Id_ShouldBeSetAndRetrievedCorrectly()
    {
        var game = new Game();
            
        game.Id = 12345;
            
        Assert.Equal(12345, game.Id);
    }
        
    [Fact]
    public void Game_Name_ShouldBeSetAndRetrievedCorrectly()
    {
        var game = new Game();
            
        game.Name = "TestGame";
            
        Assert.Equal("TestGame", game.Name);
    }
        
    [Fact]
    public void Game_Picture_ShouldBeSetAndRetrievedCorrectly()
    {
        var game = new Game();
            
        game.Picture = [0x01, 0x02, 0x03];
            
        Assert.Equal(new byte[] { 0x01, 0x02, 0x03 }, game.Picture);
    }
        
    [Fact]
    public void Game_PlatformGames_ShouldBeSetAndRetrievedCorrectly()
    {
        var game = new Game();
            
        game.PlatformGames = new List<PlatformGame> { new() { PlatformId = 12345 } };
            
        Assert.Equal(12345, game.PlatformGames.First().PlatformId);
    }
        
    [Fact]
    public void Game_GameStats_ShouldBeSetAndRetrievedCorrectly()
    {
        var game = new Game();
            
        game.GameStats = new List<GameStat> { new() { StatId = 12345 } };
            
        Assert.Equal(12345, game.GameStats.First().StatId);
    }
        
    [Fact]
    public void Game_Characters_ShouldBeSetAndRetrievedCorrectly()
    {
        var game = new Game();
            
        game.Characters = new List<Character> { new() { Id = 12345, Name = "TestCharacter" } };
            
        Assert.Equal(12345, game.Characters.First().Id);
        Assert.Equal("TestCharacter", game.Characters.First().Name);
    }

    [Fact]
    public void Game_Name_RequiredValidation()
    {
        var game = new Game();

        var validationResults = TestHelper.ValidateModel(game);
            
        Assert.NotEmpty(validationResults);
        Assert.Contains(validationResults,
            v => v.ErrorMessage != null && v.ErrorMessage.Contains("A name for the game is required."));
    }

    [Fact]
    public void Game_Name_LengthValidation()
    {
        var game = new Game
        {
            Name = new string('a', 101)
        };
            
        var validationResults = TestHelper.ValidateModel(game);

        Assert.NotEmpty(validationResults);
        Assert.Contains(validationResults,
            v => v.ErrorMessage != null && v.ErrorMessage.Contains("The name for the game can't be longer than 100 characters."));
    }
}