using Mapster;
using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Domain.Exceptions;
using RpgStats.Dto;
using RpgStats.Dto.Mapper;
using RpgStats.Repo;
using RpgStats.Services.Abstractions;
using System.Xml.Linq;

namespace RpgStats.Services;

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
            .Where(g => g.Name != null && g.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        return games.Adapt<List<GameDto>>();
    }

    public async Task<GameDto?> GetGameByIdAsync(long gameId)
    {
        var game = await _dbContext.Games
            .Include(g => g.PlatformGames)
            .Include(g => g.Characters)
            .FirstOrDefaultAsync(x => x.Id == gameId);

        if (game == null)
        {
            throw new GameNotFoundException(gameId);
        }

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

        if (game == null)
        {
            throw new GameNotFoundException(gameId);
        }

        game.Name = gameForUpdateDto.Name;
        game.Picture = gameForUpdateDto.Picture;

        _dbContext.Entry(game).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();

        return game.Adapt<GameDto>();
    }

    public Task DeleteGameAsync(long gameId)
    {
        Game? game = _dbContext.Games.FirstOrDefaultAsync(x => x.Id == gameId).Result;

        if (game == null)
        {
            return Task.CompletedTask;
        }

        _dbContext.Remove(game);

        return _dbContext.SaveChangesAsync();
    }

    public async Task<List<GameDetailDto>> GetAllGameDetailDtosAsync()
    {
        var games = await _dbContext.Games
            .Include(g => g.PlatformGames)
            .Include(g => g.Characters)
            .ToListAsync();

        var platforms = await _dbContext.Platforms
            .ToListAsync();

        var gameDetailDtos = new List<GameDetailDto>();
        var gameMapper = new GameMapper();

        foreach (var game in games)
        {
            var platformsFiltered = new List<Platform?>();
            if (game.PlatformGames != null)
            {
                foreach (var pg in game.PlatformGames)
                {
                    platformsFiltered.Add(platforms
                        .FirstOrDefault(p => p.Id == pg.PlatformId));
                }
            }

            gameDetailDtos.Add(gameMapper.MapToGameDetailDto(game, platformsFiltered));
        }

        return gameDetailDtos;
    }

    public async Task<List<GameDetailDto>> GetAllGameDetailDtosByNameAsync(string name)
    {
        var games = await _dbContext.Games
            .Include(g => g.PlatformGames)
            .Include(g => g.Characters)
            .Where(g => g.Name != null && g.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        var platforms = await _dbContext.Platforms
            .ToListAsync();

        var gameDetailDtos = new List<GameDetailDto>();
        var gameMapper = new GameMapper();

        foreach (var game in games)
        {
            var platformsFiltered = new List<Platform?>();
            if (game.PlatformGames != null)
            {
                foreach (var pg in game.PlatformGames)
                {
                    platformsFiltered.Add(platforms
                        .FirstOrDefault(p => p.Id == pg.PlatformId));
                }
            }

            gameDetailDtos.Add(gameMapper.MapToGameDetailDto(game, platformsFiltered));
        }

        return gameDetailDtos;
    }

    public async Task<GameDetailDto?> GetGameDetailDtoByIdAsync(long gameId)
    {
        var game = await _dbContext.Games
            .Include(g => g.PlatformGames)
            .Include(g => g.Characters)
            .FirstOrDefaultAsync(g => g.Id == gameId);

        var platforms = await _dbContext.Platforms
            .ToListAsync();

        var gameDetailDto = new GameDetailDto();
        var gameMapper = new GameMapper();

        var platformsFiltered = new List<Platform?>();
        if (game?.PlatformGames != null)
        {
            foreach (var pg in game.PlatformGames)
            {
                platformsFiltered.Add(platforms
                    .FirstOrDefault(p => p.Id == pg.PlatformId));
            }
        }

        gameDetailDto = gameMapper.MapToGameDetailDto(game, platformsFiltered);

        return gameDetailDto;
    }
}