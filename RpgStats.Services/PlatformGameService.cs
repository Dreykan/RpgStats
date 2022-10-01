using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Repo;
using RpgStats.Services.Abstractions;

namespace RpgStats.Services;

public class PlatformGameService : IPlatformGameService
{
    private readonly RpgStatsContext _dbContext;

    public PlatformGameService(RpgStatsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<PlatformGame>> GetAllPlatformGamesAsync()
    {
        return await _dbContext.PlatformGames
            .Include(pg => pg.Game)
            .Include(pg => pg.Platform)
            .ToListAsync();
    }

    public async Task<List<PlatformGame>> GetAllPlatformGamesByPlatformAsync(Platform platform)
    {
        return await _dbContext.PlatformGames
            .Include(pg => pg.Game)
            .Include(pg => pg.Platform)
            .Where(pg => pg.Platform == platform)
            .ToListAsync();
    }

    public async Task<List<PlatformGame>> GetAllPlatformGamesByGameAsync(Game game)
    {
        return await _dbContext.PlatformGames
            .Include(pg => pg.Game)
            .Include(pg => pg.Platform)
            .Where(pg => pg.Game == game)
            .ToListAsync();
    }

    public async Task<PlatformGame?> GetPlatformGameByIdAsync(long platformGameId)
    {
        return await _dbContext.PlatformGames
            .Include(pg => pg.Game)
            .Include(pg => pg.Platform)
            .FirstOrDefaultAsync(pg => pg.Id == platformGameId);
    }

    public async Task<PlatformGame?> CreatePlatformGameAsync(PlatformGame platformGame)
    {
        _dbContext.PlatformGames.Add(platformGame);
        await _dbContext.SaveChangesAsync();
        return await Task.FromResult(platformGame);
    }

    public async Task<PlatformGame?> UpdatePlatformGameAsync(PlatformGame platformGame)
    {
        _dbContext.Entry(platformGame).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return await Task.FromResult(platformGame);
    }

    public Task DeletePlatformGameAsync(long platformId)
    {
        PlatformGame? platformGame = _dbContext.PlatformGames.FirstOrDefaultAsync(pg => pg.Id == platformId).Result;

        if (platformGame == null)
        {
            return Task.CompletedTask;
        }

        _dbContext.Remove(platformGame);
        return _dbContext.SaveChangesAsync();
    }
}