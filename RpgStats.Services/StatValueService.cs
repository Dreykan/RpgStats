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
            .ToListAsync();

        return statValues.Adapt<List<StatValueDto>>();
    }

    public async Task<List<StatValueDto>> GetAllStatValuesByCharacterIdAsync(long characterId)
    {
        var character = await _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == characterId);
        if (character == null)
            throw new CharacterNotFoundException(characterId);

        var statValues = await _dbContext.StatValues
            .Where(sv => sv.CharacterId == characterId)
            .ToListAsync();

        return statValues.Adapt<List<StatValueDto>>();
    }

    public async Task<List<StatValueDto>> GetAllStatValuesByStatIdAsync(long statId)
    {
        var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == statId);
        if (stat == null)
            throw new StatNotFoundException(statId);

        var statValues = await _dbContext.StatValues
            .Where(sv => sv.StatId == statId)
            .ToListAsync();

        return statValues.Adapt<List<StatValueDto>>();
    }

    public async Task<Dictionary<long, int>> GetHighestLevelByCharactersAsync(List<long> characterIds)
    {
        if (characterIds == null || characterIds.Count == 0)
            throw new ArgumentException("Character IDs list cannot be null or empty");

        var result = new Dictionary<long, int>();

        foreach (var characterId in characterIds)
        {
            var character = await _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == characterId);
            if (character == null)
                throw new CharacterNotFoundException(characterId);

            var highestLevel = await _dbContext.StatValues
                .Where(sv => sv.CharacterId == characterId)
                .MaxAsync(sv => (int?)sv.Level) ?? 0;

            result[characterId] = highestLevel;
        }

        return result;
    }

    public async Task<StatValueDto?> GetStatValueByIdAsync(long statValueId)
    {
        var statValue = await _dbContext.StatValues
            .FirstOrDefaultAsync(sv => sv.Id == statValueId);

        return statValue?.Adapt<StatValueDto>();
    }

    public async Task<StatValueDto> CreateStatValueAsync(StatValueForCreationDto statValueForCreationDto)
    {
        var character = await _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == statValueForCreationDto.CharacterId);
        if (character == null)
            throw new CharacterNotFoundException(statValueForCreationDto.CharacterId);

        var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == statValueForCreationDto.StatId);
        if (stat == null)
            throw new StatNotFoundException(statValueForCreationDto.StatId);

        var statValue = statValueForCreationDto.Adapt<StatValue>();

        _dbContext.Add(statValue);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            throw new InvalidOperationException("StatValue could not be created");

        return statValue.Adapt<StatValueDto>();
    }

    public async Task<List<StatValueDto>> CreateMultipleStatValuesAsync(List<StatValueForCreationDto> statValueForCreationDto)
    {
        var statValues = statValueForCreationDto.Adapt<List<StatValue>>();

        _dbContext.AddRange(statValues);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            throw new InvalidOperationException("StatValues could not be created");

        return statValues.Adapt<List<StatValueDto>>();
    }

    public async Task<StatValueDto> UpdateStatValueAsync(long statValueId, long characterId, long statId,
        StatValueForUpdateDto statValueForUpdateDto)
    {
        var statValue = await _dbContext.StatValues.FirstOrDefaultAsync(sv => sv.Id == statValueId);
        if (statValue == null)
            throw new StatValueNotFoundException(statValueId);

        var character = await _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == characterId);
        if (character == null)
            throw new CharacterNotFoundException(characterId);

        var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == statId);
        if (stat == null)
            throw new StatNotFoundException(statId);

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
            throw new InvalidOperationException("StatValue could not be updated");

        return statValue.Adapt<StatValueDto>();
    }

    public async Task<StatValueDto?> DeleteStatValueAsync(long statValueId)
    {
        var statValue = await _dbContext.StatValues.FirstOrDefaultAsync(sv => sv.Id == statValueId);
        if (statValue == null)
            throw new StatValueNotFoundException(statValueId);

        _dbContext.Remove(statValue);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            throw new InvalidOperationException("StatValue could not be deleted");

        return statValue.Adapt<StatValueDto>();
    }

    public async Task<List<StatValueDto>> DeleteStatValuesByCharacterIdAndLevelAsync(long characterId, int level)
    {
        var character = await _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == characterId);
        if (character == null)
            throw new CharacterNotFoundException(characterId);

        var statValues = await _dbContext.StatValues
            .Where(sv => sv.CharacterId == characterId && sv.Level == level)
            .ToListAsync();

        if (statValues.Count == 0)
            throw new ArgumentException($"StatValues with Character ID {characterId} and Level {level} not found");

        _dbContext.RemoveRange(statValues);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            throw new InvalidOperationException("StatValues could not be deleted");

        return statValues.Adapt<List<StatValueDto>>();
    }
}