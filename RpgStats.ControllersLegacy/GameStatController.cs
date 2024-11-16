using Microsoft.AspNetCore.Mvc;
using RpgStats.Services.Abstractions;
using Swashbuckle.AspNetCore.Annotations;

namespace RpgStats.ControllersLegacy;

[ApiController]
[Route("api/gameStats")]
public class GameStatController : ControllerBase
{
    private readonly IGameStatService _gameStatService;

    public GameStatController(IGameStatService gameStatService)
    {
        _gameStatService = gameStatService;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all GameStats")]
    public async Task<IActionResult> GetAllGameStats()
    {
        var gameStats = await _gameStatService.GetAllGameStatsAsync();
        return Ok(gameStats);
    }

    [HttpGet("byGame")]
    [SwaggerOperation(Summary = "Get all GameStats by Game")]
    public async Task<IActionResult> GetAllGameStatsByGame(long gameId)
    {
        var gameStats = await _gameStatService.GetAllGameStatsByGameIdAsync(gameId);
        return Ok(gameStats);
    }

    [HttpGet("byStat")]
    [SwaggerOperation(Summary = "Get all GameStats by Stat")]
    public async Task<IActionResult> GetAllGameStatsByStat(long statId)
    {
        var gameStats = await _gameStatService.GetAllGameStatsByStatIdAsync(statId);
        return Ok(gameStats);
    }

    [HttpGet("{gameStatId:long}")]
    [SwaggerOperation(Summary = "Get a GameStat by Id")]
    public async Task<IActionResult> GetGameStatById(long gameStatId)
    {
        var gameStat = await _gameStatService.GetGameStatByIdAsync(gameStatId);
        return Ok(gameStat);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a GameStat")]
    public async Task<IActionResult> CreateGameStat(long gameId, long statId)
    {
        var response = await _gameStatService.CreateGameStatAsync(gameId, statId);
        if (response != null)
            return CreatedAtAction(nameof(GetGameStatById),
                new { gameStatId = response.Id, gameId = response.GameId, statId = response.StatId },
                response);
        return BadRequest();
    }

    [HttpPut("{gameStatId:long}")]
    [SwaggerOperation(Summary = "Update a GameStat")]
    public async Task<IActionResult> UpdateGameStat(long gameStatId, long gameId, long statId)
    {
        var response = await _gameStatService.UpdateGameStatAsync(gameStatId, gameId, statId);
        if (response != null)
            return Ok(response);
        return BadRequest();
    }

    [HttpDelete("{gameStatId:long}")]
    [SwaggerOperation(Summary = "Delete a GameStat")]
    public async Task<IActionResult> DeleteGameStat(long gameStatId)
    {
        var response = await _gameStatService.DeleteGameStatAsync(gameStatId);
        if (response == Task.CompletedTask)
            return Ok();
        return BadRequest();
    }

    [HttpDelete("byGame/{gameId:long}")]
    [SwaggerOperation(Summary = "Delete all GameStats by Game")]
    public async Task<IActionResult> DeleteGameStatByGame(long gameId)
    {
        var response = await _gameStatService.DeleteGameStatByGameIdAsync(gameId);
        if (response == Task.CompletedTask)
            return Ok();
        return BadRequest();
    }

    [HttpDelete("byStat/{statId:long}")]
    [SwaggerOperation(Summary = "Delete all GameStats by Stat")]
    public async Task<IActionResult> DeleteGameStatByStat(long statId)
    {
        var response = await _gameStatService.DeleteGameStatByStatIdAsync(statId);
        if (response == Task.CompletedTask)
            return Ok();
        return BadRequest();
    }
}