using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Repo;
using RpgStats.Services.Abstractions;

namespace RpgStats.Services;

public class StatService : IStatService
{
    private readonly RpgStatsContext _dbContext;

    public StatService(RpgStatsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Stat>> GetAllStatsAsync()
    {
        return await _dbContext.Stats.ToListAsync();
    }

    public async Task<Stat?> GetStatByIdAsync(long statId)
    {
        return await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == statId);
    }

    public async Task<Stat?> CreateStatAsync(Stat stat)
    {
        _dbContext.Add(stat);
        await _dbContext.SaveChangesAsync();
        return await Task.FromResult(stat);
    }

    public async Task<Stat> UpdateStatAsync(Stat stat)
    {
        _dbContext.Entry(stat).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return await Task.FromResult(stat);
    }

    public Task DeleteStatAsync(long statId)
    {
        Stat? stat = _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == statId).Result;

        if (stat == null)
        {
            return Task.CompletedTask;
        }

        _dbContext.Remove(stat);
        return _dbContext.SaveChangesAsync();
    }
}