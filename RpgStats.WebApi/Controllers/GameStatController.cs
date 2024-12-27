using Microsoft.AspNetCore.Mvc;
using RpgStats.Services.Abstractions;
using Swashbuckle.AspNetCore.Annotations;

namespace RpgStats.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class GameStatController : ControllerBase
{
    private readonly IGameStatService _gameStatService;

    public GameStatController(IGameStatService gameStatService)
    {
        _gameStatService = gameStatService;
    }

    [HttpGet("GetGameStats")]
    [SwaggerOperation(Summary = "Get all GameStats")]
    public async Task<IActionResult> GetGameStats()
    {
        var result = await _gameStatService.GetAllGameStatsAsync();
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetGameStatsByGame/{gameId:long}")]
    [SwaggerOperation(Summary = "Get all GameStats by Game")]
    public async Task<IActionResult> GetGameStatsByGame(long gameId)
    {
        var result = await _gameStatService.GetAllGameStatsByGameIdAsync(gameId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetGameStatsByStat/{statId:long}")]
    [SwaggerOperation(Summary = "Get all GameStats by Stat")]
    public async Task<IActionResult> GetGameStatsByStat(long statId)
    {
        var result = await _gameStatService.GetAllGameStatsByStatIdAsync(statId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetGameStatById/{gameStatId:long}")]
    [SwaggerOperation(Summary = "Get a GameStat by Id")]
    public async Task<IActionResult> GetGameStatById(long gameStatId)
    {
        var result = await _gameStatService.GetGameStatByIdAsync(gameStatId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpPost("CreateGameStat/{gameId:long}/{statId:long}")]
    [SwaggerOperation(Summary = "Create a GameStat")]
    public async Task<IActionResult> CreateGameStat(long gameId, long statId)
    {
        var result = await _gameStatService.CreateGameStatAsync(gameId, statId);
        if (result.Success)
            return CreatedAtAction(nameof(GetGameStatById), new { gameStatId = result.Data?.Id }, result);
        return BadRequest(result);
    }

    [HttpPut("UpdateGameStat/{gameStatId:long}/{gameId:long}/{statId:long}")]
    [SwaggerOperation(Summary = "Update a GameStat")]
    public async Task<IActionResult> UpdateGameStat(long gameStatId, long gameId, long statId)
    {
        var result = await _gameStatService.UpdateGameStatAsync(gameStatId, gameId, statId);
        if (result.Success)
            return CreatedAtAction(nameof(GetGameStatById), new { gameStatId = result.Data?.Id }, result);
        return BadRequest(result);
    }

    [HttpDelete("DeleteGameStat/{gameStatId:long}")]
    [SwaggerOperation(Summary = "Delete a GameStat")]
    public async Task<IActionResult> DeleteGameStat(long gameStatId)
    {
        var result = await _gameStatService.DeleteGameStatAsync(gameStatId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpDelete("DeleteGameStatsByGame/{gameId:long}")]
    [SwaggerOperation(Summary = "Delete all GameStats by Game")]
    public async Task<IActionResult> DeleteGameStatsByGame(long gameId)
    {
        var result = await _gameStatService.DeleteGameStatsByGameIdAsync(gameId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpDelete("DeleteGameStatsByStat/{statId:long}")]
    [SwaggerOperation(Summary = "Delete all GameStats by Stat")]
    public async Task<IActionResult> DeleteGameStatByStat(long statId)
    {
        var result = await _gameStatService.DeleteGameStatsByStatIdAsync(statId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }
}