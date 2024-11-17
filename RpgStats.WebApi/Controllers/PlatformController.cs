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
        var platforms = await _platformService.GetAllPlatformsAsync();
        return Ok(platforms);
    }

    [HttpGet("GetPlatformsDetail")]
    [SwaggerOperation(Summary = "Get all Platforms with Details")]
    public async Task<IActionResult> GetPlatformsDetail()
    {
        var platforms = await _platformService.GetAllPlatformDetailDtosAsync();
        return Ok(platforms);
    }

    [HttpGet("GetPlatformsDetailByName/{name}")]
    [SwaggerOperation(Summary = "Get all Platforms with Details by Name")]
    public async Task<IActionResult> GetPlatformsDetailByName(string name)
    {
        var platforms = await _platformService.GetAllPlatformDetailDtosByNameAsync(name);
        return Ok(platforms);
    }

    [HttpGet("GetPlatformById/{platformId:long}")]
    [SwaggerOperation(Summary = "Get a Platform by Id")]
    public async Task<IActionResult> GetPlatformById(long platformId)
    {
        var platform = await _platformService.GetPlatformByIdAsync(platformId);
        return Ok(platform);
    }

    [HttpGet("GetPlatformDetailById/{platformId:long}")]
    [SwaggerOperation(Summary = "Get a Platform with Details by Id")]
    public async Task<IActionResult> GetPlatformDetailById(long platformId)
    {
        var platform = await _platformService.GetPlatformDetailDtoByIdAsync(platformId);
        return Ok(platform);
    }

    [HttpPost("CreatePlatform")]
    [SwaggerOperation(Summary = "Create a Platform")]
    public async Task<IActionResult> CreatePlatform([FromBody] PlatformForCreationDto platformForCreationDto)
    {
        var response = await _platformService.CreatePlatformAsync(platformForCreationDto);
        if (response != null)
            return CreatedAtAction(nameof(GetPlatformById), new { platformId = response.Id }, response);
        return BadRequest();
    }

    [HttpPut("UpdatePlatform/{platformId:long}")]
    [SwaggerOperation(Summary = "Update a Platform")]
    public async Task<IActionResult> UpdatePlatform(long platformId, [FromBody] PlatformForUpdateDto platformForUpdateDto)
    {
        var response = await _platformService.UpdatePlatformAsync(platformId, platformForUpdateDto);
        if (response != null)
            return Ok(response);
        return BadRequest();
    }

    [HttpDelete("DeletePlatform/{platformId:long}")]
    [SwaggerOperation(Summary = "Delete a Platform")]
    public async Task<IActionResult> DeletePlatform(long platformId)
    {
        var response = await _platformService.DeletePlatformAsync(platformId);
        if (response == Task.CompletedTask)
            return Ok();
        return BadRequest();
    }
}