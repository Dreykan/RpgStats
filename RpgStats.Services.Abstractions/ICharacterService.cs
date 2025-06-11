using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface ICharacterService
{
    Task<List<CharacterDto>> GetAllCharactersAsync();
    Task<List<CharacterDto>> GetAllCharactersByGameIdAsync(long gameId);
    Task<List<CharacterDto>> GetAllCharactersByNameAsync(string name);
    Task<CharacterDto?> GetCharacterByIdAsync(long characterId);
    Task<ServiceResult<CharacterDto>> CreateCharacterAsync(long gameId, CharacterForCreationDto characterForCreationDto);

    Task<ServiceResult<CharacterDto>> UpdateCharacterAsync(long characterId, long gameId,
        CharacterForUpdateDto characterForUpdateDto);

    Task<ServiceResult<CharacterDto>> DeleteCharacterAsync(long characterId);
    Task<ServiceResult<List<CharacterDetailDto>>> GetAllCharacterDetailDtosAsync();
    Task<ServiceResult<List<CharacterDetailDto>>> GetAllCharacterDetailDtosByGameIdAsync(long gameId);
    Task<ServiceResult<List<CharacterDetailDto>>> GetAllCharacterDetailDtosByNameAsync(string name);
    Task<ServiceResult<CharacterDetailDto>> GetCharacterDetailDtoByIdAsync(long characterId);
}