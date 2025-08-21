using System.Diagnostics.CodeAnalysis;
using Mapster;
using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Dto;
using RpgStats.Dto.Mapper;
using RpgStats.Repo;
using RpgStats.Services.Abstractions;

namespace RpgStats.Services;

[SuppressMessage("Performance",
    "CA1862:\"StringComparison\"-Methodenüberladungen verwenden, um Zeichenfolgevergleiche ohne Beachtung der Groß-/Kleinschreibung durchzuführen")]
public class PlatformService : IPlatformService
{
    private readonly RpgStatsContext _dbContext;

    public PlatformService(RpgStatsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<PlatformDto>> GetAllPlatformsAsync()
    {
        var platforms = await _dbContext.Platforms
            .ToListAsync();

        return platforms.Adapt<List<PlatformDto>>();
    }

    public async Task<PlatformDto?> GetPlatformByIdAsync(long platformId)
    {
        var platform = await _dbContext.Platforms
            .FirstOrDefaultAsync(p => p.Id == platformId);

        return platform.Adapt<PlatformDto>();
    }

    public async Task<PlatformDto> CreatePlatformAsync(PlatformForCreationDto platformForCreationDto)
    {
        var platform = platformForCreationDto.Adapt<Platform>();

        await _dbContext.Platforms.AddAsync(platform);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            throw new InvalidOperationException("Platform could not be created");

        return platform.Adapt<PlatformDto>();
    }

    public async Task<PlatformDto> UpdatePlatformAsync(long platformId,
        PlatformForUpdateDto platformForUpdateDto)
    {
        var platform = await _dbContext.Platforms
            .FirstOrDefaultAsync(p => p.Id == platformId);
        if (platform == null)
            throw new InvalidOperationException("$Platform with ID {platformId} not found");

        platform.Name = platformForUpdateDto.Name;

        _dbContext.Entry(platform).State = EntityState.Modified;
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            throw new InvalidOperationException("Platform could not be updated");

        return platform.Adapt<PlatformDto>();
    }

    public async Task<PlatformDto?> DeletePlatformAsync(long platformId)
    {
        var platform = await _dbContext.Platforms
            .FirstOrDefaultAsync(p => p.Id == platformId);
        if (platform == null)
            throw new InvalidOperationException("$Platform with ID {platformId} not found");

        _dbContext.Platforms.Remove(platform);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            throw new InvalidOperationException("Platform could not be deleted");

        return platform.Adapt<PlatformDto>();
    }
}