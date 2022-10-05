﻿using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface IPlatformService
{
    Task<List<PlatformDto>> GetAllPlatformsAsync();
    Task<PlatformDto?> GetPlatformByIdAsync(long platformId);
    Task<PlatformDto?> CreatePlatformAsync(PlatformForCreationDto platformForCreationDto);
    Task<PlatformDto?> UpdatePlatformAsync(long platformId, PlatformForUpdateDto platformForUpdateDto);
    Task DeletePlatformAsync(long platformId);
}