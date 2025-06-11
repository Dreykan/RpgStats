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

    public async Task<ServiceResult<List<GameStatDto>>> GetAllGameStatsAsync()
    {
        var gameStats = await _dbContext.GameStats
            .ToListAsync();

        if (gameStats.Count == 0)
            return ServiceResult<List<GameStatDto>>.ErrorResult("No GameStats found");

        return ServiceResult<List<GameStatDto>>.SuccessResult(gameStats.Adapt<List<GameStatDto>>());
    }

    public async Task<ServiceResult<List<GameStatDto>>> GetAllGameStatsByGameIdAsync(long gameId)
    {
        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);
        if (game == null)
            return ServiceResult<List<GameStatDto>>.ErrorResult($"Game with ID {gameId} not found");

        var gameStats = await _dbContext.GameStats
            .Where(gs => gs.GameId == gameId)
            .ToListAsync();

        if (gameStats.Count == 0)
            return ServiceResult<List<GameStatDto>>.ErrorResult("No GameStats found");

        return ServiceResult<List<GameStatDto>>.SuccessResult(gameStats.Adapt<List<GameStatDto>>());
    }

    public async Task<ServiceResult<List<GameStatDto>>> GetAllGameStatsByStatIdAsync(long statId)
    {
        var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == statId);
        if (stat == null)
            return ServiceResult<List<GameStatDto>>.ErrorResult($"Stat with ID {statId} not found");

        var gameStats = await _dbContext.GameStats
            .Where(gs => gs.StatId == statId)
            .ToListAsync();

        if (gameStats.Count == 0)
            return ServiceResult<List<GameStatDto>>.ErrorResult("No GameStats found");

        return ServiceResult<List<GameStatDto>>.SuccessResult(gameStats.Adapt<List<GameStatDto>>());
    }

    public async Task<ServiceResult<GameStatDto>> GetGameStatByIdAsync(long gameStatId)
    {
        var gameStat = await _dbContext.GameStats
            .FirstOrDefaultAsync(gs => gs.Id == gameStatId);

        if (gameStat == null)
            return ServiceResult<GameStatDto>.ErrorResult($"GameStat with ID {gameStatId} not found");

        return ServiceResult<GameStatDto>.SuccessResult(gameStat.Adapt<GameStatDto>());
    }

    public async Task<ServiceResult<GameStatDto>> CreateGameStatAsync(GameStatForCreationDto gameStatForCreationDto)
    {
        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameStatForCreationDto.GameId);
        if (game == null)
            return ServiceResult<GameStatDto>.ErrorResult($"Game with ID {gameStatForCreationDto.GameId} not found");

        var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == gameStatForCreationDto.StatId);
        if (stat == null)
            return ServiceResult<GameStatDto>.ErrorResult($"Stat with ID {gameStatForCreationDto.StatId} not found");

        var gameStat = gameStatForCreationDto.Adapt<GameStat>();

        _dbContext.GameStats.Add(gameStat);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<GameStatDto>.ErrorResult("GameStat could not be created");

        return ServiceResult<GameStatDto>.SuccessResult(gameStat.Adapt<GameStatDto>());
    }

    public async Task<ServiceResult<GameStatDto>> UpdateGameStatAsync(long gameStatId, GameStatForUpdateDto gameStatForUpdateDto)
    {
        var gameStat = await _dbContext.GameStats.FirstOrDefaultAsync(gs => gs.Id == gameStatId);
        if (gameStat == null)
            return ServiceResult<GameStatDto>.ErrorResult($"GameStat with ID {gameStatId} not found");

        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameStatForUpdateDto.GameId);
        if (game == null) return ServiceResult<GameStatDto>.ErrorResult($"Game with ID {gameStatForUpdateDto.GameId} not found");

        var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == gameStatForUpdateDto.StatId);
        if (stat == null)
            return ServiceResult<GameStatDto>.ErrorResult($"Stat with ID {gameStatForUpdateDto.StatId} not found");

        gameStat.SortIndex = gameStatForUpdateDto.SortIndex;
        gameStat.StatId = gameStatForUpdateDto.StatId;
        gameStat.Stat = stat;
        gameStat.GameId = gameStatForUpdateDto.GameId;
        gameStat.Game = game;

        _dbContext.Entry(gameStat).State = EntityState.Modified;
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<GameStatDto>.ErrorResult("GameStat could not be updated");

        return ServiceResult<GameStatDto>.SuccessResult(gameStat.Adapt<GameStatDto>());
    }

    public async Task<ServiceResult<GameStatDto>> DeleteGameStatAsync(long gameStatId)
    {
        var gameStat = await _dbContext.GameStats.FirstOrDefaultAsync(gs => gs.Id == gameStatId);
        if (gameStat == null)
            return ServiceResult<GameStatDto>.ErrorResult($"GameStat with ID {gameStatId} not found");

        _dbContext.Remove(gameStat);

        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<GameStatDto>.ErrorResult("GameStat could not be deleted");

        return ServiceResult<GameStatDto>.SuccessResult(gameStat.Adapt<GameStatDto>());
    }

    public async Task<ServiceResult<List<GameStatDto>>> DeleteGameStatsByGameIdAsync(long gameId)
    {
        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);
        if (game == null)
            return ServiceResult<List<GameStatDto>>.ErrorResult($"Game with ID {gameId} not found");

        var gameStats = await _dbContext.GameStats.Where(gs => gs.GameId == gameId).ToListAsync();
        if (gameStats.Count == 0)
            return ServiceResult<List<GameStatDto>>.ErrorResult("No GameStats found");


        _dbContext.RemoveRange(gameStats);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<List<GameStatDto>>.ErrorResult("GameStats could not be deleted");

        return ServiceResult<List<GameStatDto>>.SuccessResult(gameStats.Adapt<List<GameStatDto>>());
    }

    public async Task<ServiceResult<List<GameStatDto>>> DeleteGameStatsByStatIdAsync(long statId)
    {
        var stat = await _dbContext.Stats.FirstOrDefaultAsync(s => s.Id == statId);
        if (stat == null)
            return ServiceResult<List<GameStatDto>>.ErrorResult($"Stat with ID {statId} not found");

        var gameStats = await _dbContext.GameStats.Where(gs => gs.StatId == statId).ToListAsync();
        if (gameStats.Count == 0)
            return ServiceResult<List<GameStatDto>>.ErrorResult("No GameStats found");

        _dbContext.RemoveRange(gameStats);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<List<GameStatDto>>.ErrorResult("GameStats could not be deleted");

        return ServiceResult<List<GameStatDto>>.SuccessResult(gameStats.Adapt<List<GameStatDto>>());
    }
}