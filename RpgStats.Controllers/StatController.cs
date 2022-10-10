using Microsoft.AspNetCore.Mvc;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;
using Swashbuckle.AspNetCore.Annotations;

namespace RpgStats.Controllers;

[ApiController]
[Route("api/stats")]
public class StatController : ControllerBase
{
    private readonly IStatService _statService;

    public StatController(IStatService statService)
    {
        _statService = statService;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all Stats")]
    public async Task<IActionResult> GetAllStats()
    {
        var stats = await _statService.GetAllStatsAsync();
        return Ok(stats);
    }

    [HttpGet("byName")]
    [SwaggerOperation(Summary = "Get all Stats by Name")]
    public async Task<IActionResult> GetAllStatsByName(string name)
    {
        var stats = await _statService.GetAllStatsByNameAsync(name);
        return Ok(stats);
    }

    [HttpGet("byShortname")]
    [SwaggerOperation(Summary = "Get all Stats by Shortname")]
    public async Task<IActionResult> GetAllStatsByShortname(string shortName)
    {
        var stats = await _statService.GetAllStatsByShortNameAsync(shortName);
        return Ok(stats);
    }

    [HttpGet("details")]
    [SwaggerOperation(Summary = "Get all Stats with Details")]
    public async Task<IActionResult> GetAllStatDetailDtos()
    {
        var stats = await _statService.GetAllStatDetailDtosAsync();
        return Ok(stats);
    }

    [HttpGet("detailsByName")]
    [SwaggerOperation(Summary = "Get all Stats with Details by Name")]
    public async Task<IActionResult> GetAllStatDetailDtosByName(string name)
    {
        var stats = await _statService.GetAllStatDetailDtosByNameAsync(name);
        return Ok(stats);
    }

    [HttpGet("detailsByShortName")]
    [SwaggerOperation(Summary = "Get all Stats with Details by ShortName")]
    public async Task<IActionResult> GetAllStatDetailDtosByShortName(string shortName)
    {
        var stats = await _statService.GetAllStatDetailDtosByShortNameAsync(shortName);
        return Ok(stats);
    }

    [HttpGet("{statId:long}")]
    [SwaggerOperation(Summary = "Get a Stat")]
    public async Task<IActionResult> GetStatById(long statId)
    {
        var stat = await _statService.GetStatByIdAsync(statId);
        return Ok(stat);
    }

    [HttpGet("details{statId:long}")]
    [SwaggerOperation(Summary = "Get a Stat with Details by Id")]
    public async Task<IActionResult> GetStatDetailDtoById(long statId)
    {
        var stat = await _statService.GetStatDetailDtoByIdAsync(statId);
        return Ok(stat);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a Stat")]
    public async Task<IActionResult> CreateStat(StatForCreationDto statForCreationDto)
    {
        var response = await _statService.CreateStatAsync(statForCreationDto);
        if (response != null) return CreatedAtAction(nameof(GetStatById), new { statId = response.Id }, response);
        return BadRequest();
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update a Stat")]
    public async Task<IActionResult> UpdateStat(long statId, StatForUpdateDto statForUpdateDto)
    {
        await _statService.UpdateStatAsync(statId, statForUpdateDto);
        return NoContent();
    }

    [HttpDelete]
    [SwaggerOperation(Summary = "Delete a Stat")]
    public async Task<IActionResult> DeleteStat(long statId)
    {
        await _statService.DeleteStatAsync(statId);
        return NoContent();
    }
}