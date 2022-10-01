using RpgStats.Domain.Entities;

namespace RpgStats.Services.Abstractions;

public interface IStatService
{
    Task<List<Stat>> GetAllStatsAsync();
    Task<Stat?> GetStatByIdAsync(long statId);
    Task<Stat?> CreateStatAsync(Stat stat);
    Task<Stat> UpdateStatAsync(Stat stat);
    Task DeleteStatAsync(long statId);
}