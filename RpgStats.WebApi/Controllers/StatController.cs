using Microsoft.AspNetCore.Mvc;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;
using Swashbuckle.AspNetCore.Annotations;

namespace RpgStats.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class StatController : ControllerBase
{
    private readonly IStatService _statService;

    public StatController(IStatService statService)
    {
        _statService = statService;
    }

    [HttpGet("GetAllStats")]
    [SwaggerResponse(200, "Returns a list of all Stats", typeof(ApiResponse<List<StatDto>>))]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get all Stats")]
    public async Task<IActionResult> GetAllStats()
    {
        var stats = await _statService.GetAllStatsAsync();
        if (stats.Count == 0)
            return Ok(ApiResponse<List<StatDto>>.ErrorResult("No Stats found."));

        return Ok(ApiResponse<List<StatDto>>.SuccessResult(stats));
    }

    [HttpGet("GetAllStatsByName/{name}")]
    [SwaggerResponse(200, "Returns a list of Stats by name", typeof(ApiResponse<List<StatDto>>))]
    [SwaggerResponse(400, "Invalid name")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get all Stats by Name")]
    public async Task<IActionResult> GetAllStatsByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest(ApiResponse<List<StatDto>>.ErrorResult("Invalid name."));

        var stats = await _statService.GetAllStatsByNameAsync(name);
        if (stats.Count == 0)
            return Ok(ApiResponse<List<StatDto>>.ErrorResult($"No Stats found for the specified name: {name}"));

        return Ok(ApiResponse<List<StatDto>>.SuccessResult(stats));
    }

    [HttpGet("GetAllStatsByShortName/{shortName}")]
    [SwaggerResponse(200, "Returns a list of Stats by short name", typeof(ApiResponse<List<StatDto>>))]
    [SwaggerResponse(400, "Invalid short name")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get all Stats by Short Name")]
    public async Task<IActionResult> GetAllStatsByShortName(string shortName)
    {
        if (string.IsNullOrWhiteSpace(shortName))
            return BadRequest(ApiResponse<List<StatDto>>.ErrorResult("Invalid short name."));

        var stats = await _statService.GetAllStatsByShortNameAsync(shortName);
        if (stats.Count == 0)
            return Ok(ApiResponse<List<StatDto>>.ErrorResult($"No Stats found for the specified short name: {shortName}"));

        return Ok(ApiResponse<List<StatDto>>.SuccessResult(stats));
    }

    [HttpGet("GetAllStatsByGameId/{gameId:long}")]
    [SwaggerResponse(200, "Returns a list of Stats by game ID", typeof(ApiResponse<List<StatDto>>))]
    [SwaggerResponse(400, "Invalid game ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get all Stats by Game ID")]
    public async Task<IActionResult> GetAllStatsByGameId(long gameId)
    {
        if (gameId <= 0)
            return BadRequest(ApiResponse<List<StatDto>>.ErrorResult("Invalid game ID."));

        try
        {
            var stats = await _statService.GetAllStatsByGameIdAsync(gameId);
            if (stats.Count == 0)
                return Ok(ApiResponse<List<StatDto>>.ErrorResult($"No Stats found for the specified game ID: {gameId}"));

            return Ok(ApiResponse<List<StatDto>>.SuccessResult(stats));
        }
        catch (ArgumentException e)
        {
            return NotFound(ApiResponse<List<StatDto>>.ErrorResult(e.Message));
        }
    }

    [HttpGet("GetStatById/{statId:long}")]
    [SwaggerResponse(200, "Returns a Stat by ID", typeof(ApiResponse<StatDto>))]
    [SwaggerResponse(400, "Invalid stat ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get a Stat by ID")]
    public async Task<IActionResult> GetStatById(long statId)
    {
        if (statId <= 0)
            return BadRequest(ApiResponse<StatDto>.ErrorResult("Invalid stat ID."));

        var stat = await _statService.GetStatByIdAsync(statId);
        if (stat == null)
            return Ok(ApiResponse<StatDto>.ErrorResult($"Stat with ID {statId} not found."));

        return Ok(ApiResponse<StatDto>.SuccessResult(stat));
    }

    [HttpPost("CreateStat")]
    [SwaggerResponse(201, "Stat created successfully", typeof(ApiResponse<StatDto>))]
    [SwaggerResponse(400, "Invalid Stat data")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Create a Stat")]
    public async Task<IActionResult> CreateStat([FromBody] StatForCreationDto? statForCreationDto)
    {
        try
        {
            var stat = await _statService.CreateStatAsync(statForCreationDto);
            return CreatedAtAction(nameof(GetStatById), new { statId = stat.Id },
                ApiResponse<StatDto>.SuccessResult(stat));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse<StatDto>.ErrorResult($"An error occurred while creating the Stat: {e.Message}"));
        }
    }

    [HttpPut("UpdateStat/{statId:long}")]
    [SwaggerResponse(200, "Stat updated successfully", typeof(ApiResponse<StatDto>))]
    [SwaggerResponse(400, "Invalid stat ID or data")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Update a Stat")]
    public async Task<IActionResult> UpdateStat(long statId, [FromBody] StatForUpdateDto statForUpdateDto)
    {
        if (statId <= 0)
            return BadRequest(ApiResponse<StatDto>.ErrorResult("Invalid stat ID."));

        try
        {
            var stat = await _statService.UpdateStatAsync(statId, statForUpdateDto);
            return Ok(ApiResponse<StatDto>.SuccessResult(stat));
        }
        catch (ArgumentException e)
        {
            return NotFound(ApiResponse<StatDto>.ErrorResult(e.Message));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse<StatDto>.ErrorResult($"An error occurred while updating the Stat: {e.Message}"));
        }
    }

    [HttpDelete("DeleteStat/{statId:long}")]
    [SwaggerResponse(200, "Stat deleted successfully", typeof(ApiResponse<StatDto>))]
    [SwaggerResponse(400, "Invalid stat ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Delete a Stat")]
    public async Task<IActionResult> DeleteStat(long statId)
    {
        if (statId <= 0)
            return BadRequest(ApiResponse<StatDto>.ErrorResult("Invalid stat ID."));

        try
        {
            var stat = await _statService.DeleteStatAsync(statId);
            if (stat == null)
                return NotFound(ApiResponse<StatDto>.ErrorResult($"Stat with ID {statId} not found or could not be deleted."));

            return Ok(ApiResponse<StatDto>.SuccessResult(stat));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse<StatDto>.ErrorResult($"An error occurred while deleting the Stat: {e.Message}"));
        }
    }
}