using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface IStatService
{
    Task<List<StatDto>> GetAllStatsAsync();
    Task<List<StatDto>> GetAllStatsByNameAsync(string name);
    Task<List<StatDto>> GetAllStatsByShortNameAsync(string shortName);
    Task<List<StatDto>> GetAllStatsByGameIdAsync(long gameId);
    Task<StatDto?> GetStatByIdAsync(long statId);
    Task<StatDto> CreateStatAsync(StatForCreationDto? statForCreationDto);
    Task<StatDto> UpdateStatAsync(long statId, StatForUpdateDto statForUpdateDto);
    Task<StatDto?> DeleteStatAsync(long statId);
}