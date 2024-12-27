using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface IGameStatService
{
    Task<ServiceResult<List<GameStatDto>>> GetAllGameStatsAsync();
    Task<ServiceResult<List<GameStatDto>>> GetAllGameStatsByGameIdAsync(long gameId);
    Task<ServiceResult<List<GameStatDto>>> GetAllGameStatsByStatIdAsync(long statId);
    Task<ServiceResult<GameStatDto>> GetGameStatByIdAsync(long gameStatId);
    Task<ServiceResult<GameStatDto>> CreateGameStatAsync(long gameId, long statId);
    Task<ServiceResult<GameStatDto>> UpdateGameStatAsync(long gameStatId, long gameId, long statId);
    Task<ServiceResult<GameStatDto>> DeleteGameStatAsync(long gameStatId);
    Task<ServiceResult<List<GameStatDto>>> DeleteGameStatsByGameIdAsync(long gameId);
    Task<ServiceResult<List<GameStatDto>>> DeleteGameStatsByStatIdAsync(long statId);
}