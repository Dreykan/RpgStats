using Microsoft.AspNetCore.Mvc;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;
using Swashbuckle.AspNetCore.Annotations;

namespace RpgStats.Controllers;

[ApiController]
[Route("api/games")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all Games")]
    public async Task<IActionResult> GetAllGames()
    {
        var games = await _gameService.GetAllGamesAsync();
        return Ok(games);
    }

    [HttpGet("{gameId:long}")]
    [SwaggerOperation(Summary = "Get a Game by Id")]
    public async Task<IActionResult> GetGameById(long gameId)
    {
        var game = await _gameService.GetGameByIdAsync(gameId);
        return Ok(game);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a Game")]
    public async Task<IActionResult> CreateGame([FromBody] GameForCreationDto gameForCreationDto)
    {
        var response = await _gameService.CreateGameAsync(gameForCreationDto);
        if (response != null) return CreatedAtAction(nameof(GetGameById), new { gameId = response.Id }, response);
        return BadRequest();
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update a Game")]
    public async Task<IActionResult> UpdateGame([FromBody] GameForUpdateDto gameForUpdateDto, long gameId)
    {
        await _gameService.UpdateGameAsync(gameId, gameForUpdateDto);
        return NoContent();
    }

    [HttpDelete]
    [SwaggerOperation(Summary = "Delete a Game")]
    public async Task<IActionResult> DeleteGame(long gameId)
    {
        await _gameService.DeleteGameAsync(gameId);
        return NoContent();
    }
}