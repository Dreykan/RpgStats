using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface IGameService
{
    Task<List<GameDto>> GetAllGamesAsync();
    Task<GameDto?> GetGameByIdAsync(long gameId);
    Task<GameDto?> CreateGameAsync(GameForCreationDto gameForCreationDto);
    Task<GameDto?> UpdateGameAsync(long gameId, GameForUpdateDto gameForUpdateDto);
    Task DeleteGameAsync(long gameId);
}