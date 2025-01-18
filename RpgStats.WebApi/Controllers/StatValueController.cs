using Microsoft.AspNetCore.Mvc;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;
using Swashbuckle.AspNetCore.Annotations;

namespace RpgStats.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class StatValueController : ControllerBase
{
    private readonly IStatValueService _statValueService;

    public StatValueController(IStatValueService statValueService)
    {
        _statValueService = statValueService;
    }

    [HttpGet("GetStatValues")]
    [SwaggerOperation(Summary = "Get all StatValues")]
    public async Task<IActionResult> GetStatValues()
    {
        var result = await _statValueService.GetAllStatValuesAsync();
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetStatValuesByCharacter/{characterId:long}")]
    [SwaggerOperation(Summary = "Get all StatValues by Character")]
    public async Task<IActionResult> GetStatValuesByCharacter(long characterId)
    {
        var result = await _statValueService.GetAllStatValuesByCharacterIdAsync(characterId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetStatValuesByStat/{statId:long}")]
    [SwaggerOperation(Summary = "Get all StatValues by Stat")]
    public async Task<IActionResult> GetStatValuesByStat(long statId)
    {
        var result = await _statValueService.GetAllStatValuesByStatIdAsync(statId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetStatValueById/{statValueId:long}")]
    [SwaggerOperation(Summary = "Get StatValues by Id")]
    public async Task<IActionResult> GetStatValueById(long statValueId)
    {
        var result = await _statValueService.GetStatValueByIdAsync(statValueId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpPost("CreateStatValue/{characterId:long}/{statId:long}")]
    [SwaggerOperation(Summary = "Create a StatValue")]
    public async Task<IActionResult> CreateStatValue([FromBody] StatValueForCreationDto statValueForCreationDto,
        long characterId, long statId)
    {
        var result = await _statValueService.CreateStatValueAsync(characterId, statId, statValueForCreationDto);
        if (result.Success)
            return CreatedAtAction(nameof(GetStatValueById), new {statValueId = result.Data?.Id}, result);
        return BadRequest(result);
    }

    [HttpPut("UpdateStatValue/{statValueId:long}/{characterId:long}/{statId:long}")]
    [SwaggerOperation(Summary = "Update a StatValue")]
    public async Task<IActionResult> UpdateStatValue([FromBody] StatValueForUpdateDto statValueForUpdateDto,
        long statValueId, long characterId, long statId)
    {
        var result = await _statValueService.UpdateStatValueAsync(statValueId, characterId, statId, statValueForUpdateDto);
        if (result.Success)
            return CreatedAtAction(nameof(GetStatValueById), new {statValueId = result.Data?.Id}, result);
        return BadRequest(result);
    }

    [HttpDelete("DeleteStatValue/{statValueId:long}")]
    [SwaggerOperation(Summary = "Delete a StatValue")]
    public async Task<IActionResult> DeleteStatValue(long statValueId)
    {
        var result = await _statValueService.DeleteStatValueAsync(statValueId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpDelete("DeleteStatValuesByCharacterAndLevel/{characterId:long}/{level:int}")]
    [SwaggerOperation(Summary = "Delete StatValues by Character and Level")]
    public async Task<IActionResult> DeleteStatValuesByCharacterAndLevel(long characterId, int level)
    {
        var result = await _statValueService.DeleteStatValuesByCharacterIdAndLevelAsync(characterId, level);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }
}