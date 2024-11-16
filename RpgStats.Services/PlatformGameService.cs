using Mapster;
using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Domain.Exceptions;
using RpgStats.Dto;
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

    public async Task<List<PlatformGameDto>> GetAllPlatformGamesAsync()
    {
        var platformGames = await _dbContext.PlatformGames
            .ToListAsync();

        return platformGames.Adapt<List<PlatformGameDto>>();
    }

    public async Task<List<PlatformGameDto>> GetAllPlatformGamesByPlatformIdAsync(long platformId)
    {
        var platformGames = await _dbContext.PlatformGames
            .Where(pg => pg.PlatformId == platformId)
            .ToListAsync();

        return platformGames.Adapt<List<PlatformGameDto>>();
    }

    public async Task<List<PlatformGameDto>> GetAllPlatformGamesByGameIdAsync(long gameId)
    {
        var platformGames = await _dbContext.PlatformGames
            .Where(pg => pg.GameId == gameId)
            .ToListAsync();

        return platformGames.Adapt<List<PlatformGameDto>>();
    }

    public async Task<PlatformGameDto?> GetPlatformGameByIdAsync(long platformGameId)
    {
        var platformGame = await _dbContext.PlatformGames
            .FirstOrDefaultAsync(pg => pg.Id == platformGameId);

        if (platformGame == null) throw new PlatformGameNotFoundException(platformGameId);

        return platformGame.Adapt<PlatformGameDto>();
    }

    public async Task<PlatformGameDto?> CreatePlatformGameAsync(long platformId, long gameId)
    {
        var platform = await _dbContext.Platforms.FirstOrDefaultAsync(p => p.Id == platformId);

        if (platform == null) throw new PlatformNotFoundException(platformId);

        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);

        if (game == null) throw new GameNotFoundException(gameId);

        var platformGame = new PlatformGameDto().Adapt<PlatformGame>();
        platformGame.PlatformId = platformId;
        platformGame.Platform = platform;
        platformGame.GameId = gameId;
        platformGame.Game = game;


        _dbContext.PlatformGames.Add(platformGame);
        await _dbContext.SaveChangesAsync();

        return platformGame.Adapt<PlatformGameDto>();
    }

    public async Task<PlatformGameDto?> UpdatePlatformGameAsync(long platformGameId, long platformId, long gameId)
    {
        var platformGame = await _dbContext.PlatformGames.FirstOrDefaultAsync(pg => pg.Id == platformGameId);

        if (platformGame == null) throw new PlatformGameNotFoundException(platformGameId);

        var platform = await _dbContext.Platforms.FirstOrDefaultAsync(p => p.Id == platformId);

        if (platform == null) throw new PlatformNotFoundException(platformId);

        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);

        if (game == null) throw new GameNotFoundException(gameId);

        platformGame.PlatformId = platform.Id;
        platformGame.Platform = platform;
        platformGame.GameId = game.Id;
        platformGame.Game = game;


        _dbContext.Entry(platformGame).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();

        return platformGame.Adapt<PlatformGameDto>();
    }

    public async Task<Task> DeletePlatformGameAsync(long platformGameId)
    {
        var platformGame = await _dbContext.PlatformGames.FirstOrDefaultAsync(pg => pg.Id == platformGameId);

        if (platformGame == null) return Task.CompletedTask;

        _dbContext.Remove(platformGame);

        await _dbContext.SaveChangesAsync();

        return Task.CompletedTask;
    }

    public async Task<Task> DeletePlatformGameByGameIdAsync(long gameId)
    {
        var platformGames = _dbContext.PlatformGames.Where(pg => pg.GameId == gameId).ToList();

        if (platformGames.Count == 0) return Task.CompletedTask;
        foreach (var platformGame in platformGames) await DeletePlatformGameAsync(platformGame.Id);

        return Task.CompletedTask;
    }

    public async Task<Task> DeletePlatformGameByPlatformIdAsync(long platformId)
    {
        var platformGames = _dbContext.PlatformGames.Where(pg => pg.PlatformId == platformId).ToList();

        if (platformGames.Count == 0) return Task.CompletedTask;
        foreach (var platformGame in platformGames) await DeletePlatformGameAsync(platformGame.Id);

        return Task.CompletedTask;
    }
}