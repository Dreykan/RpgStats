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

    [HttpGet("GetCharacters")]
    [SwaggerOperation(Summary = "Get all Characters")]
    public async Task<IActionResult> GetCharacters()
    {
        var result = await _characterService.GetAllCharactersAsync();
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetCharactersByGame/{gameId:long}")]
    [SwaggerOperation(Summary = "Get all Characters by Game")]
    public async Task<IActionResult> GetCharactersByGame(long gameId)
    {
        var result = await _characterService.GetAllCharactersByGameIdAsync(gameId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetCharactersByName/{name}")]
    [SwaggerOperation(Summary = "Get all Characters by Name")]
    public async Task<IActionResult> GetCharactersByName(string name)
    {
        var result = await _characterService.GetAllCharactersByNameAsync(name);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
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
    [SwaggerOperation(Summary = "Get a Character by Id")]
    public async Task<IActionResult> GetCharacterById(long characterId)
    {
        var result = await _characterService.GetCharacterByIdAsync(characterId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
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
        // if (result != null)
        //     return CreatedAtAction(nameof(GetCharacterById),
        //         new { characterId = result.Id, gameId = result.GameId }, result);
        // return BadRequest();

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
        // if (result != null)
        //     return Ok(result);
        // return BadRequest();
        if (result.Success)
            return CreatedAtAction(nameof(GetCharacterById), new { characterId = result.Data?.Id }, result);
        return BadRequest(result);
    }

    [HttpDelete("DeleteCharacter/{characterId:long}")]
    [SwaggerOperation(Summary = "Delete a Character")]
    public async Task<IActionResult> DeleteCharacter(long characterId)
    {
        var result = await _characterService.DeleteCharacterAsync(characterId);

        // if (result.IsCompletedSuccessfully)
        //     return Ok(result);
        //
        // return BadRequest();

        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }
}