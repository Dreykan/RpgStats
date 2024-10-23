using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface ICharacterService
{
    Task<List<CharacterDto>> GetAllCharactersAsync();
    Task<List<CharacterDto>> GetAllCharactersByGameIdAsync(long gameId);
    Task<List<CharacterDto>> GetAllCharactersByNameAsync(string name);
    Task<CharacterDto?> GetCharacterByIdAsync(long characterId);
    Task<CharacterDto?> CreateCharacterAsync(long gameId, CharacterForCreationDto characterForCreationDto);
    Task<CharacterDto?> UpdateCharacterAsync(long characterId, long gameId , CharacterForUpdateDto characterForUpdateDto);
    Task<Task> DeleteCharacterAsync(long characterId);
    Task<List<CharacterDetailDto>> GetAllCharacterDetailDtosAsync();
    Task<List<CharacterDetailDto>> GetAllCharacterDetailDtosByGameIdAsync(long gameId);
    Task<List<CharacterDetailDto>> GetAllCharacterDetailDtosByNameAsync(string name);
    Task<CharacterDetailDto> GetCharacterDetailDtoByIdAsync(long characterId);
}