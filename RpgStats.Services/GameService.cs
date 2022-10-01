using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
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

    public async Task<List<Game>> GetAllGamesAsync()
    {
        return await _dbContext.Games.ToListAsync();
    }

    public async Task<Game?> GetGameByIdAsync(long gameId)
    {
        return await _dbContext.Games.FirstOrDefaultAsync(x => x.Id == gameId);
    }

    public async Task<Game?> CreateGameAsync(Game game)
    {
        _dbContext.Games.Add(game);
        await _dbContext.SaveChangesAsync();
        return await Task.FromResult(game);
    }

    public async Task<Game?> UpdateGameAsync(Game game)
    {
        _dbContext.Entry(game).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return await Task.FromResult(game);
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