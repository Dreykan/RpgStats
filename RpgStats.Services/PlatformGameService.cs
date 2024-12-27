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

    public async Task<ServiceResult<List<PlatformGameDto>>> GetAllPlatformGamesAsync()
    {
        var platformGames = await _dbContext.PlatformGames
            .ToListAsync();

        if (platformGames.Count == 0)
            return ServiceResult<List<PlatformGameDto>>.ErrorResult("No PlatformGames found");

        return ServiceResult<List<PlatformGameDto>>.SuccessResult(platformGames.Adapt<List<PlatformGameDto>>());
    }

    public async Task<ServiceResult<List<PlatformGameDto>>> GetAllPlatformGamesByPlatformIdAsync(long platformId)
    {
        var platformGames = await _dbContext.PlatformGames
            .Where(pg => pg.PlatformId == platformId)
            .ToListAsync();

        if (platformGames.Count == 0)
            return ServiceResult<List<PlatformGameDto>>.ErrorResult("No PlatformGames found");

        return ServiceResult<List<PlatformGameDto>>.SuccessResult(platformGames.Adapt<List<PlatformGameDto>>());
    }

    public async Task<ServiceResult<List<PlatformGameDto>>> GetAllPlatformGamesByGameIdAsync(long gameId)
    {
        var platformGames = await _dbContext.PlatformGames
            .Where(pg => pg.GameId == gameId)
            .ToListAsync();

        if (platformGames.Count == 0)
            return ServiceResult<List<PlatformGameDto>>.ErrorResult("No PlatformGames found");

        return ServiceResult<List<PlatformGameDto>>.SuccessResult(platformGames.Adapt<List<PlatformGameDto>>());
    }

    public async Task<ServiceResult<PlatformGameDto>> GetPlatformGameByIdAsync(long platformGameId)
    {
        var platformGame = await _dbContext.PlatformGames
            .FirstOrDefaultAsync(pg => pg.Id == platformGameId);

        if (platformGame == null)
            return ServiceResult<PlatformGameDto>.ErrorResult($"PlatformGame with ID {platformGameId} not found");

        return ServiceResult<PlatformGameDto>.SuccessResult(platformGame.Adapt<PlatformGameDto>());
    }

    public async Task<ServiceResult<PlatformGameDto>> CreatePlatformGameAsync(long platformId, long gameId)
    {
        if (await PlatformGameExists(platformId, gameId))
            return ServiceResult<PlatformGameDto>.ErrorResult($"PlatformGame with PlatformId: {platformId} and GameId: {gameId} already exists");

        var platform = await _dbContext.Platforms.FirstOrDefaultAsync(p => p.Id == platformId);
        if (platform == null)
            return ServiceResult<PlatformGameDto>.ErrorResult($"Platform with ID {platformId} not found");

        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);
        if (game == null)
            return ServiceResult<PlatformGameDto>.ErrorResult($"Game with ID {gameId} not found");

        var platformGame = new PlatformGameDto().Adapt<PlatformGame>();
        platformGame.PlatformId = platformId;
        platformGame.Platform = platform;
        platformGame.GameId = gameId;
        platformGame.Game = game;


        _dbContext.PlatformGames.Add(platformGame);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<PlatformGameDto>.ErrorResult("PlatformGame could not be created");

        return ServiceResult<PlatformGameDto>.SuccessResult(platformGame.Adapt<PlatformGameDto>());
    }

    public async Task<ServiceResult<PlatformGameDto>> UpdatePlatformGameAsync(long platformGameId, long platformId, long gameId)
    {
        var platformGame = await _dbContext.PlatformGames.FirstOrDefaultAsync(pg => pg.Id == platformGameId);
        if (platformGame == null)
            return ServiceResult<PlatformGameDto>.ErrorResult($"PlatformGame with ID {platformGameId} not found");

        var platform = await _dbContext.Platforms.FirstOrDefaultAsync(p => p.Id == platformId);
        if (platform == null)
            return ServiceResult<PlatformGameDto>.ErrorResult($"Platform with ID {platformId} not found");

        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);
        if (game == null)
            return ServiceResult<PlatformGameDto>.ErrorResult($"Game with ID {gameId} not found");

        platformGame.PlatformId = platform.Id;
        platformGame.Platform = platform;
        platformGame.GameId = game.Id;
        platformGame.Game = game;


        _dbContext.Entry(platformGame).State = EntityState.Modified;
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<PlatformGameDto>.ErrorResult("PlatformGame could not be updated");

        return ServiceResult<PlatformGameDto>.SuccessResult(platformGame.Adapt<PlatformGameDto>());
    }

    public async Task<ServiceResult<PlatformGameDto>> DeletePlatformGameAsync(long platformGameId)
    {
        var platformGame = await _dbContext.PlatformGames.FirstOrDefaultAsync(pg => pg.Id == platformGameId);
        if (platformGame == null)
            return ServiceResult<PlatformGameDto>.ErrorResult($"PlatformGame with ID {platformGameId} not found");

        _dbContext.Remove(platformGame);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<PlatformGameDto>.ErrorResult("PlatformGame could not be deleted");

        return ServiceResult<PlatformGameDto>.SuccessResult(platformGame.Adapt<PlatformGameDto>());
    }

    public async Task<ServiceResult<List<PlatformGameDto>>> DeletePlatformGameByGameIdAsync(long gameId)
    {
        if (!await GameExists(gameId))
            return ServiceResult<List<PlatformGameDto>>.ErrorResult($"Game with ID {gameId} not found");

        var platformGames = _dbContext.PlatformGames.Where(pg => pg.GameId == gameId).ToList();
        if (platformGames.Count == 0)
            return ServiceResult<List<PlatformGameDto>>.SuccessResult(platformGames.Adapt<List<PlatformGameDto>>());

        _dbContext.RemoveRange(platformGames);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<List<PlatformGameDto>>.ErrorResult("PlatformGames could not be deleted");

        return ServiceResult<List<PlatformGameDto>>.SuccessResult(platformGames.Adapt<List<PlatformGameDto>>());
    }

    public async Task<ServiceResult<List<PlatformGameDto>>> DeletePlatformGameByPlatformIdAsync(long platformId)
    {
        if (!await PlatformExists(platformId))
            return ServiceResult<List<PlatformGameDto>>.ErrorResult($"Platform with ID {platformId} not found");

        var platformGames = _dbContext.PlatformGames.Where(pg => pg.PlatformId == platformId).ToList();
        if (platformGames.Count == 0)
            return ServiceResult<List<PlatformGameDto>>.SuccessResult(platformGames.Adapt<List<PlatformGameDto>>());

        _dbContext.RemoveRange(platformGames);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<List<PlatformGameDto>>.ErrorResult("PlatformGames could not be deleted");

        return ServiceResult<List<PlatformGameDto>>.SuccessResult(platformGames.Adapt<List<PlatformGameDto>>());
    }

    private async Task<bool> PlatformGameExists(long platformId, long gameId)
    {
        return await _dbContext.PlatformGames.AnyAsync(e => e.PlatformId == platformId && e.GameId == gameId);
    }

    private async Task<bool> PlatformExists(long id)
    {
        return await _dbContext.Platforms.AnyAsync(e => e.Id == id);
    }

    private async Task<bool> GameExists(long id)
    {
        return await _dbContext.Games.AnyAsync(e => e.Id == id);
    }
}