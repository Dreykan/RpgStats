using RpgStats.Domain.Entities;

namespace RpgStats.Dto.Mapper;

public static class PlatformMapper
{
    public static PlatformWithoutFkObjectsDto MapToPlatformWithoutFkObjectsDto(Platform platform)
    {
        var platformWithoutFkObjectsDto = new PlatformWithoutFkObjectsDto
        {
            Id = platform.Id,
            Name = platform.Name
        };

        return platformWithoutFkObjectsDto;
    }

    public static PlatformWithGamesDto MapToPlatformWithGamesDto(Platform platform, List<Game?> games)
    {
        // New Object and map simple properties
        var platformDetailDto = new PlatformWithGamesDto
        {
            Id = platform.Id,
            Name = platform.Name
        };

        // Map Game-Property
        if (games.Count == 0)
            return platformDetailDto;

        var gameDtos = new List<GameDto>();
        foreach (var game in games)
        {
            if (game == null)
                continue;
            var gameDto = new GameDto
            {
                Id = game.Id,
                Name = game.Name,
                Picture = game.Picture
            };
            gameDtos.Add(gameDto);
        }

        platformDetailDto.GameDtos = gameDtos;
        return platformDetailDto;
    }
}