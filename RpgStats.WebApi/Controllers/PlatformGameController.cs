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
        var result = await _platformGameService.GetAllPlatformGamesAsync();
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetPlatformGamesByPlatform/{platformId:long}")]
    [SwaggerOperation(Summary = "Get all PlatformGames by Platform")]
    public async Task<IActionResult> GetPlatformGamesByPlatform(long platformId)
    {
        var result = await _platformGameService.GetAllPlatformGamesByPlatformIdAsync(platformId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetPlatformGamesByGame/{gameId:long}")]
    [SwaggerOperation(Summary = "Get all PlatformGames by Game")]
    public async Task<IActionResult> GetPlatformGamesByGame(long gameId)
    {
        var result = await _platformGameService.GetAllPlatformGamesByGameIdAsync(gameId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetPlatformGameById/{platformGameId:long}")]
    [SwaggerOperation(Summary = "Get a Platform by Id")]
    public async Task<IActionResult> GetPlatformGameById(long platformGameId)
    {
        var result = await _platformGameService.GetPlatformGameByIdAsync(platformGameId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpPost("CreatePlatformGame/{platformId:long}/{gameId:long}")]
    [SwaggerOperation(Summary = "Create a PlatformGame")]
    public async Task<IActionResult> CreatePlatformGame(long platformId, long gameId)
    {
        var result = await _platformGameService.CreatePlatformGameAsync(platformId, gameId);
        if (result.Success)
            return CreatedAtAction(nameof(GetPlatformGameById), new { platformGameId = result.Data?.Id }, result);
        return BadRequest(result);
    }

    [HttpPut("UpdatePlatformGame/{platformGameId:long}/{platformId:long}/{gameId:long}")]
    [SwaggerOperation(Summary = "Update a PlatformGame")]
    public async Task<IActionResult> UpdatePlatformGame(long platformGameId, long platformId, long gameId)
    {
        var result = await _platformGameService.UpdatePlatformGameAsync(platformGameId, platformId, gameId);
        if (result.Success)
            return CreatedAtAction(nameof(GetPlatformGameById), new { platformGameId = result.Data?.Id }, result);
        return BadRequest(result);
    }

    [HttpDelete("DeletePlatformGame/{platformGameId:long}")]
    [SwaggerOperation(Summary = "Delete a PlatformGame")]
    public async Task<IActionResult> DeletePlatformGame(long platformGameId)
    {
        var result = await _platformGameService.DeletePlatformGameAsync(platformGameId);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("DeletePlatformGamesByGame/{gameId:long}")]
    [SwaggerOperation(Summary = "Delete all PlatformGames by Game")]
    public async Task<IActionResult> DeletePlatformGamesByGame(long gameId)
    {
        var result = await _platformGameService.DeletePlatformGameByGameIdAsync(gameId);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("DeletePlatformGamesByPlatform/{platformId:long}")]
    [SwaggerOperation(Summary = "Delete all PlatformGames by Platform")]
    public async Task<IActionResult> DeletePlatformGameByPlatform(long platformId)
    {
        var result = await _platformGameService.DeletePlatformGameByPlatformIdAsync(platformId);
        if (result.Success)
            return Ok(result);
        return BadRequest();
    }
}