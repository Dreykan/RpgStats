using Microsoft.AspNetCore.Mvc;
using RpgStats.Dto;
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

    [HttpGet("GetAllGameStats")]
    [SwaggerResponse(200, "Returns a list of all GameStats", typeof(ApiResponse<List<GameStatDto>>))]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get all GameStats")]
    public async Task<IActionResult> GetAllGameStats()
    {
        var gameStats = await _gameStatService.GetAllGameStatsAsync();
        if (gameStats.Count == 0)
            return Ok(ApiResponse<List<GameStatDto>>.ErrorResult("No GameStats found."));

        return Ok(ApiResponse<List<GameStatDto>>.SuccessResult(gameStats));
    }

    [HttpGet("GetAllGameStatsByGame/{gameId:long}")]
    [SwaggerResponse(200, "Returns a list of GameStats by game ID", typeof(ApiResponse<List<GameStatDto>>))]
    [SwaggerResponse(400, "Invalid game ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get all GameStats by Game")]
    public async Task<IActionResult> GetAllGameStatsByGame(long gameId)
    {
        if (gameId <= 0)
            return BadRequest(ApiResponse<List<GameStatDto>>.ErrorResult("Invalid game ID."));

        var gameStats = await _gameStatService.GetAllGameStatsByGameIdAsync(gameId);
        if (gameStats.Count == 0)
            return Ok(ApiResponse<List<GameStatDto>>.ErrorResult(
                $"No GameStats found for the specified gameId: {gameId}"));

        return Ok(ApiResponse<List<GameStatDto>>.SuccessResult(gameStats));
    }

    [HttpGet("GetAllGameStatsByStat/{statId:long}")]
    [SwaggerResponse(200, "Returns a list of GameStats by stat ID", typeof(ApiResponse<List<GameStatDto>>))]
    [SwaggerResponse(400, "Invalid stat ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get all GameStats by Stat")]
    public async Task<IActionResult> GetAllGameStatsByStat(long statId)
    {
        if (statId <= 0)
            return BadRequest(ApiResponse<List<GameStatDto>>.ErrorResult("Invalid stat ID."));

        var gameStats = await _gameStatService.GetAllGameStatsByStatIdAsync(statId);
        if (gameStats.Count == 0)
            return Ok(ApiResponse<List<GameStatDto>>.ErrorResult(
                $"No GameStats found for the specified statId: {statId}"));

        return Ok(ApiResponse<List<GameStatDto>>.SuccessResult(gameStats));
    }

    [HttpGet("GetGameStatById/{gameStatId:long}")]
    [SwaggerResponse(200, "Returns a GameStat by ID", typeof(ApiResponse<GameStatDto>))]
    [SwaggerResponse(400, "Invalid gameStat ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get a GameStat by Id")]
    public async Task<IActionResult> GetGameStatById(long gameStatId)
    {
        if (gameStatId <= 0)
            return BadRequest(ApiResponse<GameStatDto>.ErrorResult("Invalid gameStat ID."));

        var gameStat = await _gameStatService.GetGameStatByIdAsync(gameStatId);
        if (gameStat == null)
            return Ok(ApiResponse<GameStatDto>.ErrorResult($"GameStat with ID {gameStatId} not found."));

        return Ok(ApiResponse<GameStatDto>.SuccessResult(gameStat));
    }

    [HttpPost("CreateGameStat")]
    [SwaggerResponse(201, "GameStat created successfully", typeof(ApiResponse<GameStatDto>))]
    [SwaggerResponse(400, "Invalid GameStat data")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Create a GameStat")]
    public async Task<IActionResult> CreateGameStat([FromBody] GameStatForCreationDto gameStatForCreationDto)
    {
        try
        {
            var gameStat = await _gameStatService.CreateGameStatAsync(gameStatForCreationDto);
            return CreatedAtAction(nameof(GetGameStatById), new { gameStatId = gameStat.Id },
                ApiResponse<GameStatDto>.SuccessResult(gameStat));
        }
        catch (Exception e)
        {
            return BadRequest(
                ApiResponse<GameStatDto>.ErrorResult($"An error occured while creating the GameStat: {e.Message}"));
        }
    }

    [HttpPut("UpdateGameStat/{gameStatId:long}")]
    [SwaggerResponse(200, "GameStat updated successfully", typeof(ApiResponse<GameStatDto>))]
    [SwaggerResponse(400, "Invalid gameStat ID or data")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Update a GameStat")]
    public async Task<IActionResult> UpdateGameStat([FromBody] GameStatForUpdateDto gameStatForUpdateDto,
        long gameStatId)
    {
        if (gameStatId <= 0)
            return BadRequest(ApiResponse<GameStatDto>.ErrorResult("Invalid gameStat ID."));

        try
        {
            var gameStat = await _gameStatService.UpdateGameStatAsync(gameStatId, gameStatForUpdateDto);
            return Ok(ApiResponse<GameStatDto>.SuccessResult(gameStat));
        }
        catch (ArgumentException e)
        {
            return NotFound(ApiResponse<GameStatDto>.ErrorResult(e.Message));
        }
        catch (Exception e)
        {
            return BadRequest(
                ApiResponse<GameStatDto>.ErrorResult($"An error occured while updating the GameStat: {e.Message}"));
        }
    }

    [HttpDelete("DeleteGameStat/{gameStatId:long}")]
    [SwaggerResponse(200, "GameStat deleted successfully", typeof(ApiResponse<GameStatDto>))]
    [SwaggerResponse(400, "Invalid gameStat ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Delete a GameStat")]
    public async Task<IActionResult> DeleteGameStat(long gameStatId)
    {
        if (gameStatId <= 0)
            return BadRequest(ApiResponse<GameStatDto>.ErrorResult("Invalid gameStat ID."));

        try
        {
            var gameStat = await _gameStatService.DeleteGameStatAsync(gameStatId);
            if (gameStat == null)
                return NotFound(
                    ApiResponse<GameStatDto>.ErrorResult(
                        $"GameStat with ID {gameStatId} not found or could not be deleted."));
            return Ok(ApiResponse<GameStatDto>.SuccessResult(gameStat));
        }
        catch (Exception e)
        {
            return BadRequest(
                ApiResponse<GameStatDto>.ErrorResult($"An error occurred while deleting the GameStat: {e.Message}"));
        }
    }

    [HttpDelete("DeleteAllGameStatsByGame/{gameId:long}")]
    [SwaggerResponse(200, "GameStats deleted successfully", typeof(ApiResponse<List<GameStatDto>>))]
    [SwaggerResponse(400, "Invalid game ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Delete all GameStats by Game")]
    public async Task<IActionResult> DeleteAllGameStatsByGame(long gameId)
    {
        if (gameId <= 0)
            return BadRequest(ApiResponse<List<GameStatDto>>.ErrorResult("Invalid game ID."));

        try
        {
            var gameStats = await _gameStatService.DeleteGameStatsByGameIdAsync(gameId);
            if (gameStats.Count == 0)
                return NotFound(
                    ApiResponse<List<GameStatDto>>.ErrorResult(
                        $"No GameStats found for the specified gameId: {gameId}"));
            return Ok(ApiResponse<List<GameStatDto>>.SuccessResult(gameStats));
        }
        catch (Exception e)
        {
            return BadRequest(
                ApiResponse<List<GameStatDto>>.ErrorResult(
                    $"An error occurred while deleting GameStats for gameId {gameId}: {e.Message}"));
        }
    }

    [HttpDelete("DeleteAllGameStatsByStat/{statId:long}")]
    [SwaggerResponse(200, "GameStats deleted successfully", typeof(ApiResponse<List<GameStatDto>>))]
    [SwaggerResponse(400, "Invalid stat ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Delete all GameStats by Stat")]
    public async Task<IActionResult> DeleteAllGameStatByStat(long statId)
    {
        if (statId <= 0)
            return BadRequest(ApiResponse<List<GameStatDto>>.ErrorResult("Invalid stat ID."));
        try
        {
            var gameStats = await _gameStatService.DeleteGameStatsByStatIdAsync(statId);
            if (gameStats.Count == 0)
                return NotFound(
                    ApiResponse<List<GameStatDto>>.ErrorResult(
                        $"No GameStats found for the specified statId: {statId}"));
            return Ok(ApiResponse<List<GameStatDto>>.SuccessResult(gameStats));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse<List<GameStatDto>>.ErrorResult(
                $"An error occurred while deleting GameStats for statId {statId}: {e.Message}"));
        }
    }
}