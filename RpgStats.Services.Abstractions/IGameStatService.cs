using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface IGameStatService
{
    Task<List<GameStatDto>> GetAllGameStatsAsync();
    Task<List<GameStatDto>> GetAllGameStatsByGameIdAsync(long gameId);
    Task<List<GameStatDto>> GetAllGameStatsByStatIdAsync(long statId);
    Task<GameStatDto?> GetGameStatByIdAsync(long gameStatId);
    Task<GameStatDto?> CreateGameStatAsync(long gameId, long statId);
    Task<GameStatDto?> UpdateGameStatAsync(long gameStatId, long gameId, long statId);
    Task<Task> DeleteGameStatAsync(long gameStatId);
    Task<Task> DeleteGameStatsByGameIdAsync(long gameId);
    Task<Task> DeleteGameStatsByStatIdAsync(long statId);
}