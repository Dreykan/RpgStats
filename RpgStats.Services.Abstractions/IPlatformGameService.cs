using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface IPlatformGameService
{
    Task<List<PlatformGameDto>> GetAllPlatformGamesAsync();
    Task<List<PlatformGameDto>> GetAllPlatformGamesByPlatformIdAsync(long platformId);
    Task<List<PlatformGameDto>> GetAllPlatformGamesByGameIdAsync(long gameId);
    Task<PlatformGameDto?> GetPlatformGameByIdAsync(long platformGameId);
    Task<PlatformGameDto?> CreatePlatformGameAsync(long platformId, long gameId);
    Task<PlatformGameDto?> UpdatePlatformGameAsync(long platformGameId, long platformId, long gameId);
    Task DeletePlatformGameAsync(long platformGameId);
    Task DeletePlatformGameByGameIdAsync(long gameId);
    Task DeletePlatformGameByPlatformIdAsync(long platformId);

}