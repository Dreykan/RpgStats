using RpgStats.Domain.Entities;
using RpgStats.Dto.Mapper;

namespace RpgStats.Dto.Tests.Mapper;

public class CharacterMapperTests
{
    [Fact]
    public void MapToCharacterWithAllFkObjectsDto_MapsCorrectly()
    {
        // Arrange
        var character = new Character
        {
            Id = 1,
            Name = "Test Character",
            GameId = 1,
            Game = new Game { Id = 1, Name = "Test Game" },
            StatValues = new List<StatValue>
            {
                new()
                {
                    Id = 1, Value = 10, Level = 1, ContainedBonusNum = 5, ContainedBonusPercent = 5,
                    Stat = new Stat { Id = 1, Name = "Strength" }
                }
            }
        };

        // Act
        var result = CharacterMapper.MapToCharacterWithAllFkObjectsDto(character);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(character.Id, result.Id);
        Assert.Equal(character.Name, result.Name);
        Assert.Equal(character.Picture, result.Picture);
        Assert.NotNull(result.GameWithoutFkObjectsDto);
        Assert.Equal(character.Game.Id, result.GameWithoutFkObjectsDto.Id);
        if (result.StatValuesWithStatObjectDtos == null) return;
        Assert.Single(result.StatValuesWithStatObjectDtos);
        Assert.Equal(character.StatValues.FirstOrDefault()?.Id,
            result.StatValuesWithStatObjectDtos.FirstOrDefault()?.Id);
    }

    [Fact]
    public void MapToCharacterWithoutFkObjectsDto_MapsCorrectly()
    {
        // Arrange
        var character = new Character
        {
            Id = 1,
            Name = "Test Character"
        };

        // Act
        var result = CharacterMapper.MapToCharacterWithoutFkObjectsDto(character);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(character.Id, result.Id);
        Assert.Equal(character.Name, result.Name);
        Assert.Equal(character.Picture, result.Picture);
    }
}