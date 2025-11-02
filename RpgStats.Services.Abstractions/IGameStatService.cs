using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface IGameStatService
{
    Task<List<GameStatDto>> GetAllGameStatsAsync();
    Task<List<GameStatDto>> GetAllGameStatsByGameIdAsync(long gameId);
    Task<List<GameStatDto>> GetAllGameStatsByStatIdAsync(long statId);
    Task<GameStatDto?> GetGameStatByIdAsync(long gameStatId);
    Task<GameStatDto> CreateGameStatAsync(GameStatForCreationDto gameStatForCreationDto);
    Task<GameStatDto> UpdateGameStatAsync(long gameStatId, GameStatForUpdateDto gameStatForUpdateDto);
    Task<GameStatDto> DeleteGameStatAsync(long gameStatId);
    Task<List<GameStatDto>> DeleteGameStatsByGameIdAsync(long gameId);
    Task<List<GameStatDto>> DeleteGameStatsByStatIdAsync(long statId);
}