using RpgStats.Domain.Entities;

namespace RpgStats.Services.Abstractions;

public interface IStatValueService
{
    Task<List<StatValue>> GetAllStatValuesAsync();
    Task<List<StatValue>> GetAllStatValuesByCharacterAsync(Character character);
    Task<List<StatValue>> GetAllStatValuesByStatAsync(Stat stat);
    Task<StatValue?> GetStatValueByIdAsync(long statId);
    Task<StatValue?> CreateStatValueAsync(StatValue statValue);
    Task<StatValue?> UpdateStatValueAsync(StatValue statValue);
    Task DeleteStatValueAsync(long statId);
}