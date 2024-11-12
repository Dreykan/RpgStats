using Microsoft.AspNetCore.Mvc;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;
using Swashbuckle.AspNetCore.Annotations;

namespace RpgStats.WebApi.Controllers;

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

    [HttpGet("byName")]
    [SwaggerOperation(Summary = "Get all Games by Name")]
    public async Task<IActionResult> GetAllGamesByName(string name)
    {
        var games = await _gameService.GetAllGamesByNameAsync(name);
        return Ok(games);
    }

    [HttpGet("details")]
    [SwaggerOperation(Summary = "Get all Games with Details")]
    public async Task<IActionResult> GetAllGameDetailDtos()
    {
        var games = await _gameService.GetAllGameDetailDtosAsync();
        return Ok(games);
    }

    [HttpGet("detailsByName")]
    [SwaggerOperation(Summary = "Get all Games with Details by Name")]
    public async Task<IActionResult> GetAllGameDetailDtosByName(string name)
    {
        var games = await _gameService.GetAllGameDetailDtosByNameAsync(name);
        return Ok(games);
    }

    [HttpGet("{gameId:long}")]
    [SwaggerOperation(Summary = "Get a Game by Id")]
    public async Task<IActionResult> GetGameById(long gameId)
    {
        var game = await _gameService.GetGameByIdAsync(gameId);
        return Ok(game);
    }

    [HttpGet("details{gameId:long}")]
    [SwaggerOperation(Summary = "Get a Game with Details by Id")]
    public async Task<IActionResult> GetGameDetailDtoById(long gameId)
    {
        var game = await _gameService.GetGameDetailDtoByIdAsync(gameId);
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
        var response = await _gameService.UpdateGameAsync(gameId, gameForUpdateDto);
        if (response != null) return Ok(response);
        return BadRequest();
    }

    [HttpDelete]
    [SwaggerOperation(Summary = "Delete a Game")]
    public async Task<IActionResult> DeleteGame(long gameId)
    {
        var response = await _gameService.DeleteGameAsync(gameId);
        if (response == Task.CompletedTask) return Ok();
        return BadRequest();
    }
}