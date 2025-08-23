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
    [SwaggerResponse(200, "Returns a list of all characters", typeof(ApiResponse<List<CharacterDto>>))]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get all Characters")]
    public async Task<IActionResult> GetAllCharacters()
    {
        var characters = await _characterService.GetAllCharactersAsync();
        if (characters.Count == 0)
            return Ok(ApiResponse<List<CharacterDto>>.ErrorResult("No characters found."));

        return Ok(ApiResponse<List<CharacterDto>>.SuccessResult(characters));
    }

    [HttpGet("GetAllCharactersByGame/{gameId:long}")]
    [SwaggerResponse(200, "Returns a list of characters by game ID", typeof(ApiResponse<List<CharacterDto>>))]
    [SwaggerResponse(400, "Invalid game ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get all Characters by Game")]
    public async Task<IActionResult> GetAllCharactersByGame(long gameId)
    {
        if (gameId <= 0)
            return BadRequest(ApiResponse<List<CharacterDto>>.ErrorResult("Invalid game ID."));

        var characters = await _characterService.GetAllCharactersByGameIdAsync(gameId);
        if (characters.Count == 0)
            return Ok(ApiResponse<List<CharacterDto>>.ErrorResult(
                $"No characters found for the specified gameId: {gameId}"));

        return Ok(ApiResponse<List<CharacterDto>>.SuccessResult(characters));
    }

    [HttpGet("GetAllCharactersByName/{name}")]
    [SwaggerResponse(200, "Returns a list of characters by name", typeof(ApiResponse<List<CharacterDto>>))]
    [SwaggerResponse(400, "Invalid name parameter")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get all Characters by Name")]
    public async Task<IActionResult> GetAllCharactersByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest(
                ApiResponse<List<CharacterDto>>.ErrorResult("Invalid name parameter. Name cannot be null or empty."));

        var characters = await _characterService.GetAllCharactersByNameAsync(name);
        if (characters.Count == 0)
            return Ok(ApiResponse<List<CharacterDto>>.ErrorResult(
                $"No characters found with the specified name: {name}"));

        return Ok(ApiResponse<List<CharacterDto>>.SuccessResult(characters));
    }

    [HttpGet("GetCharacterById/{characterId:long}")]
    [SwaggerResponse(200, "Returns a character by ID", typeof(ApiResponse<CharacterDto>))]
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
    [SwaggerResponse(200, "Returns character details by ID", typeof(ApiResponse<CharacterDetailDto>))]
    [SwaggerResponse(400, "Invalid character ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get Character Details by Id")]
    public async Task<IActionResult> GetCharacterDetailById(long characterId)
    {
        if (characterId <= 0)
            return BadRequest(ApiResponse<CharacterDetailDto>.ErrorResult("Invalid character ID."));

        try
        {
            var characterDetail = await _characterService.GetCharacterDetailByIdAsync(characterId);
            return Ok(ApiResponse<CharacterDetailDto>.SuccessResult(characterDetail));
        }
        catch (ArgumentException e)
        {
            return NotFound(ApiResponse<CharacterDetailDto>.ErrorResult(e.Message));
        }
        catch (Exception e)
        {
            return BadRequest(
                ApiResponse<CharacterDetailDto>.ErrorResult(
                    $"An error occurred while retrieving the character details: {e.Message}"));
        }
    }

    [HttpPost("CreateCharacter/{gameId:long}")]
    [SwaggerResponse(201, "Character created successfully", typeof(ApiResponse<CharacterDto>))]
    [SwaggerResponse(400, "Invalid game ID or character data")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Create a Character")]
    public async Task<IActionResult> CreateCharacter([FromBody] CharacterForCreationDto characterForCreationDto,
        long gameId)
    {
        if (gameId <= 0)
            return BadRequest(ApiResponse<CharacterDto>.ErrorResult($"Invalid game ID: {gameId}."));
        try
        {
            var character = await _characterService.CreateCharacterAsync(gameId, characterForCreationDto);
            return CreatedAtAction(nameof(GetCharacterById), new { characterId = character.Id },
                ApiResponse<CharacterDto>.SuccessResult(character));
        }
        catch (Exception e)
        {
            return BadRequest(
                ApiResponse<CharacterDto>.ErrorResult($"An error occurred while creating the character: {e.Message}"));
        }
    }

    [HttpPut("UpdateCharacter/{gameId:long}/{characterId:long}")]
    [SwaggerResponse(201, "Character updated successfully", typeof(ApiResponse<CharacterDto>))]
    [SwaggerResponse(400, "Invalid game ID or character ID")]
    [SwaggerResponse(404, "Character not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Update a Character")]
    public async Task<IActionResult> UpdateCharacter([FromBody] CharacterForUpdateDto characterForUpdateDto,
        long characterId, long gameId)
    {
        if (characterId <= 0)
            return BadRequest(ApiResponse<CharacterDto>.ErrorResult($"Invalid character ID: {characterId}."));

        if (gameId <= 0)
            return BadRequest(ApiResponse<CharacterDto>.ErrorResult($"Invalid game ID: {gameId}."));

        try
        {
            var character = await _characterService.UpdateCharacterAsync(characterId, gameId, characterForUpdateDto);
            return CreatedAtAction(nameof(GetCharacterById), new { characterId = character.Id },
                ApiResponse<CharacterDto>.SuccessResult(character));
        }
        catch (Exception e)
        {
            return BadRequest(
                ApiResponse<CharacterDto>.ErrorResult($"An error occurred while updating the character: {e.Message}"));
        }
    }

    [HttpDelete("DeleteCharacter/{characterId:long}")]
    [SwaggerResponse(200, "Character deleted successfully", typeof(ApiResponse<CharacterDto>))]
    [SwaggerResponse(400, "Invalid character ID")]
    [SwaggerResponse(404, "Character not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Delete a Character")]
    public async Task<IActionResult> DeleteCharacter(long characterId)
    {
        if (characterId <= 0)
            return BadRequest(ApiResponse<CharacterDto>.ErrorResult($"Invalid character ID: {characterId}."));

        try
        {
            var character = await _characterService.DeleteCharacterAsync(characterId);
            return Ok(ApiResponse<CharacterDto>.SuccessResult(character));
        }
        catch (Exception e)
        {
            return BadRequest(
                ApiResponse<CharacterDto>.ErrorResult($"An error occurred while deleting the character: {e.Message}"));
        }
    }
}