using Mapster;
using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
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

        return platformGame.Adapt<PlatformGameDto>();
    }

    public async Task<PlatformGameDto> CreatePlatformGameAsync(PlatformGameForCreationDto platformGameForCreation)
    {
        var existingPlatformGame = await _dbContext.PlatformGames
            .AnyAsync(e => e.PlatformId == platformGameForCreation.PlatformId &&
                           e.GameId == platformGameForCreation.GameId);
        if (existingPlatformGame)
            throw new InvalidOperationException($"A PlatformGame with PlatformId {platformGameForCreation.PlatformId} and GameId {platformGameForCreation.GameId} already exists");

        var platform = await _dbContext.Platforms.FirstOrDefaultAsync(p => p.Id == platformGameForCreation.PlatformId);
        if (platform == null)
            throw new ArgumentException($"Platform with id {platformGameForCreation.PlatformId} not found");

        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == platformGameForCreation.GameId);
        if (game == null)
            throw new ArgumentException($"Game with id {platformGameForCreation.GameId} not found");

        var platformGame = new PlatformGameDto().Adapt<PlatformGame>();
        platformGame.PlatformId = platformGameForCreation.PlatformId;
        platformGame.Platform = platform;
        platformGame.GameId = platformGameForCreation.GameId;
        platformGame.Game = game;


        _dbContext.PlatformGames.Add(platformGame);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            throw new InvalidOperationException($"Platform could not be created");

        return platformGame.Adapt<PlatformGameDto>();
    }

    public async Task<PlatformGameDto> UpdatePlatformGameAsync(long platformGameId, PlatformGameForUpdateDto platformGameForUpdate)
    {
        var platformGame = await _dbContext.PlatformGames.FirstOrDefaultAsync(pg => pg.Id == platformGameId);
        if (platformGame == null)
            throw new ArgumentException($"Platform with id {platformGameId} not found");

        var platform = await _dbContext.Platforms.FirstOrDefaultAsync(p => p.Id == platformGameForUpdate.PlatformId);
        if (platform == null)
           throw new ArgumentException($"Platform with id {platformGameForUpdate.PlatformId} not found");

        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == platformGameForUpdate.GameId);
        if (game == null)
            throw new ArgumentException($"Game with id {platformGameForUpdate.GameId} not found");

        platformGame.PlatformId = platform.Id;
        platformGame.Platform = platform;
        platformGame.GameId = game.Id;
        platformGame.Game = game;

        _dbContext.Entry(platformGame).State = EntityState.Modified;
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
          throw new InvalidOperationException($"Platform could not be updated");

        return platformGame.Adapt<PlatformGameDto>();
    }

    public async Task<PlatformGameDto?> DeletePlatformGameAsync(long platformGameId)
    {
        var platformGame = await _dbContext.PlatformGames.FirstOrDefaultAsync(pg => pg.Id == platformGameId);
        if (platformGame == null)
            return new PlatformGameDto();

        _dbContext.Remove(platformGame);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            throw new InvalidOperationException($"PlatformGame could not be deleted");

        return platformGame.Adapt<PlatformGameDto>();
    }

    public async Task<List<PlatformGameDto>> DeletePlatformGameByGameIdAsync(long gameId)
    {
        var platformGames = _dbContext.PlatformGames.Where(pg => pg.GameId == gameId).ToList();
        if (platformGames.Count == 0)
            return new List<PlatformGameDto>();

        _dbContext.RemoveRange(platformGames);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            throw new InvalidOperationException($"PlatformGames could not be deleted");

        return platformGames.Adapt<List<PlatformGameDto>>();
    }

    public async Task<List<PlatformGameDto>> DeletePlatformGameByPlatformIdAsync(long platformId)
    {
        var platformGames = _dbContext.PlatformGames.Where(pg => pg.PlatformId == platformId).ToList();
        if (platformGames.Count == 0)
            return new List<PlatformGameDto>();

        _dbContext.RemoveRange(platformGames);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            throw new InvalidOperationException($"PlatformGames could not be deleted");

        return platformGames.Adapt<List<PlatformGameDto>>();
    }
}