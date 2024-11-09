using RpgStats.Domain.Entities;

namespace RpgStats.Dto.Mapper;

public class GameMapper
{
    public static GameWithoutFkObjectsDto MapToGameWithoutFkObjectsDto(Game game)
    {
        // New Object and map simple properties
        var gameWithoutFkObjectsDto = new GameWithoutFkObjectsDto
        {
            Id = game.Id,
            Name = game.Name,
            Picture = game.Picture
        };

        return gameWithoutFkObjectsDto;
    }

    public static GameDetailDto MapToGameDetailDto(Game game, List<Platform?> platforms, List<Stat?> stats)
    {
        // New Object and map simple properties
        var gameDetailDto = new GameDetailDto
        {
            Id = game.Id,
            Name = game.Name,
            Picture = game.Picture
        };

        // Map Characters-Property
        var characterWithoutFkObjectsDtos = new List<CharacterWithoutFkObjectsDto>();
        if (game.Characters != null)
            characterWithoutFkObjectsDtos.AddRange(
                game.Characters.Select(CharacterMapper.MapToCharacterWithoutFkObjectsDto));

        gameDetailDto.CharacterWithoutFkObjectsDtos = characterWithoutFkObjectsDtos;

        // Map Platforms-Property
        var platformWithoutFkObjectsDtos = platforms.OfType<Platform>()
            .Select(PlatformMapper.MapToPlatformWithoutFkObjectsDto).ToList();

        gameDetailDto.PlatformWithoutFkObjectsDtos = platformWithoutFkObjectsDtos;

        // Map Stats-Property
        if (stats == null) return gameDetailDto;
        var statWithoutFkObjectsDtos = stats.Select(StatMapper.MapToStatWithoutFkObjectsDto).ToList();

        gameDetailDto.StatWithoutFkObjectsDtos = statWithoutFkObjectsDtos;


        return gameDetailDto;
    }
}