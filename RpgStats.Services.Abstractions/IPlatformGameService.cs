using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface IPlatformGameService
{
    Task<ServiceResult<List<PlatformGameDto>>> GetAllPlatformGamesAsync();
    Task<ServiceResult<List<PlatformGameDto>>> GetAllPlatformGamesByPlatformIdAsync(long platformId);
    Task<ServiceResult<List<PlatformGameDto>>> GetAllPlatformGamesByGameIdAsync(long gameId);
    Task<ServiceResult<PlatformGameDto>> GetPlatformGameByIdAsync(long platformGameId);
    Task<ServiceResult<PlatformGameDto>> CreatePlatformGameAsync(long platformId, long gameId);
    Task<ServiceResult<PlatformGameDto>> UpdatePlatformGameAsync(long platformGameId, long platformId, long gameId);
    Task<ServiceResult<PlatformGameDto>> DeletePlatformGameAsync(long platformGameId);
    Task<ServiceResult<List<PlatformGameDto>>> DeletePlatformGameByGameIdAsync(long gameId);
    Task<ServiceResult<List<PlatformGameDto>>> DeletePlatformGameByPlatformIdAsync(long platformId);
}