using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface IPlatformService
{
    Task<List<PlatformDto>> GetAllPlatformsAsync();
    Task<List<PlatformWithGamesDto>> GetAllPlatformsWithGamesAsync();
    Task<List<PlatformDto>> GetAllPlatformsByNameAsync(string name);
    Task<PlatformDto?> GetPlatformByIdAsync(long platformId);
    Task<PlatformWithGamesDto?> GetPlatformWithGamesByIdAsync(long platformId);
    Task<PlatformDto> CreatePlatformAsync(PlatformForCreationDto platformForCreationDto);
    Task<PlatformDto> UpdatePlatformAsync(long platformId, PlatformForUpdateDto platformForUpdateDto);
    Task<PlatformDto> DeletePlatformAsync(long platformId);
}