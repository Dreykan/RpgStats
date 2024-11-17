using Microsoft.AspNetCore.Mvc;
using RpgStats.Services.Abstractions;
using Swashbuckle.AspNetCore.Annotations;

namespace RpgStats.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PlatformGameController : ControllerBase
{
    private readonly IPlatformGameService _platformGameService;

    public PlatformGameController(IPlatformGameService platformGameService)
    {
        _platformGameService = platformGameService;
    }

    [HttpGet("GetPlatformGames")]
    [SwaggerOperation(Summary = "Get all PlatformGames")]
    public async Task<IActionResult> GetPlatformGames()
    {
        var platformGames = await _platformGameService.GetAllPlatformGamesAsync();
        return Ok(platformGames);
    }

    [HttpGet("GetPlatformGamesByPlatform/{platformId:long}")]
    [SwaggerOperation(Summary = "Get all PlatformGames by Platform")]
    public async Task<IActionResult> GetPlatformGamesByPlatform(long platformId)
    {
        var platformGames = await _platformGameService.GetAllPlatformGamesByPlatformIdAsync(platformId);
        return Ok(platformGames);
    }

    [HttpGet("GetPlatformGamesByGame/{gameId:long}")]
    [SwaggerOperation(Summary = "Get all PlatformGames by Game")]
    public async Task<IActionResult> GetPlatformGamesByGame(long gameId)
    {
        var platformGames = await _platformGameService.GetAllPlatformGamesByGameIdAsync(gameId);
        return Ok(platformGames);
    }

    [HttpGet("GetPlatformGameById/{platformGameId:long}")]
    [SwaggerOperation(Summary = "Get a Platform by Id")]
    public async Task<IActionResult> GetPlatformGameById(long platformGameId)
    {
        var platformGame = await _platformGameService.GetPlatformGameByIdAsync(platformGameId);
        return Ok(platformGame);
    }

    [HttpPost("CreatePlatformGame/{platformId:long}/{gameId:long}")]
    [SwaggerOperation(Summary = "Create a PlatformGame")]
    public async Task<IActionResult> CreatePlatformGame(long platformId, long gameId)
    {
        var response = await _platformGameService.CreatePlatformGameAsync(platformId, gameId);
        if (response != null)
            return CreatedAtAction(nameof(GetPlatformGameById),
                new { platformGameId = response.Id, gameId = response.GameId, platformId = response.PlatformId },
                response);
        return BadRequest();
    }

    [HttpPut("UpdatePlatformGame/{platformGameId:long}/{platformId:long}/{gameId:long}")]
    [SwaggerOperation(Summary = "Update a PlatformGame")]
    public async Task<IActionResult> UpdatePlatformGame(long platformGameId, long platformId, long gameId)
    {
        var response = await _platformGameService.UpdatePlatformGameAsync(platformGameId, platformId, gameId);
        if (response != null) return Ok(response);
        return BadRequest();
    }

    [HttpDelete("DeletePlatformGame/{platformGameId:long}")]
    [SwaggerOperation(Summary = "Delete a PlatformGame")]
    public async Task<IActionResult> DeletePlatformGame(long platformGameId)
    {
        var response = await _platformGameService.DeletePlatformGameAsync(platformGameId);
        if (response == Task.CompletedTask) return Ok();
        return BadRequest();
    }

    [HttpDelete("DeletePlatformGamesByGame/{gameId:long}")]
    [SwaggerOperation(Summary = "Delete all PlatformGames by Game")]
    public async Task<IActionResult> DeletePlatformGamesByGame(long gameId)
    {
        var response = await _platformGameService.DeletePlatformGameByGameIdAsync(gameId);
        if (response == Task.CompletedTask) return Ok();
        return BadRequest();
    }

    [HttpDelete("DeletePlatformGamesByPlatform/{platformId:long}")]
    [SwaggerOperation(Summary = "Delete all PlatformGames by Platform")]
    public async Task<IActionResult> DeletePlatformGameByPlatform(long platformId)
    {
        var response = await _platformGameService.DeletePlatformGameByPlatformIdAsync(platformId);
        if (response == Task.CompletedTask) return Ok();
        return BadRequest();
    }
}