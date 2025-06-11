using Microsoft.AspNetCore.Mvc;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;
using Swashbuckle.AspNetCore.Annotations;

namespace RpgStats.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;

    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    [HttpGet("GetAllCharacters")]
    [SwaggerResponse(200, "Returns a list of all characters", typeof(ServiceResult<List<CharacterDto>>))]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get all Characters")]
    public async Task<IActionResult> GetCharacters()
    {
        var characters = await _characterService.GetAllCharactersAsync();

        if (characters.Count == 0)
            return Ok(ApiResponse<List<CharacterDto>>.ErrorResult("No characters found."));

        return Ok(ApiResponse<List<CharacterDto>>.SuccessResult(characters));
    }

    [HttpGet("GetAllCharactersByGame/{gameId:long}")]
    [SwaggerResponse(200, "Returns a list of characters by game ID", typeof(ServiceResult<List<CharacterDto>>))]
    [SwaggerResponse(400, "Invalid game ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get all Characters by Game")]
    public async Task<IActionResult> GetCharactersByGame(long gameId)
    {
        if (gameId <= 0)
            return BadRequest(ApiResponse<List<CharacterDto>>.ErrorResult("Invalid game ID."));

        var characters = await _characterService.GetAllCharactersByGameIdAsync(gameId);

        if (characters.Count == 0)
            return Ok(ApiResponse<List<CharacterDto>>.ErrorResult($"No characters found for the specified gameId: {gameId}"));

        return Ok(ApiResponse<List<CharacterDto>>.SuccessResult(characters));
    }

    [HttpGet("GetAllCharactersByName/{name}")]
    [SwaggerResponse(200, "Returns a list of characters by name", typeof(ServiceResult<List<CharacterDto>>))]
    [SwaggerResponse(400, "Invalid name parameter")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get all Characters by Name")]
    public async Task<IActionResult> GetCharactersByName(string name)
    {
        var characters = await _characterService.GetAllCharactersByNameAsync(name);

        if (characters.Count == 0)
            return Ok(ApiResponse<List<CharacterDto>>.ErrorResult($"No characters found with the specified name: {name}"));

        return Ok(ApiResponse<List<CharacterDto>>.SuccessResult(characters));
    }

    [HttpGet("GetCharactersDetail")]
    [SwaggerOperation(Summary = "Get all Characters with Details")]
    public async Task<IActionResult> GetCharactersDetail()
    {
        var result = await _characterService.GetAllCharacterDetailDtosAsync();
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetCharactersDetailByGame/{gameId:long}")]
    [SwaggerOperation(Summary = "Get all Characters with Details by Game")]
    public async Task<IActionResult> GetCharactersDetailByGame(long gameId)
    {
        var result = await _characterService.GetAllCharacterDetailDtosByGameIdAsync(gameId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetCharactersDetailByName/{name}")]
    [SwaggerOperation(Summary = "Get all Characters with Details by Name")]
    public async Task<IActionResult> GetCharactersDetailByName(string name)
    {
        var result = await _characterService.GetAllCharacterDetailDtosByNameAsync(name);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetCharacterById/{characterId:long}")]
    [SwaggerResponse(200, "Returns a character by ID", typeof(ServiceResult<CharacterDto>))]
    [SwaggerResponse(400, "Invalid character ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get a Character by Id")]
    public async Task<IActionResult> GetCharacterById(long characterId)
    {
        if (characterId <= 0)
            return BadRequest(ApiResponse<CharacterDto>.ErrorResult("Invalid character ID."));

        var character = await _characterService.GetCharacterByIdAsync(characterId);

        if (character == null)
            return Ok(ApiResponse<CharacterDto>.ErrorResult($"Character with ID {characterId} not found."));

        return Ok(ApiResponse<CharacterDto>.SuccessResult(character));
    }

    [HttpGet("GetCharacterDetailById/{characterId:long}")]
    [SwaggerOperation(Summary = "Get a Character with Details by Id")]
    public async Task<IActionResult> GetCharacterDetailById(long characterId)
    {
        var result = await _characterService.GetCharacterDetailDtoByIdAsync(characterId);

        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpPost("CreateCharacter/{gameId:long}")]
    [SwaggerOperation(Summary = "Create a Character")]
    public async Task<IActionResult> CreateCharacter([FromBody] CharacterForCreationDto characterForCreationDto,
        long gameId)
    {
        var result = await _characterService.CreateCharacterAsync(gameId, characterForCreationDto);

        if (result.Success)
            return CreatedAtAction(nameof(GetCharacterById), new { characterId = result.Data?.Id }, result);
        return BadRequest(result);
    }

    [HttpPut("UpdateCharacter/{gameId:long}/{characterId:long}")]
    [SwaggerOperation(Summary = "Update a Character")]
    public async Task<IActionResult> UpdateCharacter([FromBody] CharacterForUpdateDto characterForUpdateDto,
        long characterId, long gameId)
    {
        var result = await _characterService.UpdateCharacterAsync(characterId, gameId, characterForUpdateDto);

        if (result.Success)
            return CreatedAtAction(nameof(GetCharacterById), new { characterId = result.Data?.Id }, result);
        return BadRequest(result);
    }

    [HttpDelete("DeleteCharacter/{characterId:long}")]
    [SwaggerOperation(Summary = "Delete a Character")]
    public async Task<IActionResult> DeleteCharacter(long characterId)
    {
        var result = await _characterService.DeleteCharacterAsync(characterId);

        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }
}