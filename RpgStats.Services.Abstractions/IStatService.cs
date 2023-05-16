using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface IStatService
{
    Task<List<StatDto>> GetAllStatsAsync();
    Task<List<StatDto>> GetAllStatsByNameAsync(string name);
    Task<List<StatDto>> GetAllStatsByShortNameAsync(string shortName);
    Task<List<StatDetailDto>> GetAllStatDetailDtosAsync();
    Task<List<StatDetailDto>> GetAllStatDetailDtosByNameAsync(string name);
    Task<List<StatDetailDto>> GetAllStatDetailDtosByShortNameAsync(string shortName);
    Task<StatDto?> GetStatByIdAsync(long statId);
    Task<StatDetailDto?> GetStatDetailDtoByIdAsync(long statId);
    Task<StatDto?> CreateStatAsync(StatForCreationDto? statForCreationDto);
    Task<StatDto?> UpdateStatAsync(long statId, StatForUpdateDto statForUpdateDto);
    Task DeleteStatAsync(long statId);
}