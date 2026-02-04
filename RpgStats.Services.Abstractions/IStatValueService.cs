using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface IStatValueService
{
    Task<List<StatValueDto>> GetAllStatValuesAsync();
    Task<List<StatValueDto>> GetAllStatValuesByCharacterIdAsync(long characterId);
    Task<List<StatValueDto>> GetAllStatValuesByCharacterLevelAsync(long characterId, int level);
    Task<List<StatValueDto>> GetAllStatValuesByStatIdAsync(long statId);
    Task<Dictionary<long, int>> GetHighestLevelByCharactersAsync(List<long> characterIds);
    Task<StatValueDto?> GetStatValueByIdAsync(long statValueId);
    Task<StatValueDto?> GetStatValueByCharacterLevelAndStatIdAsync(long characterId, int level, long statId);
    Task<StatValueDto> CreateStatValueAsync(StatValueForCreationDto statValueForCreationDto);
    Task<List<StatValueDto>> CreateMultipleStatValuesAsync(List<StatValueForCreationDto> statValueForCreationDtos);
    Task<StatValueDto> UpdateStatValueAsync(long statValueId, long characterId, long statId,
        StatValueForUpdateDto statValueForUpdateDto);
    Task<StatValueDto?> DeleteStatValueAsync(long statValueId);
    Task<List<StatValueDto>> DeleteStatValuesByCharacterIdAndLevelAsync(long characterId, int level);
}