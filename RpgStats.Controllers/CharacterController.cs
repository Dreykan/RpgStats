using Microsoft.AspNetCore.Mvc;
using RpgStats.Domain.Entities;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;
using Swashbuckle.AspNetCore.Annotations;

namespace RpgStats.Controllers;

[ApiController]
[Route("api/characters")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;

    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all Characters")]
    public async Task<IActionResult> GetAllCharacters()
    {
        var characters = await _characterService.GetAllCharactersAsync();
        return Ok(characters);
    }

    [HttpGet("byGame")]
    [SwaggerOperation(Summary = "Get all Characters by Game")]
    public async Task<IActionResult> GetAllCharactersByGame(long gameId)
    {
        var characters = await _characterService.GetAllCharactersByGameIdAsync(gameId);
        return Ok(characters);
    }

    [HttpGet("byName")]
    [SwaggerOperation(Summary = "Get all Characters by Name")]
    public async Task<IActionResult> GetAllCharactersByName(string name)
    {
        var characters = await _characterService.GetAllCharactersByNameAsync(name);
        return Ok(characters);
    }

    [HttpGet("{characterId:long}")]
    [SwaggerOperation(Summary = "Get a Character by Id")]
    public async Task<IActionResult> GetCharacterById(long characterId)
    {
        var character = await _characterService.GetCharacterByIdAsync(characterId);
        return Ok(character);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a Character")]
    public async Task<IActionResult> CreateCharacter([FromBody] CharacterForCreationDto characterForCreationDto, long gameId)
    {
        var response = await _characterService.CreateCharacterAsync(gameId, characterForCreationDto);
        if (response != null)
            return CreatedAtAction(nameof(GetCharacterById), new { characterId = response.Id, gameId = response.GameId }, response);
        return BadRequest();
    }

    [HttpPut("updateCharacter")]
    [SwaggerOperation(Summary = "Update a Character")]
    public async Task<IActionResult> UpdateCharacter([FromBody] CharacterForUpdateDto characterForUpdateDto, long characterId, long gameId)
    {
        await _characterService.UpdateCharacterAsync(characterId, gameId, characterForUpdateDto);
        return NoContent();
    }

    [HttpDelete("deleteCharacter")]
    [SwaggerOperation(Summary = "Delete a Character")]
    public async Task<IActionResult> DeleteCharacter(long characterId)
    {
        await _characterService.DeleteCharacterAsync(characterId);
        return NoContent();
    }
}