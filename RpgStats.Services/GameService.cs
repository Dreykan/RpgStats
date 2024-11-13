using System.Diagnostics.CodeAnalysis;
using Mapster;
using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Domain.Exceptions;
using RpgStats.Dto;
using RpgStats.Dto.Mapper;
using RpgStats.Repo;
using RpgStats.Services.Abstractions;

namespace RpgStats.Services;

[SuppressMessage("Performance",
    "CA1862:\"StringComparison\"-Methodenüberladungen verwenden, um Zeichenfolgenvergleiche ohne Beachtung der Groß-/Kleinschreibung durchzuführen")]
public class GameService : IGameService
{
    private readonly RpgStatsContext _dbContext;

    public GameService(RpgStatsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GameDto>> GetAllGamesAsync()
    {
        var games = await _dbContext.Games
            .Include(g => g.PlatformGames)
            .Include(g => g.Characters)
            .ToListAsync();

        return games.Adapt<List<GameDto>>();
    }

    public async Task<List<GameDto>> GetAllGamesByNameAsync(string name)
    {
        var games = await _dbContext.Games
            .Include(g => g.PlatformGames)
            .Include(g => g.Characters)
            .Where(g => g.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        return games.Adapt<List<GameDto>>();
    }

    public async Task<GameDto?> GetGameByIdAsync(long gameId)
    {
        var game = await _dbContext.Games
            .Include(g => g.PlatformGames)
            .Include(g => g.Characters)
            .FirstOrDefaultAsync(x => x.Id == gameId);

        if (game == null) throw new GameNotFoundException(gameId);

        return game.Adapt<GameDto>();
    }

    public async Task<GameDto?> CreateGameAsync(GameForCreationDto gameForCreationDto)
    {
        var game = gameForCreationDto.Adapt<Game>();

        _dbContext.Games.Add(game);
        await _dbContext.SaveChangesAsync();

        return game.Adapt<GameDto>();
    }

    public async Task<GameDto?> UpdateGameAsync(long gameId, GameForUpdateDto gameForUpdateDto)
    {
        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);

        if (game == null) throw new GameNotFoundException(gameId);

        game.Name = gameForUpdateDto.Name;
        game.Picture = gameForUpdateDto.Picture;

        _dbContext.Entry(game).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();

        return game.Adapt<GameDto>();
    }

    public async Task<Task> DeleteGameAsync(long gameId)
    {
        var game = _dbContext.Games.FirstOrDefaultAsync(x => x.Id == gameId).Result;

        if (game == null) return Task.CompletedTask;

        _dbContext.Remove(game);

        await _dbContext.SaveChangesAsync();

        return Task.CompletedTask;
    }

    public async Task<List<GameDetailDto>> GetAllGameDetailDtosAsync()
    {
        var games = await _dbContext.Games
            .Include(g => g.PlatformGames)
            .Include(g => g.Characters)
            .Include(g => g.GameStats)
            .ToListAsync();

        var platforms = await _dbContext.Platforms
            .ToListAsync();

        var stats = await _dbContext.Stats
            .ToListAsync();

        var gameDetailDtos = new List<GameDetailDto>();

        foreach (var game in games)
        {
            var platformsFiltered = new List<Platform?>();
            if (game.PlatformGames != null)
                platformsFiltered.AddRange(game.PlatformGames.Select(pg =>
                    platforms.FirstOrDefault(p => p.Id == pg.PlatformId)));

            var statsFiltered = new List<Stat?>();
            if (game.GameStats != null)
                statsFiltered.AddRange(game.GameStats.Select(gs => stats.FirstOrDefault(s => s.Id == gs.StatId)));

            gameDetailDtos.Add(GameMapper.MapToGameDetailDto(game, platformsFiltered, statsFiltered));
        }

        return gameDetailDtos;
    }

    public async Task<List<GameDetailDto>> GetAllGameDetailDtosByNameAsync(string name)
    {
        var games = await _dbContext.Games
            .Include(g => g.PlatformGames)
            .Include(g => g.Characters)
            .Include(g => g.GameStats)
            .Where(g => g.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        var platforms = await _dbContext.Platforms
            .ToListAsync();

        var stats = await _dbContext.Stats
            .ToListAsync();

        var gameDetailDtos = new List<GameDetailDto>();

        foreach (var game in games)
        {
            var platformsFiltered = new List<Platform?>();
            if (game.PlatformGames != null)
                platformsFiltered.AddRange(game.PlatformGames.Select(pg =>
                    platforms.FirstOrDefault(p => p.Id == pg.PlatformId)));

            var statsFiltered = new List<Stat?>();
            if (game.GameStats != null)
                statsFiltered.AddRange(game.GameStats.Select(gs => stats.FirstOrDefault(s => s.Id == gs.StatId)));

            gameDetailDtos.Add(GameMapper.MapToGameDetailDto(game, platformsFiltered, statsFiltered));
        }

        return gameDetailDtos;
    }

    public async Task<GameDetailDto?> GetGameDetailDtoByIdAsync(long gameId)
    {
        var game = await _dbContext.Games
            .Include(g => g.PlatformGames)
            .Include(g => g.Characters)
            .Include(g => g.GameStats)
            .FirstOrDefaultAsync(g => g.Id == gameId);

        var platforms = await _dbContext.Platforms
            .ToListAsync();

        var stats = await _dbContext.Stats
            .ToListAsync();

        var gameDetailDto = new GameDetailDto();
        if (game == null) return gameDetailDto;
        var platformsFiltered = new List<Platform?>();
        if (game.PlatformGames != null)
            platformsFiltered.AddRange(game.PlatformGames.Select(pg =>
                platforms.FirstOrDefault(p => p.Id == pg.PlatformId)));

        var statsFiltered = new List<Stat?>();
        if (game.GameStats != null)
            statsFiltered.AddRange(game.GameStats.Select(gs => stats.FirstOrDefault(s => s.Id == gs.StatId)));

        gameDetailDto = GameMapper.MapToGameDetailDto(game, platformsFiltered, statsFiltered);

        return gameDetailDto;
    }
}