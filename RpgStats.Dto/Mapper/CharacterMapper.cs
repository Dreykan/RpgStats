using RpgStats.Domain.Entities;

namespace RpgStats.Dto.Mapper;

public static class CharacterMapper
{
    public static CharacterWithAllFkObjectsDto MapToCharacterWithAllFkObjectsDto(Character character)
    {
        // New Object and map simple properties
        var characterWithAllFkObjectsDto = new CharacterWithAllFkObjectsDto
        {
            Id = character.Id,
            Name = character.Name,
            Picture = character.Picture
        };

        // Map Game-Property

        if (character.Game != null)
            characterWithAllFkObjectsDto.GameWithoutFkObjectsDto =
                GameMapper.MapToGameWithoutFkObjectsDto(character.Game);

        // Map StatValue-Property
        var statValuesWithStatObjectDtos = new List<StatValueWithStatObjectDto>();

        if (character.StatValues != null)
            statValuesWithStatObjectDtos.AddRange(
                character.StatValues.Select(StatValueMapper.MapToStatValueWithStatObjectDto));

        characterWithAllFkObjectsDto.StatValuesWithStatObjectDtos = statValuesWithStatObjectDtos;


        return characterWithAllFkObjectsDto;
    }

    public static CharacterDetailDto MapToCharacterDetailDto(Character character, List<StatValue> statValues)
    {
        // New Object and map simple properties
        var characterDetailDto = new CharacterDetailDto
        {
            Id = character.Id,
            Name = character.Name,
            Picture = character.Picture
        };

        // Map Game-Property
        if (character.Game != null)
            characterDetailDto.GameWithoutFkObjectsDto = GameMapper.MapToGameWithoutFkObjectsDto(character.Game);

        // Map StatValue-Property
        var statValuesWithStatObjectDtos = statValues
            .Select(StatValueMapper.MapToStatValueWithStatObjectDto).ToList();

        characterDetailDto.StatValuesWithStatObjectDtos = statValuesWithStatObjectDtos;


        return characterDetailDto;
    }

    public static CharacterWithoutFkObjectsDto MapToCharacterWithoutFkObjectsDto(Character character)
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