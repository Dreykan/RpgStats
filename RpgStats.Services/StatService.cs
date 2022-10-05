using Mapster;
using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Domain.Exceptions;
using RpgStats.Dto;
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

    public async Task<List<StatDto>> GetAllStatsAsync()
    {
        var stats = await _dbContext.Stats
            .Include(s => s.StatValues)
            .ToListAsync();

        return stats.Adapt<List<StatDto>>();
    }

    public async Task<StatDto?> GetStatByIdAsync(long statId)
    {
        var stat = await _dbContext.Stats
            .Include(s => s.StatValues)
            .FirstOrDefaultAsync(s => s.Id == statId);

        if (stat == null)
        {
            throw new StatNotFoundException(statId);
        }

        return stat.Adapt<StatDto>();
    }

    public async Task<StatDto?> CreateStatAsync(StatForCreationDto statForCreationDto)
    {
        var stat = statForCreationDto.Adapt<Stat>();

        _dbContext.Add(stat);
        await _dbContext.SaveChangesAsync();

        return stat.Adapt<StatDto>();
    }

    public async Task<StatDto?> UpdateStatAsync(long statId, StatForUpdateDto statForUpdateDto)
    {
        var stat = _dbContext.Stats.FirstOrDefault(s => s.Id == statId);

        if (stat == null)
        {
            throw new StatNotFoundException(statId);
        }

        stat.Name = statForUpdateDto.Name;
        stat.ShortName = statForUpdateDto.ShortName;

        _dbContext.Entry(stat).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();

        return stat.Adapt<StatDto>();
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