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
    "CA1862:\"StringComparison\"-Methodenüberladungen verwenden, um Zeichenfolgenvergleiche ohne Beachtung der Groß-/Kleinschreibung durchzuführen")]
public class CharacterService : ICharacterService
{
    private readonly RpgStatsContext _dbContext;

    public CharacterService(RpgStatsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResult<List<CharacterDto>>> GetAllCharactersAsync()
    {
        var characters = await _dbContext.Characters
            .Include(c => c.StatValues)
            .ToListAsync();

        if (characters.Count == 0)
            return ServiceResult<List<CharacterDto>>.ErrorResult("No characters found");

        return ServiceResult<List<CharacterDto>>.SuccessResult(characters.Adapt<List<CharacterDto>>());
    }

    public async Task<ServiceResult<List<CharacterDto>>> GetAllCharactersByGameIdAsync(long gameId)
    {
        var characters = await _dbContext.Characters
            .Include(c => c.StatValues)
            .Where(g => g.GameId == gameId)
            .ToListAsync();

        if (characters.Count == 0)
            return ServiceResult<List<CharacterDto>>.ErrorResult("No characters found");

        return ServiceResult<List<CharacterDto>>.SuccessResult(characters.Adapt<List<CharacterDto>>());
    }

    public async Task<ServiceResult<List<CharacterDto>>> GetAllCharactersByNameAsync(string name)
    {
        var characters = await _dbContext.Characters
            .Include(c => c.StatValues)
            .Where(g => g.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        if (characters.Count == 0)
            return ServiceResult<List<CharacterDto>>.ErrorResult("No characters found");

        return ServiceResult<List<CharacterDto>>.SuccessResult(characters.Adapt<List<CharacterDto>>());
    }

    public async Task<ServiceResult<CharacterDto>> GetCharacterByIdAsync(long characterId)
    {
        var character = await _dbContext.Characters
            .Include(c => c.StatValues)
            .FirstOrDefaultAsync(c => c.Id == characterId);

        if (character == null)
            return ServiceResult<CharacterDto>.ErrorResult($"Character with ID {characterId} not found");

        return ServiceResult<CharacterDto>.SuccessResult(character.Adapt<CharacterDto>());
    }

    public async Task<ServiceResult<CharacterDto>> CreateCharacterAsync(long gameId, CharacterForCreationDto characterForCreationDto)
    {
        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);
        if (game == null)
            return ServiceResult<CharacterDto>.ErrorResult($"Game with ID {gameId} not found");

        var character = characterForCreationDto.Adapt<Character>();
        character.GameId = game.Id;
        character.Game = game;

        _dbContext.Characters.Add(character);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<CharacterDto>.ErrorResult("Character could not be created");

        return ServiceResult<CharacterDto>.SuccessResult(character.Adapt<CharacterDto>());
    }

    public async Task<ServiceResult<CharacterDto>> UpdateCharacterAsync(long characterId, long gameId,
        CharacterForUpdateDto characterForUpdateDto)
    {
        var character = await _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == characterId);
        if (character == null)
            return ServiceResult<CharacterDto>.ErrorResult($"Character with ID {characterId} not found");

        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);
        if (game == null)
            return ServiceResult<CharacterDto>.ErrorResult($"Game with ID {gameId} not found");

        character.Name = characterForUpdateDto.Name;
        character.Picture = characterForUpdateDto.Picture;
        character.GameId = game.Id;
        character.Game = game;

        _dbContext.Entry(character).State = EntityState.Modified;
        var result = await _dbContext.SaveChangesAsync();

        if (result == 0)
            return ServiceResult<CharacterDto>.ErrorResult("Character could not be updated");

        return ServiceResult<CharacterDto>.SuccessResult(character.Adapt<CharacterDto>());
    }

    public async Task<ServiceResult<CharacterDto>> DeleteCharacterAsync(long characterId)
    {
        var character = _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == characterId).Result;

        if (character == null)
            return ServiceResult<CharacterDto>.ErrorResult($"Character with ID {characterId} not found");

        _dbContext.Remove(character);
        var result = await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<CharacterDto>.ErrorResult("Character could not be deleted");

        return ServiceResult<CharacterDto>.SuccessResult(character.Adapt<CharacterDto>());
    }

    public async Task<ServiceResult<List<CharacterDetailDto>>>GetAllCharacterDetailDtosAsync()
    {
        var characters = await _dbContext.Characters
            .Include(c => c.Game)
            .Include(c => c.StatValues)
            .ToListAsync();

        if (characters.Count == 0)
            return ServiceResult<List<CharacterDetailDto>>.ErrorResult("No characters found");

        return ServiceResult<List<CharacterDetailDto>>.SuccessResult(characters.Adapt<List<CharacterDetailDto>>());

        // return (from character in characters
        //     let svTempList = character.StatValues ?? new List<StatValue>()
        //     select CharacterMapper.MapToCharacterDetailDto(character, svTempList.ToList())).ToList();
    }

    public async Task<ServiceResult<List<CharacterDetailDto>>> GetAllCharacterDetailDtosByGameIdAsync(long gameId)
    {
        var characters = await _dbContext.Characters
            .Include(c => c.Game)
            .Include(c => c.StatValues)
            .Where(c => c.GameId == gameId)
            .ToListAsync();

        if (characters.Count == 0)
            return ServiceResult<List<CharacterDetailDto>>.ErrorResult("No characters found");

        return ServiceResult<List<CharacterDetailDto>>.SuccessResult(characters.Adapt<List<CharacterDetailDto>>());

        // return (from character in characters
        //     let svTempList = character.StatValues ?? new List<StatValue>()
        //     select CharacterMapper.MapToCharacterDetailDto(character, svTempList.ToList())).ToList();
    }

    public async Task<ServiceResult<List<CharacterDetailDto>>> GetAllCharacterDetailDtosByNameAsync(string name)
    {
        var characters = await _dbContext.Characters
            .Include(c => c.Game)
            .Include(c => c.StatValues)
            .Where(g => g.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        if (characters.Count == 0)
            return ServiceResult<List<CharacterDetailDto>>.ErrorResult("No characters found");

        return ServiceResult<List<CharacterDetailDto>>.SuccessResult(characters.Adapt<List<CharacterDetailDto>>());

        // return (from character in characters
        //     let svTempList = character.StatValues ?? new List<StatValue>()
        //     select CharacterMapper.MapToCharacterDetailDto(character, svTempList.ToList())).ToList();
    }

    public async Task<ServiceResult<CharacterDetailDto>> GetCharacterDetailDtoByIdAsync(long characterId)
    {
        var character = await _dbContext.Characters
            .Include(c => c.Game)
            .Include(c => c.StatValues)
            .FirstOrDefaultAsync(c => c.Id == characterId);

        if (character == null)
            return ServiceResult<CharacterDetailDto>.ErrorResult($"Character with ID {characterId} not found");

        // return ServiceResult<CharacterDetailDto>.SuccessResult(character.Adapt<CharacterDetailDto>());
        return ServiceResult<CharacterDetailDto>.SuccessResult(CharacterMapper.MapToCharacterDetailDto(character,
                 (character.StatValues ?? new List<StatValue>()).ToList()));

        // characterDetailDto =
        //     CharacterMapper.MapToCharacterDetailDto(character,
        //         (character.StatValues ?? new List<StatValue>()).ToList());
        //
        // return characterDetailDto;
    }
}