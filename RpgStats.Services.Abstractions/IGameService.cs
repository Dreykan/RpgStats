using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface IGameService
{
    Task<List<GameDto>> GetAllGamesAsync();
    Task<List<GameDto>> GetAllGamesByNameAsync(string name);
    Task<GameDto?> GetGameByIdAsync(long gameId);
    Task<GameDto?> CreateGameAsync(GameForCreationDto gameForCreationDto);
    Task<GameDto?> UpdateGameAsync(long gameId, GameForUpdateDto gameForUpdateDto);
    Task DeleteGameAsync(long gameId);
    Task<List<GameDetailDto>> GetAllGameDetailDtosAsync();
    Task<List<GameDetailDto>> GetAllGameDetailDtosByNameAsync(string name);
    Task<GameDetailDto?> GetGameDetailDtoByIdAsync(long gameId);
}