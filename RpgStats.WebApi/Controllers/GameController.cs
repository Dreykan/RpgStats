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

    [HttpGet("GetGames")]
    [SwaggerOperation(Summary = "Get all Games")]
    public async Task<IActionResult> GetGames()
    {
        var games = await _gameService.GetAllGamesAsync();
        return Ok(games);
    }

    [HttpGet("GetGamesByName/{name}")]
    [SwaggerOperation(Summary = "Get all Games by Name")]
    public async Task<IActionResult> GetGamesByName(string name)
    {
        var games = await _gameService.GetAllGamesByNameAsync(name);
        return Ok(games);
    }

    [HttpGet("GetGamesDetail")]
    [SwaggerOperation(Summary = "Get all Games with Details")]
    public async Task<IActionResult> GetGamesDetail()
    {
        var games = await _gameService.GetAllGameDetailDtosAsync();
        return Ok(games);
    }

    [HttpGet("GetGamesDetailByName/{name}")]
    [SwaggerOperation(Summary = "Get all Games with Details by Name")]
    public async Task<IActionResult> GetGamesDetailByName(string name)
    {
        var games = await _gameService.GetAllGameDetailDtosByNameAsync(name);
        return Ok(games);
    }

    [HttpGet("GetGameById/{gameId:long}")]
    [SwaggerOperation(Summary = "Get a Game by Id")]
    public async Task<IActionResult> GetGameById(long gameId)
    {
        var game = await _gameService.GetGameByIdAsync(gameId);
        return Ok(game);
    }

    [HttpGet("GetGameDetailById{gameId:long}")]
    [SwaggerOperation(Summary = "Get a Game with Details by Id")]
    public async Task<IActionResult> GetGameDetailById(long gameId)
    {
        var game = await _gameService.GetGameDetailDtoByIdAsync(gameId);
        return Ok(game);
    }

    [HttpPost("CreateGame")]
    [SwaggerOperation(Summary = "Create a Game")]
    public async Task<IActionResult> CreateGame([FromBody] GameForCreationDto gameForCreationDto)
    {
        var response = await _gameService.CreateGameAsync(gameForCreationDto);
        if (response != null) return CreatedAtAction(nameof(GetGameById), new { gameId = response.Id }, response);
        return BadRequest();
    }

    [HttpPut("UpdateGame/{gameId:long}")]
    [SwaggerOperation(Summary = "Update a Game")]
    public async Task<IActionResult> UpdateGame([FromBody] GameForUpdateDto gameForUpdateDto, long gameId)
    {
        var response = await _gameService.UpdateGameAsync(gameId, gameForUpdateDto);
        if (response != null) return Ok(response);
        return BadRequest();
    }

    [HttpDelete("DeleteGame/{gameId:long}")]
    [SwaggerOperation(Summary = "Delete a Game")]
    public async Task<IActionResult> DeleteGame(long gameId)
    {
        var response = await _gameService.DeleteGameAsync(gameId);
        if (response == Task.CompletedTask) return Ok();
        return BadRequest();
    }
}