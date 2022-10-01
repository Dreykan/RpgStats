using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Repo;
using RpgStats.Services.Abstractions;

namespace RpgStats.Services;

public class StatValueService : IStatValueService
{
    private readonly RpgStatsContext _dbContext;

    public StatValueService(RpgStatsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<StatValue>> GetAllStatValuesAsync()
    {
        return await _dbContext.StatValues
            .Include(sv => sv.Character)
            .Include(sv => sv.Stat)
            .ToListAsync();
    }

    public async Task<List<StatValue>> GetAllStatValuesByCharacterAsync(Character character)
    {
        return await _dbContext.StatValues
            .Include(sv => sv.Character)
            .Include(sv => sv.Stat)
            .Where(sv => sv.Character == character)
            .ToListAsync();
    }

    public async Task<List<StatValue>> GetAllStatValuesByStatAsync(Stat stat)
    {
        return await _dbContext.StatValues
            .Include(sv => sv.Character)
            .Include(sv => sv.Stat)
            .Where(sv => sv.Stat == stat)
            .ToListAsync();
    }

    public async Task<StatValue?> GetStatValueByIdAsync(long statId)
    {
        return await _dbContext.StatValues
            .Include(sv => sv.Character)
            .Include(sv => sv.Stat)
            .FirstOrDefaultAsync(sv => sv.Id == statId);
    }

    public async Task<StatValue?> CreateStatValueAsync(StatValue statValue)
    {
        _dbContext.Add(statValue);
        await _dbContext.SaveChangesAsync();
        return await Task.FromResult(statValue);
    }

    public async Task<StatValue?> UpdateStatValueAsync(StatValue statValue)
    {
        _dbContext.Entry(statValue).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return await Task.FromResult(statValue);
    }

    public Task DeleteStatValueAsync(long statId)
    {
        StatValue? statValue = _dbContext.StatValues.FirstOrDefaultAsync(sv => sv.Id == statId).Result;

        if (statValue == null)
        {
            return Task.CompletedTask;
        }

        _dbContext.Remove(statValue);
        return _dbContext.SaveChangesAsync();
    }
}