using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Repo;
using RpgStats.Services.Abstractions;

namespace RpgStats.Services;

public class CharacterService : ICharacterService
{
    private readonly RpgStatsContext _dbContext;

    public CharacterService(RpgStatsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Character>> GetAllCharactersAsync()
    {
        return await _dbContext.Characters.ToListAsync();
    }

    public async Task<List<Character>> GetAllCharactersByGameAsync(Game game)
    {
        return await _dbContext.Characters
            .Include(g => g.Game)
            .Where(g => g.Game == game)
            .ToListAsync();
    }

    public async Task<List<Character>> GetAllCharactersByNameAsync(string name)
    {
        return await _dbContext.Characters
            .Include(g => g.Game)
            .Where(g => g.Name != null && g.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();
    }

    public async Task<Character?> GetCharacterByIdAsync(long characterId)
    {
        return await _dbContext.Characters
            .Include(g => g.Game)
            .FirstOrDefaultAsync(c => c.Id == characterId);
    }

    public async Task<Character?> CreateCharacterAsync(Character character)
    {
        _dbContext.Characters.Add(character);
        await _dbContext.SaveChangesAsync();
        return await Task.FromResult(character);
    }

    public async Task<Character?> UpdateCharacterAsync(Character character)
    {
        _dbContext.Entry(character).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return await Task.FromResult(character);
    }

    public Task DeleteCharacterAsync(long characterId)
    {
        Character? character = _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == characterId).Result;

        if (character == null)
        {
            return Task.CompletedTask;
        }

        _dbContext.Remove(character);
        return _dbContext.SaveChangesAsync();
    }
}