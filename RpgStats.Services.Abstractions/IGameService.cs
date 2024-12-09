using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface IGameService
{
    Task<ServiceResult<List<GameDto>>> GetAllGamesAsync();
    Task<ServiceResult<List<GameDto>>> GetAllGamesByNameAsync(string name);
    Task<ServiceResult<GameDto>> GetGameByIdAsync(long gameId);
    Task<ServiceResult<GameDto>> CreateGameAsync(GameForCreationDto gameForCreationDto);
    Task<ServiceResult<GameDto>> UpdateGameAsync(long gameId, GameForUpdateDto gameForUpdateDto);
    Task<ServiceResult<GameDto>> DeleteGameAsync(long gameId);
    Task<ServiceResult<List<GameDetailDto>>> GetAllGameDetailDtosAsync();
    Task<ServiceResult<List<GameDetailDto>>> GetAllGameDetailDtosByNameAsync(string name);
    Task<ServiceResult<GameDetailDto>> GetGameDetailDtoByIdAsync(long gameId);
}