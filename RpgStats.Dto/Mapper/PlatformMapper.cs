using RpgStats.Domain.Entities;

namespace RpgStats.Dto.Mapper;

public class PlatformMapper
{
    public PlatformWithoutFkObjectsDto MapToPlatformWithoutFkObjectsDto(Platform platform)
    {
        var platformWithoutFkObjectsDto = new PlatformWithoutFkObjectsDto
        {
            Id = platform.Id,
            Name = platform.Name
        };

        return platformWithoutFkObjectsDto;
    }

    public PlatformDetailDto MapToPlatformDetailDto(Platform platform, List<Game?> games)
    {
        // New Object and map simple properties
        var platformDetailDto = new PlatformDetailDto
        {
            Id = platform.Id,
            Name = platform.Name
        };

        // Map Game-Property
        var gameMapper = new GameMapper();
        var gameWithoutFkObjectDtos = new List<GameWithoutFkObjectsDto>();
        foreach (var g in games)
        {
            if (g != null) gameWithoutFkObjectDtos.Add(gameMapper.MapToGameWithoutFkObjectsDto(g));
        }

        platformDetailDto.GameWithoutFkObjectsDtos = gameWithoutFkObjectDtos;

        return platformDetailDto;
    }
}