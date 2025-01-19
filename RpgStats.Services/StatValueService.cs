using Mapster;
using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
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

        return ServiceResult<List<StatValueDto>>.SuccessResult(statValues.Adapt<List<StatValueDto>>());
    }

    public async Task<ServiceResult<List<StatValueDto>>> GetAllStatValuesByCharacterIdAsync(long characterId)
    {
        if (!await CharacterExists(characterId))
            return ServiceResult<List<StatValueDto>>.ErrorResult($"Character with ID {characterId} not found");

        var statValues = await _dbContext.StatValues
            .Include(sv => sv.Character)
            .Include(sv => sv.Stat)
            .Where(sv => sv.CharacterId == characterId)
            .ToListAsync();

        return ServiceResult<List<StatValueDto>>.SuccessResult(statValues.Adapt<List<StatValueDto>>());
    }

    public async Task<ServiceResult<List<StatValueDto>>> GetAllStatValuesByStatIdAsync(long statId)
    {
        if (!await StatValueExists(statId))
            return ServiceResult<List<StatValueDto>>.ErrorResult($"Stat with ID {statId} not found");

        var statValues = await _dbContext.StatValues
            .Include(sv => sv.Character)
            .Include(sv => sv.Stat)
            .Where(sv => sv.StatId == statId)
            .ToListAsync();

        return ServiceResult<List<StatValueDto>>.SuccessResult(statValues.Adapt<List<StatValueDto>>());
    }

    public async Task<ServiceResult<StatValueDto>> GetStatValueByIdAsync(long statValueId)
    {
        var statValue = await _dbContext.StatValues
            .Include(sv => sv.Character)
            .Include(sv => sv.Stat)
            .FirstOrDefaultAsync(sv => sv.Id == statValueId);

        if (statValue == null)
            return ServiceResult<StatValueDto>.ErrorResult($"StatValue with ID {statValueId} not found");

        return ServiceResult<StatValueDto>.SuccessResult(statValue.Adapt<StatValueDto>());
    }

    public async Task<ServiceResult<StatValueDto>> CreateStatValueAsync(StatValueForCreationDto statValueForCreationDto)
    {
        var character = await _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == statValueForCreationDto.CharacterId);
        if (character == null)
            return ServiceResult<StatValueDto>.ErrorResult($"Character with ID {statValueForCreationDto.CharacterId} not found");

        var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == statValueForCreationDto.StatId);
        if (stat == null)
            return ServiceResult<StatValueDto>.ErrorResult($"Stat with ID {statValueForCreationDto.StatId} not found");

        var statValue = statValueForCreationDto.Adapt<StatValue>();
        // statValue.Character = character;
        // statValue.Stat = stat;

        _dbContext.Add(statValue);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<StatValueDto>.ErrorResult("StatValue could not be created");

        return ServiceResult<StatValueDto>.SuccessResult(statValue.Adapt<StatValueDto>());
    }

    public async Task<ServiceResult<List<StatValueDto>>> CreateStatValuesAsync(
        List<StatValueForCreationDto> statValueForCreationDto)
    {
        var statValues = statValueForCreationDto.Adapt<List<StatValue>>();
        // foreach (var statValue in statValues)
        // {
        //     var character = await _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == statValue.CharacterId);
        //     if (character == null)
        //         return ServiceResult<List<StatValueDto>>.ErrorResult($"Character with ID {statValue.CharacterId} not found");
        //
        //     var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == statValue.StatId);
        //     if (stat == null)
        //         return ServiceResult<List<StatValueDto>>.ErrorResult($"Stat with ID {statValue.StatId} not found");
        //
        //     statValue.Character = character;
        //     statValue.Stat = stat;
        // }

        _dbContext.AddRange(statValues);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<List<StatValueDto>>.ErrorResult("StatValues could not be created");

        return ServiceResult<List<StatValueDto>>.SuccessResult(statValues.Adapt<List<StatValueDto>>());
    }

    public async Task<ServiceResult<StatValueDto>> UpdateStatValueAsync(long statValueId, long characterId, long statId,
        StatValueForUpdateDto statValueForUpdateDto)
    {
        var statValue = await _dbContext.StatValues.FirstOrDefaultAsync(sv => sv.Id == statValueId);
        if (statValue == null)
            return ServiceResult<StatValueDto>.ErrorResult($"StatValue with ID {statValueId} not found");

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

    public async Task<ServiceResult<StatValueDto>> DeleteStatValueAsync(long statValueId)
    {
        var statValue = _dbContext.StatValues.FirstOrDefaultAsync(sv => sv.Id == statValueId).Result;
        if (statValue == null)
            return ServiceResult<StatValueDto>.ErrorResult($"StatValue with ID {statValueId} not found");

        _dbContext.Remove(statValue);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<StatValueDto>.ErrorResult("StatValue could not be deleted");

        return ServiceResult<StatValueDto>.SuccessResult(statValue.Adapt<StatValueDto>());
    }

    public async Task<ServiceResult<List<StatValueDto>>> DeleteStatValuesByCharacterIdAndLevelAsync(long characterId,
        int level)
    {
        var statValues = _dbContext.StatValues
            .Where(sv => sv.CharacterId == characterId && sv.Level == level).ToList();
        if (statValues.Count == 0)
            return ServiceResult<List<StatValueDto>>.ErrorResult($"StatValues with Character ID {characterId} and Level {level} not found");

        _dbContext.RemoveRange(statValues);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<List<StatValueDto>>.ErrorResult("StatValues could not be deleted");

        return ServiceResult<List<StatValueDto>>.SuccessResult(statValues.Adapt<List<StatValueDto>>());
    }

    private async Task<bool> StatValueExists(long id)
    {
        return await _dbContext.StatValues.AnyAsync(e => e.Id == id);
    }

    private async Task<bool> CharacterExists(long id)
    {
        return await _dbContext.Characters.AnyAsync(e => e.Id == id);
    }
}