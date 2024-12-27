namespace RpgStats.Dto;

public record PlatformDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<PlatformGameDto>? PlatformGames { get; set; }
}