namespace RpgStats.Dto.Tests;

public class CharacterDtoTests
{
    [Fact]
    public void CharacterDto_Should_Have_Default_Values()
    {
        var character = new CharacterDto();
        Assert.Equal(0, character.Id);
        Assert.Equal(string.Empty, character.Name);
        Assert.Null(character.Picture);
        Assert.Equal(0, character.GameId);
        Assert.Null(character.StatValues);
    }

    [Fact]
    public void CharacterDto_Should_Set_And_Get_Properties()
    {
        var statValues = new List<StatValueDto> { new() };
        var character = new CharacterDto
        {
            Id = 1,
            Name = "Hero",
            Picture = new byte[] { 1, 2, 3 },
            GameId = 100,
            StatValues = statValues
        };

        Assert.Equal(1, character.Id);
        Assert.Equal("Hero", character.Name);
        Assert.Equal(new byte[] { 1, 2, 3 }, character.Picture);
        Assert.Equal(100, character.GameId);
        Assert.Equal(statValues, character.StatValues);
    }

    [Fact]
    public void CharacterDto_Should_Handle_Null_Values()
    {
        var character = new CharacterDto
        {
            Picture = null,
            StatValues = null
        };

        Assert.Equal(string.Empty, character.Name);
        Assert.Null(character.Picture);
        Assert.Null(character.StatValues);
    }
}