using Mapster;
using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Domain.Exceptions;
using RpgStats.Dto;
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

    public async Task<List<StatValueDto>> GetAllStatValuesAsync()
    {
        var statValues = await _dbContext.StatValues
            .Include(sv => sv.Character)
            .Include(sv => sv.Stat)
            .ToListAsync();

        return statValues.Adapt<List<StatValueDto>>();
    }

    public async Task<List<StatValueDto>> GetAllStatValuesByCharacterIdAsync(long characterId)
    {
        var statValues = await _dbContext.StatValues
            .Include(sv => sv.Character)
            .Include(sv => sv.Stat)
            .Where(sv => sv.CharacterId == characterId)
            .ToListAsync();

        return statValues.Adapt<List<StatValueDto>>();
    }

    public async Task<List<StatValueDto>> GetAllStatValuesByStatIdAsync(long statId)
    {
        var statValues = await _dbContext.StatValues
            .Include(sv => sv.Character)
            .Include(sv => sv.Stat)
            .Where(sv => sv.StatId == statId)
            .ToListAsync();

        return statValues.Adapt<List<StatValueDto>>();
    }

    public async Task<StatValueDto?> GetStatValueByIdAsync(long statId)
    {
        var statValue = await _dbContext.StatValues
            .Include(sv => sv.Character)
            .Include(sv => sv.Stat)
            .FirstOrDefaultAsync(sv => sv.Id == statId);

        if (statValue == null)
        {
            throw new StatNotFoundException(statId);
        }

        return statValue.Adapt<StatValueDto>();
    }

    public async Task<StatValueDto?> CreateStatValueAsync(long characterId, long statId, StatValueForCreationDto statValueForCreationDto)
    {
        var character = await _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == characterId);

        if (character == null)
        {
            throw new CharacterNotFoundException(characterId);
        }

        var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == statId);

        if (stat == null)
        {
            throw new StatNotFoundException(statId);
        }

        var statValue = statValueForCreationDto.Adapt<StatValue>();
        statValue.CharacterId = character.Id;
        statValue.Character = character;
        statValue.StatId = stat.Id;
        statValue.Stat = stat;

        _dbContext.Add(statValue);
        await _dbContext.SaveChangesAsync();

        return statValue.Adapt<StatValueDto>();
    }

    public async Task<StatValueDto?> UpdateStatValueAsync(long statValueId, long characterId, long statId, StatValueForUpdateDto statValueForUpdateDto)
    {
        var statValue = await _dbContext.StatValues.FirstOrDefaultAsync(sv => sv.Id == statValueId);

        if (statValue == null)
        {
            throw new StatValueNotFoundException(statValueId);
        }

        var character = await _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == characterId);

        if (character == null)
        {
            throw new CharacterNotFoundException(characterId);
        }

        var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == statId);

        if (stat == null)
        {
            throw new StatNotFoundException(statId);
        }

        statValue.Level = statValueForUpdateDto.Level;
        statValue.Value = statValueForUpdateDto.Value;
        statValue.ContainedBonusNum = statValueForUpdateDto.ContainedBonusNum;
        statValue.ContainedBonusPercent = statValueForUpdateDto.ContainedBonusPercent;
        statValue.CharacterId = character.Id;
        statValue.Character = character;
        statValue.StatId = stat.Id;
        statValue.Stat = stat;


        _dbContext.Entry(statValue).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();

        return statValue.Adapt<StatValueDto>();
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