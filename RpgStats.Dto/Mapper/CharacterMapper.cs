using RpgStats.Domain.Entities;

namespace RpgStats.Dto.Mapper;

public class CharacterMapper
{
    public CharacterWithAllFkObjectsDto MapToCharacterWithAllFkObjectsDto(Character character)
    {
        // New Object and map simple properties
        var characterWithAllFkObjectsDto = new CharacterWithAllFkObjectsDto
        {
            Id = character.Id,
            Name = character.Name,
            Picture = character.Picture
        };

        // Map Game-Property
        var gameMapper = new GameMapper();

        if (character.Game != null)
            characterWithAllFkObjectsDto.GameWithoutFkObjectsDto =
                gameMapper.MapToGameWithoutFkObjectsDto(character.Game);

        // Map StatValue-Property
        var statValueMapper = new StatValueMapper();
        var statValuesWithStatObjectDtos = new List<StatValueWithStatObjectDto>();

        if (character.StatValues != null)
            foreach (var statValue in character.StatValues)
            {
                statValuesWithStatObjectDtos.Add(statValueMapper.MapToStatValueWithStatObjectDto(statValue));
            }

        characterWithAllFkObjectsDto.StatValuesWithStatObjectDtos = statValuesWithStatObjectDtos;

        
        return characterWithAllFkObjectsDto;
    }

    public CharacterDetailDto MapToCharacterDetailDto(Character character, List<StatValue> statValues)
    {
        // New Object and map simple properties
        var characterDetailDto = new CharacterDetailDto
        {
            Id = character.Id,
            Name = character.Name,
            Picture = character.Picture
        };

        // Map Game-Property
        var gameMapper = new GameMapper();
        if (character.Game != null)
        {
            characterDetailDto.GameWithoutFkObjectsDto = gameMapper.MapToGameWithoutFkObjectsDto(character.Game);
        }

        // Map StatValue-Property
        var statValueMapper = new StatValueMapper();
        var statValuesWithStatObjectDtos = new List<StatValueWithStatObjectDto>();
        foreach (var statValue in statValues)
        {
            statValuesWithStatObjectDtos.Add(statValueMapper.MapToStatValueWithStatObjectDto(statValue));
        }

        characterDetailDto.StatValuesWithStatObjectDtos = statValuesWithStatObjectDtos;


        return characterDetailDto;
    }

    public CharacterWithoutFkObjectsDto MapToCharacterWithoutFkObjectsDto(Character character)
    {
        // New Object and map simple properties
        var characterWithoutFkObjectsDto = new CharacterWithoutFkObjectsDto
        {
            Id = character.Id,
            Name = character.Name,
            Picture = character.Picture
        };

        return characterWithoutFkObjectsDto;
    }
}