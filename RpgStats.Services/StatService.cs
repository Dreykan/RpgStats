using System.Diagnostics.CodeAnalysis;
using Mapster;
using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Dto;
using RpgStats.Repo;
using RpgStats.Services.Abstractions;

namespace RpgStats.Services;

[SuppressMessage("Performance",
    "CA1862:\"StringComparison\"-Methodenüberladungen verwenden, um Zeichenfolgevergleiche ohne Beachtung der Groß-/Kleinschreibung durchzuführen")]
public class StatService : IStatService
{
    private readonly RpgStatsContext _dbContext;

    public StatService(RpgStatsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResult<List<StatDto>>> GetAllStatsAsync()
    {
        var stats = await _dbContext.Stats
            .Include(s => s.StatValues)
            .ToListAsync();

        if (stats.Count == 0)
            return ServiceResult<List<StatDto>>.ErrorResult("No stats found");

        return ServiceResult<List<StatDto>>.SuccessResult(stats.Adapt<List<StatDto>>());
    }

    public async Task<ServiceResult<List<StatDto>>> GetAllStatsByNameAsync(string name)
    {
        var stats = await _dbContext.Stats
            .Include(s => s.StatValues)
            .Where(s => s.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        if (stats.Count == 0)
            return ServiceResult<List<StatDto>>.ErrorResult("No stats found");

        return ServiceResult<List<StatDto>>.SuccessResult(stats.Adapt<List<StatDto>>());
    }

    public async Task<ServiceResult<List<StatDto>>> GetAllStatsByShortNameAsync(string shortName)
    {
        var stats = await _dbContext.Stats
            .Include(s => s.StatValues)
            .Where(s => s.ShortName != null && s.ShortName.ToLower().Contains(shortName.ToLower()))
            .ToListAsync();

        if (stats.Count == 0)
            return ServiceResult<List<StatDto>>.ErrorResult("No stats found");

        return ServiceResult<List<StatDto>>.SuccessResult(stats.Adapt<List<StatDto>>());
    }

    public async Task<ServiceResult<List<StatDto>>> GetAllStatsByGameIdAsync(long gameId)
    {
        if (!await GameExists(gameId))
            return ServiceResult<List<StatDto>>.ErrorResult($"Game with Id {gameId} not found");

        var stats = await _dbContext.Stats
            .Where(s => s.GameStats != null && s.GameStats.Any(gs => gs.GameId == gameId))
            .ToListAsync();

        return ServiceResult<List<StatDto>>.SuccessResult(stats.Adapt<List<StatDto>>());
    }

    public async Task<ServiceResult<List<StatDetailDto>>> GetAllStatDetailDtosAsync()
    {
        var stats = await _dbContext.Stats
            .Include(s => s.StatValues)
            .ToListAsync();

        if (stats.Count == 0)
            return ServiceResult<List<StatDetailDto>>.ErrorResult("No stats found");

        return ServiceResult<List<StatDetailDto>>.SuccessResult(stats.Adapt<List<StatDetailDto>>());
    }

    public async Task<ServiceResult<List<StatDetailDto>>> GetAllStatDetailDtosByNameAsync(string name)
    {
        var stats = await _dbContext.Stats
            .Include(s => s.StatValues)
            .Where(s => s.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        if (stats.Count == 0)
            return ServiceResult<List<StatDetailDto>>.ErrorResult("No stats found");

        return ServiceResult<List<StatDetailDto>>.SuccessResult(stats.Adapt<List<StatDetailDto>>());
    }

    public async Task<ServiceResult<List<StatDetailDto>>> GetAllStatDetailDtosByShortNameAsync(string shortName)
    {
        var stats = await _dbContext.Stats
            .Include(s => s.StatValues)
            .Where(s => s.ShortName != null && s.ShortName.ToLower().Contains(shortName.ToLower()))
            .ToListAsync();

        if (stats.Count == 0)
            return ServiceResult<List<StatDetailDto>>.ErrorResult("No stats found");

        return ServiceResult<List<StatDetailDto>>.SuccessResult(stats.Adapt<List<StatDetailDto>>());
    }

    public async Task<ServiceResult<List<StatDetailDto>>> GetAllStatDetailDtosByGameIdAsync(long gameId)
    {
        if (!await GameExists(gameId))
            return ServiceResult<List<StatDetailDto>>.ErrorResult($"Game with Id {gameId} not found");

        var stats = await _dbContext.Stats
            .Include(s => s.StatValues)
            .Where(s => s.GameStats != null && s.GameStats.Any(gs => gs.GameId == gameId))
            .ToListAsync();

        return ServiceResult<List<StatDetailDto>>.SuccessResult(stats.Adapt<List<StatDetailDto>>());
    }


    public async Task<ServiceResult<StatDto>> GetStatByIdAsync(long statId)
    {
        var stat = await _dbContext.Stats
            .Include(s => s.StatValues)
            .FirstOrDefaultAsync(s => s.Id == statId);

        if (stat == null)
            return ServiceResult<StatDto>.ErrorResult($"Stat with Id {statId} not found");

        return ServiceResult<StatDto>.SuccessResult(stat.Adapt<StatDto>());
    }

    public async Task<ServiceResult<StatDetailDto>> GetStatDetailDtoByIdAsync(long statId)
    {
        var stat = await _dbContext.Stats
            .Include(s => s.StatValues)
            .FirstOrDefaultAsync(s => s.Id == statId);

        if (stat == null)
            return ServiceResult<StatDetailDto>.ErrorResult($"Stat with Id {statId} not found");

        return ServiceResult<StatDetailDto>.SuccessResult(stat.Adapt<StatDetailDto>());
    }

    public async Task<ServiceResult<StatDto>> CreateStatAsync(StatForCreationDto? statForCreationDto)
    {
        var stat = statForCreationDto.Adapt<Stat>();

        _dbContext.Add(stat);
        var result = await _dbContext.SaveChangesAsync();

        if (result == 0)
            return ServiceResult<StatDto>.ErrorResult("Stat could not be created");

        return ServiceResult<StatDto>.SuccessResult(stat.Adapt<StatDto>());
    }

    public async Task<ServiceResult<StatDto>> UpdateStatAsync(long statId, StatForUpdateDto statForUpdateDto)
    {
        var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == statId);

        if (stat == null)
            return ServiceResult<StatDto>.ErrorResult($"Stat with Id {statId} not found");

        stat.Name = statForUpdateDto.Name;
        stat.ShortName = statForUpdateDto.ShortName;

        _dbContext.Entry(stat).State = EntityState.Modified;
        var result = await _dbContext.SaveChangesAsync();

        if (result == 0)
            return ServiceResult<StatDto>.ErrorResult("Stat could not be updated");

        return ServiceResult<StatDto>.SuccessResult(stat.Adapt<StatDto>());
    }

    public async Task<ServiceResult<StatDto>> DeleteStatAsync(long statId)
    {
        var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == statId);

        if (stat == null)
            return ServiceResult<StatDto>.ErrorResult($"Stat with Id {statId} not found");

        _dbContext.Remove(stat);

        var result = await _dbContext.SaveChangesAsync();

        if (result == 0)
            return ServiceResult<StatDto>.ErrorResult("Stat could not be deleted");

        return ServiceResult<StatDto>.SuccessResult(stat.Adapt<StatDto>());
    }

    private async Task<bool> GameExists(long gameId)
    {
        return await _dbContext.Games.AnyAsync(g => g.Id == gameId);
    }
}