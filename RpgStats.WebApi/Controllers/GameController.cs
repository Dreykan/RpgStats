using Microsoft.AspNetCore.Mvc;
using RpgStats.Domain.Exceptions;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;
using Swashbuckle.AspNetCore.Annotations;

namespace RpgStats.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet("GetAllGames")]
    [SwaggerResponse(200, "Returns a list of all games", typeof(ApiResponse<List<GameDto>>))]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get all Games")]
    public async Task<IActionResult> GetAllGames()
    {
        var games = await _gameService.GetAllGamesAsync();

        if (games.Count == 0)
            return Ok(ApiResponse<List<GameDto>>.ErrorResult("No games found."));

        return Ok(ApiResponse<List<GameDto>>.SuccessResult(games));
    }

    [HttpGet("GetAllGamesByName/{name}")]
    [SwaggerResponse(200, "Returns a list of games by name", typeof(ApiResponse<List<GameDto>>))]
    [SwaggerResponse(400, "Invalid name parameter")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get all Games by Name")]
    public async Task<IActionResult> GetAllGamesByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest(ApiResponse<List<GameDto>>.ErrorResult("Invalid name parameter. Name cannot be null or empty."));

        var games = await _gameService.GetAllGamesByNameAsync(name);
        
        if (games.Count == 0)
            return Ok(ApiResponse<List<GameDto>>.ErrorResult($"No games found for the specified name: {name}"));
        
        return Ok(ApiResponse<List<GameDto>>.SuccessResult(games));
    }

    [HttpGet("GetGameById/{gameId:long}")]
    [SwaggerResponse(200, "Returns a game by ID", typeof(ApiResponse<GameDto>))]
    [SwaggerResponse(400, "Invalid game ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get a Game by Id")]
    public async Task<IActionResult> GetGameById(long gameId)
    {
        if (gameId <= 0)
            return BadRequest(ApiResponse<GameDto>.ErrorResult("Invalid game ID."));

        var game = await _gameService.GetGameByIdAsync(gameId);
        if (game == null)
            return NotFound(ApiResponse<GameDto>.ErrorResult($"Game with ID {gameId} not found."));

        return Ok(ApiResponse<GameDto>.SuccessResult(game));
    }

    [HttpGet("GetGameDetailById/{gameId:long}")]
    [SwaggerResponse(200, "Returns a game by ID", typeof(ApiResponse<GameDto>))]
    [SwaggerResponse(400, "Invalid game ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<IActionResult> GetGameDetailById(long gameId)
    {
        if (gameId <= 0)
            return BadRequest(ApiResponse<GameDetailDto>.ErrorResult("Invalid game ID."));

        try
        {
            var game = await _gameService.GetGameDetailByIdAsync(gameId);
            return Ok(ApiResponse<GameDetailDto>.SuccessResult(game));
        }
        catch (GameNotFoundException e)
        {
            return NotFound(ApiResponse<GameDetailDto>.ErrorResult(e.Message));
        }
        catch (Exception e)
        {
            return BadRequest(
                ApiResponse<GameDetailDto>.ErrorResult(
                    $"An error occurred while retrieving the game details: {e.Message}"));
        }
    }

    [HttpPost("CreateGame")]
    [SwaggerResponse(201, "Game created successfully", typeof(ApiResponse<GameDto>))]
    [SwaggerResponse(400, "Invalid game data")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Create a Game")]
    public async Task<IActionResult> CreateGame([FromBody] GameForCreationDto gameForCreationDto)
    {
        try
        {
            var game = await _gameService.CreateGameAsync(gameForCreationDto);
            return CreatedAtAction(nameof(GetGameById), new {gameId = game.Id}, ApiResponse<GameDto>.SuccessResult(game));
        }
        catch (Exception e)
        {
            return BadRequest(
                ApiResponse<GameDto>.ErrorResult($"An error occurred while creating the game: {e.Message}"));
        }
    }

    [HttpPut("UpdateGame/{gameId:long}")]
    [SwaggerResponse(200, "Game updated successfully", typeof(ApiResponse<GameDto>))]
    [SwaggerResponse(400, "Invalid game data or game ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Update a Game")]
    public async Task<IActionResult> UpdateGame([FromBody] GameForUpdateDto gameForUpdateDto, long gameId)
    {
        if (gameId <= 0)
            return BadRequest(ApiResponse<GameDto>.ErrorResult($"Invalid game ID: {gameId}."));

        try
        {
            var game = await _gameService.UpdateGameAsync(gameId, gameForUpdateDto);
            return Ok(ApiResponse<GameDto>.SuccessResult(game));
        }
        catch (GameNotFoundException e)
        {
            return NotFound(ApiResponse<GameDto>.ErrorResult(e.Message));
        }
        catch (Exception e)
        {
            return BadRequest(
                ApiResponse<GameDto>.ErrorResult($"An error occurred while updating the game: {e.Message}"));
        }
    }

    [HttpDelete("DeleteGame/{gameId:long}")]
    [SwaggerResponse(200, "Game deleted successfully", typeof(ApiResponse<GameDto>))]
    [SwaggerResponse(400, "Invalid game ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Delete a Game")]
    public async Task<IActionResult> DeleteGame(long gameId)
    {
        if (gameId <= 0)
            return BadRequest(ApiResponse<GameDto>.ErrorResult($"Invalid game ID: {gameId}."));

        try
        {
            var game = await _gameService.DeleteGameAsync(gameId);
            return Ok(ApiResponse<GameDto>.SuccessResult(game));
        }
        catch (GameNotFoundException e)
        {
            return NotFound(ApiResponse<GameDto>.ErrorResult(e.Message));
        }
        catch (Exception e)
        {
            return BadRequest(
                ApiResponse<GameDto>.ErrorResult($"An error occurred while deleting the game: {e.Message}"));
        }
    }
}