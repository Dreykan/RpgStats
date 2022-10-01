using RpgStats.Domain.Entities;

namespace RpgStats.Services.Abstractions;

public interface ICharacterService
{
    Task<List<Character>> GetAllCharactersAsync();
    Task<List<Character>> GetAllCharactersByGameAsync(Game game);
    Task<List<Character>> GetAllCharactersByNameAsync(string name);
    Task<Character?> GetCharacterByIdAsync(long characterId);
    Task<Character?> CreateCharacterAsync(Character character);
    Task<Character?> UpdateCharacterAsync(Character character);
    Task DeleteCharacterAsync(long characterId);

}