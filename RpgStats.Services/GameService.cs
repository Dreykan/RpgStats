using System.Diagnostics.CodeAnalysis;
using Mapster;
using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Dto;
using RpgStats.Dto.Mapper;
using RpgStats.Repo;
using RpgStats.Services.Abstractions;

namespace RpgStats.Services;

[SuppressMessage("Performance",
    "CA1862:\"StringComparison\"-Methodenüberladungen verwenden, um Zeichenfolgevergleiche ohne Beachtung der Groß-/Kleinschreibung durchzuführen")]
public class GameService : IGameService
{
    private readonly RpgStatsContext _dbContext;

    public GameService(RpgStatsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResult<List<GameDto>>> GetAllGamesAsync()
    {
        var games = await _dbContext.Games
            .Include(g => g.PlatformGames)
            .Include(g => g.Characters)
            .ToListAsync();

        if (games.Count == 0)
            return ServiceResult<List<GameDto>>.ErrorResult("No games found");

        return ServiceResult<List<GameDto>>.SuccessResult(games.Adapt<List<GameDto>>());
    }

    public async Task<ServiceResult<List<GameDto>>> GetAllGamesByNameAsync(string name)
    {
        var games = await _dbContext.Games
            .Include(g => g.PlatformGames)
            .Include(g => g.Characters)
            .Where(g => g.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        if (games.Count == 0)
            return ServiceResult<List<GameDto>>.ErrorResult("No games found");

        return ServiceResult<List<GameDto>>.SuccessResult(games.Adapt<List<GameDto>>());
    }

    public async Task<ServiceResult<GameDto>> GetGameByIdAsync(long gameId)
    {
        var game = await _dbContext.Games
            .Include(g => g.PlatformGames)
            .Include(g => g.Characters)
            .FirstOrDefaultAsync(x => x.Id == gameId);

        if (game == null)
            return ServiceResult<GameDto>.ErrorResult($"Game with ID {gameId} not found");

        return ServiceResult<GameDto>.SuccessResult(game.Adapt<GameDto>());
    }

    public async Task<ServiceResult<GameDto>> CreateGameAsync(GameForCreationDto gameForCreationDto)
    {
        var game = gameForCreationDto.Adapt<Game>();

        _dbContext.Games.Add(game);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<GameDto>.ErrorResult("Game could not be created");

        return ServiceResult<GameDto>.SuccessResult(game.Adapt<GameDto>());
    }

    public async Task<ServiceResult<GameDto>> UpdateGameAsync(long gameId, GameForUpdateDto gameForUpdateDto)
    {
        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);

        if (game == null)
            return ServiceResult<GameDto>.ErrorResult($"Game with ID {gameId} not found");

        game.Name = gameForUpdateDto.Name;
        game.Picture = gameForUpdateDto.Picture;

        _dbContext.Entry(game).State = EntityState.Modified;
        var result = await _dbContext.SaveChangesAsync();

        if (result == 0)
            return ServiceResult<GameDto>.ErrorResult("Game could not be updated");

        return ServiceResult<GameDto>.SuccessResult(game.Adapt<GameDto>());
    }

    public async Task<ServiceResult<GameDto>> DeleteGameAsync(long gameId)
    {
        var game = _dbContext.Games.FirstOrDefaultAsync(x => x.Id == gameId).Result;

        if (game == null)
            return ServiceResult<GameDto>.ErrorResult($"Game with ID {gameId} not found");

        _dbContext.Remove(game);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<GameDto>.ErrorResult("Game could not be deleted");

        return ServiceResult<GameDto>.SuccessResult(game.Adapt<GameDto>());
    }

    public async Task<ServiceResult<List<GameDetailDto>>> GetAllGameDetailDtosAsync()
    {
        var games = await _dbContext.Games
            .Include(g => g.PlatformGames!)
            .ThenInclude(g => g.Platform)
            .Include(g => g.Characters)
            .Include(g => g.GameStats!)
            .ThenInclude(g => g.Stat)
            .ToListAsync();

        if (games.Count == 0)
            return ServiceResult<List<GameDetailDto>>.ErrorResult("No games found");

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

        return ServiceResult<List<GameDetailDto>>.SuccessResult(gameDetailDtos);
    }

    public async Task<ServiceResult<List<GameDetailDto>>> GetAllGameDetailDtosByNameAsync(string name)
    {
        var games = await _dbContext.Games
            .Include(g => g.PlatformGames!)
            .ThenInclude(g => g.Platform)
            .Include(g => g.Characters)
            .Include(g => g.GameStats!)
            .ThenInclude(g => g.Stat)
            .Where(g => g.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        if (games.Count == 0)
            return ServiceResult<List<GameDetailDto>>.ErrorResult("No games found");

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

        return ServiceResult<List<GameDetailDto>>.SuccessResult(gameDetailDtos);
    }

    public async Task<ServiceResult<GameDetailDto>> GetGameDetailDtoByIdAsync(long gameId)
    {
        var game = await _dbContext.Games
            .Include(g => g.PlatformGames)
            .Include(g => g.Characters)
            .Include(g => g.GameStats)
            .FirstOrDefaultAsync(g => g.Id == gameId);

        if (game == null)
            ServiceResult<GameDetailDto>.ErrorResult($"Game with ID {gameId} not found");


        var platforms = await _dbContext.Platforms
            .ToListAsync();

        var stats = await _dbContext.Stats
            .ToListAsync();

        var platformsFiltered = new List<Platform?>();
        if (game!.PlatformGames != null)
            platformsFiltered.AddRange(game.PlatformGames.Select(pg =>
                platforms.FirstOrDefault(p => p.Id == pg.PlatformId)));

        var statsFiltered = new List<Stat?>();
        if (game.GameStats != null)
            statsFiltered.AddRange(game.GameStats.Select(gs => stats.FirstOrDefault(s => s.Id == gs.StatId)));

        var gameDetailDto = GameMapper.MapToGameDetailDto(game, platformsFiltered, statsFiltered);

        return ServiceResult<GameDetailDto>.SuccessResult(gameDetailDto);
    }
}