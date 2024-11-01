using RpgStats.Domain.Entities;

namespace RpgStats.Dto.Mapper;

public class PlatformMapper
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

    public static PlatformDetailDto MapToPlatformDetailDto(Platform platform, List<Game?> games)
    {
        // New Object and map simple properties
        var platformDetailDto = new PlatformDetailDto
        {
            Id = platform.Id,
            Name = platform.Name
        };

        // Map Game-Property
        var gameWithoutFkObjectDtos =
            games.OfType<Game>().Select(g => GameMapper.MapToGameWithoutFkObjectsDto(g)).ToList();

        platformDetailDto.GameWithoutFkObjectsDtos = gameWithoutFkObjectDtos;

        return platformDetailDto;
    }
}