using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface IPlatformGameService
{
    Task<List<PlatformGameDto>> GetAllPlatformGamesAsync();
    Task<List<PlatformGameDto>> GetAllPlatformGamesByPlatformIdAsync(long platformId);
    Task<List<PlatformGameDto>> GetAllPlatformGamesByGameIdAsync(long gameId);
    Task<PlatformGameDto?> GetPlatformGameByIdAsync(long platformGameId);
    Task<PlatformGameDto> CreatePlatformGameAsync(PlatformGameForCreationDto platformGameForCreation);
    Task<PlatformGameDto> UpdatePlatformGameAsync(long platformGameId, PlatformGameForUpdateDto platformGameForUpdate);
    Task<PlatformGameDto> DeletePlatformGameAsync(long platformGameId);
    Task<List<PlatformGameDto>> DeletePlatformGamesByGameIdAsync(long gameId);
    Task<List<PlatformGameDto>> DeletePlatformGamesByPlatformIdAsync(long platformId);
}