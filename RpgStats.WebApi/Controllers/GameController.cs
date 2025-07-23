using Microsoft.AspNetCore.Mvc;
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
            return Ok(ApiResponse<GameDto>.ErrorResult($"Game with ID {gameId} not found."));

        return Ok(ApiResponse<GameDto>.SuccessResult(game));

    }

    [HttpPost("CreateGame")]
    [SwaggerResponse(201, "Game created successfully", typeof(ApiResponse<GameDto>))]
    [SwaggerResponse(400, "Invalid game data")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Create a Game")]
    public async Task<IActionResult> CreateGame([FromBody] GameForCreationDto gameForCreationDto)
    {
        var game = await _gameService.CreateGameAsync(gameForCreationDto);
        if (game == null)
            return BadRequest(ApiResponse<GameDto>.ErrorResult("Game could not be created."));

        return Ok(ApiResponse<GameDto>.SuccessResult(game));
    }

    [HttpPut("UpdateGame/{gameId:long}")]
    [SwaggerResponse(200, "Game updated successfully", typeof(ApiResponse<GameDto>))]
    [SwaggerResponse(400, "Invalid game data or game ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Update a Game")]
    public async Task<IActionResult> UpdateGame([FromBody] GameForUpdateDto gameForUpdateDto, long gameId)
    {
        var game = await _gameService.UpdateGameAsync(gameId, gameForUpdateDto);
        if (game == null)
            return BadRequest(ApiResponse<GameDto>.ErrorResult($"Game with ID {gameId} not found or could not be updated."));

        return Ok(ApiResponse<GameDto>.SuccessResult(game));
    }

    [HttpDelete("DeleteGame/{gameId:long}")]
    [SwaggerResponse(200, "Game deleted successfully", typeof(ApiResponse<GameDto>))]
    [SwaggerResponse(400, "Invalid game ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Delete a Game")]
    public async Task<IActionResult> DeleteGame(long gameId)
    {
        var game = await _gameService.DeleteGameAsync(gameId);
        if (game == null)
            return BadRequest(ApiResponse<GameDto>.ErrorResult($"Game with ID {gameId} not found or could not be deleted."));

        return Ok(ApiResponse<GameDto>.SuccessResult(game));
    }
}