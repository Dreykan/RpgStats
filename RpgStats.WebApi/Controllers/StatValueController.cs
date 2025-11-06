using Microsoft.AspNetCore.Mvc;
using RpgStats.Domain.Exceptions;
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

    [HttpGet("GetAllStatValues")]
    [SwaggerResponse(200, "Returns a list of all StatValues", typeof(ApiResponse<List<StatValueDto>>))]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get all StatValues")]
    public async Task<IActionResult> GetAllStatValues()
    {
        var statValues = await _statValueService.GetAllStatValuesAsync();
        if (statValues.Count == 0)
            return Ok(ApiResponse<List<StatValueDto>>.ErrorResult("No StatValues found."));

        return Ok(ApiResponse<List<StatValueDto>>.SuccessResult(statValues));
    }

    [HttpGet("GetAllStatValuesByCharacter/{characterId:long}")]
    [SwaggerResponse(200, "Returns a list of StatValues by character ID", typeof(ApiResponse<List<StatValueDto>>))]
    [SwaggerResponse(400, "Invalid character ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get all StatValues by Character")]
    public async Task<IActionResult> GetAllStatValuesByCharacter(long characterId)
    {
        if (characterId <= 0)
            return BadRequest(ApiResponse<List<StatValueDto>>.ErrorResult("Invalid character ID."));

        try
        {
            var statValues = await _statValueService.GetAllStatValuesByCharacterIdAsync(characterId);
            if (statValues.Count == 0)
                return Ok(ApiResponse<List<StatValueDto>>.ErrorResult(
                    $"No StatValues found for the specified characterId: {characterId}"));

            return Ok(ApiResponse<List<StatValueDto>>.SuccessResult(statValues));
        }
        catch (CharacterNotFoundException e)
        {
            return NotFound(ApiResponse<List<StatValueDto>>.ErrorResult(e.Message));
        }
    }

    [HttpGet("GetAllStatValuesByStat/{statId:long}")]
    [SwaggerResponse(200, "Returns a list of StatValues by stat ID", typeof(ApiResponse<List<StatValueDto>>))]
    [SwaggerResponse(400, "Invalid stat ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get all StatValues by Stat")]
    public async Task<IActionResult> GetAllStatValuesByStat(long statId)
    {
        if (statId <= 0)
            return BadRequest(ApiResponse<List<StatValueDto>>.ErrorResult("Invalid stat ID."));

        try
        {
            var statValues = await _statValueService.GetAllStatValuesByStatIdAsync(statId);
            if (statValues.Count == 0)
                return Ok(ApiResponse<List<StatValueDto>>.ErrorResult(
                    $"No StatValues found for the specified statId: {statId}"));

            return Ok(ApiResponse<List<StatValueDto>>.SuccessResult(statValues));
        }
        catch (StatNotFoundException e)
        {
            return NotFound(ApiResponse<List<StatValueDto>>.ErrorResult(e.Message));
        }
    }

    [HttpGet("GetHighestLevelByCharacter/{characterId:long}")]
    [SwaggerResponse(200, "Returns the highest level for a character", typeof(ApiResponse<int>))]
    [SwaggerResponse(400, "Invalid character ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get highest level by Character")]
    public async Task<IActionResult> GetHighestLevelByCharacter(long characterId)
    {
        if (characterId <= 0)
            return BadRequest(ApiResponse<int>.ErrorResult("Invalid character ID."));

        try
        {
            var level = await _statValueService.GetHighestLevelByCharacterIdAsync(characterId);
            return Ok(ApiResponse<int>.SuccessResult(level));
        }
        catch (CharacterNotFoundException e)
        {
            return NotFound(ApiResponse<int>.ErrorResult(e.Message));
        }
    }

    [HttpGet("GetStatValueById/{statValueId:long}")]
    [SwaggerResponse(200, "Returns a StatValue by ID", typeof(ApiResponse<StatValueDto>))]
    [SwaggerResponse(400, "Invalid stat value ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get StatValue by Id")]
    public async Task<IActionResult> GetStatValueById(long statValueId)
    {
        if (statValueId <= 0)
            return BadRequest(ApiResponse<StatValueDto>.ErrorResult("Invalid stat value ID."));

        var statValue = await _statValueService.GetStatValueByIdAsync(statValueId);
        if (statValue == null)
            return NotFound(ApiResponse<StatValueDto>.ErrorResult($"StatValue with ID {statValueId} not found."));

        return Ok(ApiResponse<StatValueDto>.SuccessResult(statValue));
    }

    [HttpPost("CreateStatValue")]
    [SwaggerResponse(201, "StatValue created successfully", typeof(ApiResponse<StatValueDto>))]
    [SwaggerResponse(400, "Invalid StatValue data")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Create a StatValue")]
    public async Task<IActionResult> CreateStatValue([FromBody] StatValueForCreationDto statValueForCreationDto)
    {
        try
        {
            var statValue = await _statValueService.CreateStatValueAsync(statValueForCreationDto);
            return CreatedAtAction(nameof(GetStatValueById), new { statValueId = statValue.Id },
                ApiResponse<StatValueDto>.SuccessResult(statValue));
        }
        catch (CharacterNotFoundException e)
        {
            return NotFound(ApiResponse<StatValueDto>.ErrorResult(e.Message));
        }
        catch (StatNotFoundException e)
        {
            return NotFound(ApiResponse<StatValueDto>.ErrorResult(e.Message));
        }
        catch (Exception e)
        {
            return BadRequest(
                ApiResponse<StatValueDto>.ErrorResult($"An error occurred while creating the StatValue: {e.Message}"));
        }
    }

    [HttpPost("CreateMultipleStatValues")]
    [SwaggerResponse(201, "StatValues created successfully", typeof(ApiResponse<List<StatValueDto>>))]
    [SwaggerResponse(400, "Invalid StatValues data")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Create multiple StatValues")]
    public async Task<IActionResult> CreateMultipleStatValues([FromBody] List<StatValueForCreationDto> statValuesForCreationDto)
    {
        try
        {
            var statValues = await _statValueService.CreateMultipleStatValuesAsync(statValuesForCreationDto);
            return CreatedAtAction(nameof(GetAllStatValues), null,
                ApiResponse<List<StatValueDto>>.SuccessResult(statValues));
        }
        catch (Exception e)
        {
            return BadRequest(
                ApiResponse<List<StatValueDto>>.ErrorResult($"An error occurred while creating the StatValues: {e.Message}"));
        }
    }

    [HttpPut("UpdateStatValue/{statValueId:long}")]
    [SwaggerResponse(200, "StatValue updated successfully", typeof(ApiResponse<StatValueDto>))]
    [SwaggerResponse(400, "Invalid stat value ID or data")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Update a StatValue")]
    public async Task<IActionResult> UpdateStatValue(long statValueId, [FromBody] StatValueForUpdateDto statValueForUpdateDto)
    {
        if (statValueId <= 0)
            return BadRequest(ApiResponse<StatValueDto>.ErrorResult("Invalid stat value ID."));

        if (statValueForUpdateDto.CharacterId <= 0)
            return BadRequest(ApiResponse<StatValueDto>.ErrorResult("Invalid character ID."));

        if (statValueForUpdateDto.StatId <= 0)
            return BadRequest(ApiResponse<StatValueDto>.ErrorResult("Invalid stat ID."));

        try
        {
            var statValue = await _statValueService.UpdateStatValueAsync(statValueId, statValueForUpdateDto.CharacterId,
                statValueForUpdateDto.StatId, statValueForUpdateDto);
            return Ok(ApiResponse<StatValueDto>.SuccessResult(statValue));
        }
        catch (StatValueNotFoundException e)
        {
            return NotFound(ApiResponse<StatValueDto>.ErrorResult(e.Message));
        }
        catch (CharacterNotFoundException e)
        {
            return NotFound(ApiResponse<StatValueDto>.ErrorResult(e.Message));
        }
        catch (StatNotFoundException e)
        {
            return NotFound(ApiResponse<StatValueDto>.ErrorResult(e.Message));
        }
        catch (Exception e)
        {
            return BadRequest(
                ApiResponse<StatValueDto>.ErrorResult($"An error occurred while updating the StatValue: {e.Message}"));
        }
    }

    [HttpDelete("DeleteStatValue/{statValueId:long}")]
    [SwaggerResponse(200, "StatValue deleted successfully", typeof(ApiResponse<StatValueDto>))]
    [SwaggerResponse(400, "Invalid stat value ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Delete a StatValue")]
    public async Task<IActionResult> DeleteStatValue(long statValueId)
    {
        if (statValueId <= 0)
            return BadRequest(ApiResponse<StatValueDto>.ErrorResult("Invalid stat value ID."));

        try
        {
            var statValue = await _statValueService.DeleteStatValueAsync(statValueId);
            if (statValue == null)
                return NotFound(
                    ApiResponse<StatValueDto>.ErrorResult(
                        $"StatValue with ID {statValueId} not found or could not be deleted."));
            return Ok(ApiResponse<StatValueDto>.SuccessResult(statValue));
        }
        catch (StatValueNotFoundException e)
        {
            return NotFound(ApiResponse<StatValueDto>.ErrorResult(e.Message));
        }
        catch (Exception e)
        {
            return BadRequest(
                ApiResponse<StatValueDto>.ErrorResult($"An error occurred while deleting the StatValue: {e.Message}"));
        }
    }

    [HttpDelete("DeleteAllStatValuesByCharacterAndLevel/{characterId:long}/{level:int}")]
    [SwaggerResponse(200, "StatValues deleted successfully", typeof(ApiResponse<List<StatValueDto>>))]
    [SwaggerResponse(400, "Invalid character ID or level")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Delete StatValues by Character and Level")]
    public async Task<IActionResult> DeleteAllStatValuesByCharacterAndLevel(long characterId, int level)
    {
        if (characterId <= 0)
            return BadRequest(ApiResponse<List<StatValueDto>>.ErrorResult("Invalid character ID."));

        try
        {
            var statValues = await _statValueService.DeleteStatValuesByCharacterIdAndLevelAsync(characterId, level);
            if (statValues.Count == 0)
                return NotFound(
                    ApiResponse<List<StatValueDto>>.ErrorResult(
                        $"No StatValues found for characterId {characterId} and level {level}"));
            return Ok(ApiResponse<List<StatValueDto>>.SuccessResult(statValues));
        }
        catch (CharacterNotFoundException e)
        {
            return NotFound(ApiResponse<List<StatValueDto>>.ErrorResult(e.Message));
        }
        catch (Exception e)
        {
            return BadRequest(
                ApiResponse<List<StatValueDto>>.ErrorResult(
                    $"An error occurred while deleting StatValues for characterId {characterId} and level {level}: {e.Message}"));
        }
    }
}