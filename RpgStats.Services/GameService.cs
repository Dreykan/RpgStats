using Mapster;
using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Domain.Exceptions;
using RpgStats.Dto;
using RpgStats.Repo;
using RpgStats.Services.Abstractions;

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
}