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

    public async Task<List<GameDto>> GetAllGamesAsync()
    {
        var games = await _dbContext.Games
            .ToListAsync();

        return games.Adapt<List<GameDto>>();
    }

    public async Task<List<GameDto>> GetAllGamesByNameAsync(string name)
    {
        var games = await _dbContext.Games
            .Where(g => g.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        return games.Adapt<List<GameDto>>();
    }

    public async Task<GameDto?> GetGameByIdAsync(long gameId)
    {
        var game = await _dbContext.Games
            .FirstOrDefaultAsync(x => x.Id == gameId);

        return game.Adapt<GameDto>();
    }

    public async Task<GameDto?> CreateGameAsync(GameForCreationDto gameForCreationDto)
    {
        var game = gameForCreationDto.Adapt<Game>();

        _dbContext.Games.Add(game);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return null;

        return game.Adapt<GameDto>();
    }

    public async Task<GameDto?> UpdateGameAsync(long gameId, GameForUpdateDto gameForUpdateDto)
    {
        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);
        if (game == null)
            return null;

        game.Name = gameForUpdateDto.Name;
        game.Picture = gameForUpdateDto.Picture;

        _dbContext.Entry(game).State = EntityState.Modified;
        var result = await _dbContext.SaveChangesAsync();

        if (result == 0)
            return null;

        return game.Adapt<GameDto>();
    }

    public async Task<GameDto?> DeleteGameAsync(long gameId)
    {
        var game = _dbContext.Games.FirstOrDefaultAsync(x => x.Id == gameId).Result;
        if (game == null)
            return null;

        _dbContext.Remove(game);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return null;

        return game.Adapt<GameDto>();
    }
}