using RpgStats.Domain.Entities;

namespace RpgStats.Dto.Mapper;

public class GameMapper
{
    public GameWithoutFkObjectsDto MapToGameWithoutFkObjectsDto(Game game)
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

    public GameDetailDto MapToGameDetailDto(Game game, List<Platform?> platforms, List<Stat?> stats)
    {
        // New Object and map simple properties
        var gameDetailDto = new GameDetailDto
        {
            Id = game.Id,
            Name = game.Name,
            Picture = game.Picture
        };

        // Map Characters-Property
        var characterMapper = new CharacterMapper();
        var characterWithoutFkObjectsDtos = new List<CharacterWithoutFkObjectsDto>();
        if (game.Characters != null)
        {
            foreach (var c in game.Characters)
            {
                characterWithoutFkObjectsDtos.Add(characterMapper.MapToCharacterWithoutFkObjectsDto(c));
            }
        }

        gameDetailDto.CharacterWithoutFkObjectsDtos = characterWithoutFkObjectsDtos;
        
        // Map Platforms-Property
        var platformMapper = new PlatformMapper();
        var platformWithoutFkObjectsDtos = new List<PlatformWithoutFkObjectsDto>();
        foreach (var p in platforms)
        {
            if (p != null) platformWithoutFkObjectsDtos.Add(platformMapper.MapToPlatformWithoutFkObjectsDto(p));
        }

        gameDetailDto.PlatformWithoutFkObjectsDtos = platformWithoutFkObjectsDtos;

        // Map Stats-Property
        var statMapper = new StatMapper();
        var statWithoutFkObjectsDtos = new List<StatWithoutFkObjectsDto>();
        if (stats == null) return gameDetailDto;
        foreach (var stat in stats)
        {
            statWithoutFkObjectsDtos.Add(statMapper.MapToStatWithoutFkObjectsDto(stat));
        }

        gameDetailDto.StatWithoutFkObjectsDtos = statWithoutFkObjectsDtos;


        return gameDetailDto;
    }
}