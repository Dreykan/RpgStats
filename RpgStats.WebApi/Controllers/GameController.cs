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
        var result = await _gameService.GetAllGamesAsync();
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetGamesByName/{name}")]
    [SwaggerOperation(Summary = "Get all Games by Name")]
    public async Task<IActionResult> GetGamesByName(string name)
    {
        var result = await _gameService.GetAllGamesByNameAsync(name);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetGamesDetail")]
    [SwaggerOperation(Summary = "Get all Games with Details")]
    public async Task<IActionResult> GetGamesDetail()
    {
        var result = await _gameService.GetAllGameDetailDtosAsync();
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetGamesDetailByName/{name}")]
    [SwaggerOperation(Summary = "Get all Games with Details by Name")]
    public async Task<IActionResult> GetGamesDetailByName(string name)
    {
        var result = await _gameService.GetAllGameDetailDtosByNameAsync(name);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetGameById/{gameId:long}")]
    [SwaggerOperation(Summary = "Get a Game by Id")]
    public async Task<IActionResult> GetGameById(long gameId)
    {
        var result = await _gameService.GetGameByIdAsync(gameId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetGameDetailById/{gameId:long}")]
    [SwaggerOperation(Summary = "Get a Game with Details by Id")]
    public async Task<IActionResult> GetGameDetailById(long gameId)
    {
        var result = await _gameService.GetGameDetailDtoByIdAsync(gameId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpPost("CreateGame")]
    [SwaggerOperation(Summary = "Create a Game")]
    public async Task<IActionResult> CreateGame([FromBody] GameForCreationDto gameForCreationDto)
    {
        var result = await _gameService.CreateGameAsync(gameForCreationDto);
        if (result.Success)
            return CreatedAtAction(nameof(GetGameById), new { gameId = result.Data?.Id }, result);
        return BadRequest(result);
    }

    [HttpPut("UpdateGame/{gameId:long}")]
    [SwaggerOperation(Summary = "Update a Game")]
    public async Task<IActionResult> UpdateGame([FromBody] GameForUpdateDto gameForUpdateDto, long gameId)
    {
        var result = await _gameService.UpdateGameAsync(gameId, gameForUpdateDto);
        if (result.Success)
            return CreatedAtAction(nameof(GetGameById), new { gameId = result.Data?.Id }, result);
        return BadRequest(result);
    }

    [HttpDelete("DeleteGame/{gameId:long}")]
    [SwaggerOperation(Summary = "Delete a Game")]
    public async Task<IActionResult> DeleteGame(long gameId)
    {
        var result = await _gameService.DeleteGameAsync(gameId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }
}