using Microsoft.AspNetCore.Mvc;
using RpgStats.Domain.Entities;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;
using Swashbuckle.AspNetCore.Annotations;

namespace RpgStats.Controllers;

[ApiController]
[Route("api/statValues")]
public class StatValueController : ControllerBase
{
    private readonly IStatValueService _statValueService;

    public StatValueController(IStatValueService statValueService)
    {
        _statValueService = statValueService;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all StatValues")]
    public async Task<IActionResult> GetAllStatValues()
    {
        var statValues = await _statValueService.GetAllStatValuesAsync();
        return Ok(statValues);
    }

    [HttpGet("byCharacter")]
    [SwaggerOperation(Summary = "Get all StatValues by Character")]
    public async Task<IActionResult> GetAllStatValuesByCharacter(long characterId)
    {
        var statValues = await _statValueService.GetAllStatValuesByCharacterIdAsync(characterId);
        return Ok(statValues);
    }

    [HttpGet("byStat")]
    [SwaggerOperation(Summary = "Get all StatValues by Stat")]
    public async Task<IActionResult> GetAllStatValuesByStat(long statId)
    {
        var statValues = await _statValueService.GetAllStatValuesByStatIdAsync(statId);
        return Ok(statValues);
    }

    [HttpGet("{statValueId:long}")]
    [SwaggerOperation(Summary = "Get StatValues by Id")]
    public async Task<IActionResult> GetStatValueById(long statValueId)
    {
        var statValue = await _statValueService.GetStatValueByIdAsync(statValueId);
        return Ok(statValue);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a StatValue")]
    public async Task<IActionResult> CreateStatValue([FromBody] StatValueForCreationDto statValueForCreationDto, long characterId, long statId)
    {
        var response = await _statValueService.CreateStatValueAsync(characterId, statId, statValueForCreationDto);
        if (response != null)
            return CreatedAtAction(nameof(GetStatValueById),
                new { statValueId = response.Id, statId = response.StatId, characterId = response.CharacterId },
                response);
        return BadRequest();
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update a StatValue")]
    public async Task<IActionResult> UpdateStatValue([FromBody] StatValueForUpdateDto statValueForUpdateDto, long statValueId, long characterId, long statId)
    {
        await _statValueService.UpdateStatValueAsync(statValueId, characterId, statId, statValueForUpdateDto);
        return NoContent();
    }

    [HttpDelete]
    [SwaggerOperation(Summary = "Delete a StatValue")]
    public async Task<IActionResult> DeleteStatValue(long statValueId)
    {
        await _statValueService.DeleteStatValueAsync(statValueId);
        return NoContent();
    }
}