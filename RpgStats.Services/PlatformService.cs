using Mapster;
using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Domain.Exceptions;
using RpgStats.Dto;
using RpgStats.Repo;
using RpgStats.Services.Abstractions;

namespace RpgStats.Services;

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
            .Include(p => p.PlatformGames)
            .ToListAsync();

        return platforms.Adapt<List<PlatformDto>>();
    }

    public async Task<PlatformDto?> GetPlatformByIdAsync(long platformId)
    {
        var platform = await _dbContext.Platforms
            .Include(p => p.PlatformGames)
            .FirstOrDefaultAsync(p => p.Id == platformId);

        if (platform == null)
        {
            throw new PlatformNotFoundException(platformId);
        }

        return platform.Adapt<PlatformDto>();
    }

    public async Task<PlatformDto?> CreatePlatformAsync(PlatformForCreationDto platformForCreationDto)
    {
        var platform = platformForCreationDto.Adapt<Platform>();

        _dbContext.Platforms.Add(platform);
        await _dbContext.SaveChangesAsync();

        return platform.Adapt<PlatformDto>();
    }

    public async Task<PlatformDto?> UpdatePlatformAsync(long platformId, PlatformForUpdateDto platformForUpdateDto)
    {
        var platform = await _dbContext.Platforms.FirstOrDefaultAsync(p => p.Id == platformId);

        if (platform == null)
        {
            throw new PlatformNotFoundException(platformId);
        }

        platform.Name = platformForUpdateDto.Name;

        _dbContext.Entry(platform).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();

        return platform.Adapt<PlatformDto>();
    }

    public Task DeletePlatformAsync(long platformId)
    {
        var platform = _dbContext.Platforms.FirstOrDefaultAsync(p => p.Id == platformId).Result;

        if (platform == null)
        {
            return Task.CompletedTask;
        }

        _dbContext.Remove(platform);

        return _dbContext.SaveChangesAsync();
    }
}