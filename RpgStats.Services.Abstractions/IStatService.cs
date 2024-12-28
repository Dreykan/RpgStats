using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface IStatService
{
    Task<ServiceResult<List<StatDto>>> GetAllStatsAsync();
    Task<ServiceResult<List<StatDto>>> GetAllStatsByNameAsync(string name);
    Task<ServiceResult<List<StatDto>>> GetAllStatsByShortNameAsync(string shortName);
    Task<ServiceResult<List<StatDto>>> GetAllStatsByGameIdAsync(long gameId);
    Task<ServiceResult<List<StatDetailDto>>> GetAllStatDetailDtosAsync();
    Task<ServiceResult<List<StatDetailDto>>> GetAllStatDetailDtosByNameAsync(string name);
    Task<ServiceResult<List<StatDetailDto>>> GetAllStatDetailDtosByShortNameAsync(string shortName);
    Task<ServiceResult<List<StatDetailDto>>> GetAllStatDetailDtosByGameIdAsync(long gameId);
    Task<ServiceResult<StatDto>> GetStatByIdAsync(long statId);
    Task<ServiceResult<StatDetailDto>> GetStatDetailDtoByIdAsync(long statId);
    Task<ServiceResult<StatDto>> CreateStatAsync(StatForCreationDto? statForCreationDto);
    Task<ServiceResult<StatDto>> UpdateStatAsync(long statId, StatForUpdateDto statForUpdateDto);
    Task<ServiceResult<StatDto>> DeleteStatAsync(long statId);
}