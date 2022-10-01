using RpgStats.Domain.Entities;

namespace RpgStats.Services.Abstractions;

public interface IPlatformGameService
{
    Task<List<PlatformGame>> GetAllPlatformGamesAsync();
    Task<List<PlatformGame>> GetAllPlatformGamesByPlatformAsync(Platform platform);
    Task<List<PlatformGame>> GetAllPlatformGamesByGameAsync(Game game);
    Task<PlatformGame?> GetPlatformGameByIdAsync(long platformGameId);
    Task<PlatformGame?> CreatePlatformGameAsync(PlatformGame platformGame);
    Task<PlatformGame?> UpdatePlatformGameAsync(PlatformGame platformGame);
    Task DeletePlatformGameAsync(long platformId);

}