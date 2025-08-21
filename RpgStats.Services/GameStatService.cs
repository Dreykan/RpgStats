using Mapster;
using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
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

        return gameStat.Adapt<GameStatDto>();
    }

    public async Task<GameStatDto> CreateGameStatAsync(GameStatForCreationDto gameStatForCreationDto)
    {
        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameStatForCreationDto.GameId);
        if (game == null)
            throw new ArgumentException($"Game with ID {gameStatForCreationDto.GameId} not found");

        var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == gameStatForCreationDto.StatId);
        if (stat == null)
            throw new ArgumentException($"Stat with ID {gameStatForCreationDto.StatId} not found");

        var gameStat = gameStatForCreationDto.Adapt<GameStat>();
        gameStat.GameId = game.Id;
        gameStat.Game = game;
        gameStat.StatId = stat.Id;
        gameStat.Stat = stat;

        _dbContext.GameStats.Add(gameStat);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            throw new InvalidOperationException("GameStat could not be created");

        return gameStat.Adapt<GameStatDto>();
    }

    public async Task<GameStatDto> UpdateGameStatAsync(long gameStatId, GameStatForUpdateDto gameStatForUpdateDto)
    {
        var gameStat = await _dbContext.GameStats.FirstOrDefaultAsync(gs => gs.Id == gameStatId);
        if (gameStat == null)
            throw new ArgumentException($"GameStat with ID {gameStatId} not found");

        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameStatForUpdateDto.GameId);
        if (game == null)
            throw new ArgumentException($"Game with ID {gameStatForUpdateDto.GameId} not found");

        var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == gameStatForUpdateDto.StatId);
        if (stat == null)
            throw new ArgumentException($"Stat with ID {gameStatForUpdateDto.StatId} not found");

        gameStat.SortIndex = gameStatForUpdateDto.SortIndex;
        gameStat.StatId = gameStatForUpdateDto.StatId;
        gameStat.Stat = stat;
        gameStat.GameId = gameStatForUpdateDto.GameId;
        gameStat.Game = game;

        _dbContext.Entry(gameStat).State = EntityState.Modified;
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            throw new InvalidOperationException("GameStat could not be updated");

        return gameStat.Adapt<GameStatDto>();
    }

    public async Task<GameStatDto?> DeleteGameStatAsync(long gameStatId)
    {
        var gameStat = await _dbContext.GameStats.FirstOrDefaultAsync(gs => gs.Id == gameStatId);
        if (gameStat == null)
            throw new ArgumentException($"GameStat with ID {gameStatId} not found");

        _dbContext.Remove(gameStat);

        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            throw new InvalidOperationException("GameStat could not be deleted");

        return gameStat.Adapt<GameStatDto>();
    }

    public async Task<List<GameStatDto>> DeleteGameStatsByGameIdAsync(long gameId)
    {
        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);
        if (game == null)
            throw new ArgumentException($"Game with ID {gameId} not found");

        var gameStats = await _dbContext.GameStats.Where(gs => gs.GameId == gameId).ToListAsync();
        if (gameStats.Count == 0)
            throw new ArgumentException($"GameStats for Game with ID {gameId} not found");


        _dbContext.RemoveRange(gameStats);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            throw new InvalidOperationException("GameStats could not be deleted");

        return gameStats.Adapt<List<GameStatDto>>();
    }

    public async Task<List<GameStatDto>> DeleteGameStatsByStatIdAsync(long statId)
    {
        var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == statId);
        if (stat == null)
            throw new ArgumentException($"Stat with ID {statId} not found");

        var gameStats = await _dbContext.GameStats.Where(gs => gs.StatId == statId).ToListAsync();
        if (gameStats.Count == 0)
            throw new ArgumentException($"GameStats for Stat with ID {statId} not found");

        _dbContext.RemoveRange(gameStats);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            throw new InvalidOperationException("GameStats could not be deleted");

        return gameStats.Adapt<List<GameStatDto>>();
    }
}