using RpgStats.Domain.Entities;
using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface IStatValueService
{
    Task<List<StatValueDto>> GetAllStatValuesAsync();
    Task<List<StatValueDto>> GetAllStatValuesByCharacterIdAsync(long characterId);
    Task<List<StatValueDto>> GetAllStatValuesByStatIdAsync(long statId);
    Task<StatValueDto?> GetStatValueByIdAsync(long statId);
    Task<StatValueDto?> CreateStatValueAsync(long characterId, long statId, StatValueForCreationDto statValueForCreationDto);
    Task<StatValueDto?> UpdateStatValueAsync(long statValueId, long characterId, long statId, StatValueForUpdateDto statValueForUpdateDto);
    Task DeleteStatValueAsync(long statId);
}