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

    [HttpGet("GetStats")]
    [SwaggerOperation(Summary = "Get all Stats")]
    public async Task<IActionResult> GetStats()
    {
        var stats = await _statService.GetAllStatsAsync();
        return Ok(stats);
    }

    [HttpGet("GetStatsByName/{name}")]
    [SwaggerOperation(Summary = "Get all Stats by Name")]
    public async Task<IActionResult> GetStatsByName(string name)
    {
        var stats = await _statService.GetAllStatsByNameAsync(name);
        return Ok(stats);
    }

    [HttpGet("GetStatsByShortname/{shortName}")]
    [SwaggerOperation(Summary = "Get all Stats by Shortname")]
    public async Task<IActionResult> GetStatsByShortname(string shortName)
    {
        var stats = await _statService.GetAllStatsByShortNameAsync(shortName);
        return Ok(stats);
    }

    [HttpGet("GetStatsDetail")]
    [SwaggerOperation(Summary = "Get all Stats with Details")]
    public async Task<IActionResult> GetStatsDetail()
    {
        var stats = await _statService.GetAllStatDetailDtosAsync();
        return Ok(stats);
    }

    [HttpGet("GetStatsDetailByName/{name}")]
    [SwaggerOperation(Summary = "Get all Stats with Details by Name")]
    public async Task<IActionResult> GetStatsDetailByName(string name)
    {
        var stats = await _statService.GetAllStatDetailDtosByNameAsync(name);
        return Ok(stats);
    }

    [HttpGet("GetStatsDetailByShortName/{shortName}")]
    [SwaggerOperation(Summary = "Get all Stats with Details by ShortName")]
    public async Task<IActionResult> GetStatsDetailByShortName(string shortName)
    {
        var stats = await _statService.GetAllStatDetailDtosByShortNameAsync(shortName);
        return Ok(stats);
    }

    [HttpGet("GetStatById/{statId:long}")]
    [SwaggerOperation(Summary = "Get a Stat")]
    public async Task<IActionResult> GetStatById(long statId)
    {
        var stat = await _statService.GetStatByIdAsync(statId);
        return Ok(stat);
    }

    [HttpGet("GetStatDetailById/{statId:long}")]
    [SwaggerOperation(Summary = "Get a Stat with Details by Id")]
    public async Task<IActionResult> GetStatDetailById(long statId)
    {
        var stat = await _statService.GetStatDetailDtoByIdAsync(statId);
        return Ok(stat);
    }

    [HttpPost("CreateStat")]
    [SwaggerOperation(Summary = "Create a Stat")]
    public async Task<IActionResult> CreateStat([FromBody] StatForCreationDto? statForCreationDto)
    {
        var response = await _statService.CreateStatAsync(statForCreationDto);
        if (response != null) return CreatedAtAction(nameof(GetStatById), new { statId = response.Id }, response);
        return BadRequest();
    }

    [HttpPut("UpdateStat/{statId:long}")]
    [SwaggerOperation(Summary = "Update a Stat")]
    public async Task<IActionResult> UpdateStat(long statId, [FromBody] StatForUpdateDto statForUpdateDto)
    {
        var response = await _statService.UpdateStatAsync(statId, statForUpdateDto);
        if (response != null) return Ok(response);
        return BadRequest();
    }

    [HttpDelete("DeleteStat/{statId:long}")]
    [SwaggerOperation(Summary = "Delete a Stat")]
    public async Task<IActionResult> DeleteStat(long statId)
    {
        var response = await _statService.DeleteStatAsync(statId);
        if (response == Task.CompletedTask) return Ok();
        return BadRequest();
    }
}