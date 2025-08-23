using Mapster;
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
        var gameDtos = games.Adapt<List<GameDto>>();
        platformDetailDto.GameDtos = gameDtos;

        return platformDetailDto;
    }
}