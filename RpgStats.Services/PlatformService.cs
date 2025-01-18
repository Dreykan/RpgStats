using System.Diagnostics.CodeAnalysis;
using Mapster;
using Microsoft.EntityFrameworkCore;
using RpgStats.Domain.Entities;
using RpgStats.Domain.Exceptions;
using RpgStats.Dto;
using RpgStats.Dto.Mapper;
using RpgStats.Repo;
using RpgStats.Services.Abstractions;

namespace RpgStats.Services;

[SuppressMessage("Performance",
    "CA1862:\"StringComparison\"-Methodenüberladungen verwenden, um Zeichenfolgevergleiche ohne Beachtung der Groß-/Kleinschreibung durchzuführen")]
public class PlatformService : IPlatformService
{
    private readonly RpgStatsContext _dbContext;

    public PlatformService(RpgStatsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResult<List<PlatformDto>>> GetAllPlatformsAsync()
    {
        var platforms = await _dbContext.Platforms
            .Include(p => p.PlatformGames)
            .ToListAsync();

        if (platforms.Count == 0)
            return ServiceResult<List<PlatformDto>>.ErrorResult("No platforms found");

        return ServiceResult<List<PlatformDto>>.SuccessResult(platforms.Adapt<List<PlatformDto>>());
    }

    public async Task<ServiceResult<PlatformDto>> GetPlatformByIdAsync(long platformId)
    {
        var platform = await _dbContext.Platforms
            .Include(p => p.PlatformGames)
            .FirstOrDefaultAsync(p => p.Id == platformId);

        if (platform == null)
            return ServiceResult<PlatformDto>.ErrorResult($"Platform with ID {platformId} not found");

        return ServiceResult<PlatformDto>.SuccessResult(platform.Adapt<PlatformDto>());
    }

    public async Task<ServiceResult<PlatformDto>> CreatePlatformAsync(PlatformForCreationDto platformForCreationDto)
    {
        var platform = platformForCreationDto.Adapt<Platform>();

        _dbContext.Platforms.Add(platform);
        var result = await _dbContext.SaveChangesAsync();

        if (result == 0)
            return ServiceResult<PlatformDto>.ErrorResult("Platform could not be created");

        return ServiceResult<PlatformDto>.SuccessResult(platform.Adapt<PlatformDto>());
    }

    public async Task<ServiceResult<PlatformDto>> UpdatePlatformAsync(long platformId, PlatformForUpdateDto platformForUpdateDto)
    {
        var platform = await _dbContext.Platforms.FirstOrDefaultAsync(p => p.Id == platformId);
        if (platform == null)
            return ServiceResult<PlatformDto>.ErrorResult($"Platform with ID {platformId} not found");

        platform.Name = platformForUpdateDto.Name;

        _dbContext.Entry(platform).State = EntityState.Modified;
        var result = await _dbContext.SaveChangesAsync();

        if (result == 0)
            return ServiceResult<PlatformDto>.ErrorResult("Platform could not be updated");

        return ServiceResult<PlatformDto>.SuccessResult(platform.Adapt<PlatformDto>());
    }

    public async Task<ServiceResult<PlatformDto>> DeletePlatformAsync(long platformId)
    {
        var platform = await _dbContext.Platforms.FirstOrDefaultAsync(p => p.Id == platformId);

        if (platform == null)
            return ServiceResult<PlatformDto>.ErrorResult($"Platform with ID {platformId} not found");

        _dbContext.Remove(platform);
        var result =await _dbContext.SaveChangesAsync();
        if (result == 0)
            return ServiceResult<PlatformDto>.ErrorResult("Platform could not be deleted");

        return ServiceResult<PlatformDto>.SuccessResult(platform.Adapt<PlatformDto>());
    }

    public async Task<ServiceResult<List<PlatformDetailDto>>> GetAllPlatformDetailDtosAsync()
    {
        var platforms = await _dbContext.Platforms
            .Include(p => p.PlatformGames)
            .ToListAsync();

        if (platforms.Count == 0)
            return ServiceResult<List<PlatformDetailDto>>.ErrorResult("No platforms found");

        var games = await _dbContext.Games
            .Include(g => g.PlatformGames)
            .ToListAsync();

        var platformDetailDtos = new List<PlatformDetailDto>();

        if (games.Count == 0)
            return ServiceResult<List<PlatformDetailDto>>.SuccessResult(platforms
                .Adapt<List<PlatformDetailDto>>());

        foreach (var platform in platforms)
        {
            var tmpGames = new List<Game?>();
            if (platform.PlatformGames != null)
                tmpGames.AddRange(platform.PlatformGames.Select(pg => games.FirstOrDefault(g => g.Id == pg.GameId)));

            platformDetailDtos.Add(PlatformMapper.MapToPlatformDetailDto(platform, tmpGames));
        }

        return ServiceResult<List<PlatformDetailDto>>.SuccessResult(platformDetailDtos);
    }

    public async Task<ServiceResult<List<PlatformDetailDto>>> GetAllPlatformDetailDtosByNameAsync(string name)
    {
        var platforms = await _dbContext.Platforms
            .Include(p => p.PlatformGames)
            .Where(p => p.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();

        if (platforms.Count == 0)
            return ServiceResult<List<PlatformDetailDto>>.ErrorResult("No platforms found");

        var games = await _dbContext.Games
            .Include(g => g.PlatformGames)
            .ToListAsync();

        var platformDetailDtos = new List<PlatformDetailDto>();

        if (games.Count == 0)
            return ServiceResult<List<PlatformDetailDto>>.SuccessResult(platformDetailDtos
                .Adapt<List<PlatformDetailDto>>());

        foreach (var platform in platforms)
        {
            var tmpGames = new List<Game?>();
            if (platform.PlatformGames != null)
                tmpGames.AddRange(platform.PlatformGames.Select(pg => games.FirstOrDefault(g => g.Id == pg.GameId)));

            platformDetailDtos.Add(PlatformMapper.MapToPlatformDetailDto(platform, tmpGames));
        }

        return ServiceResult<List<PlatformDetailDto>>.SuccessResult(platformDetailDtos
            .Adapt<List<PlatformDetailDto>>());
    }

    public async Task<ServiceResult<PlatformDetailDto>> GetPlatformDetailDtoByIdAsync(long platformId)
    {
        var platform = await _dbContext.Platforms
            .Include(p => p.PlatformGames)
            .FirstOrDefaultAsync(p => p.Id == platformId);

        if (platform == null)
            return ServiceResult<PlatformDetailDto>.ErrorResult($"Platform with ID {platformId} not found");

        var games = await _dbContext.Games
            .Include(g => g.PlatformGames)
            .ToListAsync();

        var platformDetailDto = new PlatformDetailDto();

        if (games.Count == 0 || platform.PlatformGames == null)
            return ServiceResult<PlatformDetailDto>.SuccessResult(platformDetailDto.Adapt<PlatformDetailDto>());

        var tmpGames = platform.PlatformGames.Select(pg => games.FirstOrDefault(g => g.Id == pg.GameId)).ToList();
        platformDetailDto = PlatformMapper.MapToPlatformDetailDto(platform, tmpGames);


        return ServiceResult<PlatformDetailDto>.SuccessResult(platformDetailDto.Adapt<PlatformDetailDto>());
    }
}