using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface ICharacterService
{
    Task<ServiceResult<List<CharacterDto>>> GetAllCharactersAsync();
    Task<ServiceResult<List<CharacterDto>>> GetAllCharactersByGameIdAsync(long gameId);
    Task<ServiceResult<List<CharacterDto>>> GetAllCharactersByNameAsync(string name);
    Task<ServiceResult<CharacterDto>> GetCharacterByIdAsync(long characterId);
    Task<ServiceResult<CharacterDto>> CreateCharacterAsync(long gameId, CharacterForCreationDto characterForCreationDto);

    Task<ServiceResult<CharacterDto>> UpdateCharacterAsync(long characterId, long gameId,
        CharacterForUpdateDto characterForUpdateDto);

    Task<ServiceResult<CharacterDto>> DeleteCharacterAsync(long characterId);
    Task<ServiceResult<List<CharacterDetailDto>>> GetAllCharacterDetailDtosAsync();
    Task<ServiceResult<List<CharacterDetailDto>>> GetAllCharacterDetailDtosByGameIdAsync(long gameId);
    Task<ServiceResult<List<CharacterDetailDto>>> GetAllCharacterDetailDtosByNameAsync(string name);
    Task<ServiceResult<CharacterDetailDto>> GetCharacterDetailDtoByIdAsync(long characterId);
}