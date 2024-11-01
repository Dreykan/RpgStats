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
            characterWithoutFkObjectsDtos.AddRange(game.Characters.Select(c =>
                CharacterMapper.MapToCharacterWithoutFkObjectsDto(c)));

        gameDetailDto.CharacterWithoutFkObjectsDtos = characterWithoutFkObjectsDtos;

        // Map Platforms-Property
        var platformWithoutFkObjectsDtos = platforms.OfType<Platform>()
            .Select(p => PlatformMapper.MapToPlatformWithoutFkObjectsDto(p)).ToList();

        gameDetailDto.PlatformWithoutFkObjectsDtos = platformWithoutFkObjectsDtos;

        // Map Stats-Property
        if (stats == null) return gameDetailDto;
        var statWithoutFkObjectsDtos = stats.Select(stat => StatMapper.MapToStatWithoutFkObjectsDto(stat)).ToList();

        gameDetailDto.StatWithoutFkObjectsDtos = statWithoutFkObjectsDtos;


        return gameDetailDto;
    }
}