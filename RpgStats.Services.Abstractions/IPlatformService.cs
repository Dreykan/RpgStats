using RpgStats.Dto;

namespace RpgStats.Services.Abstractions;

public interface IPlatformService
{
    Task<ServiceResult<List<PlatformDto>>> GetAllPlatformsAsync();
    Task<ServiceResult<PlatformDto>> GetPlatformByIdAsync(long platformId);
    Task<ServiceResult<PlatformDto>> CreatePlatformAsync(PlatformForCreationDto platformForCreationDto);
    Task<ServiceResult<PlatformDto>> UpdatePlatformAsync(long platformId, PlatformForUpdateDto platformForUpdateDto);
    Task<ServiceResult<PlatformDto>> DeletePlatformAsync(long platformId);
    Task<ServiceResult<List<PlatformDetailDto>>> GetAllPlatformDetailDtosAsync();
    Task<ServiceResult<List<PlatformDetailDto>>> GetAllPlatformDetailDtosByNameAsync(string name);
    Task<ServiceResult<PlatformDetailDto>> GetPlatformDetailDtoByIdAsync(long platformId);
}