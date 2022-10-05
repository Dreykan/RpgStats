using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface IStatService
{
    Task<List<StatDto>> GetAllStatsAsync();
    Task<StatDto?> GetStatByIdAsync(long statId);
    Task<StatDto?> CreateStatAsync(StatForCreationDto statForCreationDto);
    Task<StatDto?> UpdateStatAsync(long statId, StatForUpdateDto statForUpdateDto);
    Task DeleteStatAsync(long statId);
}