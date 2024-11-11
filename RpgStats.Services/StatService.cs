using System.Diagnostics.CodeAnalysis;
using Mapster;
using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Domain.Exceptions;
using RpgStats.Dto;
using RpgStats.Dto.Mapper;
using RpgStats.Repo;
using RpgStats.Services.Abstractions;

namespace RpgStats.Services;

[SuppressMessage("Performance",
    "CA1862:\"StringComparison\"-Methodenüberladungen verwenden, um Zeichenfolgenvergleiche ohne Beachtung der Groß-/Kleinschreibung durchzuführen")]
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

    public async Task<List<StatDto>> GetAllStatsByNameAsync(string name)
    {
        var stats = await _dbContext.Stats
            .Include(s => s.StatValues)
            .Where(s => s.Name != null && s.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        return stats.Adapt<List<StatDto>>();
    }

    public async Task<List<StatDto>> GetAllStatsByShortNameAsync(string shortName)
    {
        var stats = await _dbContext.Stats
            .Include(s => s.StatValues)
            .Where(s => s.ShortName != null && s.ShortName.ToLower().Contains(shortName.ToLower()))
            .ToListAsync();

        return stats.Adapt<List<StatDto>>();
    }

    public async Task<List<StatDetailDto>> GetAllStatDetailDtosAsync()
    {
        var stats = await _dbContext.Stats
            .Include(s => s.StatValues)
            .ToListAsync();

        var statValues = await _dbContext.StatValues
            .Include(sv => sv.Character)
            .ToListAsync();

        return (from stat in stats
            let svTempList = statValues.Where(sv => sv.StatId == stat.Id)
                .ToList()
            select StatMapper.MapToStatDetailDto(stat, svTempList)).ToList();
    }

    public async Task<List<StatDetailDto>> GetAllStatDetailDtosByNameAsync(string name)
    {
        var stats = await _dbContext.Stats
            .Include(s => s.StatValues)
            .Where(s => s.Name != null && s.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        var statValues = await _dbContext.StatValues
            .Include(sv => sv.Character)
            .ToListAsync();

        return (from stat in stats
            let svTempList = statValues.Where(sv => sv.StatId == stat.Id)
                .ToList()
            select StatMapper.MapToStatDetailDto(stat, svTempList)).ToList();
    }

    public async Task<List<StatDetailDto>> GetAllStatDetailDtosByShortNameAsync(string shortName)
    {
        var stats = await _dbContext.Stats
            .Include(s => s.StatValues)
            .Where(s => s.ShortName != null && s.ShortName.ToLower().Contains(shortName.ToLower()))
            .ToListAsync();

        var statValues = await _dbContext.StatValues
            .Include(sv => sv.Character)
            .ToListAsync();

        return (from stat in stats
            let svTempList = statValues.Where(sv => sv.StatId == stat.Id)
                .ToList()
            select StatMapper.MapToStatDetailDto(stat, svTempList)).ToList();
    }

    public async Task<StatDto?> GetStatByIdAsync(long statId)
    {
        var stat = await _dbContext.Stats
            .Include(s => s.StatValues)
            .FirstOrDefaultAsync(s => s.Id == statId);

        if (stat == null) throw new StatNotFoundException(statId);

        return stat.Adapt<StatDto>();
    }

    public async Task<StatDetailDto?> GetStatDetailDtoByIdAsync(long statId)
    {
        var stat = await _dbContext.Stats
            .Include(s => s.StatValues)
            .FirstOrDefaultAsync(s => s.Id == statId);

        var statValues = await _dbContext.StatValues
            .Include(sv => sv.Character)
            .ToListAsync();

        var statDetailDto = new StatDetailDto();

        if (stat == null) return statDetailDto;

        var svTempList = statValues
            .Where(sv => sv.StatId == statId)
            .ToList();

        statDetailDto = StatMapper.MapToStatDetailDto(stat, svTempList);

        return statDetailDto;
    }

    public async Task<StatDto?> CreateStatAsync(StatForCreationDto? statForCreationDto)
    {
        var stat = statForCreationDto.Adapt<Stat>();

        _dbContext.Add(stat);
        await _dbContext.SaveChangesAsync();

        return stat.Adapt<StatDto>();
    }

    public async Task<StatDto?> UpdateStatAsync(long statId, StatForUpdateDto statForUpdateDto)
    {
        var stat = _dbContext.Stats.FirstOrDefault(s => s.Id == statId);

        if (stat == null) throw new StatNotFoundException(statId);

        stat.Name = statForUpdateDto.Name;
        stat.ShortName = statForUpdateDto.ShortName;

        _dbContext.Entry(stat).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();

        return stat.Adapt<StatDto>();
    }

    public async Task<Task> DeleteStatAsync(long statId)
    {
        var stat = _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == statId).Result;

        if (stat == null) return Task.CompletedTask;

        _dbContext.Remove(stat);

        await _dbContext.SaveChangesAsync();

        return Task.CompletedTask;
    }
}