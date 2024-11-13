using RpgStats.Domain.Entities;
// ReSharper disable UseObjectOrCollectionInitializer

namespace RpgStats.Domain.Tests.Entities;

public class CharacterTests
{
    [Fact]
    public void Character_Id_ShouldBeSetAndRetrievedCorrectly()
    {
        var character = new Character
        {
            Name = "TestCharacter"
        };
            
        character.Id = 12345;
            
        Assert.Equal(12345, character.Id);
    }
        
    [Fact]
    public void Character_Name_ShouldBeSetAndRetrievedCorrectly()
    {
        var character = new Character
        {
            Name = "TestCharacter"
        };
            
        Assert.Equal("TestCharacter", character.Name);
    }
        
    [Fact]
    public void Character_Picture_ShouldBeSetAndRetrievedCorrectly()
    {
        var character = new Character
        {
            Name = "TestCharacter"
        };
            
        character.Picture = [0x01, 0x02, 0x03];
            
        Assert.Equal([0x01, 0x02, 0x03], character.Picture);
    }
        
    [Fact]
    public void Character_GameId_ShouldBeSetAndRetrievedCorrectly()
    {
        var character = new Character
        {
            Name = "TestCharacter"
        };
            
        character.GameId = 12345;
            
        Assert.Equal(12345, character.GameId);
    }
        
    [Fact]
    public void Character_Game_ShouldBeSetAndRetrievedCorrectly()
    {
        var character = new Character
        {
            Name = "TestCharacter"
        };
            
        character.Game = new Game { Id = 12345, Name = "TestGame" };
            
        Assert.Equal(12345, character.Game.Id);
        Assert.Equal("TestGame", character.Game.Name);
    }
        
    [Fact]
    public void Character_StatValues_ShouldBeSetAndRetrievedCorrectly()
    {
        var character = new Character
        {
            Name = "TestCharacter"
        };
            
        character.StatValues = new List<StatValue> { new() { Id = 12345, Value = 10 } };
            
        Assert.Equal( 12345, character.StatValues.First().Id);
        Assert.Equal(10, character.StatValues.First().Value);
    }

    [Fact]
    public void Character_Name_LengthValidation()
    {
        var character = new Character
        {
            Name = new string('a', 61),
            GameId = 1
        };
            
        var validationResults = TestHelper.ValidateModel(character);

        Assert.NotEmpty(validationResults);
        Assert.Contains(validationResults,
            v => v.ErrorMessage != null && v.ErrorMessage.Contains("The Name can't be longer than 60 characters."));
    }
}