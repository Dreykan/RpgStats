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

    [HttpGet("{statId:long}")]
    [SwaggerOperation(Summary = "Get a Stat")]
    public async Task<IActionResult> GetStatById(long statId)
    {
        var stat = await _statService.GetStatByIdAsync(statId);
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