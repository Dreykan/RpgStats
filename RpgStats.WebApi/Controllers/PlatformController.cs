using Microsoft.AspNetCore.Mvc;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;
using Swashbuckle.AspNetCore.Annotations;

namespace RpgStats.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PlatformController : ControllerBase
{
    private readonly IPlatformService _platformService;

    public PlatformController(IPlatformService platformService)
    {
        _platformService = platformService;
    }

    [HttpGet("GetAllPlatforms")]
    [SwaggerResponse(200, "Returns all platforms", typeof(ApiResponse<List<PlatformDto>>))]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get all Platforms")]
    public async Task<IActionResult> GetAllPlatforms()
    {
        var platforms = await _platformService.GetAllPlatformsAsync();
        if (platforms.Count == 0)
            return Ok(ApiResponse<List<PlatformDto>>.ErrorResult("No Platforms found."));

        return Ok(ApiResponse<List<PlatformDto>>.SuccessResult(platforms));
    }

    [HttpGet("GetAllPlatformsWithGames")]
    [SwaggerResponse(200, "Returns all platforms with games", typeof(ApiResponse<List<PlatformWithGamesDto>>))]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get all Platforms with games")]
    public async Task<IActionResult> GetPlatformsWithGamesAsync()
    {
        var platforms = await _platformService.GetAllPlatformsWithGamesAsync();
        if (platforms.Count == 0)
            return Ok(ApiResponse<List<PlatformWithGamesDto>>.ErrorResult("No Platforms found."));

        return Ok(ApiResponse<List<PlatformWithGamesDto>>.SuccessResult(platforms));
    }

    [HttpGet("GetAllPlatformsByName/{name}")]
    [SwaggerResponse(200, "Returns all platforms by name", typeof(ApiResponse<List<PlatformDto>>))]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get all Platforms by name")]
    public async Task<IActionResult> GetAllPlatformsByNameAsync(string name)
    {
        var platforms = await _platformService.GetAllPlatformsByNameAsync(name);
        if (platforms.Count == 0)
            return Ok(ApiResponse<List<PlatformDto>>.ErrorResult("No Platforms found."));

        return Ok(ApiResponse<List<PlatformDto>>.SuccessResult(platforms));
    }

    [HttpGet("GetPlatformById/{platformId:long}")]
    [SwaggerResponse(200, "Returns platform by ID",  typeof(ApiResponse<PlatformDto>))]
    [SwaggerResponse(400, "Invalid ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Get a Platform by Id")]
    public async Task<IActionResult> GetPlatformById(long platformId)
    {
        if (platformId <= 0)
            return BadRequest(ApiResponse<PlatformDto>.ErrorResult("Invalid platform ID."));

        var platform = await _platformService.GetPlatformByIdAsync(platformId);
        if (platform == null)
            return BadRequest(ApiResponse<PlatformDto>.ErrorResult($"Platform with ID {platformId} not found."));

        return Ok(ApiResponse<PlatformDto>.SuccessResult(platform));
    }

    [HttpGet("GetPlatformWithGamesById/{platformId:long}")]
    [SwaggerResponse(200, "Returns platform with games by ID", typeof(ApiResponse<PlatformWithGamesDto>))]
    [SwaggerResponse(400, "Invalid ID")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<IActionResult> GetPlatformWithGamesByIdAsync(long platformId)
    {
        if (platformId <= 0)
            return BadRequest(ApiResponse<PlatformWithGamesDto>.ErrorResult("Invalid platform ID."));

        var platformWithGames = await _platformService.GetPlatformWithGamesByIdAsync(platformId);
        if (platformWithGames == null)
            return BadRequest(ApiResponse<PlatformWithGamesDto>.ErrorResult($"Platform with ID {platformId} not found."));

        return Ok(ApiResponse<PlatformWithGamesDto>.SuccessResult(platformWithGames));
    }

    [HttpPost("CreatePlatform")]
    [SwaggerResponse(201, "Returns a Platform created", typeof(ApiResponse<PlatformDto>))]
    [SwaggerResponse(400, "Invalid Platform data")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Create a Platform")]
    public async Task<IActionResult> CreatePlatform([FromBody] PlatformForCreationDto platformForCreationDto)
    {
        try
        {
            var platform = await _platformService.CreatePlatformAsync(platformForCreationDto);
            return CreatedAtAction(nameof(GetPlatformById), new { platformId = platform.Id },
                ApiResponse<PlatformDto>.SuccessResult(platform));
        }
        catch (Exception e)
        {
            return BadRequest(
                ApiResponse<PlatformDto>.ErrorResult($"An error occured while creating platform: {e.Message}"));
        }
    }

    [HttpPut("UpdatePlatform/{platformId:long}")]
    [SwaggerResponse(200, "Returns a Platform updated", typeof(ApiResponse<PlatformDto>))]
    [SwaggerResponse(400, "Invalid Platform data")]
    [SwaggerResponse(404, "Resource not found")]
    [SwaggerResponse(500, "Internal server error")]
    [SwaggerOperation(Summary = "Update a Platform")]
    public async Task<IActionResult> UpdatePlatform(long platformId, [FromBody] PlatformForUpdateDto platformForUpdateDto)
    {
        if  (platformId <= 0)
            return BadRequest(ApiResponse<PlatformDto>.ErrorResult("Invalid platform ID."));

        try
        {
            var platform = await _platformService.UpdatePlatformAsync(platformId, platformForUpdateDto);
            return Ok(ApiResponse<PlatformDto>.SuccessResult(platform));
        }
        catch (Exception e)
        {
            return BadRequest(
                ApiResponse<PlatformDto>.ErrorResult($"An error occured while updating platform: {e.Message}"));
        }
    }

    [HttpDelete("DeletePlatform/{platformId:long}")]
    [SwaggerOperation(Summary = "Delete a Platform")]
    public async Task<IActionResult> DeletePlatform(long platformId)
    {
        if  (platformId <= 0)
            return BadRequest(ApiResponse<PlatformDto>.ErrorResult("Invalid platform ID."));

        try
        {
            var platform = await _platformService.DeletePlatformAsync(platformId);
            if (platform == null)
                return BadRequest(ApiResponse<PlatformDto>.ErrorResult($"Platform with ID {platformId} not found."));
            return Ok(ApiResponse<PlatformDto>.SuccessResult(platform));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse<PlatformDto>.ErrorResult($"An error occured while deleting platform: {e.Message}"));
        }
    }
}