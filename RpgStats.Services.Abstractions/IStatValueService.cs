﻿using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface IStatValueService
{
    Task<ServiceResult<List<StatValueDto>>> GetAllStatValuesAsync();
    Task<ServiceResult<List<StatValueDto>>> GetAllStatValuesByCharacterIdAsync(long characterId);
    Task<ServiceResult<List<StatValueDto>>> GetAllStatValuesByStatIdAsync(long statId);
    Task<ServiceResult<StatValueDto>> GetStatValueByIdAsync(long statValueId);
    Task<ServiceResult<StatValueDto>> CreateStatValueAsync(StatValueForCreationDto statValueForCreationDto);
    Task<ServiceResult<List<StatValueDto>>> CreateStatValuesAsync(List<StatValueForCreationDto> statValuesForCreationDto);
    Task<ServiceResult<StatValueDto>> UpdateStatValueAsync(long statValueId, long characterId, long statId,
        StatValueForUpdateDto statValueForUpdateDto);
    Task<ServiceResult<StatValueDto>> DeleteStatValueAsync(long statValueId);
    Task<ServiceResult<List<StatValueDto>>> DeleteStatValuesByCharacterIdAndLevelAsync(long characterId, int level);
}