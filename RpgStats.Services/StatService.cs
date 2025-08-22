using System.Diagnostics.CodeAnalysis;
using Mapster;
using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
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
            .ToListAsync();

        return stats.Adapt<List<StatDto>>();
    }

    public async Task<List<StatDto>> GetAllStatsByNameAsync(string name)
    {
        var stats = await _dbContext.Stats
            .Where(s => s.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        return stats.Adapt<List<StatDto>>();
    }

    public async Task<List<StatDto>> GetAllStatsByShortNameAsync(string shortName)
    {
        var stats = await _dbContext.Stats
            .Where(s => s.ShortName != null && s.ShortName.ToLower().Contains(shortName.ToLower()))
            .ToListAsync();

        return stats.Adapt<List<StatDto>>();
    }

    public async Task<List<StatDto>> GetAllStatsByGameIdAsync(long gameId)
    {
        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);
        if (game == null)
            throw new ArgumentException($"Game with ID {gameId} not found");

        var stats = await _dbContext.Stats
            .Where(s => s.GameStats != null && s.GameStats.Any(gs => gs.GameId == gameId))
            .ToListAsync();

        return stats.Adapt<List<StatDto>>();
    }

    public async Task<StatDto?> GetStatByIdAsync(long statId)
    {
        var stat = await _dbContext.Stats
            .FirstOrDefaultAsync(s => s.Id == statId);

        return stat.Adapt<StatDto>();
    }

    public async Task<StatDto> CreateStatAsync(StatForCreationDto? statForCreationDto)
    {
        var stat = statForCreationDto.Adapt<Stat>();

        _dbContext.Add(stat);
        var result = await _dbContext.SaveChangesAsync();

        if (result == 0)
            throw new InvalidOperationException("Stat could not be created");

        return stat.Adapt<StatDto>();
    }

    public async Task<StatDto> UpdateStatAsync(long statId, StatForUpdateDto statForUpdateDto)
    {
        var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == statId);
        if (stat == null)
            throw new ArgumentException($"Stat with ID {statId} not found");

        stat.Name = statForUpdateDto.Name;
        stat.ShortName = statForUpdateDto.ShortName;

        _dbContext.Entry(stat).State = EntityState.Modified;
        var result = await _dbContext.SaveChangesAsync();

        if (result == 0)
            throw new InvalidOperationException("Stat could not be updated");

        return stat.Adapt<StatDto>();
    }

    public async Task<StatDto?> DeleteStatAsync(long statId)
    {
        var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == statId);
        if (stat == null)
            throw new ArgumentException($"Stat with ID {statId} not found");

        _dbContext.Remove(stat);

        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            throw new InvalidOperationException("Stat could not be deleted");

        return stat.Adapt<StatDto>();
    }
}