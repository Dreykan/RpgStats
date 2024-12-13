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

    public async Task<ServiceResult<List<StatValueDto>>> GetAllStatValuesAsync()
    {
        var statValues = await _dbContext.StatValues
            .Include(sv => sv.Character)
            .Include(sv => sv.Stat)
            .ToListAsync();

        if (statValues.Count == 0)
            return ServiceResult<List<StatValueDto>>.ErrorResult("No stat values found");

        return ServiceResult<List<StatValueDto>>.SuccessResult(statValues.Adapt<List<StatValueDto>>());
    }

    public async Task<ServiceResult<List<StatValueDto>>> GetAllStatValuesByCharacterIdAsync(long characterId)
    {
        var statValues = await _dbContext.StatValues
            .Include(sv => sv.Character)
            .Include(sv => sv.Stat)
            .Where(sv => sv.CharacterId == characterId)
            .ToListAsync();

        if (statValues.Count == 0)
            return ServiceResult<List<StatValueDto>>.ErrorResult("No stat values found");

        return ServiceResult<List<StatValueDto>>.SuccessResult(statValues.Adapt<List<StatValueDto>>());
    }

    public async Task<ServiceResult<List<StatValueDto>>> GetAllStatValuesByStatIdAsync(long statId)
    {
        var statValues = await _dbContext.StatValues
            .Include(sv => sv.Character)
            .Include(sv => sv.Stat)
            .Where(sv => sv.StatId == statId)
            .ToListAsync();

        if (statValues.Count == 0)
            return ServiceResult<List<StatValueDto>>.ErrorResult("No stat values found");

        return ServiceResult<List<StatValueDto>>.SuccessResult(statValues.Adapt<List<StatValueDto>>());
    }

    public async Task<ServiceResult<StatValueDto>> GetStatValueByIdAsync(long statId)
    {
        var statValue = await _dbContext.StatValues
            .Include(sv => sv.Character)
            .Include(sv => sv.Stat)
            .FirstOrDefaultAsync(sv => sv.Id == statId);

        if (statValue == null)
            return ServiceResult<StatValueDto>.ErrorResult($"Stat value with ID {statId} not found");

        return ServiceResult<StatValueDto>.SuccessResult(statValue.Adapt<StatValueDto>());
    }

    public async Task<ServiceResult<StatValueDto>> CreateStatValueAsync(long characterId, long statId,
        StatValueForCreationDto statValueForCreationDto)
    {
        var character = await _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == characterId);
        if (character == null)
            return ServiceResult<StatValueDto>.ErrorResult($"Character with ID {characterId} not found");

        var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == statId);
        if (stat == null)
            return ServiceResult<StatValueDto>.ErrorResult($"Stat with ID {statId} not found");

        var statValue = statValueForCreationDto.Adapt<StatValue>();
        statValue.CharacterId = character.Id;
        statValue.Character = character;
        statValue.StatId = stat.Id;
        statValue.Stat = stat;

        _dbContext.Add(statValue);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<StatValueDto>.ErrorResult("StatValue could not be created");

        return ServiceResult<StatValueDto>.SuccessResult(statValue.Adapt<StatValueDto>());
    }

    public async Task<ServiceResult<StatValueDto>> UpdateStatValueAsync(long statValueId, long characterId, long statId,
        StatValueForUpdateDto statValueForUpdateDto)
    {
        var statValue = await _dbContext.StatValues.FirstOrDefaultAsync(sv => sv.Id == statValueId);
        if (statValue == null)
            return ServiceResult<StatValueDto>.ErrorResult($"Stat value with ID {statValueId} not found");

        var character = await _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == characterId);
        if (character == null)
            return ServiceResult<StatValueDto>.ErrorResult($"Character with ID {characterId} not found");

        var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == statId);
        if (stat == null)
            return ServiceResult<StatValueDto>.ErrorResult($"Stat with ID {statId} not found");

        statValue.Level = statValueForUpdateDto.Level;
        statValue.Value = statValueForUpdateDto.Value;
        statValue.ContainedBonusNum = statValueForUpdateDto.ContainedBonusNum;
        statValue.ContainedBonusPercent = statValueForUpdateDto.ContainedBonusPercent;
        statValue.CharacterId = character.Id;
        statValue.Character = character;
        statValue.StatId = stat.Id;
        statValue.Stat = stat;


        _dbContext.Entry(statValue).State = EntityState.Modified;
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<StatValueDto>.ErrorResult("StatValue could not be updated");

        return ServiceResult<StatValueDto>.SuccessResult(statValue.Adapt<StatValueDto>());
    }

    public async Task<ServiceResult<StatValueDto>> DeleteStatValueAsync(long statId)
    {
        var statValue = _dbContext.StatValues.FirstOrDefaultAsync(sv => sv.Id == statId).Result;
        if (statValue == null)
            return ServiceResult<StatValueDto>.ErrorResult($"Stat value with ID {statId} not found");

        _dbContext.Remove(statValue);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<StatValueDto>.ErrorResult("StatValue could not be deleted");

        return ServiceResult<StatValueDto>.SuccessResult(statValue.Adapt<StatValueDto>());
    }
}