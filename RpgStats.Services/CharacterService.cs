using System.Diagnostics.CodeAnalysis;
using Mapster;
using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Dto;
using RpgStats.Repo;
using RpgStats.Services.Abstractions;

namespace RpgStats.Services;

[SuppressMessage("Performance",
    "CA1862:\"StringComparison\"-Methodenüberladungen verwenden, um Zeichenfolgenvergleiche ohne Beachtung der Groß-/Kleinschreibung durchzuführen")]
public class CharacterService : ICharacterService
{
    private readonly RpgStatsContext _dbContext;

    public CharacterService(RpgStatsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CharacterDto>> GetAllCharactersAsync()
    {
        var characters = await _dbContext.Characters
            .ToListAsync();

        return characters.Adapt<List<CharacterDto>>();
    }

    public async Task<List<CharacterDto>> GetAllCharactersByGameIdAsync(long gameId)
    {
        var characters = await _dbContext.Characters
            .Where(g => g.GameId == gameId)
            .ToListAsync();

        return characters.Adapt<List<CharacterDto>>();
    }

    public async Task<List<CharacterDto>> GetAllCharactersByNameAsync(string name)
    {
        var characters = await _dbContext.Characters
            .Where(g => g.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        return characters.Adapt<List<CharacterDto>>();
    }

    public async Task<CharacterDto?> GetCharacterByIdAsync(long characterId)
    {
        var character = await _dbContext.Characters
            .FirstOrDefaultAsync(c => c.Id == characterId);

        return character.Adapt<CharacterDto>();
    }

    public async Task<CharacterDetailDto> GetCharacterDetailByIdAsync(long characterId)
    {
        var character = await _dbContext.Characters
            .Include(c => c.Game)
            .ThenInclude(g => g.GameStats)
            .Include(c => c.StatValues)
            .FirstOrDefaultAsync(x => x.Id == characterId);

        if (character == null)
            throw new ArgumentException($"Character with ID {characterId} not found");

        // TODO: Rework with Mapster and custom mapping configuration, I don't know how to do it yet.
        var characterDetail = character.Adapt<CharacterDetailDto>();
        characterDetail.GameStats = character.Game?.GameStats?.Adapt<List<GameStatDto>>() ?? new List<GameStatDto>();

        return characterDetail;
    }

    public async Task<CharacterDto> CreateCharacterAsync(long gameId, CharacterForCreationDto characterForCreationDto)
    {
        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);
        if (game == null)
            throw new ArgumentException($"Game with ID {gameId} not found");

        var character = characterForCreationDto.Adapt<Character>();
        character.GameId = game.Id;
        character.Game = game;

        _dbContext.Characters.Add(character);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            throw new InvalidOperationException("Character could not be created");

        return character.Adapt<CharacterDto>();
    }

    public async Task<CharacterDto> UpdateCharacterAsync(long characterId, long gameId,
        CharacterForUpdateDto characterForUpdateDto)
    {
        var character = await _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == characterId);
        if (character == null)
            throw new ArgumentException($"Character with ID {characterId} not found");

        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);
        if (game == null)
            throw new ArgumentException($"Game with ID {gameId} not found");

        character.Name = characterForUpdateDto.Name;
        character.Picture = characterForUpdateDto.Picture;
        character.GameId = game.Id;
        character.Game = game;

        _dbContext.Entry(character).State = EntityState.Modified;
        var result = await _dbContext.SaveChangesAsync();

        if (result == 0)
            throw new InvalidOperationException("Character could not be updated");

        return character.Adapt<CharacterDto>();
    }

    public async Task<CharacterDto> DeleteCharacterAsync(long characterId)
    {
        var character = _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == characterId).Result;
        if (character == null)
            throw new ArgumentException($"Character with ID {characterId} not found");

        _dbContext.Remove(character);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            throw new InvalidOperationException("Character could not be deleted");

        return character.Adapt<CharacterDto>();
    }
}