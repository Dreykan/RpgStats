using Microsoft.AspNetCore.Mvc;
using RpgStats.Dto;
using RpgStats.Services.Abstractions;
using Swashbuckle.AspNetCore.Annotations;

namespace RpgStats.ControllersLegacy;

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

    [HttpGet("details")]
    [SwaggerOperation(Summary = "Get all Platforms with Details")]
    public async Task<IActionResult> GetAllPlatformDetailDtos()
    {
        var platforms = await _platformService.GetAllPlatformDetailDtosAsync();
        return Ok(platforms);
    }

    [HttpGet("detailsByName")]
    [SwaggerOperation(Summary = "Get all Platforms with Details by Name")]
    public async Task<IActionResult> GetAllPlatformDetailDtosByName(string name)
    {
        var platforms = await _platformService.GetAllPlatformDetailDtosByNameAsync(name);
        return Ok(platforms);
    }

    [HttpGet("{platformId:long}")]
    [SwaggerOperation(Summary = "Get a Platform by Id")]
    public async Task<IActionResult> GetPlatformById(long platformId)
    {
        var platform = await _platformService.GetPlatformByIdAsync(platformId);
        return Ok(platform);
    }

    [HttpGet("details{platformId:long}")]
    [SwaggerOperation(Summary = "Get a Platform with Details by Id")]
    public async Task<IActionResult> GetPlatformDetailDtoById(long platformId)
    {
        var platform = await _platformService.GetPlatformDetailDtoByIdAsync(platformId);
        return Ok(platform);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a Platform")]
    public async Task<IActionResult> CreatePlatform(PlatformForCreationDto platformForCreationDto)
    {
        var response = await _platformService.CreatePlatformAsync(platformForCreationDto);
        if (response != null)
            return CreatedAtAction(nameof(GetPlatformById), new { platformId = response.Id }, response);
        return BadRequest();
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update a Platform")]
    public async Task<IActionResult> UpdatePlatform(long platformId, PlatformForUpdateDto platformForUpdateDto)
    {
        var response = await _platformService.UpdatePlatformAsync(platformId, platformForUpdateDto);
        if (response != null)
            return Ok(response);
        return BadRequest();
    }

    [HttpDelete]
    [SwaggerOperation(Summary = "Delete a Platform")]
    public async Task<IActionResult> DeletePlatform(long platformId)
    {
        var response = await _platformService.DeletePlatformAsync(platformId);
        if (response == Task.CompletedTask)
            return Ok();
        return BadRequest();
    }
}