using Mapster;
using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Domain.Exceptions;
using RpgStats.Dto;
using RpgStats.Repo;
using RpgStats.Services.Abstractions;

namespace RpgStats.Services;

public class GameStatService : IGameStatService
{
    private readonly RpgStatsContext _dbContext;

    public GameStatService(RpgStatsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GameStatDto>> GetAllGameStatsAsync()
    {
        var gameStats = await _dbContext.GameStats
            .ToListAsync();

        return gameStats.Adapt<List<GameStatDto>>();
    }

    public async Task<List<GameStatDto>> GetAllGameStatsByGameIdAsync(long gameId)
    {
        var gameStats = await _dbContext.GameStats
            .Where(gs => gs.GameId == gameId)
            .ToListAsync();

        return gameStats.Adapt<List<GameStatDto>>();
    }

    public async Task<List<GameStatDto>> GetAllGameStatsByStatIdAsync(long statId)
    {
        var gameStats = await _dbContext.GameStats
            .Where(gs => gs.StatId == statId)
            .ToListAsync();

        return gameStats.Adapt<List<GameStatDto>>();
    }

    public async Task<GameStatDto?> GetGameStatByIdAsync(long gameStatId)
    {
        var gameStat = await _dbContext.GameStats
            .FirstOrDefaultAsync(gs => gs.Id == gameStatId);

        if (gameStat == null) throw new GameStatNotFoundException(gameStatId);

        return gameStat.Adapt<GameStatDto>();
    }

    public async Task<GameStatDto?> CreateGameStatAsync(long gameId, long statId)
    {
        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);

        if (game == null) throw new GameNotFoundException(gameId);

        var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == statId);

        if (stat == null) throw new StatNotFoundException(statId);

        var gameStat = new GameStatDto().Adapt<GameStat>();
        gameStat.GameId = gameId;
        gameStat.Game = game;
        gameStat.StatId = statId;
        gameStat.Stat = stat;

        _dbContext.GameStats.Add(gameStat);
        await _dbContext.SaveChangesAsync();

        return gameStat.Adapt<GameStatDto>();
    }

    public async Task<GameStatDto?> UpdateGameStatAsync(long gameStatId, long gameId, long statId)
    {
        var gameStat = await _dbContext.GameStats.FirstOrDefaultAsync(gs => gs.Id == gameStatId);

        if (gameStat == null) throw new GameStatNotFoundException(gameStatId);

        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);

        if (game == null) throw new GameNotFoundException(gameId);

        var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == statId);

        if (stat == null) throw new StatNotFoundException(statId);

        gameStat.StatId = statId;
        gameStat.Stat = stat;
        gameStat.GameId = gameId;
        gameStat.Game = game;

        _dbContext.Entry(gameStat).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();

        return gameStat.Adapt<GameStatDto>();
    }

    public async Task<Task> DeleteGameStatAsync(long gameStatId)
    {
        var gameStats = await _dbContext.GameStats.FirstOrDefaultAsync(gs => gs.Id == gameStatId);

        if (gameStats == null) return Task.CompletedTask;

        _dbContext.Remove(gameStats);

        await _dbContext.SaveChangesAsync();

        return Task.CompletedTask;
    }

    public async Task<Task> DeleteGameStatByGameIdAsync(long gameId)
    {
        var gameStats = await _dbContext.GameStats.Where(gs => gs.GameId == gameId).ToListAsync();

        if (gameStats.Count == 0) return Task.CompletedTask;
        foreach (var gameStat in gameStats) await DeleteGameStatAsync(gameStat.Id);

        return Task.CompletedTask;
    }

    public async Task<Task> DeleteGameStatByStatIdAsync(long statId)
    {
        var gameStats = await _dbContext.GameStats.Where(gs => gs.StatId == statId).ToListAsync();

        if (gameStats.Count == 0) return Task.CompletedTask;
        foreach (var gameStat in gameStats) await DeleteGameStatAsync(gameStat.Id);

        return Task.CompletedTask;
    }
}