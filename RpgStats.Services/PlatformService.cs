using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
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

    public async Task<List<Platform>> GetAllPlatformsAsync()
    {
        return await _dbContext.Platforms.ToListAsync();
    }

    public async Task<Platform?> GetPlatformByIdAsync(long platformId)
    {
        return await _dbContext.Platforms.FirstOrDefaultAsync(p => p.Id == platformId);
    }

    public async Task<Platform?> CreatePlatformAsync(Platform platform)
    {
        _dbContext.Platforms.Add(platform);
        await _dbContext.SaveChangesAsync();
        return await Task.FromResult(platform);
    }

    public async Task<Platform?> UpdatePlatformAsync(Platform platform)
    {
        _dbContext.Entry(platform).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return await Task.FromResult(platform);
    }

    public Task DeletePlatformAsync(long platformId)
    {
        Platform? platform = _dbContext.Platforms.FirstOrDefaultAsync(p => p.Id == platformId).Result;

        if (platform == null)
        {
            return Task.CompletedTask;
        }

        _dbContext.Remove(platform);
        return _dbContext.SaveChangesAsync();
    }
}