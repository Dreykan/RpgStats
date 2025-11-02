using Microsoft.AspNetCore.Mvc;
using RpgStats.Domain.Exceptions;
using RpgStats.Dto;
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

    [HttpGet("GetAllPlatformGames")]
    [SwaggerResponse(200, "Return a list of all PlatformGames", typeof(ApiResponse<List<PlatformGameDto>>))]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "An error occured")]
    [SwaggerOperation(Summary = "Get all PlatformGames")]
    public async Task<IActionResult> GetAllPlatformGames()
    {
        var platformGames = await _platformGameService.GetAllPlatformGamesAsync();
        if (platformGames.Count == 0)
            return Ok(ApiResponse<List<PlatformGameDto>>.ErrorResult("No PlatformGames found"));

        return Ok(ApiResponse<List<PlatformGameDto>>.SuccessResult(platformGames));
    }

    [HttpGet("GetAllPlatformGamesByPlatform/{platformId:long}")]
    [SwaggerResponse(200, "Return a list of PlatformGames by PlatformId", typeof(ApiResponse<List<PlatformGameDto>>))]
    [SwaggerResponse(400, "Invalid PlatformId")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "An error occured")]
    [SwaggerOperation(Summary = "Get all PlatformGames by Platform")]
    public async Task<IActionResult> GetAllPlatformGamesByPlatform(long platformId)
    {
        if (platformId <= 0)
            return BadRequest(ApiResponse<List<PlatformGameDto>>.ErrorResult("Invalid PlatformId"));

        var platformGames = await _platformGameService.GetAllPlatformGamesByPlatformIdAsync(platformId);
        if (platformGames.Count == 0)
            return Ok(ApiResponse<List<PlatformGameDto>>.ErrorResult($"No PlatformGames found for the specified PlatformId: {platformId}"));

        return Ok(ApiResponse<List<PlatformGameDto>>.SuccessResult(platformGames));
    }

    [HttpGet("GetAllPlatformGamesByGame/{gameId:long}")]
    [SwaggerResponse(200, "Return a list of PlatformGames by GameId",  typeof(ApiResponse<List<PlatformGameDto>>))]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(400, "Invalid GameId")]
    [SwaggerResponse(500, "An error occured")]
    [SwaggerOperation(Summary = "Get all PlatformGames by Game")]
    public async Task<IActionResult> GetAllPlatformGamesByGame(long gameId)
    {
        if (gameId <= 0)
            return BadRequest(ApiResponse<List<PlatformGameDto>>.ErrorResult("Invalid GameId"));

        var platformGames = await _platformGameService.GetAllPlatformGamesByGameIdAsync(gameId);
        if (platformGames.Count == 0)
            return Ok(ApiResponse<List<PlatformGameDto>>.ErrorResult($"No PlatformGames found for the specified GameId: {gameId}"));

        return Ok(ApiResponse<List<PlatformGameDto>>.SuccessResult(platformGames));
    }

    [HttpGet("GetPlatformGameById/{platformGameId:long}")]
    [SwaggerResponse(200, "Returns a PlatformGame by ID", typeof(ApiResponse<PlatformGameDto>))]
    [SwaggerResponse(400, "Invalid PlatformGame Id")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "An error occured")]
    [SwaggerOperation(Summary = "Get a Platform by Id")]
    public async Task<IActionResult> GetPlatformGameById(long platformGameId)
    {
        if (platformGameId <= 0)
            return BadRequest(ApiResponse<List<PlatformGameDto>>.ErrorResult("Invalid PlatformGame ID"));

        var platformGame = await _platformGameService.GetPlatformGameByIdAsync(platformGameId);
        if (platformGame == null)
            return NotFound(ApiResponse<PlatformGameDto>.ErrorResult($"PlatformGame with ID {platformGameId} not found"));

        return Ok(ApiResponse<PlatformGameDto>.SuccessResult(platformGame));
    }

    [HttpPost("CreatePlatformGame")]
    [SwaggerResponse(201, "PlatformGame created successfully", typeof(ApiResponse<PlatformGameDto>))]
    [SwaggerResponse(400, "Invalid PlatformGame Data")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Create a PlatformGame")]
    public async Task<IActionResult> CreatePlatformGame([FromBody] PlatformGameForCreationDto platformGameForCreation)
    {
        try
        {
            var platformGame = await _platformGameService.CreatePlatformGameAsync(platformGameForCreation);
            return CreatedAtAction(nameof(GetPlatformGameById), new { platformGameId = platformGame.Id },
                ApiResponse<PlatformGameDto>.SuccessResult(platformGame));
        }
        catch (ArgumentException e)
        {
            return BadRequest(ApiResponse<PlatformGameDto>.ErrorResult(e.Message));
        }
        catch (PlatformNotFoundException e)
        {
            return NotFound(ApiResponse<PlatformGameDto>.ErrorResult(e.Message));
        }
        catch (GameNotFoundException e)
        {
            return NotFound(ApiResponse<PlatformGameDto>.ErrorResult(e.Message));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse<PlatformGameDto>.ErrorResult($"An error occured while creating the PlatformGame: {e.Message}"));
        }
    }

    [HttpPut("UpdatePlatformGame/{platformGameId:long}")]
    [SwaggerResponse(200, "PlatformGame updated successfully", typeof(ApiResponse<PlatformGameDto>))]
    [SwaggerResponse(400, "Invalid PlatformGame Id or data")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Update a PlatformGame")]
    public async Task<IActionResult> UpdatePlatformGame(long platformGameId, [FromBody] PlatformGameForUpdateDto platformGameForUpdate)
    {
        if (platformGameId <= 0)
            return BadRequest(ApiResponse<PlatformGameDto>.ErrorResult("Invalid PlatformGame ID"));

        try
        {
            var platformGame = await _platformGameService.UpdatePlatformGameAsync(platformGameId, platformGameForUpdate);
            return Ok(ApiResponse<PlatformGameDto>.SuccessResult(platformGame));
        }
        catch (PlatformGameNotFoundException e)
        {
            return NotFound(ApiResponse<PlatformGameDto>.ErrorResult(e.Message));
        }
        catch (PlatformNotFoundException e)
        {
            return NotFound(ApiResponse<PlatformGameDto>.ErrorResult(e.Message));
        }
        catch (GameNotFoundException e)
        {
            return NotFound(ApiResponse<PlatformGameDto>.ErrorResult(e.Message));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse<PlatformGameDto>.ErrorResult($"An error occured while updating the PlatformGame: {e.Message}"));
        }
    }

    [HttpDelete("DeletePlatformGame/{platformGameId:long}")]
    [SwaggerResponse(200, "PlatformGame deleted successfully", typeof(ApiResponse<PlatformGameDto>))]
    [SwaggerResponse(400, "Invalid PlatformGame Id or data")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Delete a PlatformGame")]
    public async Task<IActionResult> DeletePlatformGame(long platformGameId)
    {
        if (platformGameId <= 0)
            return BadRequest(ApiResponse<PlatformGameDto>.ErrorResult("Invalid PlatformGame ID"));

        try
        {
            var platformGame = await _platformGameService.DeletePlatformGameAsync(platformGameId);
            return Ok(ApiResponse<PlatformGameDto>.SuccessResult(platformGame));
        }
        catch (PlatformGameNotFoundException e)
        {
            return NotFound(ApiResponse<PlatformGameDto>.ErrorResult(e.Message));
        }
        catch (Exception e)
        {
            return BadRequest(
                ApiResponse<PlatformGameDto>.ErrorResult($"An error occured while deleting the PlatformGame: {e.Message}"));
        }
    }

    [HttpDelete("DeletePlatformGamesByGame/{gameId:long}")]
    [SwaggerResponse(200, "PlatformGame deleted successfully", typeof(ApiResponse<PlatformGameDto>))]
    [SwaggerResponse(400, "Invalid PlatformGame Id or data")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Delete all PlatformGames by Game")]
    public async Task<IActionResult> DeletePlatformGamesByGame(long gameId)
    {
        if (gameId <= 0)
            return BadRequest(ApiResponse<PlatformGameDto>.ErrorResult("Invalid Game ID"));

        try
        {
            var platformGames = await _platformGameService.DeletePlatformGamesByGameIdAsync(gameId);
            if (platformGames.Count == 0)
                return NotFound(
                    ApiResponse<List<PlatformGameDto>>.ErrorResult(
                        $"No PlatformGames found for the specified GameId: {gameId}"));
            return Ok(ApiResponse<List<PlatformGameDto>>.SuccessResult(platformGames));
        }
        catch (GameNotFoundException e)
        {
            return NotFound(ApiResponse<List<PlatformGameDto>>.ErrorResult(e.Message));
        }
        catch (Exception e)
        {
            return BadRequest(
                ApiResponse<List<PlatformGameDto>>.ErrorResult(
                    $"An error occured while deleting the PlatformGames: {e.Message}"));
        }
    }

    [HttpDelete("DeletePlatformGamesByPlatform/{platformId:long}")]
    [SwaggerResponse(200, "PlatformGame deleted successfully", typeof(ApiResponse<PlatformGameDto>))]
    [SwaggerResponse(400, "Invalid PlatformGame Id or data")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Delete all PlatformGames by Platform")]
    public async Task<IActionResult> DeletePlatformGamesByPlatform(long platformId)
    {
        if (platformId <= 0)
            return BadRequest(ApiResponse<PlatformGameDto>.ErrorResult("Invalid PlatformGame ID"));

        try
        {
            var platformGames = await _platformGameService.DeletePlatformGamesByPlatformIdAsync(platformId);
            if (platformGames.Count == 0)
                return NotFound(
                    ApiResponse<List<PlatformGameDto>>.ErrorResult(
                        $"No PlatformGames found for the specified PlatformId: {platformId}"));
            return Ok(ApiResponse<List<PlatformGameDto>>.SuccessResult(platformGames));
        }
        catch (PlatformNotFoundException e)
        {
            return NotFound(ApiResponse<List<PlatformGameDto>>.ErrorResult(e.Message));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse<List<PlatformGameDto>>.ErrorResult(
                $"An error occured while deleting the PlatformGame: {e.Message}"));
        }
    }
}