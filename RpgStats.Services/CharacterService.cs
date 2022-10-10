using Mapster;
using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Domain.Exceptions;
using RpgStats.Dto;
using RpgStats.Dto.Mapper;
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

    public async Task<List<CharacterDto>> GetAllCharactersAsync()
    {
        var characters = await _dbContext.Characters
            .Include(c => c.StatValues)
            .Include(c => c.Game)
            .ToListAsync();

        return characters.Adapt<List<CharacterDto>>();
    }

    public async Task<List<CharacterDto>> GetAllCharactersByGameIdAsync(long gameId)
    {
        var characters = await _dbContext.Characters
            .Include(c => c.StatValues)
            .Where(g => g.GameId == gameId)
            .ToListAsync();

        return characters.Adapt<List<CharacterDto>>();
    }

    public async Task<List<CharacterDto>> GetAllCharactersByNameAsync(string name)
    {
        var characters = await _dbContext.Characters
            .Include(c => c.StatValues)
            .Where(g => g.Name != null && g.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        return characters.Adapt<List<CharacterDto>>();
    }

    public async Task<CharacterDto?> GetCharacterByIdAsync(long characterId)
    {
        var character = await _dbContext.Characters
            .Include(c => c.StatValues)
            .FirstOrDefaultAsync(c => c.Id == characterId);

        if (character == null)
        {
            throw new CharacterNotFoundException(characterId);
        }

        return character.Adapt<CharacterDto>();
    }

    public async Task<CharacterDto?> CreateCharacterAsync(long gameId, CharacterForCreationDto characterForCreationDto)
    {
        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);

        if (game == null)
        {
            throw new GameNotFoundException(gameId);
        }

        var character = characterForCreationDto.Adapt<Character>();
        character.GameId = game.Id;
        character.Game = game;

        _dbContext.Characters.Add(character);
        await _dbContext.SaveChangesAsync();

        return character.Adapt<CharacterDto>();
    }

    public async Task<CharacterDto?> UpdateCharacterAsync(long characterId, long gameId, CharacterForUpdateDto characterForUpdateDto)
    {
        var character = await _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == characterId);

        if (character == null)
        {
            throw new CharacterNotFoundException(characterId);
        }

        var game = await _dbContext.Games.FirstOrDefaultAsync(g => g.Id == gameId);

        if (game == null)
        {
            throw new GameNotFoundException(gameId);
        }

        character.Name = characterForUpdateDto.Name;
        character.Picture = characterForUpdateDto.Picture;
        character.GameId = game.Id;
        character.Game = game;

        _dbContext.Entry(character).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();

        return character.Adapt<CharacterDto>();
    }

    public Task DeleteCharacterAsync(long characterId)
    {
        var character = _dbContext.Characters.FirstOrDefaultAsync(c => c.Id == characterId).Result;

        if (character == null)
        {
            return Task.CompletedTask;
        }

        _dbContext.Remove(character);

        return _dbContext.SaveChangesAsync();
    }

    public async Task<List<CharacterDetailDto>> GetAllCharacterDetailDtosAsync()
    {
        var characters = await _dbContext.Characters
            .Include(c => c.Game)
            .Include(c => c.StatValues)
            .ToListAsync();

        var statValues = await _dbContext.StatValues
            .Include(sv => sv.Stat)
            .ToListAsync();

        var characterDetailDtoList = new List<CharacterDetailDto>();
        var characterMapper = new CharacterMapper();
        foreach (var character in characters)
        {
            var svTempList = statValues
                .Where(sv => sv.CharacterId == character.Id)
                .ToList();
            characterDetailDtoList.Add(characterMapper.MapToCharacterDetailDto(character, svTempList));
        }

        return characterDetailDtoList;
    }

    public async Task<List<CharacterDetailDto>> GetAllCharacterDetailDtosByGameIdAsync(long gameId)
    {
        var characters = await _dbContext.Characters
            .Include(c => c.Game)
            .Include(c => c.StatValues)
            .Where(c => c.GameId == gameId)
            .ToListAsync();

        var statValues = await _dbContext.StatValues
            .Include(sv => sv.Stat)
            .ToListAsync();

        var characterDetailDtoList = new List<CharacterDetailDto>();
        var characterMapper = new CharacterMapper();
        foreach (var character in characters)
        {
            var svTempList = statValues
                .Where(sv => sv.CharacterId == character.Id)
                .ToList();
            characterDetailDtoList.Add(characterMapper.MapToCharacterDetailDto(character, svTempList));
        }

        return characterDetailDtoList;
    }

    public async Task<List<CharacterDetailDto>> GetAllCharacterDetailDtosByNameAsync(string name)
    {
        var characters = await _dbContext.Characters
            .Include(c => c.Game)
            .Include(c => c.StatValues)
            .Where(g => g.Name != null && g.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        var statValues = await _dbContext.StatValues
            .Include(sv => sv.Stat)
            .ToListAsync();

        var characterDetailDtoList = new List<CharacterDetailDto>();
        var characterMapper = new CharacterMapper();
        foreach (var character in characters)
        {
            var svTempList = statValues
                .Where(sv => sv.CharacterId == character.Id)
                .ToList();
            characterDetailDtoList.Add(characterMapper.MapToCharacterDetailDto(character, svTempList));
        }

        return characterDetailDtoList;
    }

    public async Task<CharacterDetailDto> GetCharacterDetailDtoByIdAsync(long characterId)
    {
        var character = await _dbContext.Characters
            .Include(c => c.Game)
            .Include(c => c.StatValues)
            .FirstOrDefaultAsync(c => c.Id == characterId);

        var characterDetailDto = new CharacterDetailDto();
        var characterMapper = new CharacterMapper();
        if (character == null) return characterDetailDto;
        var svTempList = await _dbContext.StatValues
            .Include(sv => sv.Stat)
            .Where(sv => sv.CharacterId == character.Id)
            .ToListAsync();
        characterDetailDto = characterMapper.MapToCharacterDetailDto(character, svTempList);

        return characterDetailDto;
    }
}