namespace RpgStats.Dto.Tests;

public class CharacterWithAllFkObjectsDtoTests
{
    [Fact]
    public void CharacterWithAllFkObjectsDto_CanBeInitializedWithValidData()
    {
        var character = new CharacterWithAllFkObjectsDto
        {
            Id = 1,
            Name = "Hero",
            Picture = "  "u8.ToArray(),
            GameWithoutFkObjectsDto = new GameWithoutFkObjectsDto(),
            StatValuesWithStatObjectDtos = new List<StatValueWithStatObjectDto> { new() }
        };

        Assert.Equal(1, character.Id);
        Assert.Equal("Hero", character.Name);
        Assert.NotNull(character.Picture);
        Assert.NotNull(character.GameWithoutFkObjectsDto);
        Assert.NotNull(character.StatValuesWithStatObjectDtos);
    }

    [Fact]
    public void CharacterWithAllFkObjectsDto_CanBeInitializedWithNullValues()
    {
        var character = new CharacterWithAllFkObjectsDto
        {
            Id = 1,
            Picture = null,
            GameWithoutFkObjectsDto = null,
            StatValuesWithStatObjectDtos = null
        };

        Assert.Equal(1, character.Id);
        Assert.Equal(string.Empty, character.Name);
        Assert.Null(character.Picture);
        Assert.Null(character.GameWithoutFkObjectsDto);
        Assert.Null(character.StatValuesWithStatObjectDtos);
    }

    [Fact]
    public void CharacterWithAllFkObjectsDto_EmptyInitialization()
    {
        var character = new CharacterWithAllFkObjectsDto();

        Assert.Equal(0, character.Id);
        Assert.Equal(string.Empty, character.Name);
        Assert.Null(character.Picture);
        Assert.Null(character.GameWithoutFkObjectsDto);
        Assert.Null(character.StatValuesWithStatObjectDtos);
    }
}