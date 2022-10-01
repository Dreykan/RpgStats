using RpgStats.Domain.Entities;

namespace RpgStats.Services.Abstractions;

public interface IGameService
{
    Task<List<Game>> GetAllGamesAsync();
    Task<Game?> GetGameByIdAsync(long gameId);
    Task<Game?> CreateGameAsync(Game game);
    Task<Game?> UpdateGameAsync(Game game);
    Task DeleteGameAsync(long gameId);
}