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

    [HttpGet("GetPlatforms")]
    [SwaggerOperation(Summary = "Get all Platforms")]
    public async Task<IActionResult> GetPlatforms()
    {
        var result = await _platformService.GetAllPlatformsAsync();
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetPlatformsDetail")]
    [SwaggerOperation(Summary = "Get all Platforms with Details")]
    public async Task<IActionResult> GetPlatformsDetail()
    {
        var result = await _platformService.GetAllPlatformDetailDtosAsync();
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetPlatformsDetailByName/{name}")]
    [SwaggerOperation(Summary = "Get all Platforms with Details by Name")]
    public async Task<IActionResult> GetPlatformsDetailByName(string name)
    {
        var result = await _platformService.GetAllPlatformDetailDtosByNameAsync(name);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetPlatformById/{platformId:long}")]
    [SwaggerOperation(Summary = "Get a Platform by Id")]
    public async Task<IActionResult> GetPlatformById(long platformId)
    {
        var result = await _platformService.GetPlatformByIdAsync(platformId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpGet("GetPlatformDetailById/{platformId:long}")]
    [SwaggerOperation(Summary = "Get a Platform with Details by Id")]
    public async Task<IActionResult> GetPlatformDetailById(long platformId)
    {
        var result = await _platformService.GetPlatformDetailDtoByIdAsync(platformId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }

    [HttpPost("CreatePlatform")]
    [SwaggerOperation(Summary = "Create a Platform")]
    public async Task<IActionResult> CreatePlatform([FromBody] PlatformForCreationDto platformForCreationDto)
    {
        var result = await _platformService.CreatePlatformAsync(platformForCreationDto);
        if (result.Success)
            return CreatedAtAction(nameof(GetPlatformById), new { platformId = result.Data?.Id }, result);
        return BadRequest(result);
    }

    [HttpPut("UpdatePlatform/{platformId:long}")]
    [SwaggerOperation(Summary = "Update a Platform")]
    public async Task<IActionResult> UpdatePlatform(long platformId, [FromBody] PlatformForUpdateDto platformForUpdateDto)
    {
        var result = await _platformService.UpdatePlatformAsync(platformId, platformForUpdateDto);
        if (result.Success)
            return CreatedAtAction(nameof(GetPlatformById), new { platformId = result.Data?.Id }, result);
        return BadRequest(result);
    }

    [HttpDelete("DeletePlatform/{platformId:long}")]
    [SwaggerOperation(Summary = "Delete a Platform")]
    public async Task<IActionResult> DeletePlatform(long platformId)
    {
        var result = await _platformService.DeletePlatformAsync(platformId);
        if (result.Success)
            return Ok(result);
        return NotFound(result);
    }
}