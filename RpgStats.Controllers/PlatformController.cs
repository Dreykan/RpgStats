using Microsoft.AspNetCore.Mvc;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;
using Swashbuckle.AspNetCore.Annotations;

namespace RpgStats.Controllers;

[ApiController]
[Route("api/platforms")]
public class PlatformController : ControllerBase
{
    private readonly IPlatformService _platformService;

    public PlatformController(IPlatformService platformService)
    {
        _platformService = platformService;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all Platforms")]
    public async Task<IActionResult> GetAllPlatforms()
    {
        var platforms = await _platformService.GetAllPlatformsAsync();
        return Ok(platforms);
    }

    [HttpGet("{platformId:long}")]
    [SwaggerOperation(Summary = "Get a Platform by Id")]
    public async Task<IActionResult> GetPlatformById(long platformId)
    {
        var platform = await _platformService.GetPlatformByIdAsync(platformId);
        return Ok(platform);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a Platform")]
    public async Task<IActionResult> CreatePlatform(PlatformForCreationDto platformForCreationDto)
    {
        var response = await _platformService.CreatePlatformAsync(platformForCreationDto);
        if (response != null)
            return CreatedAtAction(nameof(GetPlatformById), new { platformId = response.Id }, response);
        return BadRequest(platformForCreationDto);
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update a Platform")]
    public async Task<IActionResult> UpdatePlatform(long platformId, PlatformForUpdateDto platformForUpdateDto)
    {
        await _platformService.UpdatePlatformAsync(platformId, platformForUpdateDto);
        return NoContent();
    }

    [HttpDelete]
    [SwaggerOperation(Summary = "Delete a Platform")]
    public async Task<IActionResult> DeletePlatform(long platformId)
    {
        await _platformService.DeletePlatformAsync(platformId);
        return NoContent();
    }
}