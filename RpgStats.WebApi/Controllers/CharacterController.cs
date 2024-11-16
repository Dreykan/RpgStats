using Microsoft.AspNetCore.Mvc;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;
using Swashbuckle.AspNetCore.Annotations;

namespace RpgStats.WebApi.Controllers;

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

    [HttpGet("details")]
    [SwaggerOperation(Summary = "Get all Characters with Details")]
    public async Task<IActionResult> GetAllCharacterDetailDtos()
    {
        var characters = await _characterService.GetAllCharacterDetailDtosAsync();
        return Ok(characters);
    }

    [HttpGet("detailsByGame")]
    [SwaggerOperation(Summary = "Get all Characters with Details by Game")]
    public async Task<IActionResult> GetAllCharacterDetailDtosByGame(long gameId)
    {
        var characters = await _characterService.GetAllCharacterDetailDtosByGameIdAsync(gameId);
        return Ok(characters);
    }

    [HttpGet("detailsByName")]
    [SwaggerOperation(Summary = "Get all Characters with Details by Name")]
    public async Task<IActionResult> GetAllCharacterDetailDtosByName(string name)
    {
        var characters = await _characterService.GetAllCharacterDetailDtosByNameAsync(name);
        return Ok(characters);
    }

    [HttpGet("{characterId:long}")]
    [SwaggerOperation(Summary = "Get a Character by Id")]
    public async Task<IActionResult> GetCharacterById(long characterId)
    {
        var character = await _characterService.GetCharacterByIdAsync(characterId);
        return Ok(character);
    }

    [HttpGet("details/{characterId:long}")]
    [SwaggerOperation(Summary = "Get a Character with Details by Id")]
    public async Task<IActionResult> GetCharacterDetailDtoById(long characterId)
    {
        var character = await _characterService.GetCharacterDetailDtoByIdAsync(characterId);
        return Ok(character);
    }

    [HttpPost("{gameId:long}")]
    [SwaggerOperation(Summary = "Create a Character")]
    public async Task<IActionResult> CreateCharacter([FromBody] CharacterForCreationDto characterForCreationDto,
        long gameId)
    {
        var response = await _characterService.CreateCharacterAsync(gameId, characterForCreationDto);
        if (response != null)
            return CreatedAtAction(nameof(GetCharacterById),
                new { characterId = response.Id, gameId = response.GameId }, response);
        return BadRequest();
    }

    [HttpPut("{gameId:long}/{characterId:long}")]
    [SwaggerOperation(Summary = "Update a Character")]
    public async Task<IActionResult> UpdateCharacter([FromBody] CharacterForUpdateDto characterForUpdateDto,
        long characterId, long gameId)
    {
        var response = await _characterService.UpdateCharacterAsync(characterId, gameId, characterForUpdateDto);
        if (response != null)
            return Ok(response);
        return BadRequest();
    }

    [HttpDelete("{characterId:long}")]
    [SwaggerOperation(Summary = "Delete a Character")]
    public async Task<IActionResult> DeleteCharacter(long characterId)
    {
        var response = await _characterService.DeleteCharacterAsync(characterId);
        if (response == Task.CompletedTask)
            return Ok();
        return BadRequest();
    }
}