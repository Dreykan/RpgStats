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
        var statValues = await _statValueService.GetAllStatValuesAsync();
        return Ok(statValues);
    }

    [HttpGet("GetStatValuesByCharacter/{characterId:long}")]
    [SwaggerOperation(Summary = "Get all StatValues by Character")]
    public async Task<IActionResult> GetStatValuesByCharacter(long characterId)
    {
        var statValues = await _statValueService.GetAllStatValuesByCharacterIdAsync(characterId);
        return Ok(statValues);
    }

    [HttpGet("GetStatValuesByStat/{statId:long}")]
    [SwaggerOperation(Summary = "Get all StatValues by Stat")]
    public async Task<IActionResult> GetStatValuesByStat(long statId)
    {
        var statValues = await _statValueService.GetAllStatValuesByStatIdAsync(statId);
        return Ok(statValues);
    }

    [HttpGet("GetStatValueById/{statValueId:long}")]
    [SwaggerOperation(Summary = "Get StatValues by Id")]
    public async Task<IActionResult> GetStatValueById(long statValueId)
    {
        var statValue = await _statValueService.GetStatValueByIdAsync(statValueId);
        return Ok(statValue);
    }

    [HttpPost("CreateStatValue/{characterId:long}/{statId:long}")]
    [SwaggerOperation(Summary = "Create a StatValue")]
    public async Task<IActionResult> CreateStatValue([FromBody] StatValueForCreationDto statValueForCreationDto,
        long characterId, long statId)
    {
        var response = await _statValueService.CreateStatValueAsync(characterId, statId, statValueForCreationDto);
        if (response != null)
            return CreatedAtAction(nameof(GetStatValueById),
                new { statValueId = response.Id, statId = response.StatId, characterId = response.CharacterId },
                response);
        return BadRequest();
    }

    [HttpPut("UpdateStatValue/{statValueId:long}/{characterId:long}/{statId:long}")]
    [SwaggerOperation(Summary = "Update a StatValue")]
    public async Task<IActionResult> UpdateStatValue([FromBody] StatValueForUpdateDto statValueForUpdateDto,
        long statValueId, long characterId, long statId)
    {
        var response =
            await _statValueService.UpdateStatValueAsync(statValueId, characterId, statId, statValueForUpdateDto);
        if (response != null)
            return Ok(response);
        return BadRequest();
    }

    [HttpDelete("DeleteStatValue/{statValueId:long}")]
    [SwaggerOperation(Summary = "Delete a StatValue")]
    public async Task<IActionResult> DeleteStatValue(long statValueId)
    {
        var response = await _statValueService.DeleteStatValueAsync(statValueId);
        if (response == Task.CompletedTask) return Ok();
        return BadRequest();
    }
}