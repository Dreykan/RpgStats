using RpgStats.Domain.Entities;

namespace RpgStats.Services.Abstractions;

public interface IPlatformService
{
    Task<List<Platform>> GetAllPlatformsAsync();
    Task<Platform?> GetPlatformByIdAsync(long platformId);
    Task<Platform?> CreatePlatformAsync(Platform platform);
    Task<Platform?> UpdatePlatformAsync(Platform platform);
    Task DeletePlatformAsync(long platformId);
}