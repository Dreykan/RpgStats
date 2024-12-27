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
        var result = await _statService.GetAllStatsAsync();
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("GetStatsByName/{name}")]
    [SwaggerOperation(Summary = "Get all Stats by Name")]
    public async Task<IActionResult> GetStatsByName(string name)
    {
        var result = await _statService.GetAllStatsByNameAsync(name);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("GetStatsByShortname/{shortName}")]
    [SwaggerOperation(Summary = "Get all Stats by Shortname")]
    public async Task<IActionResult> GetStatsByShortname(string shortName)
    {
        var result = await _statService.GetAllStatsByShortNameAsync(shortName);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("GetStatsDetail")]
    [SwaggerOperation(Summary = "Get all Stats with Details")]
    public async Task<IActionResult> GetStatsDetail()
    {
        var result = await _statService.GetAllStatDetailDtosAsync();
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("GetStatsDetailByName/{name}")]
    [SwaggerOperation(Summary = "Get all Stats with Details by Name")]
    public async Task<IActionResult> GetStatsDetailByName(string name)
    {
        var result = await _statService.GetAllStatDetailDtosByNameAsync(name);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("GetStatsDetailByShortName/{shortName}")]
    [SwaggerOperation(Summary = "Get all Stats with Details by ShortName")]
    public async Task<IActionResult> GetStatsDetailByShortName(string shortName)
    {
        var result = await _statService.GetAllStatDetailDtosByShortNameAsync(shortName);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("GetStatById/{statId:long}")]
    [SwaggerOperation(Summary = "Get a Stat")]
    public async Task<IActionResult> GetStatById(long statId)
    {
        var result = await _statService.GetStatByIdAsync(statId);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("GetStatDetailById/{statId:long}")]
    [SwaggerOperation(Summary = "Get a Stat with Details by Id")]
    public async Task<IActionResult> GetStatDetailById(long statId)
    {
        var result = await _statService.GetStatDetailDtoByIdAsync(statId);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("CreateStat")]
    [SwaggerOperation(Summary = "Create a Stat")]
    public async Task<IActionResult> CreateStat([FromBody] StatForCreationDto? statForCreationDto)
    {
        var result = await _statService.CreateStatAsync(statForCreationDto);
        if (result.Success)
            return CreatedAtAction(nameof(GetStatById), new { statId = result.Data?.Id }, result);
        return BadRequest();
    }

    [HttpPut("UpdateStat/{statId:long}")]
    [SwaggerOperation(Summary = "Update a Stat")]
    public async Task<IActionResult> UpdateStat(long statId, [FromBody] StatForUpdateDto statForUpdateDto)
    {
        var result = await _statService.UpdateStatAsync(statId, statForUpdateDto);
        if (result.Success)
            return CreatedAtAction(nameof(GetStatById), new { statId = result.Data?.Id }, result);
        return BadRequest();
    }

    [HttpDelete("DeleteStat/{statId:long}")]
    [SwaggerOperation(Summary = "Delete a Stat")]
    public async Task<IActionResult> DeleteStat(long statId)
    {
        var result = await _statService.DeleteStatAsync(statId);
        if (result.Success)
            return Ok();
        return BadRequest();
    }
}